using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.UserModel
{
    public class SessionLoginModel
    {

        public long UserId {  get; set; }

        public string FisrtName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public string Token {  get; set; }
    }
}
