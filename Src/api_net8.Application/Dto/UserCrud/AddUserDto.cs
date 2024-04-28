using api.net.Models;
using System.ComponentModel.DataAnnotations;

namespace api.net.Dto.getCharacter
{
    public class AddUserDto
    {
        [StringLength(40)]
        [Required]
        [MinLength(3)]
        public string name { get; set; }
        [StringLength(50)]
        [Required]
        [MinLength(3)]
        public string family { get; set; }

        [Range(1, 100, ErrorMessage = "")]
        public int age { get; set; }
        [Required]
        [RegularExpression("^[0-9]{10}$",ErrorMessage ="Phone Number Must Be 10 Digits")]
        public string phone { get; set; }
        [StringLength(50)]
        public string city { get; set; }
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
    }
}