using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Domain.Entities
{
    public enum TransactionType
    {
        Expense = 0,
        Income = 1
    }

    public class Transaction : Base<int>
    {
        [Required, StringLength(100)]
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")] // Crucial for database precision
        public decimal Amount { get; set; }

        public TransactionType Type { get; set; }

        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

        public string? Note { get; set; }
    }

}
