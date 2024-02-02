using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BankApplication.Core.Interfaces;
using BankApplication.Domain.DTO;
using BankApplication.Domain.Entities;

namespace BankApplication.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ICustomerService service, IMapper mapper, ILogger<CustomerController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("signin")]
        [AllowAnonymous]
        public async Task<IActionResult> SignInAsync([FromBody] SignInRequestDTO signInRequest)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(signInRequest.Username) || string.IsNullOrWhiteSpace(signInRequest.Password))
                {
                    return BadRequest("Invalid credentials");
                }

                // If sign-in credentials are correct, return Jwt token
                var token = await _service.CustomerSignInAsync(signInRequest.Username, signInRequest.Password); // could change this to _service.CustomerSignInAndGetTokenAsync(signInRequest.Username, signInRequest.Password);

                if (token != null)
                {
                    // Return the token in the response
                    return Ok(new { Token = token });
                }
                else
                {
                    return Unauthorized("Invalid credentials");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error during sign-in");
            }
        }

        [HttpPost("register")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegistrationDTO registrationDto)
        {
            try
            {
                if (registrationDto.CustomerDto == null || registrationDto.AccountDto == null)
                {
                    return BadRequest("Invalid customer or account data");
                }

                // Map DTOs to entities
                var customer = _mapper.Map<Customer>(registrationDto.CustomerDto);
                var account = _mapper.Map<Account>(registrationDto.AccountDto);

                // Create a new customer and account
                await _service.CreateNewCustomerAndAccountAsync(customer, account);


                return StatusCode(StatusCodes.Status201Created, "Customer and account created successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error during registration");
            }
        }

        [HttpGet("search")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SearchCustomersByName([FromQuery] string customerName)
        {
            try
            {
                // Get a list of the search result based on the keyword entered
                var matchingCustomers = await _service.SearchCustomersByNameAsync(customerName);

                // Map the entities to DTOs
                var customerDtos = _mapper.Map<IEnumerable<CustomerDTO>>(matchingCustomers);

                return Ok(customerDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error searching for customers");
            }
        }
    }
}
