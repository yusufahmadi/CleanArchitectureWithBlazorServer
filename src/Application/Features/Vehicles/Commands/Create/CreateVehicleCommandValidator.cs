// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Vehicles.Commands.Create;

public class CreateVehicleCommandValidator : AbstractValidator<CreateVehicleCommand>
{
        public CreateVehicleCommandValidator()
        {
           
            RuleFor(v => v.Name)
                 .MaximumLength(256)
                 .NotEmpty();
        
        }
       
}

