using HelpLine.Modules.Helpdesk.Domain.Tickets.Events;
using MongoDB.Bson.Serialization;

namespace HelpLine.Modules.Helpdesk.Infrastructure.Domain.Tickets;

internal class TicketReminderExecutedEventMap : BsonClassMap<TicketReminderExecutedEvent>
{
    public TicketReminderExecutedEventMap()
    {
        AutoMap();
        SetDiscriminator(nameof(TicketReminderExecutedEvent));
        MapMember(x => x.ReminderId);
    }
}