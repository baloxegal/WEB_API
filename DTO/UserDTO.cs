using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API.Models;

namespace WEB_API.DTO
{
    public class UserDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsEmployee { get; set; }
    }
}
