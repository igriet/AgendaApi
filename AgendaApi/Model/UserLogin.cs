using System.ComponentModel.DataAnnotations;

namespace AgendaApi.Model
{
    public class UserLogin
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public UserLogin()
        {

        }
    }
}
