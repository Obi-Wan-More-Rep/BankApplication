using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BankApplication.Core.Interfaces;
using BankApplication.Domain.DTO;

namespace BankApplication.Controllers
{
    [Route("api/transaction")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(ITransactionService service, IMapper mapper, ILogger<TransactionController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("account/{accountId}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetAccountTransactionsAsync(int accountId)
        {
            // Get customer ID from the claims
            var customerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            // Get a list of all transactions associated with an account
            var transactions = await _service.GetAccountTransactions(accountId);

            // Map the entities to DTOs
            var transactionDtos = _mapper.Map<IEnumerable<TransactionDTO>>(transactions);

            return Ok(transactionDtos);
        }
    }
}
