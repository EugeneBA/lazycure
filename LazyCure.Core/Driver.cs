using System;
using System.Collections.Generic;
using System.Data;
using LifeIdea.LazyCure.Core.Activities;
using LifeIdea.LazyCure.Core.IO;
using LifeIdea.LazyCure.Core.Localization;
using LifeIdea.LazyCure.Core.Plugins;
using LifeIdea.LazyCure.Core.Reports;
using LifeIdea.LazyCure.Core.Tasks;
using LifeIdea.LazyCure.Core.Time;
using LifeIdea.LazyCure.Core.Time.TimeLogs;
using LifeIdea.LazyCure.Shared.Interfaces;
using LifeIdea.LazyCure.Shared.Structures;
using LifeIdea.LazyCure.Shared.Tools;

namespace LifeIdea.LazyCure.Core
{
    /// <summary>
    /// Initialize required for LazyCure work objects. Provide data to UI forms.
    /// </summary>
    public class Driver : ILazyCureDriver
    {
        #region Fields

        private IEfficiencyCalculator efficiencyCalculator;
        private IFileManager fileManager;// = new FileManager();
        private ILanguageSwitcher languageSwitcher;
        private ITaskCollection taskCollection;
        private ITimeManager timeManager;
        private IWorkingTimeManager workingTime;
        //private IHistoryDataProvider historyDataProvider;
        private ITimeLogsManager timeLogsManager;
        private IExternalPoster externalPoster = null;//new Twitter();

        #endregion Fields

        #region Properties

        public IEfficiencyCalculator EfficiencyCalculator
        {
            get { return efficiencyCalculator; }
            set { efficiencyCalculator = value; }
        }

        public IFileManager FileManager
        {
            get { return fileManager; }
            set
            {
                fileManager = value;
                if (timeLogsManager != null)
                    timeLogsManager.FileManager = fileManager;
            }
        }

        public static string FirstActivityName = "starting LazyCure";

        public ILanguageSwitcher LanguageSwitcher
        {
            get { return languageSwitcher; }
        }

        public bool SaveAfterDone = false;
        
        public ITaskCollection TaskCollection
        {
            get { return taskCollection; }
            set
            {
                taskCollection = value;
                if (WorkingTime != null)
                    WorkingTime.WorkDefiner = taskCollection;
                if ((HistoryDataProvider as HistoryDataProvider) != null)
                    (HistoryDataProvider as HistoryDataProvider).TaskCollection = taskCollection;
            }
        }

        public ITaskViewDataSource TaskViewDataSource
        {
            get { return taskCollection as ITaskViewDataSource; }
        }

        public ITimeManager TimeManager
        {
            get{ return timeManager;}
            set{ timeManager = value;}
        }

        public string TimeLogsFolder { get { return FileManager.TimeLogsFolder; } set { FileManager.TimeLogsFolder = value; } }

        public IExternalPoster ExternalPoster { get { return externalPoster; } set { externalPoster = value; } }

        public IWorkingTimeManager WorkingTime
        {
            get { return workingTime; }
            set { workingTime = value; }
        }

        public ITimeLogsManager TimeLogsManager
        {
            set
            {
                this.timeLogsManager = value;
                if(HistoryDataProvider != null)
                    HistoryDataProvider.TimeLogsManager = this.timeLogsManager;
            }
            get
            {
                return this.timeLogsManager;
            }
        }

        public IHistoryDataProvider HistoryDataProvider { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initialize all subsystems
        /// </summary>
        /// <param name="timeSystem"></param>
        /// <param name="settings"></param>
        public Driver(ISettings settings)
        {
            //when reordering, be carefull, in order to pass only initialized objects
            this.fileManager = new FileManager(settings);
            this.languageSwitcher = new LanguageSwitcher(settings);
            //probably all of them should be properties, not fields, in order to automatically update referencies
            TaskCollection = LifeIdea.LazyCure.Core.Tasks.TaskCollection.Default;
            this.timeLogsManager = new TimeLogsManager(this.fileManager);
            HistoryDataProvider = new HistoryDataProvider(timeLogsManager, TaskCollection);
            this.timeManager = new TimeManager(TimeLogsManager);
            HistoryDataProvider.CreateSummaries(TimeManager.TimeLog);
            this.workingTime = new WorkingTimeForDay(TimeManager.TimeLog, TaskCollection);
            this.efficiencyCalculator = new EfficiencyCalculator(workingTime);
            ApplySettings(settings);
        }
        
        #endregion Constructors

        #region ILazyCureDriver Members

        public bool CalculateAutomaticallyWorkingIntervals
        {
            set { WorkingTime.CalculateAutomatically = value; }
        }

        public IActivity CurrentActivity { get { return TimeManager.CurrentActivity; } }

        public double Efficiency { get { return EfficiencyCalculator.Efficiency; } }

        public TimeSpan PossibleWorkInterruptionDuration
        {
            get { return WorkingTime.PossibleWorkInterruption; }
            set { workingTime.PossibleWorkInterruption = value;}
        }

        public bool TimeToUpdateTimeLog
        {
            get
            {
                return TimeManager.CurrentActivityIsLastingTooLong;
            }
        }

        public TimeSpan TimeOnWork { get { return WorkingTime.TimeOnWork; } }

        public TimeSpan WorkingActivitiesTime
        {
            get { return WorkingTime.WorkingTasksTime; }
        }

        public object WorkingTimeIntervalsData
        {
            get { return WorkingTime.Intervals; }
        }

        public void ApplySettings(ISettings settings)
        {
            if (settings != null)
            {
                SaveAfterDone = settings.SaveAfterDone;
                if (HistoryDataProvider != null)
                    HistoryDataProvider.ApplySettings(settings);
                TimeManager.MaxDuration = settings.ReminderTime;
                TimeManager.SplitByComma = settings.SplitByComma;
                TimeManager.SwitchAtMidnight = settings.SwitchTimeLogAtMidnight;
                ApplyTweetingActivity(settings);
                //ExternalPoster.AccessTokens = new TokensPair(settings.TwitterAccessToken, settings.TwitterAccessTokenSecret);
            }
        }

        private void ApplyTweetingActivity(ISettings settings)
        {
            // this hardcode could be replaced for getting the english version from constants, but don't know the simple way to do that
            const string tweetingInEn = "tweeting";
            if (settings.TweetingActivity == string.Empty || settings.TweetingActivity == tweetingInEn)
            {
                settings.TweetingActivity = LazyCure.Shared.Constants.Constants.Tweeting;
                settings.Save();
            }
            TimeManager.TweetingActivity = (settings.UseTweetingActivity) ? settings.TweetingActivity : null;
        }

        public void AuthorizeInExternalPoster()
        {
            ExternalPoster.ShowAuthorizationPage();
        }

        public void RenameActivity(string before, string after)
        {
            TimeManager.TimeLog.RenameActivities(before, after);
            HistoryDataProvider.ActivitiesHistory.RenameActivity(before, after);
        }

        public void PostToTwitter(string activity)
        {
            ExternalPoster.PostAsync(activity);
        }

        public object TimeLogData { get { return TimeManager.TimeLog.Data; } }

        public void FinishActivity(string finishedActivityName, string nextActivityName, bool postToExternals)
        {
            if (postToExternals)
            {
                string tweet = finishedActivityName;
                string tweetingActivity = TimeManager.TweetingActivity;
                if(tweetingActivity!=null)
                    finishedActivityName = tweetingActivity;
                this.PostToTwitter(tweet);
            }
            List<IActivity> finishedActivities = TimeManager.FinishActivity(finishedActivityName, nextActivityName);
            HistoryDataProvider.ActivitiesHistory.AddActivities(finishedActivities);
            if (SaveAfterDone && timeLogsManager != null)
                timeLogsManager.SaveActiveTimeLog();
        }

        public void FinishActivity(string finishedActivityName, string nextActivityName)
        {
            FinishActivity(finishedActivityName, nextActivityName, false);
        }

        public string TimeLogDate { get { return Format.Date(TimeManager.TimeLog.Date); } }

        public bool LoadTimeLog(string filename)
        {
            ITimeLog loadedTimeLog = fileManager.GetTimeLog(filename);
            if (loadedTimeLog != null)
            {
                ActivateTimeLog(loadedTimeLog);
                return true;
            }
            else
                return false;
        }

        public TokensPair SetExternalPosterAuthorizationPin(string pin)
        {
            if (ExternalPoster != null)
                return ExternalPoster.SetPin(pin);
            return TokensPair.Empty;
        }

        public void ActivateTimeLog(ITimeLog timeLog)
        {
            //TimeManager.TimeLog = timeLog;
            DateTime date = timeLog.Date;
            ActivateTimeLog(date);
        }

        private void ActivateTimeLog(DateTime date)
        {
            ITimeLog activeTimeLog = TimeLogsManager.ActivateTimeLog(date);
            HistoryDataProvider.UpdateTimeLog(activeTimeLog);
            workingTime.TimeLog = activeTimeLog;
        }

        public bool LoadTimeLog(DateTime date)
        {
            ITimeLog timeLog = null;
            if (timeLogsManager != null)
                timeLog = timeLogsManager.ActivateTimeLog(date);
            return (timeLog != null);
        }

        public bool Save()
        {
            fileManager.SaveHistory(HistoryDataProvider.ActivitiesHistory);
            fileManager.SaveTasks(TaskCollection);
            return fileManager.SaveTimeLog(TimeManager.TimeLog);
        }

        public bool SaveTimeLog(string filename)
        {
            return fileManager.SaveTimeLog(TimeManager.TimeLog, filename);
        }

        public void UpdateIsWorkingTaskProperty(string task, bool isWorking)
        {
            TaskCollection.UpdateIsWorkingProperty(task, isWorking);
        }

        #endregion

        //need to remove ITimeLogsManager from driver
        #region ITimeLogsManager Members

        public List<IActivity> GetActivities(DateTime day)
        {
            if (timeLogsManager != null)
                return timeLogsManager.GetActivities(day);
            return null;
        }

        
        public List<DateTime> AvailableDays
        {
            get { return null; }
        }

        #endregion ITimeLogsManager Members

        #region IHistoryDataProvider Members

        #endregion IHistoryDataProvider Members

        #region Private Members

        public bool Load()
        {
            ITaskCollection loadedTasks = fileManager.GetTasks();
            if (loadedTasks != null)
                TaskCollection = loadedTasks;
            fileManager.LoadHistory(HistoryDataProvider.ActivitiesHistory);
            LoadTimeLog(DateTime.Now);
            return true;
        }

        #endregion Private Members
    }
}
