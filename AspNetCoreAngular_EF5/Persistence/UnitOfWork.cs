using AspNetCoreAngular_EF5.Core;
using System.Threading.Tasks;

namespace AspNetCoreAngular_EF5.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly NewDbContext _context;

        public UnitOfWork(NewDbContext context)
        {
            this._context = context;
        }

        public async Task CompleteAsync()
        {
            this._context.SaveChanges();
        }
    }
}