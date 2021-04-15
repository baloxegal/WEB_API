using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API.DTO;
using WEB_API.Models;

namespace WEB_API.Infrastructure
{
    public class GenericRepository<T, DTO> : IRepository<T, DTO> where T : class, DTO
    {
        public DbContext _context;
        public DbSet<T> _table;

        public GenericRepository(ToDoContext toDoContext)
        {
            _context = toDoContext;
            _table = _context.Set<T>();
        }        

        // GET: api/TodoItems
        public async Task<ActionResult<IEnumerable<DTO>>> ReadToDoItems()
        {
            return await _table.Select(x => ItemToDTO(x)).ToListAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DTO>> ReadToDoItem(long id)
        {
            var todoItem = await _table.FindAsync(id);
            return ItemToDTO(todoItem);
        }

        // PUT: api/TodoItems/5
        [HttpPut("{id}")]
        public async Task<ActionResult<T>> UpdateToDoItem(long id, T toDoItem)
        {
            var toDoItemBase = await _table.FindAsync(id);
            if (toDoItemBase == null)
            {
                return toDoItemBase;
            }

            toDoItemBase = toDoItem;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ToDoItemExists(id))
            {
                throw;
            }

            return toDoItemBase;
        }

        // PATCH: api/TodoItems/5
        [HttpPatch("{id}")]
        public async Task<ActionResult<DTO>> UpdateToDoItem(long id, DTO todoItemDTO)
        {
            var todoItem = await _table.FindAsync(id);
            if (todoItem == null)
            {
                return todoItem;
            }

            todoItem = (T)todoItemDTO;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ToDoItemExists(id))
            {
                throw;
            }

            return todoItem;
        }

        // POST: api/TodoItems
        [HttpPost]
        public async Task<ActionResult<DTO>> CreateToDoItem(DTO toDoItemDTO)
        {
            //T toDoItem = (T) toDoItemDTO;
            
            _table.Add((T)toDoItemDTO);
            await _context.SaveChangesAsync();

            return toDoItemDTO;
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DTO>> DeleteToDoItem(long id)
        {
            var todoItem = await _table.FindAsync(id);
            if (todoItem == null)
            {
                return todoItem;
            }

            _table.Remove(todoItem);
            await _context.SaveChangesAsync();

            return todoItem;
        }

        private bool ToDoItemExists(long id)
        {
            if (_table.Find(id) != null)
                return true;
            
            return false;
        }

        private static DTO ItemToDTO(T toDoItem)
        {
            DTO toDoItemDTO = toDoItem;
            return toDoItemDTO;
        }
            
            







        //public DbContext _context;
        //public DbSet<T> _table;

        //public GenericRepository(ToDoContext toDoContext)
        //{
        //    _context = toDoContext;
        //    _table = _context.Set<T>();
        //}

        //public IEnumerable<T> FindAll()
        //{
        //    return _table.AsNoTracking().ToList();
        //}
        //public IEnumerable<T> FindByPredicate(Func<T, bool> predicate)
        //{
        //    return _table.Where(predicate).ToList();
        //}
        //public T FindById(int id)
        //{
        //    return _table.Find(id);
        //}
        //public void Insert(T obj)
        //{
        //    _table.Add(obj);
        //}
        //public void InsertRange(params T[] obj)
        //{

        //    foreach (var o in obj)
        //    {
        //        _table.Add(o);
        //    }
        //}
        //public void Update(T obj)
        //{
        //    _table.Update(obj);
        //}
        //public bool Delete(int id)
        //{
        //    T existing = _table.Find(id);
        //    if (existing != null)
        //    {
        //        _table.Remove(existing);
        //        return true;
        //    }
        //    return false;
        //}
        //public bool Delete(T obj)
        //{
        //    if (obj != null)
        //    {
        //        _table.Remove(obj);
        //        return true;
        //    }
        //    return false;
        //}
        //public void Save()
        //{
        //    _context.SaveChanges();
        //}
    }
}
