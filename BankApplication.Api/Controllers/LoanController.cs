using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BankApplication.Core.Interfaces;
using BankApplication.Domain.DTO;
using BankApplication.Domain.Entities;

namespace BankApplication.Controllers
{
    [Route("api/loan")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<LoanController> _logger;

        public LoanController(ILoanService service, IMapper mapper, ILogger<LoanController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("create-loan")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateLoanForCustomerAsync([FromBody] CreateLoanDTO loanDto)
        {
            try
            {
                // Validate loan DTO
                if (loanDto.AccountId <= 0 || loanDto.Amount <= 0 || loanDto.Duration <= 0)
                {
                    return BadRequest("Invalid loan data");
                }

                // Map DTO to entity
                var loan = _mapper.Map<Loan>(loanDto);

                // Create the loan for the account
                await _service.CreateLoanForCustomerAsync(loan);

                return Ok("Loan created successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error during loan creation");
            }
        }
    }
}
