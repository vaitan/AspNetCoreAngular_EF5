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
    public class MakesController : Controller
    {
        private readonly NewDbContext _context;
        private readonly IMapper _mapper;

        public MakesController(NewDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        [HttpGet("/api/makes")]
        public async Task<IEnumerable<MakeResource>> GetMakes()
        {
            var makes = await this._context.Makes.Include(m => m.Models).ToListAsync();

            return this._mapper.Map<List<Make>, List<MakeResource>>(makes);
        }
    }
}