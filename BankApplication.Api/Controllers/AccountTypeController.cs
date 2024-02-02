using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BankApplication.Core.Interfaces;

namespace BankApplication.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class AccountTypeController : ControllerBase
    {
        private readonly IAccountTypeService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountTypeController> _logger;

        public AccountTypeController(IAccountTypeService service, IMapper mapper, ILogger<AccountTypeController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }



    }
}
