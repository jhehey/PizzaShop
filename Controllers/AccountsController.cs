using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Contexts;
using PizzaShop.Entities;
using PizzaShop.Extensions;
using PizzaShop.Models;

namespace PizzaShop.Controllers
{
    [ApiController]
    [Route ("api/accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly PizzaShopDbContext context;
        private readonly IMapper mapper;
        private DbSet<Account> Accounts => context.Accounts;

        public AccountsController (PizzaShopDbContext context, IMapper mapper)
        {
            this.context = context ??
                throw new ArgumentNullException ();
            this.mapper = mapper ??
                throw new ArgumentNullException ();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountDto>>> GetAccounts ()
        {
            var accounts = await Accounts.ToListAsync ();
            return Ok (mapper.Map<IEnumerable<AccountDto>> (accounts));
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<AccountDto>> GetAccountById (long id)
        {
            var account = await Accounts.FindAsync (id);
            if (account == null) return NotFound ();
            return Ok (mapper.Map<AccountDto> (account));
        }

        [HttpPost]
        public async Task<ActionResult<AccountDto>> CreateAccount (AccountForCreationDto createAccountDto)
        {
            // duplicate field validation
            var usernameExists = await UsernameExists (createAccountDto.Username);
            if (usernameExists) return Conflict ("Username already exists");

            var emailExists = await EmailExists (createAccountDto.Email);
            if (emailExists) return Conflict ("Email already exists");

            // since contact number is not required we should check if its null first
            if (createAccountDto.ContactNumber != null)
            {
                var contactExists = await ContactExists (createAccountDto.ContactNumber);
                if (contactExists) return Conflict ("Contact Number already exists");
            }

            // map dto to entity
            var account = mapper.Map<Account> (createAccountDto);

            // add the registration date
            account.RegistrationDate = DateTime.Now;

            // save the account
            Accounts.Add (account);
            await context.SaveChangesAsync ();

            // return account dto at location
            var accountDto = mapper.Map<AccountDto> (account);

            return CreatedAtAction (nameof (GetAccountById), new { id = accountDto.Id }, accountDto);
        }

        [HttpPut ("{id}")]
        public async Task<IActionResult> UpdateAccount (long id, AccountForUpdateDto updateAccountDto)
        {
            var (newEmail, newContact) = (updateAccountDto.Email, updateAccountDto.ContactNumber);

            // can't update if param id is different from dto id
            if (id != updateAccountDto.Id) return BadRequest ();

            // find account with given id param
            var account = await Accounts.FindAsync (id);
            if (account == null) return NotFound ();

            // check if new email is "not null and different" from current email, check for duplicate
            if (!newEmail.IsNullOrEqual (account.Email))
            {
                var emailExists = await EmailExists (newEmail);
                if (emailExists) return Conflict ("Email already exists");
            }

            // check if new contact is "not null and different" from current contact, check for duplicate
            if (!newContact.IsNullOrEqual (account.ContactNumber))
            {
                var contactExists = await ContactExists (newContact);
                if (contactExists) return Conflict ("Contact Number already exists");
            }

            // perform update
            mapper.Map<AccountForUpdateDto, Account> (updateAccountDto, account);

            try
            {
                await context.SaveChangesAsync ();
            }
            catch (DbUpdateConcurrencyException) when (IdDoesntExist (id))
            {
                return NotFound ();
            }

            return NoContent ();
        }

        [HttpDelete ("{id}")]
        public async Task<IActionResult> DeleteAccount (long id)
        {
            var account = await Accounts.FindAsync (id);
            if (account == null) return NotFound ();

            Accounts.Remove (account);
            await context.SaveChangesAsync ();
            return NoContent ();
        }

        private bool IdDoesntExist (long id) => Accounts.Any (a => a.Id == id);
        private Task<bool> UsernameExists (string username) => Accounts.AnyAsync (a => a.Username == username);
        private Task<bool> EmailExists (string email) => Accounts.AnyAsync (a => a.Email == email);
        private Task<bool> ContactExists (string contact) => Accounts.AnyAsync (a => a.ContactNumber == contact);
    }
}
