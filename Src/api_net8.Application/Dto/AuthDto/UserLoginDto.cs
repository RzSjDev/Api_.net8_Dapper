using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace dotnet_rpg.Dtos.User
{
    public class UserLoginDto
    {
        [Required]
        public string UserNameOrEmail { get; set; }

        [DataType(dataType:DataType.Password)]
        [Required]
        public string Password { get; set; }
    }
}