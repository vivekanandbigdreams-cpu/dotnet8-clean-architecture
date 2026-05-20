using Api.Domain.DTOs;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Api.Domain.DTOs.ProductDtos;

namespace First_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductsController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        // READ ALL
        [HttpGet]
        public IActionResult GetAll() => Ok(_unitOfWork.Products.GetAll());

        // READ BY ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _unitOfWork.Products.GetById(id);

            if (product == null) return NotFound(new
            {
                status = 404,
                message = "Product not found",
                success = false
            }); ;

            // Mapping Entity -> DTO
            //var response = new ProductDtos.Read
            //{
            //    Id = product.Id,
            //    Code = product.Code,
            //    Name = product.Name,
            //    Price = product.Price,
            //    Quantity = product.Quantity,
            //    Description = product.Description,
            //    IsActive = product.IsActive
            //};

            // MAP: Turn the Entity into the DTO
            var response = _mapper.Map<ProductDtos.Read>(product);


            return Ok(new
            {
                status = 200,
                message = "Successfully retrieved",
                success = true,
                data = response
            });
        }

        // CREATE
        [HttpPost]
        public IActionResult Create(ProductDtos.Create request)
        {

            var product = new Product
            {
                Code = request.Code,
                Name = request.Name,
                Price = request.Price,
                Quantity = request.Quantity,
                Description = request.Description,
                IsActive = true // Default value for new products
            };
            _unitOfWork.Products.Add(product);
            _unitOfWork.Complete(); // Commits to DB
            //return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
            return Ok();
        }

        // UPDATE
        [HttpPut("{id}")]
        public IActionResult Update(int id, Product product)
        {
            var existing = _unitOfWork.Products.GetById(id);
            if (existing == null) return NotFound();

            existing.Name = product.Name;
            existing.Price = product.Price;
            existing.Quantity = product.Quantity;
            existing.IsActive = product.IsActive;
            existing.Description = product.Description;

            _unitOfWork.Complete();
            return NoContent();
        }

        // DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _unitOfWork.Products.GetById(id);
            if (product == null) return NotFound();

            _unitOfWork.Products.Remove(product);
            _unitOfWork.Complete();
            return Ok();
        }
    }

}
