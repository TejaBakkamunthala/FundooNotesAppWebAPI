using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ModelLayer.NotesModel;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using System.Xml;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RepositoryLayer.Service
{
    public class NotesRL : INotesRL
    {
        public readonly FundooContext fundooContext;

        public NotesRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        public NotesEntity NotesCreate(NotesCreateModel notesCreate, long UserId)
        {
            try
            {
                if (UserId > 0)
                {
                    UserEntity user = fundooContext.UserTablee.FirstOrDefault(user => user.UserId == UserId);

                    if (user != null)
                    {
                        NotesEntity notes = new NotesEntity();
                        notes.Title = notesCreate.Title;
                        notes.Description = notesCreate.Description;
                        notes.Reminder = notesCreate.Reminder;
                        notes.Color = notesCreate.Color;
                        notes.Image = notesCreate.Image;
                        notes.Archive = notesCreate.Archive;
                        notes.PinNotes = notesCreate.PinNotes;
                        notes.Trash = notesCreate.Trash;
                        notes.Created = notesCreate.Created;
                        notes.Modified = notesCreate.Modified;
                        notes.UserId = UserId;
                        fundooContext.NotesTablee.Add(notes);
                        fundooContext.SaveChanges();
                        return notes;
                    }
                }
                else
                {
                    return null;
                }
                return null;

            }
            catch(Exception ex)
            {
                throw ex;
            }
            }
            
           

        //GETALLNOTES

        public object GetAllNotes()
        {
            try
            {
                var result = fundooContext.NotesTablee.ToList();
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

        //UPDATE BTY ID
        public object UpdateNotesById(long NotesId,long UserId,NotesUpdateModel NotesUpdate)
        {
            try
            {
                var result = fundooContext.NotesTablee.FirstOrDefault(note => note.NotesId == NotesId && note.UserId == UserId);
                if (result != null)
                {
                    result.Title = NotesUpdate.Title;
                    result.Description = NotesUpdate.Description;
                    result.Reminder = NotesUpdate.Reminder;
                    result.Color = NotesUpdate.Color;
                    result.Image = NotesUpdate.Image;
                    result.Archive = NotesUpdate.Archive;
                    result.PinNotes = NotesUpdate.PinNotes;
                    result.Trash = NotesUpdate.Trash;
                    result.Created = NotesUpdate.Created;
                    result.Modified = NotesUpdate.Modified;

                    fundooContext.SaveChanges();
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


        //DELETE  BY NOTES ID
        public Object DeleteByNotesId(long NotesId,long UserId)
        {
            try
            {
                var result = fundooContext.NotesTablee.FirstOrDefault(note => note.NotesId == NotesId && note.UserId == UserId);

                if (result != null)
                {
                    fundooContext.NotesTablee.Remove(result);
                    fundooContext.SaveChanges();
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

        public object ArchiveNotes(long NotesId,long UserId)
        {
            try
            {
                var archive = fundooContext.NotesTablee.FirstOrDefault(note => note.NotesId == NotesId && note.UserId == UserId);

                if (archive != null)
                {
                    if (archive.Archive == true)
                    {
                        archive.Archive = false;

                        if (archive.PinNotes == true)
                        {
                            archive.PinNotes = false;
                        }
                        if (archive.Trash == true)
                        {
                            archive.Trash = false;
                        }
                        fundooContext.Entry(archive).State = EntityState.Modified;
                        fundooContext.SaveChanges();
                        return archive;
                    }
                    else
                    {

                        archive.Archive = true;
                        if (archive.PinNotes == true)
                        {
                            archive.PinNotes = false;
                        }
                        if (archive.Trash == true)
                        {
                            archive.Trash = false;
                        }
                        fundooContext.Entry(archive).State = EntityState.Modified;
                        fundooContext.SaveChanges();
                        return archive;
                    }
                }
                return null; ;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public object PinNotes(long NotesId,long UserId)
        {
            try
            {
                var pin = fundooContext.NotesTablee.FirstOrDefault(note => note.NotesId == NotesId && note.UserId == UserId);
                if (pin != null)
                {
                    if (pin.PinNotes == true)
                    {
                        pin.PinNotes = false;
                        fundooContext.Entry(pin).State = EntityState.Modified;
                        fundooContext.SaveChanges();
                        return "UnPin Successfully";
                    }
                    else
                    {
                        pin.PinNotes = true;
                        fundooContext.Entry(pin).State = EntityState.Modified;
                        fundooContext.SaveChanges();
                        return "Pin Successfully";
                    }
                }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public object TrashNotes(long NotesId, long UserId)
        {
            try
            {
                var trash = fundooContext.NotesTablee.FirstOrDefault(note => note.NotesId == NotesId && note.UserId == UserId);
                if (trash != null)
                {
                    if (trash.Trash == true)
                    {
                        trash.Trash = false;

                        if (trash.Archive == true)
                        {
                            trash.Archive = false;
                        }
                        if (trash.PinNotes == true)
                        {
                            trash.PinNotes = false;
                        }
                        fundooContext.Entry(trash).State = EntityState.Modified;
                        fundooContext.SaveChanges();
                        return "Move From Trash To  Notes";
                    }
                    else
                    {
                        trash.Trash = true;
                        if (trash.Archive == true)
                        {
                            trash.Archive = false;
                        }
                        if (trash.PinNotes == true)
                        {
                            trash.PinNotes = false;
                        }
                        fundooContext.Entry(trash).State = EntityState.Modified;
                        fundooContext.SaveChanges();
                        return "Move From Notes to Trash";
                    }
                }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

      public object ChangeColor(string Color,long NotesId,long UserId)
        {
            try
            {
                var notes = fundooContext.NotesTablee.FirstOrDefault(note => note.NotesId == NotesId && note.UserId == UserId);

                if (notes != null)
                {
                    notes.Color = Color;

                    fundooContext.Entry(notes).State = EntityState.Modified;
                    fundooContext.SaveChanges();

                    return notes;
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

        public object AddRemainder(long UserId,long NotesId,DateTime Remainder)
        {
            try
            {
                var result = fundooContext.NotesTablee.FirstOrDefault(note => note.NotesId == NotesId && note.UserId == UserId);

                if (result != null)
                {
                    if (Remainder > DateTime.Now)
                    {
                        result.Reminder = Remainder;
                        fundooContext.Entry(result).State = EntityState.Modified;
                        fundooContext.SaveChanges();
                        return result;
                    }
                    else
                    {
                        return "date is expired or previous date";
                    }
                }
                return null;
            }
            catch(Exception ex) 
            {
              throw ex;
            }

        }



        //Add Image
        public object UploadImage(long NotesId, long UserId, IFormFile Image)
        {
            try
            {
                var note = fundooContext.NotesTablee.FirstOrDefault(note => note.NotesId == NotesId && note.UserId == UserId);
                if (note != null)
                {
                    Account account = new Account("dv1bbkwe3", "135423148671541", "cKx11wYPMfczf4WQa_r3joksXKw");

                    Cloudinary cloudinary = new Cloudinary(account);

                    ImageUploadParams uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(Image.FileName, Image.OpenReadStream()),
                        PublicId = note.Title
                    };

                    ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);

                    note.Modified = DateTime.Now;
                    note.Image = uploadResult.Url.ToString(); ;
                    fundooContext.SaveChanges();

                    return note;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Add Image2

        public object UploadImage2(long NotesId, long UserId, string FilePath)
        {
            try
            {
                var note = fundooContext.NotesTablee.FirstOrDefault(note => note.NotesId == NotesId && note.UserId ==UserId);
                if (note != null)
                {
                    Account account = new Account("dv1bbkwe3", "135423148671541", "cKx11wYPMfczf4WQa_r3joksXKw");

                    Cloudinary cloudinary = new Cloudinary(account);

                    ImageUploadParams uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(FilePath),
                        PublicId = note.Title
                    };

                    ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);

                    note.Modified = DateTime.Now;
                    note.Image = uploadResult.Url.ToString(); ;
                    fundooContext.SaveChanges();

                    return note;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



        //  1)-> search note using title and description, show details of that note
        public object GetNoteByTitleAndDesc(long NotesId, string Description)
        {
            try
            {
                var result = fundooContext.NotesTablee.FirstOrDefault(note => note.NotesId == NotesId && note.Description == Description);

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


        //count no of notes belongs to a particular user
        public object GetNotesByUserId(long UserId)
        {
            try
            {
                var result = fundooContext.NotesTablee.Where(user => user.UserId == UserId).Count();
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

        ////-> find note on the basis of the date the notes were created
        public object GetNotesByNotesCreatedDate(DateTime created)
        {
            try
            {
                var result = fundooContext.NotesTablee.Where(note => note.Created == created).ToList();
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return "Notes not present Or Null";
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        //get notes by notes id
        public NotesEntity GetNotesByNotesId(long NotesId)
        {
            try
            {
                var result = fundooContext.NotesTablee.FirstOrDefault(note => note.NotesId == NotesId);
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



        //GET NOTES BY FIRST LETTER
        public object GetNotesByFirstLetter(string FirstLetter)
        {
            var result=fundooContext.NotesTablee.Where(note=>note.Title.StartsWith(FirstLetter)).ToList();
            if(result != null)
            {
                return result;
            }
            else
            {
                return result;
            }
        }



     //    2) fetch a note of user using title and description

        public object GetNotesByTitleAndDescription(string Title, string Description)
        {
            try
            {
                var result = fundooContext.NotesTablee.FirstOrDefault(note => note.Title == Title && note.Description == Description);

                if (result != null)
                {
                    return result;
                }
                else
                {
                    return "Data is not present";
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


    //  3) search a note on basis of their created date and fetch details of notes */

        public object GetNotesByCreatedDate(DateTime CreatedDate)
        {
            try
            {
                var result = fundooContext.NotesTablee.Where(dat => dat.Created == CreatedDate).ToList();
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

        
        public object GetNotesByCretatedDateOnly(DateOnly CreatedDate)
        {
            var result = fundooContext.NotesTablee.Where(dat => dat.Created.Equals(CreatedDate)).ToList();
            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }

        }








    }
}
