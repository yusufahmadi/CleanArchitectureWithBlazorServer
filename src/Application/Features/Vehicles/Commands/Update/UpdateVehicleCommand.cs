// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System.ComponentModel;
using CleanArchitecture.Blazor.Application.Features.Vehicles.DTOs;
using CleanArchitecture.Blazor.Application.Features.Vehicles.Caching;

namespace CleanArchitecture.Blazor.Application.Features.Vehicles.Commands.Update;

public class UpdateVehicleCommand: ICacheInvalidatorRequest<Result<int>>
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
            CreateMap<VehicleDto,UpdateVehicleCommand>(MemberList.None);
            CreateMap<UpdateVehicleCommand,Vehicle>(MemberList.None);
        }
    }
}

    public class UpdateVehicleCommandHandler : IRequestHandler<UpdateVehicleCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<UpdateVehicleCommandHandler> _localizer;
        public UpdateVehicleCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<UpdateVehicleCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {

           var item =await _context.Vehicles.FindAsync( new object[] { request.Id }, cancellationToken)?? throw new NotFoundException($"Vehicle with id: [{request.Id}] not found.");
           item = _mapper.Map(request, item);
		    // raise a update domain event
		   item.AddDomainEvent(new VehicleUpdatedEvent(item));
           await _context.SaveChangesAsync(cancellationToken);
           return await Result<int>.SuccessAsync(item.Id);
        }
    }

