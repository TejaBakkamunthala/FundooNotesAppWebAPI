

using BusinessLayer.Interface;
using MassTransit;
using MassTransit.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ModelLayer;
using ModelLayer.Model;
using ModelLayer.UserModel;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Migrations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace FundooNotesApp1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL iuserBL;
        private readonly IBus bus;


        public UserController(IUserBL iuserBL,IBus bus)
        {
            this.iuserBL = iuserBL;
            this.bus = bus;

        }

        [HttpPost]
        [Route("Register")]
        public IActionResult RegisterUser(UserRegistrationModel registrationModel)
        {
            try
            {
                var response = iuserBL.CheckEmailExists(registrationModel.Email);
                if (!response)
                {
                    var result = iuserBL.UserRegistration(registrationModel);
                    if (result != null)
                    {
                        
                        return Ok(new { success = true, message = "Registration Successful", data = result });
                    }
                    else
                    {
                        return BadRequest(new { success = false, message = "Registration Failed" });
                    }
                }

                else
                {
                    return BadRequest(new { success = false, message = " Email Already Exists" });

                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }


        [HttpGet]
        [Route("GetAllData")]
        public IActionResult GetAllUserss()
        {
            try
            {
                var result = iuserBL.GetAllUsers();

                if (result != null)
                {
                    return Ok(new { success = true, message = "Get all data successfull", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Get all data failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPut]
        [Route("Update Data")]

        public IActionResult UpdateUserss(long UserId, UserUpdateModel updateModel)
        {
            try
            {
                bool result = iuserBL.UpdateUser(UserId, updateModel);
                if (result)
                {

                    return Ok(new { success = true, message = "Successfully Data Updated ", data = result });

                }
                else
                {

                    return Ok(new { succes = false, messaage = "Data Updation Failed " });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        [Route("Delete Data")]
        public IActionResult DeleteUserss(long UserId)
        {
            try
            {
                bool result = iuserBL.DeleteUserId(UserId);
                if (result)
                {
                    return Ok(new { success = true, message = "Data Deleted Successfully ", data = result });
                }
                else
                {
                    return Ok(new { success = false, message = "Data Deleted Failed" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpPost]
        [Route("Userlogin")]
        public IActionResult LoginUserr(UserLoginModel userLoginModel)
        {
            try
            {
                
                var result = iuserBL.UserLogin(userLoginModel);
                if (result != null)
                { 
                   
                   
                    return Ok(new { success = true, message = "Login Successfull", data = result });
                }
                else
                {
                    return Ok(new { success = false, message = "Login Failed" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //[HttpPut]
        //[Route("check Eamil Exists")]
        //public IActionResult CheckEmailExistss(string email)
        //{
        //    try
        //    {
        //        bool result = iuserBL.CheckEmailExists(email);
        //        if (result)
        //        {
        //            return Ok(new { success = true, message = "Email Exists", data = result });
        //        }
        //        else
        //        {
        //            return Ok(new { success = false, message = "Email Not Exists" });
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }

        //}



        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(string Email)
        {

            var password = iuserBL.ForgotPassword(Email);

            if (password != null)
            {
                SendModelLayer send = new SendModelLayer();
                ForgotPasswordModel forgotPasswordModel = iuserBL.ForgotPassword(Email);
                send.SendMail(forgotPasswordModel.Email, forgotPasswordModel.Token);
                Uri uri = new Uri("rabbitmq:://localhost/FunDooNotesEmailQueue");
                var endPoint = await bus.GetSendEndpoint(uri);
                await endPoint.Send(forgotPasswordModel);
                return Ok(new { success = true, message = "Mail sent Successfully", data = password.Token });
            }
            else
            {
                return BadRequest(new { success = false, message = "Email Does not Exist" });
            }


        }


        [Authorize]
        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword(ResetPasswordModel reset)
        {
            string Email = User.Claims.FirstOrDefault(x => x.Type == "Email").Value;
            var res = iuserBL.ResetPassword(Email, reset);
            if (res)
            {
                return Ok(new { success = true, message = "Password Reset is done" });

            }
            else
            {
                return BadRequest("Password is not Updated");
            }
        }


        [HttpGet]
        [Route("GetByID")]
        public IActionResult GetById(long UserId)
        {
            var result=iuserBL.GetById(UserId);

            if (result != null)
            {
                return Ok(new { success = true, message = "Get Data By Id Successfull", data = result });
            }
            else
            {
                return BadRequest(new { success = false, message = "Id is not present " });
            }
        }


        [HttpGet]
        [Route("GetByName")]
        public IActionResult GetByName(string FisrtName)
        {
            var result=iuserBL.GetByName(FisrtName);
            if(result != null)
            {
                return Ok(new ResponseModelLayer<object> { IsSuccess = true, Message = "Get Data By Name Successfull", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModelLayer<object> { IsSuccess = false, Message = "Name is not present " });
            }

        }

       

        [HttpPut]
        [Route("UpdateUserr")]
        public IActionResult UpadteUserr(long UserId,UserUpdateModell UserUpdate)
        {
            var result=iuserBL.UpdateUserr(UserId, UserUpdate);

            if (result != null)
            {
                return Ok(new ResponseModelLayer<object> { IsSuccess = true, Message = "Data Update Successfully", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModelLayer<object> { IsSuccess = false, Message = "Data Not Updated Or UserId is not present" });
            }
        }

        [HttpGet]
        [Route("GetSameUserName")]
        public IActionResult GetSameUser(string FisrtName, String LastName)
        {
            var result = iuserBL.GetSameUserName(FisrtName, LastName);

            if (result != null)
            {
                return Ok(new ResponseModelLayer<object> { IsSuccess = true, Message = "Dat Retreive Successfully", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModelLayer<object> { IsSuccess = false, Message = "Data Retreive Failed" });
            }
        }

        [HttpGet]
        [Route("GetAllUsersCount")]
        public IActionResult GetUsersCount()
        {

            int result = iuserBL.GetUsersCount();

            if (result != 0)
            {
                return Ok(new ResponseModelLayer<int> { IsSuccess = true, Message = "Get Users Count Successfull", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModelLayer<int> { IsSuccess = false, Message = "Table is not present Or Data is not present in that table" });
            }
        }


        [HttpPost]
        [Route("UserLoginMethod")]
        public IActionResult UserLoginMethod(UserLoginModel loginModel)
        {
            var result=iuserBL.UserLoginMethod(loginModel);


            if (result != null)
            {

                HttpContext.Session.SetInt32("UserID", (int)result.UserId);

                return Ok(new ResponseModelLayer<object> { IsSuccess=true,Message="Login Successfully ",Data=result});
            }
            else
            {
                return BadRequest(new ResponseModelLayer<object> { IsSuccess = false, Message = "Login Failed ", Data = result });
            }
        }

       

       
        [HttpPost]
        [Route("UpdateAnInsert")]

        public IActionResult UpdateAndInsertt(long UserId,UserUpdateModel updateModel)
        {
            var result=iuserBL.UpadteAndInsertt(UserId,updateModel);

            if (result != null)
            {
                return Ok(new ResponseModelLayer<object> { IsSuccess = true, Message = "Updated And Inserted Succesfully ", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModelLayer<object> { IsSuccess = false, Message = "Updated And Inserted Failed " });
            }
        }

    }
}
