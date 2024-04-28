using System.ComponentModel.DataAnnotations;
using api_net8.Domain.Models.Enum;

namespace api.net.Models
{
    public class UserAuth
    {
        [Key]
        public int UserAuthId { get; set; }
        public string UserName { get; set; }      
        public string Email { get; set; }
        public Role role { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] Passwordhash { get; set; }
        public List<UsersContact> usersContacts{ get; set; }
    }
}
