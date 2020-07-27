using System;
using System.Collections.Generic;
using LifeIdea.LazyCure.Core.Time.TimeLogs;

namespace LifeIdea.LazyCure.Core.IO
{
    public interface ITimeLogsFileManager
    {
        List<DateTime> AllTimeLogDates { get; }

        ITimeLog GetTimeLog(DateTime day);

        bool SaveTimeLog(ITimeLog timeLog);
    }
}
