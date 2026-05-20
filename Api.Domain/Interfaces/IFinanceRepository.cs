using Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Api.Domain.DTOs.FinanceDtos;

namespace Api.Domain.Interfaces
{
    public interface IFinanceRepository : IGenericRepository<Transaction>
    {
        Task<decimal> GetTotalByDateRangeAsync(DateTime start, DateTime end);
    }
}
