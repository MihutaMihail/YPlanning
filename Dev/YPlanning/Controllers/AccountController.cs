using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YPlanning.Interfaces;
using YPlanning.Dto;
using YPlanning.Models;
using YPlanning.Repository;

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
        [ProducesResponseType(404)]
        public IActionResult GetAccount(int accountId)
        {
            if (!_accountRepository.AccountExists(accountId))
                return NotFound();

            var accountDto = _mapper.Map<AccountDto>(_accountRepository.GetAccountById(accountId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(accountDto);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult CreateAccount([FromBody] AccountDto accountCreate)
        {
            if (accountCreate == null)
                return BadRequest("Account cannot be null");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingAccount = _accountRepository.GetAccounts()
                .Where(ac => ac.UserId == accountCreate.UserId)
                .FirstOrDefault();

            if (existingAccount != null)
            {
                ModelState.AddModelError("", "Account already exists");
                return Conflict(ModelState);
            }

            var accountMap = _mapper.Map<Account>(accountCreate);

            if (!_accountRepository.CreateAccount(accountMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Account successfully created");
        }

        [HttpPut("{accountId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult UpdateAccount(int accountId, [FromBody] AccountDto updatedAccount)
        {
            if (updatedAccount == null)
                return BadRequest("Account cannot be null");

            if (updatedAccount.Id != 0 && accountId != updatedAccount.Id)
                return BadRequest("Ids are not matching");

            if (!_accountRepository.AccountExists(accountId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            updatedAccount.Id = accountId;

            var accountMap = _mapper.Map<Account>(updatedAccount);

            if (!_accountRepository.UpdateAccount(accountMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
