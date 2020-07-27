using System;

namespace LifeIdea.LazyCure.Core.Activities
{
    public interface IActivity
    {
        string Name { get;set;}
        DateTime Start { get;set;}
        TimeSpan Duration { get;set;}
        DateTime End { get;}
    }
}
