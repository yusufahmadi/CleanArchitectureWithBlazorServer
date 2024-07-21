// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System.ComponentModel;
using CleanArchitecture.Blazor.Application.Features.Vehicles.DTOs;
using CleanArchitecture.Blazor.Application.Features.Vehicles.Caching;

namespace CleanArchitecture.Blazor.Application.Features.Vehicles.Commands.Create;

public class CreateVehicleCommand: ICacheInvalidatorRequest<Result<int>>
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

      public string CacheKey => VehicleCacheKey.GetAllCacheKey;
      public CancellationTokenSource? SharedExpiryTokenSource => VehicleCacheKey.GetOrCreateTokenSource();
    private class Mapping : Profile
    {
        public Mapping()
        {
             CreateMap<VehicleDto,CreateVehicleCommand>(MemberList.None);
             CreateMap<CreateVehicleCommand,Vehicle>(MemberList.None);
        }
    }
}
    
    public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CreateVehicleCommand> _localizer;
        public CreateVehicleCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<CreateVehicleCommand> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
           var item = _mapper.Map<Vehicle>(request);
           // raise a create domain event
	       item.AddDomainEvent(new VehicleCreatedEvent(item));
           _context.Vehicles.Add(item);
           await _context.SaveChangesAsync(cancellationToken);
           return  await Result<int>.SuccessAsync(item.Id);
        }
    }

