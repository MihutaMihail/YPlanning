﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YPlanning.Models;
using YPlanning.Interfaces.Services;
using YPlanning.Dto;
using Microsoft.AspNetCore.Identity;
using YPlanning.Authorize;

namespace YPlanning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<Account> _passwordHasher;

        public AccountController(IAccountService accountService, ITokenService tokenService, 
            IMapper mapper, IPasswordHasher<Account> passwordHasher)
        {
            _accountService = accountService;
            _tokenService = tokenService;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            var login = loginDto.Login;
            var password = loginDto.Password;

            if (!_accountService.Authenticate(login, password))
                return Unauthorized("Invalid credentials");

            var user = _accountService.GetUserByAccount(login, password);

            var existingTokenValue = _tokenService.GetTokenValueByUserId(user.Id);
            if (existingTokenValue != "null")
                return Ok(new { Token = existingTokenValue });

            if (!_tokenService.CreateTokenForUser(user))
            {
                ModelState.AddModelError("", "Something went wrong while creating token");
                return StatusCode(500, ModelState);
            }
            
            var newTokenValue = _tokenService.GetTokenValueByUserId(user.Id);
            return Ok(new { Token = newTokenValue });
        }
        
        [HttpGet]
        [AuthorizeRole("admin", "teacher", "student")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AccountDto>))]
        public IActionResult GetAccounts()
        {
            var accounts = _accountService.GetAccounts();
            var accountsDto = _mapper.Map<List<AccountDto>>(accounts);
            
            return Ok(accountsDto);
        }
        
        [HttpGet("{accountId:int}")]
        [AuthorizeRole("admin", "teacher", "student")]
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
        [AuthorizeRole("admin", "teacher", "student")]
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
        [AuthorizeRole("admin")]
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
            accountMap.Password = _passwordHasher.HashPassword(accountMap, accountCreate.Password);
            accountMap.AccountCreationDate = DateTime.UtcNow;

            if (!_accountService.CreateAccount(accountMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Account successfully created");
        }

        [HttpPut("{accountId:int}")]
        [AuthorizeRole("admin")]
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
            accountMap.Password = _passwordHasher.HashPassword(accountMap, updatedAccount.Password);
            accountMap.AccountCreationDate = _accountService.GetAccountCreationDateById(accountId);

            if (!_accountService.UpdateAccount(accountMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            
            return NoContent();
        }
        
        [HttpDelete("{accountId:int}")]
        [AuthorizeRole("admin")]
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
        [AuthorizeRole("admin")]
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
