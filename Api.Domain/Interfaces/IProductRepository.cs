using Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Domain.Interfaces
{

    public interface IProductRepository : IGenericRepository<Product>
    {
        // Add product-specific methods here if needed
        Task<IEnumerable<Product>> GetActiveProductsAsync();
    }

}
