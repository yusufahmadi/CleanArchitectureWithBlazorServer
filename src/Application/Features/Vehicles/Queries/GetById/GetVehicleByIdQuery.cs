// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Vehicles.DTOs;
using CleanArchitecture.Blazor.Application.Features.Vehicles.Caching;
using CleanArchitecture.Blazor.Application.Features.Vehicles.Specifications;

namespace CleanArchitecture.Blazor.Application.Features.Vehicles.Queries.GetById;

public class GetVehicleByIdQuery : ICacheableRequest<VehicleDto>
{
   public required int Id { get; set; }
   public string CacheKey => VehicleCacheKey.GetByIdCacheKey($"{Id}");
   public MemoryCacheEntryOptions? Options => VehicleCacheKey.MemoryCacheEntryOptions;
}

public class GetVehicleByIdQueryHandler :
     IRequestHandler<GetVehicleByIdQuery, VehicleDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<GetVehicleByIdQueryHandler> _localizer;

    public GetVehicleByIdQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStringLocalizer<GetVehicleByIdQueryHandler> localizer
        )
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<VehicleDto> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.Vehicles.ApplySpecification(new VehicleByIdSpecification(request.Id))
                     .ProjectTo<VehicleDto>(_mapper.ConfigurationProvider)
                     .FirstAsync(cancellationToken) ?? throw new NotFoundException($"Vehicle with id: [{request.Id}] not found.");
        return data;
    }
}
