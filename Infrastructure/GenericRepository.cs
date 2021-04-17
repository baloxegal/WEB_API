using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API.Models;

namespace WEB_API.Infrastructure
{
    public class GenericRepository<T, DTO> : IRepository<T, DTO> where T : class, DTO
    {
        public DbContext _context;
        public DbSet<T> _table;

        public GenericRepository(UserContext userContext)
        {
            _context = userContext;
            _table = _context.Set<T>();
        }        

        // GET: api/Users
        public async Task<ActionResult<IEnumerable<DTO>>> ReadUsers()
        {
            return await _table.Select(user => UserToUserDTO(user)).ToListAsync();
        }

        // GET: api/Users/5
        public async Task<ActionResult<DTO>> ReadUser(long id)
        {
            var user = await _table.FindAsync(id);
            if (user == null)
                return user;
            return UserToUserDTO(user);
        }

        // PUT: api/Users/5
        public async Task<ActionResult<T>> UpdateUser(long id, T user)
        {
            var userBase = await _table.FindAsync(id);
            if (userBase == null)
            {
                return userBase;
            }

            ModifyUser(userBase, user);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!UserExists(id))
            {
                throw;
            }

            return userBase;
        }

        // PATCH: api/Users/5
        public async Task<ActionResult<DTO>> UpdateUser(long id, DTO userDTO)
        {
            var user = await _table.FindAsync(id);
            if (user == null)
            {
                return user;
            }

            ModifyUserDTO(user, userDTO);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!UserExists(id))
            {
                throw;
            }

            return userDTO;
        }

        // POST: api/Users
        public async Task<ActionResult<T>> CreateUser(DTO userDTO)
        {
            T user = UserDTOToUser(userDTO);
            try
            {
                _table.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return user;
        }

        // DELETE: api/Users/5
        public async Task<ActionResult<DTO>> DeleteUser(long id)
        {
            var user = await _table.FindAsync(id);
            if (user == null)
            {
                return user;
            }

            _table.Remove(user);
            await _context.SaveChangesAsync();

            return UserToUserDTO(user);
        }

        private bool UserExists(long id)
        {
            if (_table.Find(id) != null)
                return true;
            
            return false;
        }
        public virtual DTO UserToUserDTO(T user)
        {
            DTO userDTO = user;
            return userDTO;
        }
        public virtual T UserDTOToUser(DTO userDTO)
        {
            T user = (T)userDTO;
            return user;
        }
        public virtual void ModifyUserDTO(DTO userBase, DTO user)
        {
        }
        public virtual void ModifyUser(T userBase, T user)
        {
        }
    }
}
