﻿using System.Collections.Generic;
using System.Linq;
using HelpLine.BuildingBlocks.Domain.EventsSourcing;

namespace HelpLine.Modules.Helpdesk.Domain.UnitTests.SeedWork
{
    public static class EventSourcingTestHelper
    {
        public static List<EventBase<TId>> GetUncommitedEvents<TId>(IEventsSourcingAggregate<TId> aggregate)
        {
            return aggregate.UncommittedEvents.ToList();
        }
    }
}
