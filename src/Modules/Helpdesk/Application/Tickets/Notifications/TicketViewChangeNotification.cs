using System;
using System.Collections.Generic;
using HelpLine.BuildingBlocks.Bus.EventsBus;
using Newtonsoft.Json;

namespace HelpLine.Modules.Helpdesk.Application.Tickets.Notifications
{
    public class TicketViewChangeNotification : IntegrationEvent
    {
        public string TicketId { get; }
        public string Project { get; }
        public IEnumerable<Guid> NewEvents { get; }

        public TicketViewChangeNotification(Guid id, DateTime occurredOn, string ticketId, string project, IEnumerable<Guid> newEvents) : base(id, occurredOn)
        {
            TicketId = ticketId;
            Project = project;
            NewEvents = newEvents;
        }
    }
}
