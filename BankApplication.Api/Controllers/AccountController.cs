using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BankApplication.Core.Interfaces;
using BankApplication.Domain.DTO;
using BankApplication.Domain.Entities;
using BankApplication.Domain.Profiles;

namespace BankApplication.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountService service, IMapper mapper, ILogger<AccountController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("create")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CreateAccountAsync([FromBody] AccountCreationDTO accountDto)
        {
            try
            {
                if (accountDto.AccountTypesId < 1 || accountDto.AccountTypesId > 2)
                {
                    return BadRequest("Invalid account AccountTypesId");
                }

                // Get customer ID from the claims
                var customerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                // Map DTO to entity
                var account = _mapper.Map<Account>(accountDto);

                // Create a new account for the Customer
               await _service.CreateCustomerAccountAsync(customerId, account);

                return StatusCode(StatusCodes.Status201Created, "Account created successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error during account creation");
            }
        }

        [HttpGet("customer-accounts")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetCustomerAccountsAsync()
        {
            try
            {
                // Get customer ID from the claims
                var customerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                // Get a list of all bank accounts associated with the customer
                var customerAccounts = await _service.GetAllCustomerAccountsAsync(customerId);

                // Map the entities to DTOs
                var customerAccountsDto = _mapper.Map<List<AccountDTO>>(customerAccounts);

                return Ok(customerAccountsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving customer accounts");
            }
        }

        [HttpGet("{customerId}/accounts")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCustomerAccountsAsync(int customerId)
        {
            try
            {
                // Get a list of all bank accounts associated with a customer
                var customerAccounts = await _service.GetAllCustomerAccountsAsync(customerId);

                // Map the entities to DTOs
                var customerAccountsDto = _mapper.Map<List<AccountDTO>>(customerAccounts);

                return Ok(customerAccountsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving customer accounts");
            }
        }

        

        [HttpGet("{accountId}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetAccountDetailsAsync(int accountId)
        {
            try
            {
                // Get customer ID from the claims
                var customerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                // Get account details from the service
                var accountDetails = await _service.GetAccountDetailsAsync(customerId, accountId);

                if (accountDetails != null)
                {
                    // Map the entity to DTO
                    var accountDto = _mapper.Map<AccountDTO>(accountDetails);
                    return Ok(accountDto);
                }
                else
                {
                    return NotFound($"Account with ID {accountId} not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error getting account details");
            }
        }

        // Need to update comments to better ones later
        [HttpPost("transfer")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> TransferBetweenAccountsAsync([FromBody] TransferBetweenAccountsDTO transferBetweenAccountsDTO)
        {
            try
            {
                // Get customer ID from the claims
                var customerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                // Validate transfer DTO
                if (transferBetweenAccountsDTO.FromAccountId <= 0 || transferBetweenAccountsDTO.ToAccountId <= 0 || transferBetweenAccountsDTO.Amount <= 0)
                {
                    return BadRequest("Invalid transfer data");
                }

                // Try performing the transfer
                var transferResult = await _service.TransferBetweenAccountsAsync(customerId, transferBetweenAccountsDTO);

                if (transferResult)
                {
                    return Ok("Transfer successful");
                }
                else
                {
                    return NotFound("One or both accounts not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error during transfer");
            }
        }


        [HttpPost("transfer-to-other-customer")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> TransferToOtherCustomerAsync([FromBody] TransferToOtherCustomerDTO transferDto)
        {
            try
            {
                // Validate transfer DTO
                if (transferDto.FromAccountId <= 0 || string.IsNullOrWhiteSpace(transferDto.ToAccountNumber) || transferDto.Amount <= 0)
                {
                    return BadRequest("Invalid transfer data");
                }

                // Get customer ID from the claims
                var customerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                // Try performing the transfer
                var transferResult = await _service.TransferToOtherCustomerAsync(customerId, transferDto);

                if (transferResult)
                {
                    return Ok("Transfer to other customer successful");
                }
                else
                {
                    return NotFound("One or both accounts not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error during transfer to other customer");
            }
        }
    }
}
