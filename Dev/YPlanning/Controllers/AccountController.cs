using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YPlanning.Models;
using YPlanning.Interfaces.Services;
using YPlanning.Dto;

namespace YPlanning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AccountDto>))]
        public IActionResult GetAccounts()
        {
            var accounts = _accountService.GetAccounts();
            var accountsDto = _mapper.Map<List<AccountDto>>(accounts);
            
            return Ok(accountsDto);
        }

        [HttpGet("{accountId:int}")]
        [ProducesResponseType(200, Type = typeof(AccountDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetAccountById(int? accountId)
        {
            if (accountId == null)
                return BadRequest("Account ID cannot be null");

            if (!_accountService.DoesAccountExistsById(accountId))
                return NotFound();

            var account = _accountService.GetAccountById(accountId);
            var accountDto = _mapper.Map<AccountDto>(account);

            return Ok(accountDto);
        }

        [HttpGet("user/{userId:int}")]
        [ProducesResponseType(200, Type = typeof(AccountDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetAccountByUserId(int? userId)
        {
            if (userId == null)
                return BadRequest("User ID cannot be null");

            if (!_accountService.DoesAccountExistsByUserId(userId))
                return NotFound();
            
            var account = _accountService.GetAccountByUserId(userId);
            var accountDto = _mapper.Map<AccountDto>(account);

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

            if (_accountService.DoesAccountExistsByUserId(accountCreate.UserId))
            {
                ModelState.AddModelError("", "Account already exists");
                return Conflict(ModelState);
            }
            
            var accountMap = _mapper.Map<Account>(accountCreate);
            if (!_accountService.CreateAccount(accountMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Account successfully created");
        }

        [HttpPut("{accountId:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdateAccount(int? accountId, [FromBody] AccountDto updatedAccount)
        {
            if (accountId == null)
                return BadRequest("Account ID cannot be null");

            if (updatedAccount == null)
                return BadRequest("Account cannot be null");
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            if (!_accountService.DoesAccountExistsById(accountId))
                return NotFound();
            
            var accountMap = _mapper.Map<Account>(updatedAccount);
            accountMap.Id = accountId ?? -1;

            if (!_accountService.UpdateAccount(accountMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            
            return NoContent();
        }
        
        [HttpDelete("{accountId:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteAccountById(int? accountId)
        {
            if (accountId == null)
                return BadRequest("Account ID cannot be null");

            if (!_accountService.DoesAccountExistsById(accountId))
                return NotFound();
            
            if (!_accountService.DeleteAccountById(accountId))
            {
                ModelState.AddModelError("", "Something went wrong deleting the account");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("user/{userId:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteAccountByUserId(int? userId)
        {
            if (userId == null)
                return BadRequest("User ID cannot be null");
            
            if (!_accountService.DoesAccountExistsByUserId(userId))
                return NotFound();

            if (!_accountService.DeleteAccountByUserId(userId))
            {
                ModelState.AddModelError("", "Something went wrong deleting the account");
                return StatusCode(500, ModelState);
            }
            
            return NoContent();
        }
    }
}
