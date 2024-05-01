using BusinessLayer.Interface;
using ModelLayer;
using ModelLayer.Model;
using ModelLayer.UserModel;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL iuserRL;

        public UserBL(IUserRL iuserRL)
        {
            this.iuserRL = iuserRL;
        }


        public UserEntity UserRegistration(UserRegistrationModel registrationModel)
        {
            try
            {
                return iuserRL.UserRegistration(registrationModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public object GetAllUsers()
        {
            try
            {
                return iuserRL.GetAllUsers();
            }
            catch (Exception ex) {
                throw ex;

            }
        }


        public bool UpdateUser(long UserId, UserUpdateModel updateModel)
        {
            try
            {
                return iuserRL.UpdateUser(UserId, updateModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool DeleteUserId(long UserId)
        {
            try
            {
                return iuserRL.DeleteUserId(UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object UserLogin(UserLoginModel loginModel)
        {
            try
            {
                return iuserRL.UserLogin(loginModel);
            }
            catch( Exception ex)
            {
                throw ex;
            }
        }


        public bool CheckEmailExists(string email)
        {
            try
            {
                return iuserRL.CheckEmailExists(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        


        public ForgotPasswordModel ForgotPassword(string Email)
        {
            try
            {
                return iuserRL.ForgotPassword(Email);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool ResetPassword(string email,ResetPasswordModel resetPassword)
        {
            try
            {
                return iuserRL.ResetPassword(email, resetPassword);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public object GetById(long UserId)
        {
            return iuserRL.GetById(UserId);
        }


        public object GetByName(string FisrtName)
        {
            return iuserRL.GetByName(FisrtName);
        }

       

        public object UpdateUserr(long UserId, UserUpdateModell UserUpdate)
        {
            try
            {
                return iuserRL.UpdateUserr(UserId, UserUpdate);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public object GetSameUserName(string FisrtName, string LastName)
        {
            try
            {
                return iuserRL.GetSameUserName(FisrtName, LastName);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public int GetUsersCount()
        {
            try
            {
                int res = iuserRL.GetUsersCount();
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public SessionLoginModel UserLoginMethod(UserLoginModel loginModel)
        {
            return iuserRL.UserLoginMethod(loginModel);
        }


        public object UpadteAndInsertt(long UserId, UserUpdateModel UpdateModel)
        {
            return iuserRL.UpadteAndInsertt(UserId, UpdateModel);
        }

      



    }


}
