using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IDeveloperRepository Developers { get; }
        IProjectRepository Projects { get; }

        IProductRepository Products { get; }

        IFinanceRepository Finances { get; }

        Task<int> CompleteAsync();

        int Complete();
    }
}
