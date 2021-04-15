using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API.DTO;
using WEB_API.Models;

namespace WEB_API.Infrastructure
{
    public class ToDoItemRepository : GenericRepository<ToDoItem, ToDoItemDTO>
    {
        public ToDoItemRepository(ToDoContext toDoContext) : base(toDoContext)
        {
        }
    }
}
