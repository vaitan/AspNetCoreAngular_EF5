using System.Threading.Tasks;

namespace AspNetCoreAngular_EF5.Core
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}