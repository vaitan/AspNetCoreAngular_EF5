using AspNetCoreAngular_EF5.Controllers.Resources;
using AspNetCoreAngular_EF5.Models;
using AspNetCoreAngular_EF5.Persistence;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreAngular_EF5.Controllers
{
    public class FeaturesController : Controller
    {
        private readonly NewDbContext _context;
        private readonly IMapper _mapper;

        public FeaturesController(NewDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        [HttpGet("api/features")]
        public async Task<IEnumerable<FeatureResource>> Get()
        {
            var features = await this._context.Features.ToListAsync();

            return this._mapper.Map<List<Feature>, List<FeatureResource>>(features);
        }
    }
}
