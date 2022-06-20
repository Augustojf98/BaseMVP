using BaseMVP.Data;
using BaseMVP.Entities.Classes;
using Entities.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseMVP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private MyDBContext myDbContext;
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger, MyDBContext context)
        {
            _logger = logger;
            myDbContext = context;
        }

        [HttpGet("all")]
        public IEnumerable<UserSchema> GetUsers()
        {
            IEnumerable<UserDTO> usersDTO = myDbContext.Users;
            IList<UserSchema> users = new List<UserSchema>();

            foreach (UserDTO user in usersDTO)
            {
                users.Add((UserSchema)user);
            }

            return users;
        }

        //  Endpoint to obtain all active users.
        [HttpGet("active")]
        public IEnumerable<UserSchema> GetActiveUsers()
        {
            return GetUsers().Where(w => w.Active);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            UserDTO userDTO = await myDbContext.Users.FindAsync(id);
            UserSchema user = (UserSchema)userDTO;

            return Ok(user);
        }

        //  Endpoint to create a new User (The user must be created Active)
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody]NewUser userData)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserDTO user = new UserDTO(userData.Name, userData.Birth_Date);

            myDbContext.Users.Add(user);
            await myDbContext.SaveChangesAsync();

            return CreatedAtAction("GetUsers", new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromRoute] int id, [FromBody] UserDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            myDbContext.Entry(user).State = EntityState.Modified;

            try
            {
                await myDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!userExists(user.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //  Endpoint to update the User state (Active field).
        [HttpPut("/updateState/{id}")]
        public async Task<IActionResult> UpdateUserState([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserDTO user = await myDbContext.Users.FindAsync(id);

            user.Active = !user.Active;

            myDbContext.Entry(user).State = EntityState.Modified;

            try
            {
                await myDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!userExists(user.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //  Endpoint to delete users.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await myDbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            myDbContext.Users.Remove(user);
            await myDbContext.SaveChangesAsync();

            return Ok(user);
        }
        private bool userExists(int id)
        {
            return GetUsers().Where(m => m.Id == id).Any();
        }
    }
}
