using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API.Models;

namespace WEB_API.DTO
{
    public class ToDoItemDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }

        public ToDoItemDTO(TodoItem toDoItem)
        {
            Id = toDoItem.Id;
            Name = toDoItem.Name;
            IsComplete = toDoItem.IsComplete;
        }
    }
}
