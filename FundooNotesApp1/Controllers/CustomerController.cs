using BusinessLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using ModelLayer.Model;
using ModelLayer.UserModel;
using RepositoryLayer.Entity;

namespace FundooNotesApp1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly ICustomerBL icustomerBL;
        private readonly ILogger<CustomerController> logger;
        public CustomerController(ICustomerBL icustomerBL,ILogger<CustomerController> logger)
        {
            this.icustomerBL = icustomerBL;
            this.logger = logger;
        }


        [HttpPost]
        [Route("CustomerRegistration")]

        public IActionResult CustomerRegistrationn(CustomerRegistrationModel customerRegistartionModel)
        {
            var result=icustomerBL.CustomerRegistartion(customerRegistartionModel);
            if (result != null)
            {
                return Ok(new { success = true, message = "Customer Registration Succesfull ", data = result });
            }
            else
            {
                return BadRequest(new { success = false, message = "Customer Registration Failed " });
            }
        }

        [HttpGet]
        [Route("GetAllCustomers")]
        public IActionResult GetAllCustomers()
        {
            try
            {
                var result = icustomerBL.GetAllCustomers();
                if (result != null)
                {
                    throw new Exception("Error Occured");
                    return Ok(new ResponseModelLayer<object> { IsSuccess = true, Message = "Get All Customers ", Data = result });


                }
                else
                {
                    
                    return BadRequest(new ResponseModelLayer<object> { IsSuccess = false, Message = "Not able to retreive customers data" });

                }
            }
            catch(Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }

            
            
        }


        [HttpPut]
        [Route("CustomerUpdate")]
        public IActionResult CustomerUpdate(long CustomerId,CustomerUpdateModel CustomerUpdate)
        {
            var result=icustomerBL.CustomerUpdate(CustomerId,CustomerUpdate);

            if (result != null)
            {
                return Ok(new ResponseModelLayer<bool> { IsSuccess = true, Message = "Data Updated Successfully ", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModelLayer<bool> { IsSuccess = false, Message = "Data Not updated Or id is not present " });
            }
        }


        [HttpPut]
        [Route("CustomerUpdate2")]
        public IActionResult CustomerUpadte2(long CustomerId,CustomerUpdateModel CustomerUpdate)
        {
            var result=icustomerBL.CustomerUpdate2(CustomerId,CustomerUpdate);

            if (result != null){

                return Ok(new ResponseModelLayer<object> { IsSuccess = true, Message = "Data Upadted Successfully", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModelLayer<object> { IsSuccess = false, Message = "Data Not Updated Or that id is n ot present", Data = result });
            }

        }


        [HttpDelete]
        [Route("DeleteById")]
        public IActionResult DeleteCustomerById(long CustomerId)
        {
            var result=icustomerBL.DeleteByCustomerId(CustomerId);
            if(result != null)
            {
                return Ok(new ResponseModelLayer<object> { IsSuccess = true,Message="Deleted Successfully",Data= result});  
            }
            else
            {
                return BadRequest(new ResponseModelLayer<object> { IsSuccess = false, Message = "Not Deleted Or Customer Id is not present ", Data = result });
            }
        }


       

    }
}
