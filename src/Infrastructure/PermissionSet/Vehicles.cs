// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel;

namespace CleanArchitecture.Blazor.Infrastructure.PermissionSet;

public static partial class Permissions
{
    [DisplayName("Vehicles")]
    [Description("Vehicles Permissions")]
    public static class Vehicles
    {
        public const string View = "Permissions.Vehicles.View";
        public const string Create = "Permissions.Vehicles.Create";
        public const string Edit = "Permissions.Vehicles.Edit";
        public const string Delete = "Permissions.Vehicles.Delete";
        public const string Search = "Permissions.Vehicles.Search";
        public const string Export = "Permissions.Vehicles.Export";
        public const string Import = "Permissions.Vehicles.Import";
    }
}

