using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Api.Domain.DTOs
{
    public static class ProductDtos
    {
        // For returning data
        public class Read
        {
            public int Id { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
            public double Price { get; set; }
            public int Quantity { get; set; }
            public string? Description { get; set; }
            public bool IsActive { get; set; }
        }

        // For creating data
        public class Create
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public double Price { get; set; }
            public int Quantity { get; set; }
            public string? Description { get; set; }
        }

        // For updating (often subset of Create)
        public class Update
        {
            public string Name { get; set; }
            public double Price { get; set; }
            public string? Description { get; set; }
        }
    }
}
