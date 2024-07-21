// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Vehicles.EventHandlers;

    public class VehicleUpdatedEventHandler : INotificationHandler<VehicleUpdatedEvent>
    {
        private readonly ILogger<VehicleUpdatedEventHandler> _logger;

        public VehicleUpdatedEventHandler(
            ILogger<VehicleUpdatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(VehicleUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().FullName);
            return Task.CompletedTask;
        }
    }
