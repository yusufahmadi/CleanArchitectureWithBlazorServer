// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Vehicles.DTOs;
using CleanArchitecture.Blazor.Application.Features.Vehicles.Caching;

namespace CleanArchitecture.Blazor.Application.Features.Vehicles.Queries.GetAll;

public class GetAllVehiclesQuery : ICacheableRequest<IEnumerable<VehicleDto>>
{
   public string CacheKey => VehicleCacheKey.GetAllCacheKey;
   public MemoryCacheEntryOptions? Options => VehicleCacheKey.MemoryCacheEntryOptions;
}

public class GetAllVehiclesQueryHandler :
     IRequestHandler<GetAllVehiclesQuery, IEnumerable<VehicleDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<GetAllVehiclesQueryHandler> _localizer;

    public GetAllVehiclesQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStringLocalizer<GetAllVehiclesQueryHandler> localizer
        )
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<IEnumerable<VehicleDto>> Handle(GetAllVehiclesQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.Vehicles
                     .ProjectTo<VehicleDto>(_mapper.ConfigurationProvider)
                     .AsNoTracking()
                     .ToListAsync(cancellationToken);
        return data;
    }
}


