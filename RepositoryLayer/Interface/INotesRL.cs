using Microsoft.AspNetCore.Http;
using ModelLayer.NotesModel;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface INotesRL
    {

        public NotesEntity NotesCreate(NotesCreateModel notesCreate, long UserId);

        public object GetAllNotes();

        public object UpdateNotesById(long NotesId, long UserId, NotesUpdateModel NotesUpdate);

        public Object DeleteByNotesId(long NotesId, long UserId);

        public object ArchiveNotes(long NotesId,long  PinCode);

        public object PinNotes(long NotesId, long UserId);

        public object TrashNotes(long NotesId, long UserId);

        public object ChangeColor(string color, long NotesId, long UserId);

        public object AddRemainder(long UserId, long NotesId, DateTime Remainder);

        public object GetNoteByTitleAndDesc(long NotesId, string Description);

        public object GetNotesByUserId(long UserId);
        public object GetNotesByNotesCreatedDate(DateTime created);

        public NotesEntity GetNotesByNotesId(long NotesId);

        public object UploadImage(long NotesId, long UserId, IFormFile Image);

        public object UploadImage2(long NotesId, long UserId, string FilePath);

        public object GetNotesByFirstLetter(string FirstLetter);

        public object GetNotesByTitleAndDescription(string Title, string Description);

        public object GetNotesByCreatedDate(DateTime CreatedDate);

        public object GetNotesByCretatedDateOnly(DateOnly CreatedDate);


















    }
}
