using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API.DTO;

namespace WEB_API.Models
{
    public class User : UserDTO
    {
        public string Secret { get; set; }
    }
}
