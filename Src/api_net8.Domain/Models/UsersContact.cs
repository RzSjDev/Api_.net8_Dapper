using System.ComponentModel.DataAnnotations;

namespace api.net.Models
{
    public class UsersContact
    {
        [Key]
        public int UserId { get; set; }
        public string name { get; set; }
        public string family { get; set; }
        public int age { get; set; }
        public string phone { get; set; }
        public string city { get; set; }
        public string email { get; set; }
        public int UserAuthId { get; set; }
        public UserAuth userAuth{ get; set; }

    }
}