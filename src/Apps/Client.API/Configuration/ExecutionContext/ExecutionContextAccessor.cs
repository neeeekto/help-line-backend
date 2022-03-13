﻿using System;
using System.Linq;
using HelpLine.Apps.Client.API.Configuration.Middlewares;
using HelpLine.BuildingBlocks.Application;
using Microsoft.AspNetCore.Http;

namespace HelpLine.Apps.Client.API.Configuration.ExecutionContext
{
    public class ExecutionContextAccessor : IExecutionContextAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ExecutionContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId
        {
            get
            {
                if (_httpContextAccessor
                    .HttpContext?
                    .User?
                    .Claims?
                    .SingleOrDefault(x => x.Type == "sub")?
                    .Value != null)

                {
                    var userId = _httpContextAccessor.HttpContext.User.Claims.Single(x => x.Type == "sub").Value;
                    return Guid.TryParse(userId, out var userGuid) ? userGuid : Guid.Empty;
                }
                throw new ApplicationException("User context is not available");
            }
        }

        public Guid CorrelationId
        {
            get
            {
                if (IsAvailable &&
                    _httpContextAccessor.HttpContext.Request.Headers.Keys.Any(x =>
                        x == CorrelationMiddleware.CorrelationHeaderKey))
                {
                    return Guid.Parse(
                        _httpContextAccessor.HttpContext.Request.Headers[CorrelationMiddleware.CorrelationHeaderKey]);
                }

                throw new ApplicationException("Http context and correlation id is not available");
            }
        }

        public bool IsAvailable => _httpContextAccessor.HttpContext != null;

        public string Project => _httpContextAccessor.HttpContext?.Request.Headers
            .FirstOrDefault(pair => pair.Key == ProjectMiddleware.ProjectHeaderKey).Value ?? "";
    }
}
