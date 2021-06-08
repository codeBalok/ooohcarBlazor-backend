using AutoMapper;
using ooohCar.Application.Interfaces.Services;
using ooohCar.Application.Models.Audit;
using ooohCar.Application.Responses.Audit;
using ooohCar.Infrastructure.Contexts;
using ooohCar.Shared.Wrapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;

namespace ooohCar.Infrastructure.Services
{
    public class AuditService : IAuditService
    {
        private readonly CarsContext _context;
        private readonly IMapper _mapper;
        private readonly IExcelService _excelService;
        private readonly IStringLocalizer<AuditService> _localizer;

        public AuditService(
            IMapper mapper,
            CarsContext context,
            IExcelService excelService,
            IStringLocalizer<AuditService> localizer)
        {
            _mapper = mapper;
            _context = context;
            _excelService = excelService;
            _localizer = localizer;
        }

        public async Task<IResult<IEnumerable<AuditResponse>>> GetCurrentUserTrailsAsync(string userId)
        {
            var trails = await _context.AuditTrails.Where(a => a.UserId == userId).OrderByDescending(a => a.Id).Take(250).ToListAsync();
            var mappedLogs = _mapper.Map<List<AuditResponse>>(trails);
            return await Result<IEnumerable<AuditResponse>>.SuccessAsync(mappedLogs);
        }

        public async Task<string> ExportToExcelAsync(string userId)
        {
            var trails = await _context.AuditTrails.Where(x => x.UserId == userId)
                .OrderByDescending(a => a.DateTime).ToListAsync();
            var result = await _excelService.ExportAsync(trails, sheetName: _localizer["Audit trails"],
                mappers: new Dictionary<string, Func<Audit, object>>()
                {
                    { _localizer["Table Name"], item => item.TableName },
                    { _localizer["Type"], item => item.Type },
                    { _localizer["Date Time (Local)"], item => DateTime.SpecifyKind(item.DateTime, DateTimeKind.Utc).ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss") },
                    { _localizer["Date Time (UTC)"], item => item.DateTime.ToString("dd/MM/yyyy HH:mm:ss") },
                    { _localizer["Primary Key"], item => item.PrimaryKey },
                    { _localizer["Old Values"], item => item.OldValues },
                    { _localizer["New Values"], item => item.NewValues },
                });

            return result;
        }
    }
}