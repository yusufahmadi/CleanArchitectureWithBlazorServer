﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Infrastructure.Constants.User;

public abstract class UserName
{
    public const string Administrator = nameof(Administrator);
    public const string Operation = nameof(Operation);
    public const string Maintenance = nameof(Maintenance);
    public const string Users = nameof(Users);
    public const string DefaultPassword = "Password123!";
}