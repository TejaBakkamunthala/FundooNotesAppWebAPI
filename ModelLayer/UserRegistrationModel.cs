using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class UserRegistrationModel
    {
        [Required(ErrorMessage ="FisrtName is Required")]
        public string FisrtName  {get; set; }

        [Required(ErrorMessage ="LastName is Required")]
        public string LastName { get; set; }

        [Required(ErrorMessage ="Email is Required")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Password is Required")]
        public string Password { get; set; }
    }
}
