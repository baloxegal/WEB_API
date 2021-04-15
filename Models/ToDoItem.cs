using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API.DTO;

namespace WEB_API.Models
{
    public class ToDoItem : ToDoItemDTO
    {
        public string Secret { get; set; }
    }
}
