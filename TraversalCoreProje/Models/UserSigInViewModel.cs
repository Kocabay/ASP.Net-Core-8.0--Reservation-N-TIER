using System.ComponentModel.DataAnnotations;

namespace TraversalCoreProje.Models
{
    public class UserSigInViewModel
    {
        [Required(ErrorMessage ="Lütfen Kullanıcı Adınızı Giriniz.")]
        public string username { get; set; }



        [Required(ErrorMessage = "Lütfen Şifrenizi Giriniz.")]
        public string password { get; set; }
    }
}
