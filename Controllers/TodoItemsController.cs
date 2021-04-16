using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_API.DTO;
using WEB_API.Infrastructure;
using WEB_API.Models;

namespace WEB_API.Controllers
{

    [Route("api/TodoItems")]
    [ApiController]
    public class ToDoItemsController : ControllerBase
    {
        private readonly IRepository<ToDoItem, ToDoItemDTO> _repo;

        public ToDoItemsController(ToDoContext context)
        {
            _repo = new ToDoItemRepository(context);

        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItemDTO>>> ReadToDoItems()
        {
            var collection = await _repo.ReadToDoItems();
            if (collection.Value.Count() == 0)
                return NoContent();
            return collection;
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItemDTO>> ReadToDoItem(long id)
        {
            var todoItemDTO = await _repo.ReadToDoItem(id);

            if (todoItemDTO == null)
            {
                return NotFound();
            }

            return todoItemDTO;
        }

        // PUT: api/TodoItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateToDoItem(long id, ToDoItem toDoItem)
        {
            if (id != toDoItem.Id)
            {
                return BadRequest();
            }
                        
            try
            {
                var toDoItemBase = await _repo.UpdateToDoItem(id, toDoItem);
                if (toDoItemBase == null)
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

        // PATCH: api/TodoItems/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateToDoItem(long id, ToDoItemDTO toDoItemDTO)
        {
            if (id != toDoItemDTO.Id)
            {
                return BadRequest();
            }

            try
            {                
                var toDoItemDTOBase = await _repo.UpdateToDoItem(id, toDoItemDTO);
                if (toDoItemDTOBase == null)
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

        // POST: api/TodoItems
        [HttpPost]
        public async Task<ActionResult<ToDoItemDTO>> CreateToDoItem(ToDoItemDTO toDoItemDTO)
        {
            ToDoItem toDoItem = null;
            try
            {
                toDoItem = await _repo.CreateToDoItem(toDoItemDTO);
            }
            catch (DbUpdateException)
            {
                return Conflict();
            }

            return CreatedAtAction(
                nameof(ReadToDoItem),
                new { id = toDoItem.Id },
                ItemToDTO(toDoItem));
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoItem(long id)
        {
            var todoItem = await _repo.DeleteToDoItem(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            return Ok();
        }

        public ToDoItemDTO ItemToDTO(ToDoItem toDoItem) =>
            new ToDoItemDTO
            {
                Id = toDoItem.Id,
                Name = toDoItem.Name,
                IsComplete = toDoItem.IsComplete
            };
    }



    //[Route("api/TodoItems")]
    //[ApiController]
    //public class ToDoItemsController : ControllerBase
    //{
    //    private readonly ToDoContext _context;

    //    public ToDoItemsController(ToDoContext context)
    //    {
    //        _context = context;
    //    }

    //    // GET: api/TodoItems
    //    [HttpGet]
    //    public async Task<ActionResult<IEnumerable<ToDoItemDTO>>> ReadToDoItems()
    //    {
    //        return await _context.ToDoItems.Select(x => ItemToDTO(x)).ToListAsync();
    //    }

    //    // GET: api/TodoItems/5
    //    [HttpGet("{id}")]
    //    public async Task<ActionResult<ToDoItemDTO>> ReadToDoItem(long id)
    //    {
    //        var todoItem = await _context.ToDoItems.FindAsync(id);

    //        if (todoItem == null)
    //        {
    //            return NotFound();
    //        }

    //        return ItemToDTO(todoItem);
    //    }

    //    // PUT: api/TodoItems/5
    //    [HttpPut("{id}")]
    //    public async Task<IActionResult> UpdateToDoItem(long id, ToDoItem toDoItem)
    //    {
    //        if (id != toDoItem.Id)
    //        {
    //            return BadRequest();
    //        }

    //        var toDoItemBase = await _context.ToDoItems.FindAsync(id);
    //        if (toDoItemBase == null)
    //        {
    //            return NotFound();
    //        }

    //        toDoItemBase.Name = toDoItem.Name;
    //        toDoItemBase.IsComplete = toDoItem.IsComplete;
    //        toDoItemBase.Secret = toDoItem.Secret;

    //        try
    //        {
    //            await _context.SaveChangesAsync();
    //        }
    //        catch (DbUpdateConcurrencyException) when (!ToDoItemExists(id))
    //        {
    //            return NotFound();
    //        }

    //        return NoContent();
    //    }

    //    // PATCH: api/TodoItems/5
    //    [HttpPatch("{id}")]
    //    public async Task<IActionResult> UpdateToDoItem(long id, ToDoItemDTO todoItemDTO)
    //    {
    //        if (id != todoItemDTO.Id)
    //        {
    //            return BadRequest();
    //        }

    //        var todoItem = await _context.ToDoItems.FindAsync(id);
    //        if (todoItem == null)
    //        {
    //            return NotFound();
    //        }

    //        todoItem.Name = todoItemDTO.Name;
    //        todoItem.IsComplete = todoItemDTO.IsComplete;

    //        try
    //        {
    //            await _context.SaveChangesAsync();
    //        }
    //        catch (DbUpdateConcurrencyException) when (!ToDoItemExists(id))
    //        {
    //            return NotFound();
    //        }

    //        return NoContent();
    //    }

    //    // POST: api/TodoItems
    //    [HttpPost]
    //    public async Task<ActionResult<ToDoItemDTO>> CreateToDoItem(ToDoItemDTO toDoItemDTO)
    //    {
    //        var toDoItem = new ToDoItem
    //        {
    //            IsComplete = toDoItemDTO.IsComplete,
    //            Name = toDoItemDTO.Name
    //        };

    //        _context.ToDoItems.Add(toDoItem);
    //        await _context.SaveChangesAsync();

    //        return CreatedAtAction(
    //            nameof(ReadToDoItem),
    //            new { id = toDoItem.Id },
    //            ItemToDTO(toDoItem));
    //    }

    //    // DELETE: api/TodoItems/5
    //    [HttpDelete("{id}")]
    //    public async Task<IActionResult> DeleteToDoItem(long id)
    //    {
    //        var todoItem = await _context.ToDoItems.FindAsync(id);
    //        if (todoItem == null)
    //        {
    //            return NotFound();
    //        }

    //        _context.ToDoItems.Remove(todoItem);
    //        await _context.SaveChangesAsync();

    //        return NoContent();
    //    }

    //    private bool ToDoItemExists(long id)
    //    {
    //        return _context.ToDoItems.Any(e => e.Id == id);
    //    }

    //    private static ToDoItemDTO ItemToDTO(ToDoItem todoItem) =>
    //        new ToDoItemDTO
    //        {
    //            Id = todoItem.Id,
    //            Name = todoItem.Name,
    //            IsComplete = todoItem.IsComplete
    //        };
    //}
}
