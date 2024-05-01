using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using RepositoryLayer.Entity;
using System.Xml.Linq;

namespace FundooNotesApp1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL ilableBL;

        public LabelController(ILabelBL ilableBL)
        {
            this.ilableBL = ilableBL;
        }

        [Authorize]
        [HttpPost]
        [Route("AddLabel")]
        public IActionResult AddLabel(long NotesId, string LabelName)
        {
            try { 
            long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(user => user.Type == "UserId").Value);

            var result = ilableBL.AddLabel(NotesId, UserId, LabelName);
           
                if (result != null)
                {
                    return Ok(new ResponseModelLayer<LabelEntity> { IsSuccess = true, Message = "Label Added Successfullt", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModelLayer<LabelEntity> { IsSuccess = false, Message = "Label Added Failed " });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPut]
        [Route("UpdateLabel")]
        public IActionResult UpdateLabel(long LabelId, String LabelName)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(user => user.Type == "UserId").Value);
                var result = ilableBL.Updatelabel(LabelId, UserId, LabelName);

                if (result != null)
                {
                    return Ok(new ResponseModelLayer<LabelEntity> { IsSuccess = true, Message = "Update label Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModelLayer<LabelEntity> { IsSuccess = false, Message = "Update Label Failed", Data = result });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }



        [HttpGet]
        [Route("GetAllLabels")]
        public IActionResult GetAllLabels()
        {
            try
            {
                var result = ilableBL.GetAllLabels();

                if (result != null)
                {
                    return Ok(new ResponseModelLayer<object> { IsSuccess = true, Message = "Get All Labels Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModelLayer<object> { IsSuccess = false, Message = "Get All Labels Failed ", Data = result });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteByLabelId")]
        public IActionResult DeleteLableByLabelId(long LabelId)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(user => user.Type == "UserId").Value);

                var result = ilableBL.DeleteLabelByLabelId(LabelId, UserId);

                if (result != null)
                {
                    return Ok(new ResponseModelLayer<object> { IsSuccess = true, Message = "Successfully Deleted ", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModelLayer<object> { IsSuccess = false, Message = "Deleted Failed", Data = result });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }




        [HttpGet]
        [Route("FindNotesPrticularLabelName")]
        public IActionResult FindNotesParticularlabelName(string LabelName)
        {
            var result = ilableBL.FindNotesParticularlabelName(LabelName);

            if (result != null)
            {
                return Ok(new ResponseModelLayer<object> { IsSuccess = true, Message = "Retrieve Notes Succesfully By Label Name", Data = result });

            }
            else
            {
                return BadRequest(new ResponseModelLayer<object> { IsSuccess = false, Message = "Retrieve Notes Failed",Data = result });
            }
        }


    }
}
