// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Vehicles.Commands.Delete;

public class DeleteVehicleCommandValidator : AbstractValidator<DeleteVehicleCommand>
{
        public DeleteVehicleCommandValidator()
        {
          
            RuleFor(v => v.Id).NotNull().ForEach(v=>v.GreaterThan(0));
          
        }
}
    

