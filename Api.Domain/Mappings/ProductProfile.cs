using AutoMapper;
using Api.Domain.DTOs;
using Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Domain.Mappings
{
    // You CREATE this class, but it INHERITS the built-in Profile
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // CreateMap is a method provided by the built-in Profile class
            CreateMap<Product, ProductDtos.Read>();
            CreateMap<ProductDtos.Create, Product>();
            CreateMap<FinanceDtos.TransactionCreateDto, Transaction>();
            // Add this line to fix the error
            CreateMap<Api.Domain.Entities.Transaction, Api.Domain.DTOs.FinanceDtos.FinanceDto>();
        }
    }
}
