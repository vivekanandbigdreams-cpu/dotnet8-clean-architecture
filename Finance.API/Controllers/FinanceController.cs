using Api.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Api.Domain.DTOs.FinanceDtos;

namespace Finance.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FinanceController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; // Optional: for AutoMapper

        public FinanceController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var transactions = await _unitOfWork.Finances.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<FinanceDto>>(transactions);
            return Ok(dtos);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransactionCreateDto dto)
        { 

            var entity = _mapper.Map<Api.Domain.Entities.Transaction>(dto);
            entity.EntryDate = DateTime.UtcNow; // Manual audit entry

            await _unitOfWork.Finances.AddAsync(entity);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetAll), new { id = entity.Id }, dto);
        }
    }

}
