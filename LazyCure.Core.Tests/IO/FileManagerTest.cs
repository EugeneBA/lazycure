using System;
using System.IO;
using NMock2;
using NUnit.Framework;
using LifeIdea.LazyCure.Core.Tasks;
using LifeIdea.LazyCure.Core.Time.TimeLogs;
using LifeIdea.LazyCure.Shared.Interfaces;
using LifeIdea.LazyCure.Shared.Tools;

namespace LifeIdea.LazyCure.Core.IO
{
    [TestFixture]
    public class FileManagerTest:Mockery
    {
        private FileManager fileManager;
        private string filename = null;
        private readonly string content = "<?xml version=\"1.0\" standalone=\"yes\"?><LazyCureData Date=\"2102-03-12\"><Records>" +
                  "<Activity>changed</Activity><Begin>14:35:02</Begin><Duration>0:00:07</Duration>" +
                  "</Records></LazyCureData>";
        [SetUp]
        public void SetUp()
        {
            fileManager = new FileManager();
        }
        [TearDown]
        public void TearDown()
        {
            if (File.Exists(filename))
            {
                try
                {
                    File.Delete(filename);
                }catch(Exception)
                {
                }
                filename = null;
            }
        }
        [Test]
        public void FileManagerUseTimeLogsFolderSetting()
        {
            ITimeLogsFolderSettingSource settings = NewMock<ITimeLogsFolderSettingSource>();
            Stub.On(settings).GetProperty("TimeLogsFolder").Will(Return.Value(@"c:\myworkingfolder"));

            FileManager fileManagerWithSettings = new FileManager(settings);

            Assert.AreEqual(@"c:\myworkingfolder", fileManagerWithSettings.TimeLogsFolder);
        }
        [Test]
        public void GetTimeLogFileName()
        {
            fileManager.TimeLogsFolder = @"c:\test";
            Assert.AreEqual(@"c:\test\2109-12-31.timelog", fileManager.GetTimeLogFileName(DateTime.Parse("2109-12-31")));
        }
        [Test]
        public void GetTimeLogFileNameWithNullFolder()
        {
            Assert.AreEqual("2109-12-31.timelog",fileManager.GetTimeLogFileName(DateTime.Parse("2109-12-31")));
        }
        [Test]
        public void SaveTasksWriteToFile()
        {
            ITaskCollection taskCollection = new TaskCollection();
            fileManager.TasksFileName = "SaveTasksWriteToFile.tmp";
            File.Delete("SaveTasksWriteToFile.tmp");

            fileManager.SaveTasks(taskCollection);

            Assert.IsTrue(File.Exists("SaveTasksWriteToFile.tmp"));
        }
        [Test]
        public void DefaultTasksFileName()
        {
            Assert.That(fileManager.TasksFileName.IndexOf("tasks.xml")>1);
        }
        [Test]
        public void DefaultHistoryFileName()
        {
            Assert.That(fileManager.HistoryFileName.IndexOf("history.txt") > 1);
        }
        [Test]
        public void FileIsClosing()
        {
            ITaskCollection taskCollection = new TaskCollection();
            fileManager.TasksFileName = "FileIsClosing.tmp";
            File.Delete("FileIsClosing.tmp");

            fileManager.SaveTasks(taskCollection);
            fileManager.SaveTasks(taskCollection);
        }
        [Test]
        public void SaveTasksCheckTaskCollectionForNull()
        {
            Assert.IsFalse(fileManager.SaveTasks(null));
        }
        [Test]
        public void SaveTasksSerializeTasks()
        {
            ITaskCollection taskCollection = new TaskCollection();
            taskCollection.Add(new Task("task1"));
            fileManager.TasksFileName = "SaveTasksSerializeTasks.tmp";
            File.Delete("SaveTasksSerializeTasks.tmp");

            fileManager.SaveTasks(taskCollection);

            Assert.IsTrue(File.OpenText(fileManager.TasksFileName).ReadToEnd().Contains("task1"));
        }
        [Test]
        public void SaveTasksIfFileIsOpened()
        {
            ITaskCollection taskCollection = new TaskCollection();
            filename = "SaveTasksIfFileIsOpened.tmp";
            File.WriteAllText(filename,"text");
            File.OpenText(filename);

            fileManager.TasksFileName = filename;
            Assert.IsFalse(fileManager.SaveTasks(taskCollection));
        }
        [Test]
        public void GetNotNullTimeLog()
        {
            filename = "GetNotNullTimeLog.timelog";
            File.WriteAllText(filename, content);

            ITimeLog timeLog = fileManager.GetTimeLog(filename);
            Assert.IsNotNull(timeLog);
        }
        [Test]
        public void GetTimeLogForUnexistentFile()
        {
            filename = "UnexistentFile.timelog";

            ITimeLog timeLog = fileManager.GetTimeLog(filename);
            Assert.IsNull(timeLog);
        }
        [Test]
        public void GetTimeLogGetDateFromFileName()
        {
            filename = "2013-12-21.timelog";
            File.WriteAllText(filename,content);

            ITimeLog timeLog = fileManager.GetTimeLog(filename);
            Assert.AreEqual("2013-12-21",timeLog.Date.ToString("yyyy-MM-dd"));
        }
        [Test]
        public void GetTimeLogDateFromXmlIfFileNameIsNotDate()
        {
            filename = "TimeLog~1.timelog";
            File.WriteAllText(filename, content);

            ITimeLog timeLog = fileManager.GetTimeLog(filename);

            Assert.AreEqual("2102-03-12", timeLog.Date.ToString("yyyy-MM-dd"));
        }
        [Test]
        public void GetTasksWhenThereIsNoTasksFile()
        {
            Log.Writer = new StringWriter();
            File.Delete(fileManager.TasksFileName);
            ITaskCollection taskCollection = fileManager.GetTasks();
            Assert.IsNull(taskCollection);
            Assert.AreEqual("", Log.Writer.ToString());
        }
        [Test]
        public void GetTasksReturnsWhatWasSaved()
        {
            ITaskCollection taskCollection = new TaskCollection();
            taskCollection.Add(new Task("task1"));
            taskCollection.Add(new Task("task2"));

            fileManager.SaveTasks(taskCollection);
            Assert.AreEqual(taskCollection, fileManager.GetTasks());
        }
        [Test]
        public void SaveTimeLogCreateFile()
        {
            ITimeLog timeLog = new TimeLog(DateTime.Now);
            fileManager.SaveTimeLog(timeLog, "SaveTimeLog.tmp");
            Assert.IsTrue(File.Exists("SaveTimeLog.tmp"));
        }
        [Test]
        public void GetTimeLogFileNameForSpecifiedTimeLog()
        {
            ITimeLog timeLog = NewMock<ITimeLog>();
            Expect.AtLeastOnce.On(timeLog).GetProperty("FileName").Will(Return.Value("test.time.log"));
            Assert.AreEqual("test.time.log", fileManager.GetTimeLogFileName(timeLog));
        }
        [Test]
        public void GetTimeLogSaveFileName()
        {
            File.WriteAllText("GetTimeLog.SaveFileName", content);
            ITimeLog timeLog = fileManager.GetTimeLog("GetTimeLog.SaveFileName");
            Assert.AreEqual("GetTimeLog.SaveFileName", timeLog.FileName);
        }
        [Test]
        public void AllTimeLogDatesLooksAtDisk()
        {
            this.filename = fileManager.GetTimeLogFileName(DateTime.Parse("2000-01-23"));
            string folder = Path.Combine(Environment.GetEnvironmentVariable("TMP"), "LC");
            this.filename = Path.Combine(folder, this.filename);
            if (Directory.Exists(folder))
                Directory.Delete(folder, true);
            Directory.CreateDirectory(folder);
            File.CreateText(filename).Close(); //will be deleted in TearDown

            fileManager.TimeLogsFolder = folder;
            var list = fileManager.AllTimeLogDates;
            Assert.AreEqual(1, list.Count, this.filename);
            Assert.AreEqual(DateTime.Parse("2000-01-23"), list[0]);
        }
        [Test]
        public void GetTimeLogFileNameForNull()
        {
            ITimeLog timelog = null;
            Assert.IsNull(fileManager.GetTimeLogFileName(timelog));
        }
        [Test]
        public void SaveTimeLogWithNull()
        {
            ITimeLog timelog = null;
            Assert.IsFalse(fileManager.SaveTimeLog(timelog));
        }
    }
}
