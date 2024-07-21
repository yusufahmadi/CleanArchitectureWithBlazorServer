﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Vehicles.Commands.Import;

public class ImportVehiclesCommandValidator : AbstractValidator<ImportVehiclesCommand>
{
        public ImportVehiclesCommandValidator()
        {
           
           RuleFor(v => v.Data)
                .NotNull()
                .NotEmpty();

        }
}

