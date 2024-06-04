using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YPlanning.Interfaces;
using YPlanning.Dto;

namespace YPlanning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AccountController(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AccountDto>))]
        public IActionResult GetAccounts()
        {
            var accountsDto = _mapper.Map<List<AccountDto>>(_accountRepository.GetAccounts());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(accountsDto);
        }

        [HttpGet("{accountId}")]
        [ProducesResponseType(200, Type = typeof(AccountDto))]
        [ProducesResponseType(400)]
        public IActionResult GetAccount(int accountId)
        {
            if (!_accountRepository.AccountExists(accountId))
                return NotFound();

            var accountDto = _mapper.Map<AccountDto>(_accountRepository.GetAccountById(accountId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(accountDto);
        }
    }
}
