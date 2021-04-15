using Microsoft.AspNetCore.Mvc;
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

        public static implicit operator ToDoItemDTO(ActionResult<ToDoItemDTO> v)
        {
            throw new NotImplementedException();
        }
    }
}
