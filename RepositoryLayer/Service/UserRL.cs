using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModelLayer;
using ModelLayer.Model;
using ModelLayer.UserModel;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        private readonly FundooContext fundooContext;

        private readonly IConfiguration configuration;
        public UserRL(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }

        public UserEntity UserRegistration(UserRegistrationModel registrationModel)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FisrtName = registrationModel.FisrtName;
                userEntity.LastName = registrationModel.LastName;
                userEntity.Email = registrationModel.Email;
                userEntity.Password = EncryptPassword(registrationModel.Password);


                fundooContext.UserTablee.Add(userEntity);
                int result = fundooContext.SaveChanges();
                if (result != 0)
                {
                    return userEntity;
                }
                else
                {
                    return null;
                }

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
                var result = fundooContext.UserTablee.ToList();

                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public bool UpdateUser(long UserId, UserUpdateModel updateModel)
        {
            try
            {
                var res = fundooContext.UserTablee.FirstOrDefault(user => user.UserId == UserId);
                if (res != null)
                {
                    res.FisrtName = updateModel.FisrtName;
                    res.LastName = updateModel.LastName;
                    res.Email = updateModel.email;
                    res.Password = updateModel.password;
                    fundooContext.SaveChanges();
                    return true;

                }
                else
                {
                    return false;
                }
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
                var result = fundooContext.UserTablee.FirstOrDefault(x => x.UserId == UserId);
                if (result != null)
                {
                    fundooContext.UserTablee.Remove(result);
                    fundooContext.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public static string EncryptPassword(string password)
        {
            try
            {
                byte[] encrypt_password = new byte[password.Length];
                encrypt_password = Encoding.UTF8.GetBytes(password);
                string encodedPassword = Convert.ToBase64String(encrypt_password);
                return encodedPassword;

            }
            catch (Exception ex)
            {
                return $"Encryption Failed.! {ex.Message}";
            }
        }


        public static string DecryptPassword(string encryptedPassword)
        {
            try
            {
                byte[] decrypt_password = Convert.FromBase64String(encryptedPassword);
                string originalPassword = Encoding.UTF8.GetString(decrypt_password);
                return originalPassword;
            }
            catch (Exception ex)
            {
                return $"Decryption Failed.! {ex.Message}";
            }
        }


        public object UserLogin(UserLoginModel loginModel)
        {
            try
            {
                string encodedPassword = EncryptPassword(loginModel.Password);
                var userlogin = fundooContext.UserTablee.FirstOrDefault(u => u.Email == loginModel.Email && u.Password == encodedPassword);

                //string decodedPassword = DecryptPassword(loginModel.Password);
                //var userlogin = fundooContext.UserTablee.FirstOrDefault(u => u.Email == loginModel.Email && u.Password == decodedPassword);


                if (userlogin != null)
                {
                    var token = GenerateToken(userlogin.UserId, userlogin.Email);
                    return token;
                    
                }
                else
                {
                    return null;
                }
            }


            catch (Exception ex)
            {
                return ex;
            }

          

        }

      
        private string GenerateToken(long UserId, string Email)
        {
            // Create a symmetric security key using the JWT key specified in the configuration
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            // Create signing credentials using the security key and HMAC-SHA256 algorithm
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            // Define claims to be included in the JWT
            var claims = new[]
            {
                new Claim("Email",Email),
                new Claim("UserId", UserId.ToString())
            };
            // Create a JWT with specified issuer, audience, claims, expiration time, and signing credentials
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMonths(3),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        public bool CheckEmailExists(string email)
        {
            try
            {
                bool res = fundooContext.UserTablee.Any(x => x.Email == email);
                if (res)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ForgotPasswordModel ForgotPassword(string Email)
        {
            UserEntity user = fundooContext.UserTablee.ToList().Find(user=>user.Email == Email);

            ForgotPasswordModel forgotPasswordModel = new ForgotPasswordModel();
            forgotPasswordModel.UserId = user.UserId;
            forgotPasswordModel.Email=user.Email;
            forgotPasswordModel.Token=GenerateToken(user.UserId,user.Email);

            return forgotPasswordModel;

        }

        public bool ResetPassword(string Email, ResetPasswordModel resetPasswordModel)
        {
            UserEntity User = fundooContext.UserTablee.ToList().Find(x => x.Email == Email);
            if (User != null)
            {
                User.Password = EncryptPassword(resetPasswordModel.ConfirmPassword);
                fundooContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }
        

        //USER  BY USERID


        public object GetById( long UserId) {
        
            //var result=fundooContext.UserTablee.FirstOrDefault(user=>user.UserId== UserId);
            var result=fundooContext.UserTablee.ToList().Find(user=>user.UserId == UserId);

            if (result!=null)
            {
                return result;
            }
            else
            {
                return null;
            }

        }

        //GetByName
        public object GetByName(string FisrtName)
        {
            var result=fundooContext.UserTablee.ToList().Find(user=>user.FisrtName == FisrtName);

            if(result!=null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }





        /* Check availability of User using any table parameter(columns) and if user exist update lastname and firstname.
     -> search for User using username, if user exist show user details.If more than one user exists show details of all of them.
     -> count the no of users
     Priyanshu Singh A
     10:12 AM
     2) create a table product(productid, brand , productname) */


        //    Check availability of User using any table parameter(columns) and if user exist update lastname and firstname.

        public object UpdateUserr(long UserId,UserUpdateModell UserUpdate)
        {
            var result=fundooContext.UserTablee.FirstOrDefault(user=>user.UserId == UserId);

            if (result!=null)
            {
               result.FisrtName=UserUpdate.FisrtName;
               result.LastName=UserUpdate.LastName;

                fundooContext.SaveChanges();
                return result;

            }
            else
            {
                return null;
            }
        }

        // search for User using username, if user exist show user details.If more than one user exists show details of all of them.


        public object GetSameUserName(string FisrtName,string LastName)
        {
            try
            {
                var result = fundooContext.UserTablee.Where(user => user.FisrtName == FisrtName && user.LastName == LastName).ToList();

                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }



        //count
        public int GetUsersCount()
        {
            try
            {
                int result = fundooContext.UserTablee.Count();
                if (result != 0)
                {
                    return result;
                }
                else
                {
                    return 0;
                }
            }catch(Exception ex)
            {
                throw ex;
            }
        }


        // FOR SE3SSION
        public SessionLoginModel UserLoginMethod(UserLoginModel loginModel)
        {
            string encodedPassword = EncryptPassword(loginModel.Password);
            var userlogin = fundooContext.UserTablee.FirstOrDefault(user => user.Email == loginModel.Email && user.Password == encodedPassword);

            if(userlogin != null)
            {
                SessionLoginModel sessionModel=new SessionLoginModel();
                sessionModel.UserId=userlogin.UserId;
                sessionModel.FisrtName=userlogin.FisrtName;
                sessionModel.LastName=userlogin.LastName;
                sessionModel.Email=userlogin.Email;
                sessionModel.Password=encodedPassword;
                sessionModel.Token = GenerateToken(userlogin.UserId, userlogin.Email);
                return  sessionModel;
                
            }
            else
            {
                return null;
            }

    
        }




        /* 1) check if a userid exist. if exists, update the userdetails else insert a new deatail of user
         2) fetch a note of user using title and description
         3) search a note on basis of their created date and fetch details of notes */

        // 1) check if a userid exist. if exists, update the userdetails else insert a new deatail of user

        public object UpadteAndInsertt(long UserId,UserUpdateModel UpdateModel)
        {

            try
            {
                var user = fundooContext.UserTablee.FirstOrDefault(user => user.UserId == UserId);
                if (user != null)
                {
                    user.FisrtName = UpdateModel.FisrtName;
                    user.LastName = UpdateModel.LastName;
                    user.Email = UpdateModel.email;
                    user.Password = UpdateModel.password;
                    fundooContext.SaveChanges();
                    return "Data Updated Successfully";
                }
                else
                {
                    UserEntity userEntity = new UserEntity();
                    userEntity.FisrtName = UpdateModel.FisrtName;
                    userEntity.LastName = UpdateModel.LastName;
                    userEntity.Email = UpdateModel.email;
                    userEntity.Password = UpdateModel.password;
                    fundooContext.Add(userEntity);
                    fundooContext.SaveChanges();
                    return "Data Inserted Succesfully";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }






    }

}
