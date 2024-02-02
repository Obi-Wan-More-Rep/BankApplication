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
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IAdminService service, IMapper mapper, ILogger<AdminController> logger)
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
                var token = await _service.AdminSignInAsync(signInRequest.Username, signInRequest.Password);

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
    }
}
