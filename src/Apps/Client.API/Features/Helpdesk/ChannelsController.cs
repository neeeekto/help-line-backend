using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelpLine.Apps.Client.API.Features.Helpdesk.Response;
using HelpLine.Modules.Helpdesk.Application.Contracts;
using HelpLine.Modules.Helpdesk.Application.Tickets.Channels.DTO;
using HelpLine.Modules.Helpdesk.Application.Tickets.Channels.Models;
using HelpLine.Modules.Helpdesk.Application.Tickets.Channels.Queries.GetEmailTemplatePreview;
using HelpLine.Services.TemplateRenderer;
using HelpLine.Services.TemplateRenderer.Application.Queries.GetTemplates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpLine.Apps.Client.API.Features.Helpdesk
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v1/hd/channels")]
    [Authorize]
    public class ChannelsController : ControllerBase
    {
        private readonly IHelpdeskModule _helpdesk;

        public ChannelsController(IHelpdeskModule helpdesk)
        {
            _helpdesk = helpdesk;
        }

        [HttpGet]
        [Route("email/previews/{ticketId}/feedback/{feedbackId:guid}")]
        public async Task<ActionResult<EmailRendererResult>> GetFeedbackPreview(string ticketId, Guid feedbackId)
        {
            var result =
                await _helpdesk.ExecuteQueryAsync(new GetEmailFeedbackTemplatePreviewQuery(ticketId, feedbackId));
            return Ok(result);
        }

        [HttpGet]
        [Route("email/previews/{ticketId}/messages")]
        public async Task<ActionResult<EmailRendererResult>> GetMessagesPreview(string ticketId)
        {
            var result = await _helpdesk.ExecuteQueryAsync(new GetEmailMessageTemplatePreviewQuery(ticketId));
            return Ok(result);
        }
    }
}
