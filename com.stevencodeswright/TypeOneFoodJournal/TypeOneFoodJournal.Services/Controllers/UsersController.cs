using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TypeOneFoodJournal.Data;
using TypeOneFoodJournal.Entities;
using TypeOneFoodJournal.Models;

namespace TypeOneFoodJournal.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly FoodJournalContext context;

        public UsersController(FoodJournalContext context)
        {
            this.context = context;
        }

#if !DEBUG
        [Authorize]
#endif
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUsers([FromQuery] string email)
        {
            var upperEmail = email.ToUpper();
            var retVal = new List<UserModel>();
            try
            {
                var user = await this.context.Users.FirstOrDefaultAsync(x => x.Email.ToUpper().Equals(upperEmail));


                if (user is null)
                {
                    return BadRequest("Invalid");
                }
                else
                {
                    retVal.Add(new UserModel()
                    {
                        Id = user.Id,
                        Email = user.Email,
                    });
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(retVal);
        }

        [AllowAnonymous]
        [HttpPost("createaccount")]
        public async Task<IActionResult> CreateAccount([FromBody] UserModel model)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(x => x.Email.ToUpper().Equals(model.Email.ToUpper()));

            if (user != null)
            {
                return BadRequest(new { message = "Email already exists" });
            }
            else
            {
                var newUser = new User()
                {
                    Email = model.Email,
                    UserPassword = new UserPassword()
                    {
                        Password = new Password()
                        {
                            Text = SecurePasswordHasher.Hash(model.Password),
                        }
                    }
                };

                this.context.Users.Add(newUser);
                await this.context.SaveChangesAsync();

                return Ok(new UserModel()
                {
                    Id = newUser.Id,
                    Email = newUser.Email
                });

            }
        }
    }
}
