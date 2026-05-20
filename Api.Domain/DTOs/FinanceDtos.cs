using Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Domain.DTOs
{

    public static class FinanceDtos
    {
          public class TransactionCreateDto
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }

        [Required]
        public int Type { get; set; } // Map to your TransactionType Enum

        [Required]
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

        public string? Note { get; set; }
    }
        public class FinanceDto
        {
            public string Title { get; set; } = string.Empty;
            public decimal Amount { get; set; }
            public int Type { get; set; }
            public DateTime TransactionDate { get; set; }
            public string? Note { get; set; }
        }
    }
}
