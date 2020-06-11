using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Authentification.Models;
using Z.EntityFramework.Plus;
using System.Text.Json;
using System.Text;

namespace Authentification.Controllers
{
    [Route("api")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AuthContext _context;

        public UsersController(AuthContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<User>> GetUser([FromBody]UserIdentity userIdentity)
        {
            if (UserExists(userIdentity))
            {
                User user = await _context.User.Where(b => b.Token == userIdentity.Token).FirstAsync();
                return new OkObjectResult(GetPublic.GetPublicUser(user));
            }

            return NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> PutUser([FromBody]PublicUser publicuser)
        {
            if (!UserExists(publicuser))
            {
                return NotFound();
            }
            try
            {
                _context.User.Where(b => b.Id == publicuser.Id).Update(b => new User()
                {
                    Name = publicuser.Name,
                    MiddleName = publicuser.MiddleName,
                    SurName = publicuser.SurName
                });

            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok();
        }

        // DELETE: api/Users/5
        [HttpDelete]
        public async Task<ActionResult> DeleteUser([FromBody]UserIdentity userIdentity)
        {
            if (!UserExists(userIdentity))
            {
                return NotFound();
            }

            await _context.User.Where(b => b.Token == userIdentity.Token).DeleteAsync();

            return Ok();
        }

        [HttpGet]
        [Route("getLogin")]
        public bool GetLogins(string login)
        {
            return _context.User.Any(b=>b.Login==login);
        }


        public bool UserExists(UserIdentity userIdentity)
        {

            return _context.User.Any(b => b.Token == userIdentity.Token && b.Id == userIdentity.Id && b.Login == userIdentity.Login);
        }
    }
}
