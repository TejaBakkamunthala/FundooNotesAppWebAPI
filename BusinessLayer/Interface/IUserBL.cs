using Microsoft.AspNetCore.Http;
using ModelLayer;
using ModelLayer.Model;
using ModelLayer.UserModel;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        public UserEntity UserRegistration(UserRegistrationModel registrationModel);

        public object GetAllUsers();

       public bool UpdateUser(long UserId, UserUpdateModel updateModel);

        public bool DeleteUserId(long UserId);

        public object UserLogin(UserLoginModel loginModel);

        public bool CheckEmailExists(string email);

        public ForgotPasswordModel ForgotPassword(string email);

        public bool ResetPassword(string email, ResetPasswordModel resetPassword);

        public object GetById(long UserId);

        public object GetByName(string FisrtName);

        public object UpdateUserr(long UserId, UserUpdateModell UserUpdate);

        public object GetSameUserName(string FisrtName, string LastName);

        public int GetUsersCount();

        public SessionLoginModel UserLoginMethod(UserLoginModel loginModel);

      
        public object UpadteAndInsertt(long UserId, UserUpdateModel UpdateModel);



















    }
}
