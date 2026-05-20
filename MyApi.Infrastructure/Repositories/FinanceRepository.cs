using Api.Domain.Interfaces;
using Api.Domain.Entities; // Replace 'Api.Domain.Entities' with your actual namespace
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Api.Domain.DTOs.FinanceDtos;


namespace DataAccess.EFCore.Repositories
{
    public class FinanceRepository : GenericRepository<Transaction>, IFinanceRepository
    {
        public FinanceRepository(ApplicationContext context) : base(context) { }

        public async Task<decimal> GetTotalByDateRangeAsync(DateTime start, DateTime end)
        {
            return await _context.Transactions
             .Where(t => t.TransactionDate >= start && t.TransactionDate <= end)
             .SumAsync(t => (decimal?)t.Amount) ?? 0;

        }
    }
}
