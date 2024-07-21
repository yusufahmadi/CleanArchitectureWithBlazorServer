// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Vehicles.DTOs;
using CleanArchitecture.Blazor.Application.Features.Vehicles.Specifications;
using CleanArchitecture.Blazor.Application.Features.Vehicles.Queries.Pagination;

namespace CleanArchitecture.Blazor.Application.Features.Vehicles.Queries.Export;

public class ExportVehiclesQuery : VehicleAdvancedFilter, IRequest<Result<byte[]>>
{
      public VehicleAdvancedSpecification Specification => new VehicleAdvancedSpecification(this);
}
    
public class ExportVehiclesQueryHandler :
         IRequestHandler<ExportVehiclesQuery, Result<byte[]>>
{
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IExcelService _excelService;
        private readonly IStringLocalizer<ExportVehiclesQueryHandler> _localizer;
        private readonly VehicleDto _dto = new();
        public ExportVehiclesQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IExcelService excelService,
            IStringLocalizer<ExportVehiclesQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _excelService = excelService;
            _localizer = localizer;
        }
        #nullable disable warnings
        public async Task<Result<byte[]>> Handle(ExportVehiclesQuery request, CancellationToken cancellationToken)
        {
            var data = await _context.Vehicles.ApplySpecification(request.Specification)
                       .OrderBy($"{request.OrderBy} {request.SortDirection}")
                       .ProjectTo<VehicleDto>(_mapper.ConfigurationProvider)
                       .AsNoTracking()
                       .ToListAsync(cancellationToken);
            var result = await _excelService.ExportAsync(data,
                new Dictionary<string, Func<VehicleDto, object?>>()
                {
                    // TODO: Define the fields that should be exported, for example:
                    {_localizer[_dto.GetMemberDescription(x=>x.Name)],item => item.Name}, 
{_localizer[_dto.GetMemberDescription(x=>x.Description)],item => item.Description}, 
{_localizer[_dto.GetMemberDescription(x=>x.JenisKendaraan)],item => item.JenisKendaraan}, 
{_localizer[_dto.GetMemberDescription(x=>x.Merk)],item => item.Merk}, 
{_localizer[_dto.GetMemberDescription(x=>x.Tipe)],item => item.Tipe}, 
{_localizer[_dto.GetMemberDescription(x=>x.NoChasis)],item => item.NoChasis}, 
{_localizer[_dto.GetMemberDescription(x=>x.NoRangka)],item => item.NoRangka}, 
{_localizer[_dto.GetMemberDescription(x=>x.Pabrikasi)],item => item.Pabrikasi}, 
{_localizer[_dto.GetMemberDescription(x=>x.TahunPembuatan)],item => item.TahunPembuatan}, 
{_localizer[_dto.GetMemberDescription(x=>x.TahunOperasi)],item => item.TahunOperasi}, 
{_localizer[_dto.GetMemberDescription(x=>x.ServiceA)],item => item.ServiceA}, 
{_localizer[_dto.GetMemberDescription(x=>x.ServiceB)],item => item.ServiceB}, 
{_localizer[_dto.GetMemberDescription(x=>x.ServiceC)],item => item.ServiceC}, 
{_localizer[_dto.GetMemberDescription(x=>x.LastPerbaikan)],item => item.LastPerbaikan}, 

                }
                , _localizer[_dto.GetClassDescription()]);
            return await Result<byte[]>.SuccessAsync(result);
        }
}
