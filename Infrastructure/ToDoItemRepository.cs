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
        public override ToDoItemDTO ItemToDTO(ToDoItem toDoItem) =>
            new ToDoItemDTO
            {
                Id = toDoItem.Id,
                Name = toDoItem.Name,
                IsComplete = toDoItem.IsComplete
            };
        public override ToDoItem DTOToItem(ToDoItemDTO toDoItemDTO) =>
            new ToDoItem
            {
                Id = toDoItemDTO.Id,
                Name = toDoItemDTO.Name,
                IsComplete = toDoItemDTO.IsComplete
            };

        public override void ModifyToDoItemDTO(ToDoItemDTO toDoItemBase, ToDoItemDTO toDoItem)
        {
            toDoItemBase.Id = toDoItem.Id;
            toDoItemBase.Name = toDoItem.Name;
            toDoItemBase.IsComplete = toDoItem.IsComplete;
        }
        public override void ModifyToDoItem(ToDoItem toDoItemBase, ToDoItem toDoItem)
        {
            toDoItemBase.Id = toDoItem.Id;
            toDoItemBase.Name = toDoItem.Name;
            toDoItemBase.IsComplete = toDoItem.IsComplete;
            toDoItemBase.Secret = toDoItem.Secret;
        }
    }
}
