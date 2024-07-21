// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Vehicles.DTOs;
using CleanArchitecture.Blazor.Application.Features.Vehicles.Caching;
using CleanArchitecture.Blazor.Application.Features.Vehicles.Specifications;

namespace CleanArchitecture.Blazor.Application.Features.Vehicles.Queries.Pagination;

public class VehiclesWithPaginationQuery : VehicleAdvancedFilter, ICacheableRequest<PaginatedData<VehicleDto>>
{
    public override string ToString()
    {
        return $"Listview:{ListView}, Search:{Keyword}, {OrderBy}, {SortDirection}, {PageNumber}, {PageSize}";
    }
    public string CacheKey => VehicleCacheKey.GetPaginationCacheKey($"{this}");
    public MemoryCacheEntryOptions? Options => VehicleCacheKey.MemoryCacheEntryOptions;
    public VehicleAdvancedSpecification Specification => new VehicleAdvancedSpecification(this);
}
    
public class VehiclesWithPaginationQueryHandler :
         IRequestHandler<VehiclesWithPaginationQuery, PaginatedData<VehicleDto>>
{
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<VehiclesWithPaginationQueryHandler> _localizer;

        public VehiclesWithPaginationQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<VehiclesWithPaginationQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedData<VehicleDto>> Handle(VehiclesWithPaginationQuery request, CancellationToken cancellationToken)
        {
           var data = await _context.Vehicles.OrderBy($"{request.OrderBy} {request.SortDirection}")
                                    .ProjectToPaginatedDataAsync<Vehicle, VehicleDto>(request.Specification, request.PageNumber, request.PageSize, _mapper.ConfigurationProvider, cancellationToken);
            return data;
        }
}