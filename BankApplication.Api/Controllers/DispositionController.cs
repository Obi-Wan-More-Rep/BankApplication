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
    [Route("api/disposition")]
    [ApiController]
    public class DispositionController : ControllerBase
    {
        private readonly IDispositionService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<DispositionController> _logger;

        public DispositionController(IDispositionService service, IMapper mapper, ILogger<DispositionController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }
    }
}
