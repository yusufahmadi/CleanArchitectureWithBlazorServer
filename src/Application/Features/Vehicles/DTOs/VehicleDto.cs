// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Vehicles.DTOs;

[Description("Vehicles")]
public class VehicleDto
{
    [Description("Id")]
    public int Id { get; set; }
        [Description("Name")]
    public string Name {get;set;} = String.Empty; 
    [Description("Description")]
    public string? Description {get;set;} 
    [Description("Jenis Kendaraan")]
    public string? JenisKendaraan {get;set;} 
    [Description("Merk")]
    public string? Merk {get;set;} 
    [Description("Tipe")]
    public string? Tipe {get;set;} 
    [Description("No Chasis")]
    public string? NoChasis {get;set;} 
    [Description("No Rangka")]
    public string? NoRangka {get;set;} 
    [Description("Pabrikasi")]
    public string? Pabrikasi {get;set;} 
    [Description("Tahun Pembuatan")]
    public string? TahunPembuatan {get;set;} 
    [Description("Tahun Operasi")]
    public string? TahunOperasi {get;set;} 
    [Description("Service A")]
    public string? ServiceA {get;set;} 
    [Description("Service B")]
    public string? ServiceB {get;set;} 
    [Description("Service C")]
    public string? ServiceC {get;set;} 
    [Description("Last Perbaikan")]
    public string? LastPerbaikan {get;set;} 


    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Vehicle, VehicleDto>().ReverseMap();
        }
    }
}

