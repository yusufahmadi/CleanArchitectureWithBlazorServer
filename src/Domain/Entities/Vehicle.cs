using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CleanArchitecture.Blazor.Domain.Common.Entities;

namespace CleanArchitecture.Blazor.Domain.Entities;

public class Vehicle : BaseAuditableEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? JenisKendaraan { get; set; }
    public string? Merk { get; set; }
    public string? Tipe { get; set; }
    public string? NoChasis { get; set; }
    public string? NoRangka { get; set; }
    public string? Pabrikasi { get; set; }
    public string? TahunPembuatan { get; set; }
    public string? TahunOperasi { get; set; }
    public string? ServiceA { get; set; }
    public string? ServiceB { get; set; }
    public string? ServiceC { get; set; }
    public string? LastPerbaikan { get; set; }
}
