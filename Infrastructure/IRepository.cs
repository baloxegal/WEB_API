using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WEB_API.Infrastructure
{
    public interface IRepository<T, DTO> where T : class, DTO
    {
        public Task<ActionResult<IEnumerable<DTO>>> ReadUsers();
        public Task<ActionResult<DTO>> ReadUser(long id);
        public Task<ActionResult<T>> UpdateUser(long id, T user);
        public Task<ActionResult<DTO>> UpdateUser(long id, DTO userDTO);
        public Task<ActionResult<T>> CreateUser(DTO userDTO);
        public Task<ActionResult<DTO>> DeleteUser(long id);  
    }
}
