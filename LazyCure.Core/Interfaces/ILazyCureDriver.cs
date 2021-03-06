using System;
using System.Windows.Forms;
using LifeIdea.LazyCure.Core.Activities;
using LifeIdea.LazyCure.Shared.Structures;

namespace LifeIdea.LazyCure.Shared.Interfaces
{
    public interface ILazyCureDriver
    {
        bool CalculateAutomaticallyWorkingIntervals { set; }

        IActivity CurrentActivity { get; }

        double Efficiency { get; }

        TimeSpan PossibleWorkInterruptionDuration { get; set; }

        ITaskViewDataSource TaskViewDataSource { get;}

        object TimeLogData { get; }

        string TimeLogDate { get; }

        string TimeLogsFolder { get; }

        TimeSpan TimeOnWork { get; }

        bool TimeToUpdateTimeLog { get; }

        TimeSpan WorkingActivitiesTime { get; }

        Object WorkingTimeIntervalsData { get; }

        void ApplySettings(ISettings settings);

        void FinishActivity(string finishedActivity, string nextActivity, bool postToExternals);

        IHistoryDataProvider HistoryDataProvider { get; }

        bool LoadTimeLog(string filename);

        void RenameActivity(string before, string after);

        void PostToTwitter(string activity);

        bool Save();

        bool SaveTimeLog(string filename);

        void AuthorizeInExternalPoster();

        TokensPair SetExternalPosterAuthorizationPin(string p);
    }
}
