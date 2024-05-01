using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using RepositoryLayer.Entity;
using System.Net.NetworkInformation;

namespace FundooNotesApp1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorController : ControllerBase
    {

        private readonly ICollaboratorBL icollaboratorBL;

        public CollaboratorController(ICollaboratorBL icollaboratorBL)
        {
            this.icollaboratorBL = icollaboratorBL;
        }




        [Authorize]
        [HttpPost]
        [Route("AddCollaborator")]
        public IActionResult AddCollaborator(long NotesId,string CollaboratorEmail)
        {

            long UserId =Convert.ToInt32(User.Claims.FirstOrDefault(user=>user.Type=="UserId").Value);
            var result=icollaboratorBL.AddCollaborator(UserId,NotesId, CollaboratorEmail);
            if (result != null)
            {
                return Ok(new ResponseModelLayer<CollaboratorEntity> { IsSuccess = true ,Message="Collaborator Added Succesfully",Data=result});
            }
            else
            {
                return BadRequest(new ResponseModelLayer<CollaboratorEntity> { IsSuccess = false, Message = "Collaborator Failed ", Data = result });
            }

        }

        [Authorize]
        [HttpPost]
        [Route("UpdateCollaborator")]
        public IActionResult UpdateController(long collaboratorId,string CollaboratorEmail)
        {
            long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(user => user.Type == "UserId").Value);
            var result = icollaboratorBL.UpdateCollaborator(UserId, collaboratorId, CollaboratorEmail);

            if (result != null)
            {
                return Ok(new ResponseModelLayer<CollaboratorEntity> { IsSuccess = true, Message = "Update Collaborator Successfully", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModelLayer<CollaboratorEntity> { IsSuccess = false, Message = "Update Collaborator Failed " });
            }

        }


        [HttpGet]
        [Route("GetAllCollaborators")]
        public IActionResult GetAllCollaborators()
        {
            var result=icollaboratorBL.GetAllCollaborators();
            if (result != null)
            {
                return Ok(new ResponseModelLayer<object> { IsSuccess = true, Message = "Get All Collaborators Failed ", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModelLayer<object> { IsSuccess=false,Message="Get All Collaborators Failed ",Data=result});
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteColloboratorId")]
        public IActionResult DeleteByCollaboratorId(long CollaboratorId) {

            long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(user => user.Type == "UserId").Value);

            var result = icollaboratorBL.DeleteByCollaboratorId(UserId,CollaboratorId);
            if(result != null) 
            {
                return Ok(new ResponseModelLayer<object> { IsSuccess = true, Message = "Deleted Collaborator Id Successfully ", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModelLayer<object> { IsSuccess = false, Message = "Deleted Collaborator Id Failed ", Data = result });
            }
        }


        [HttpGet]
        [Route("GetCollaboratorsByNotesId")]
        public object CollaboratorByNotesId(long NotesId)
        {
            var result=icollaboratorBL.CollaboratorByNotesId(NotesId);
            if (result != null)
            {
                return Ok(new ResponseModelLayer<object> { IsSuccess = true, Message = " CollaboratorByNotesId Retrieve Successfully ", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModelLayer<object> { IsSuccess = false, Message = "Retrieve Failed ", Data = result });
            }
        }

        [HttpGet]
        [Route("CountCollaboratorByUserId")]
        public object CountCollaboratorsByUserId(long UserId)
        {
            var result = icollaboratorBL.CountCollaboratorsByUserId(UserId);
            if (result != null)
            {
                return Ok(new ResponseModelLayer<object> { IsSuccess = true, Message = "Successfully Count Collaborators By UserId ", Data = result });
            }
            else
            {
                return Ok(new ResponseModelLayer<object> { IsSuccess=false,Message="Count Failed ",Data=result});
            }
        }
    }
}
