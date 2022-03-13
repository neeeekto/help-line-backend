using System;

namespace HelpLine.Modules.Helpdesk.Domain.UnitTests.SeedWork
{
    public class EventNotPublishedException<T> : Exception
    {
        public EventNotPublishedException() : base($"{typeof(T).Name} event not published")
        {
        }
    }

    public class EventsNotPublishedException<T> : Exception
    {
        public EventsNotPublishedException() : base($"{typeof(T).Name} events not published")
        {
        }
    }
}
