using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_API.DTO;
using WEB_API.Infrastructure;
using WEB_API.Models;

namespace WEB_API.Controllers
{

    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRepository<User, UserDTO> _repo;

        public UsersController(UserContext context)
        {
            _repo = new UserRepository(context);

        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> ReadUsers()
        {
            var collection = await _repo.ReadUsers();
            if (collection.Value.Count() == 0)
                return NoContent();
            return collection;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> ReadUser(long id)
        {
            var userDTO = await _repo.ReadUser(id);

            if (userDTO == null)
            {
                return NotFound();
            }

            return userDTO;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(long id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
                        
            try
            {
                var userBase = await _repo.UpdateUser(id, user);
                if (userBase.Value == null)
                {
                    return NotFound();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return Ok();
        }

        // PATCH: api/Users/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUser(long id, UserDTO userDTO)
        {
            if (id != userDTO.Id)
            {
                return BadRequest();
            }

            try
            {                
                var userDTOBase = await _repo.UpdateUser(id, userDTO);
                if (userDTOBase == null)
                {
                    return NotFound();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return Ok();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<UserDTO>> CreateUser(UserDTO toDoItemDTO)
        {
            User user = null;
            try
            {
                var userResult = await _repo.CreateUser(toDoItemDTO);
                user = userResult.Value;
            }
            catch (DbUpdateException)
            {
                return Conflict();
            }
            UserDTO ItemToDTO(User user) =>
                new UserDTO
                {
                    Id = user.Id,
                    Name = user.Name,
                    IsEmployee = user.IsEmployee
                };

            return CreatedAtAction(
                nameof(ReadUser),
                new { id = user.Id },
                ItemToDTO(user));
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var user = await _repo.DeleteUser(id);
            if (user.Value == null)
            {
                return NotFound();
            }

            return Ok();
        }       
    }
}
