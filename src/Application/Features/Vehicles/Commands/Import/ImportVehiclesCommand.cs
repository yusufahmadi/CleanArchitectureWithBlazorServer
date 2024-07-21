// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Vehicles.DTOs;
using CleanArchitecture.Blazor.Application.Features.Vehicles.Caching;

namespace CleanArchitecture.Blazor.Application.Features.Vehicles.Commands.Import;

    public class ImportVehiclesCommand: ICacheInvalidatorRequest<Result<int>>
    {
        public string FileName { get; set; }
        public byte[] Data { get; set; }
        public string CacheKey => VehicleCacheKey.GetAllCacheKey;
        public CancellationTokenSource? SharedExpiryTokenSource => VehicleCacheKey.GetOrCreateTokenSource();
        public ImportVehiclesCommand(string fileName,byte[] data)
        {
           FileName = fileName;
           Data = data;
        }
    }
    public record class CreateVehiclesTemplateCommand : IRequest<Result<byte[]>>
    {
 
    }

    public class ImportVehiclesCommandHandler : 
                 IRequestHandler<CreateVehiclesTemplateCommand, Result<byte[]>>,
                 IRequestHandler<ImportVehiclesCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ImportVehiclesCommandHandler> _localizer;
        private readonly IExcelService _excelService;
        private readonly VehicleDto _dto = new();

        public ImportVehiclesCommandHandler(
            IApplicationDbContext context,
            IExcelService excelService,
            IStringLocalizer<ImportVehiclesCommandHandler> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _excelService = excelService;
            _mapper = mapper;
        }
        #nullable disable warnings
        public async Task<Result<int>> Handle(ImportVehiclesCommand request, CancellationToken cancellationToken)
        {

           var result = await _excelService.ImportAsync(request.Data, mappers: new Dictionary<string, Func<DataRow, VehicleDto, object?>>
            {
                { _localizer[_dto.GetMemberDescription(x=>x.Name)], (row, item) => item.Name = row[_localizer[_dto.GetMemberDescription(x=>x.Name)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.Description)], (row, item) => item.Description = row[_localizer[_dto.GetMemberDescription(x=>x.Description)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.JenisKendaraan)], (row, item) => item.JenisKendaraan = row[_localizer[_dto.GetMemberDescription(x=>x.JenisKendaraan)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.Merk)], (row, item) => item.Merk = row[_localizer[_dto.GetMemberDescription(x=>x.Merk)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.Tipe)], (row, item) => item.Tipe = row[_localizer[_dto.GetMemberDescription(x=>x.Tipe)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.NoChasis)], (row, item) => item.NoChasis = row[_localizer[_dto.GetMemberDescription(x=>x.NoChasis)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.NoRangka)], (row, item) => item.NoRangka = row[_localizer[_dto.GetMemberDescription(x=>x.NoRangka)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.Pabrikasi)], (row, item) => item.Pabrikasi = row[_localizer[_dto.GetMemberDescription(x=>x.Pabrikasi)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.TahunPembuatan)], (row, item) => item.TahunPembuatan = row[_localizer[_dto.GetMemberDescription(x=>x.TahunPembuatan)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.TahunOperasi)], (row, item) => item.TahunOperasi = row[_localizer[_dto.GetMemberDescription(x=>x.TahunOperasi)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.ServiceA)], (row, item) => item.ServiceA = row[_localizer[_dto.GetMemberDescription(x=>x.ServiceA)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.ServiceB)], (row, item) => item.ServiceB = row[_localizer[_dto.GetMemberDescription(x=>x.ServiceB)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.ServiceC)], (row, item) => item.ServiceC = row[_localizer[_dto.GetMemberDescription(x=>x.ServiceC)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.LastPerbaikan)], (row, item) => item.LastPerbaikan = row[_localizer[_dto.GetMemberDescription(x=>x.LastPerbaikan)]].ToString() }, 

            }, _localizer[_dto.GetClassDescription()]);
            if (result.Succeeded && result.Data is not null)
            {
                foreach (var dto in result.Data)
                {
                    var exists = await _context.Vehicles.AnyAsync(x => x.Name == dto.Name, cancellationToken);
                    if (!exists)
                    {
                        var item = _mapper.Map<Vehicle>(dto);
                        // add create domain events if this entity implement the IHasDomainEvent interface
				        // item.AddDomainEvent(new VehicleCreatedEvent(item));
                        await _context.Vehicles.AddAsync(item, cancellationToken);
                    }
                 }
                 await _context.SaveChangesAsync(cancellationToken);
                 return await Result<int>.SuccessAsync(result.Data.Count());
           }
           else
           {
               return await Result<int>.FailureAsync(result.Errors);
           }
        }
        public async Task<Result<byte[]>> Handle(CreateVehiclesTemplateCommand request, CancellationToken cancellationToken)
        {
            // TODO: Implement ImportVehiclesCommandHandler method 
            var fields = new string[] {
                   // TODO: Define the fields that should be generate in the template, for example:
                   _localizer[_dto.GetMemberDescription(x=>x.Name)], 
_localizer[_dto.GetMemberDescription(x=>x.Description)], 
_localizer[_dto.GetMemberDescription(x=>x.JenisKendaraan)], 
_localizer[_dto.GetMemberDescription(x=>x.Merk)], 
_localizer[_dto.GetMemberDescription(x=>x.Tipe)], 
_localizer[_dto.GetMemberDescription(x=>x.NoChasis)], 
_localizer[_dto.GetMemberDescription(x=>x.NoRangka)], 
_localizer[_dto.GetMemberDescription(x=>x.Pabrikasi)], 
_localizer[_dto.GetMemberDescription(x=>x.TahunPembuatan)], 
_localizer[_dto.GetMemberDescription(x=>x.TahunOperasi)], 
_localizer[_dto.GetMemberDescription(x=>x.ServiceA)], 
_localizer[_dto.GetMemberDescription(x=>x.ServiceB)], 
_localizer[_dto.GetMemberDescription(x=>x.ServiceC)], 
_localizer[_dto.GetMemberDescription(x=>x.LastPerbaikan)], 

                };
            var result = await _excelService.CreateTemplateAsync(fields, _localizer[_dto.GetClassDescription()]);
            return await Result<byte[]>.SuccessAsync(result);
        }
    }

