using BusinessLayer.Interface;
using Microsoft.AspNetCore.Http;
using ModelLayer.NotesModel;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
 public class NotesBL : INotesBL
    {

        private readonly INotesRL inotesRL;

       public  NotesBL(INotesRL inotesRL)
        {
            this.inotesRL = inotesRL;     
        }
    public NotesEntity NotesCreate(NotesCreateModel notesCreate, long UserId)
    {
            return inotesRL.NotesCreate(notesCreate, UserId);

    }

        public object UpdateNotesById(long NotesId, long UserId, NotesUpdateModel NotesUpdate)
        {
            return inotesRL.UpdateNotesById(NotesId,UserId, NotesUpdate);
        }


   public object GetAllNotes()
        {
            return inotesRL.GetAllNotes();
        }

        public Object DeleteByNotesId(long NotesId, long UserId)
        {
            return inotesRL.DeleteByNotesId(NotesId,UserId);
        }


        public object ArchiveNotes(long NotesId, long UserId)
        {
            return inotesRL.ArchiveNotes(NotesId,UserId);
        }


        public object PinNotes(long NotesId, long UserId)
        {
            return inotesRL.PinNotes(NotesId, UserId);
        }

        public object TrashNotes(long NotesId, long UserId)
        {
            return inotesRL.TrashNotes(NotesId,UserId);
        }

        public object ChangeColor(string Color, long NotesId, long UserId)
        {
            return inotesRL.ChangeColor(Color, NotesId, UserId);
        }

        public object AddRemainder(long UserId, long NotesId, DateTime Remainder)
        {
            return inotesRL.AddRemainder(UserId, NotesId, Remainder);
        }

        public object GetNoteByTitleAndDesc(long NotesId, string Description)
        {
            return inotesRL.GetNoteByTitleAndDesc(NotesId, Description);
        }

        public object GetNotesByUserId(long UserId)
        {
            return inotesRL.GetNotesByUserId(UserId);
        }

        public object GetNotesByNotesCreatedDate(DateTime created)
        {
            return inotesRL.GetNotesByNotesCreatedDate(created);
        }

        public NotesEntity GetNotesByNotesId(long NotesId)
        {
            return inotesRL.GetNotesByNotesId(NotesId);
        }


        public object UploadImage(long NotesId, long UserId, IFormFile Image)
        {
            return inotesRL.UploadImage(NotesId, UserId, Image);
        }


        public object UploadImage2(long NotesId, long UserId, string FilePath)
        {
            return inotesRL.UploadImage2(NotesId, UserId, FilePath);
        }


        public object GetNotesByFirstLetter(string FirstLetter)
        {
            return inotesRL.GetNotesByFirstLetter(FirstLetter);
        }


        public object GetNotesByTitleAndDescription(string Title, string Description)
        {
            return inotesRL.GetNotesByTitleAndDescription(Title, Description);
        }

        public object GetNotesByCreatedDate(DateTime CreatedDate)
        {
            return inotesRL.GetNotesByCreatedDate( CreatedDate);
        }

        public object GetNotesByCretatedDateOnly(DateOnly CreatedDate)
        {
            return inotesRL.GetNotesByCretatedDateOnly(CreatedDate);
        }







    }
}
