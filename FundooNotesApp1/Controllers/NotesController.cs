using BusinessLayer.Interface;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Protocols;
using ModelLayer;
using ModelLayer.NotesModel;
using Newtonsoft.Json;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System.Text;

namespace FundooNotesApp1.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesBL inotesBL;
        private readonly FundooContext fundooContext;
        private readonly IDistributedCache idistributedCache;

        public NotesController( INotesBL inotesBL,FundooContext fundooContext,IDistributedCache idistributedCache)
        {
            this.inotesBL=inotesBL;
            this.fundooContext=fundooContext;
            this.idistributedCache = idistributedCache;
        }

       // [Authorize]
        [HttpPost]
        [Route("Create")]
        public IActionResult NotesCreate(NotesCreateModel notesCreate)
        {
            try
            {


                //int UserId = (int)(HttpContext.Session.GetInt32("userID"));
                //long UserID = UserId;

                
                 long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(user => user.Type == "UserId").Value);

                var notes = inotesBL.NotesCreate(notesCreate, UserId);

                if (notes != null)
                {
                    return Ok(new ResponseModelLayer<NotesEntity> { IsSuccess = true, Message = "Notes Added Successfully", Data = notes });
                }
                else
                {
                    return BadRequest(new ResponseModelLayer<NotesEntity> { IsSuccess =false, Message = "Failed To Add Notes" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }



        [HttpGet]
        [Route("GetAllNotes")]
        public IActionResult GetAllNotes()
        {
            try
            {
                var result = inotesBL.GetAllNotes();
                if (result != null)
                {
                    return Ok(new ResponseModelLayer<Object> { IsSuccess = true, Message = " Successfuly Get All Notes", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModelLayer<object> { IsSuccess = false, Message = "Failed Get All Notes" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPut]
        [Route("UpdateByNotesId")]
        public IActionResult  UpdateNotesById(long NotesId,NotesUpdateModel notesUpdate)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(user => user.Type == "UserId").Value);

                var result = inotesBL.UpdateNotesById(NotesId, UserId, notesUpdate);
                if (result != null)
                {
                    return Ok(new ResponseModelLayer<object> { IsSuccess = true, Message = "Data Updated Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModelLayer<Object> { IsSuccess = false, Message = "Data id not updated or NotesId  is not present" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        [Authorize]
        [HttpDelete]
        [Route("DeleteByNotesId")]
        public IActionResult DeleteNotesById(long NotesId)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(user => user.Type == "UserId").Value);

                var result = inotesBL.DeleteByNotesId(NotesId, UserId);
                if (result != null)
                {
                    return Ok(new ResponseModelLayer<object> { IsSuccess = true, Message = "Data Deleted Successfully ", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModelLayer<object> { IsSuccess = false, Message = "Data Not Deleted Or NotesId is not present ", Data = result });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [Authorize]
        [HttpGet]
        [Route("Archive Notes")]
        public IActionResult ArchiveNotes(long NotesId)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(user => user.Type == "UserId").Value);

                var result = inotesBL.ArchiveNotes(NotesId, UserId);
                if (result != null)
                {
                    return Ok(new ResponseModelLayer<object> { IsSuccess = true, Message = "Archive  Or UnArchive Notes Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModelLayer<object> { IsSuccess = false, Message = "Archive Notes Failed" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        [Authorize]
        [HttpGet]
        [Route("PinNotes")]
        public IActionResult PinNotes(long NotesId)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(user => user.Type == "UserId").Value);

                var result = inotesBL.PinNotes(NotesId, UserId);

                if (result != null)
                {
                    return Ok(new ResponseModelLayer<object> { IsSuccess = true, Message = "Pin Notes Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModelLayer<object> { IsSuccess = false, Message = "Pin Notes Failed " });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("TrashNotes")]
        public IActionResult TrashNotes(long NotesId)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(user => user.Type == "UserId").Value);

                var result = inotesBL.TrashNotes(NotesId, UserId);
                if (result != null)
                {
                    return Ok(new ResponseModelLayer<object> { IsSuccess = true, Message = "Trash Or UnTrashNotes Sucessfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModelLayer<object> { IsSuccess = false, Message = "Trash Or UnTrash Failed " });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        [Authorize]
        [HttpGet]
        [Route("ChangeColor")]
        public IActionResult ChangeColor(string color,long NotesId)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(user => user.Type == "UserId").Value);
                var result = inotesBL.ChangeColor(color, NotesId, UserId);
                if (result != null)
                {
                    return Ok(new ResponseModelLayer<object> { IsSuccess = true, Message = "Change Color Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModelLayer<object> { IsSuccess = false, Message = "Change Color Failed" });
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        [Authorize]
        [HttpGet]
        [Route("AddRemainder")]
        public IActionResult AddRemainder(long NotesId, DateTime Remainder)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(user => user.Type == "UserId").Value);

                var result = inotesBL.AddRemainder(UserId, NotesId, Remainder);
                if (result != null)
                {
                    return Ok(new ResponseModelLayer<object> { IsSuccess = true, Message = "Add Remainder Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModelLayer<object> { IsSuccess = false, Message = "Add Remainder Failed " });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        [Authorize]
        [HttpPost]
        [Route("UploadImage")]
        public IActionResult UploadImage(long NotesId, IFormFile Image)
        {
            long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(user => user.Type == "UserId").Value);

            var result = inotesBL.UploadImage(NotesId, UserId, Image);

            if (result != null)
            {
                return Ok(new ResponseModelLayer<object> { IsSuccess=true,Message="Uploading Image  Successfully", Data=result});
            }
            else
            {
                return Ok(new ResponseModelLayer<object> { IsSuccess = false, Message = "Uploading Image  Failed"});
            }
        }

        [Authorize]
        [HttpPost]
        [Route("UploadImage2")]
        public IActionResult UploadImage(long NotesId,string FilePath)
        {
            long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(user => user.Type == "UserId").Value);
            var result=inotesBL.UploadImage2( NotesId,UserId, FilePath);
            if(result != null)
            {
                return Ok(new ResponseModelLayer<object> { IsSuccess=true,Message="Uploading Image  Successfully",Data=result});
            }
            else
            {
                return BadRequest(new ResponseModelLayer<object> { IsSuccess = true, Message = "Uploading Image Failed " });
            }

        }




        [HttpGet]
        [Route("RedisCache")]
        public async Task<IActionResult> GetAllNotesUsingRedisCache()
        {
            var cacheKey = "Teja";
            string serializedNotedList;

            var NotesList = new List<NotesEntity>();
            byte[] redisNotesList = await idistributedCache.GetAsync(cacheKey);
            if (redisNotesList != null)
            {
                serializedNotedList = Encoding.UTF8.GetString(redisNotesList);
                NotesList = JsonConvert.DeserializeObject<List<NotesEntity>>(serializedNotedList);

            }
            else
            {
                NotesList = fundooContext.NotesTablee.ToList();
                serializedNotedList = JsonConvert.SerializeObject(NotesList);
                redisNotesList = Encoding.UTF8.GetBytes(serializedNotedList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await idistributedCache.SetAsync(cacheKey, redisNotesList, options);
            }
            return Ok(NotesList);
        }



        [HttpGet]
        [Route("GetNotesByIdAndDesc")]
        public IActionResult GetNotesByNotesIdAndDesc(long NotesId, string Description)
        {
            try
            {
                var result = inotesBL.GetNoteByTitleAndDesc(NotesId, Description);
                if (result != null)
                {
                    return Ok(new ResponseModelLayer<object> { IsSuccess = true, Message = "Data Retrieve Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModelLayer<object> { IsSuccess = true, Message = "Data Not Retrieve Successfully" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetNotesByUserId")]
       public IActionResult GetNotesByUserId(long UserId)
        {
            try
            {
                var result = inotesBL.GetNotesByUserId(UserId);
                if (result != null)
                {
                    return Ok(new ResponseModelLayer<object> { IsSuccess = true, Message = "Data Retrieve Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModelLayer<object> { IsSuccess = false, Message = "Data Retrieve Failed" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetNotesByCreatedDate")]
        public IActionResult GetNotesByCreatedDate(DateTime created)
        {
            try
            {
                var result = inotesBL.GetNotesByNotesCreatedDate(created);
                if (result != null)
                {
                    return Ok(new ResponseModelLayer<object> { IsSuccess = true, Message = "Data Retrieve Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModelLayer<object> { IsSuccess = false, Message = "Data Retrieve Failed" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetNotesByBNotesId")]
      public IActionResult GetNotesByNotesId(long NotesId)
        {
            try
            {
                var result = inotesBL.GetNotesByNotesId(NotesId);
                if (result != null)
                {
                    return Ok(new ResponseModelLayer<object> { IsSuccess = true, Message = "Data Retrieve Succesfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModelLayer<object> { IsSuccess = false, Message = "Data Retreive Failed " });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet]
        [Route("GetByFirstLetter")]
        public IActionResult GetNotesByFirstLetter(string  FirstLetter)
        {
            var result = inotesBL.GetNotesByFirstLetter(FirstLetter);
            if (result != null)
            {
                return Ok(new ResponseModelLayer<object> { IsSuccess = true, Message = "Successfully Retrieve By First Letter", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModelLayer<object> { IsSuccess = false, Message = "Failed ", Data = result });
            }
        }



        [Authorize]
        [HttpGet]
        [Route("GetNotesByTitleAndDescription")]
        public IActionResult GetNotesByTitleAndDescription(string Title, string Description)
        {
            try
            {
                var result = inotesBL.GetNotesByTitleAndDescription(Title, Description);
                if (result != null)
                {
                    return Ok(new ResponseModelLayer<object> { IsSuccess = true, Message = "Succesfully Retrieve Notes By Title And Description  ", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModelLayer<object> { IsSuccess = false, Message = "Failed To Retrieve Notes ", Data = result });
                }
            }
            catch(Exception ex) {
                throw ex;
            }
        }


        [HttpGet]
        [Route("GetNotesByCretaedDatee")]
        public IActionResult GetNotesByCreatedDatee(DateTime createdDate) 
        {
            try
            {
                var result = inotesBL.GetNotesByCreatedDate(createdDate);
                if (result != null)
                {
                    return Ok(new ResponseModelLayer<object> { IsSuccess = true, Message = "Successfully Notes Retrieve By CreatedDate ", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModelLayer<object> { IsSuccess = true, Message = "Failed To Retrieve Notes By CreatedDate" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
                
           }



        [HttpGet]
        [Route("GetNotesByCretaedDateOnly")]
        public IActionResult GetNotesByCreatedDateOnly(DateOnly createdDate)
        {
            var result=inotesBL.GetNotesByCretatedDateOnly(createdDate);

            if (result != null)
            {
                return Ok(new ResponseModelLayer<object> { IsSuccess=true,Message="Successfully ",Data= result});
            }
            else
            {
                return BadRequest(new ResponseModelLayer<object> { IsSuccess=true,Message="Failed"});
            }

           
               
        }


    }
}


