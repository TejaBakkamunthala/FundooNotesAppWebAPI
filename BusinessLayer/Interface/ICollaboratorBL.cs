using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ICollaboratorBL
    {
        public CollaboratorEntity AddCollaborator(long UserId, long NotesId, string CollaboratorEmail);

        public CollaboratorEntity UpdateCollaborator(long UserId, long CollaboratorId, string ColloboratorEmail);

        public object GetAllCollaborators();

        public object DeleteByCollaboratorId(long UserId, long CollaboratorId);

        public object CollaboratorByNotesId(long NotesId);

        public object CountCollaboratorsByUserId(long UserId);





    }
}
