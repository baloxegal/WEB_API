using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WEB_API.Infrastructure
{
    public interface IRepository<T, DTO> where T : class, DTO
    {
        public Task<ActionResult<IEnumerable<DTO>>> ReadToDoItems();
        public Task<ActionResult<DTO>> ReadToDoItem(long id);
        public Task<ActionResult<T>> UpdateToDoItem(long id, T toDoItem);
        public Task<ActionResult<DTO>> UpdateToDoItem(long id, DTO todoItemDTO);
        public Task<ActionResult<T>> CreateToDoItem(DTO toDoItemDTO);
        public Task<ActionResult<DTO>> DeleteToDoItem(long id);       


        //IEnumerable<T> FindAll();
        //IEnumerable<T> FindByPredicate(Func<T, bool> predicate);
        //T FindById(int id);
        //void Insert(T obj);
        //void InsertRange(params T[] obj);
        //void Update(T obj);
        //bool Delete(int id);
        //bool Delete(T obj);
        //void Save();
    }
}
