using BusinessLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class CollaboratorBL :ICollaboratorBL
    {
        private readonly ICollaboratorRL icollaboratorRL;

        public CollaboratorBL(ICollaboratorRL icollaboratorRl)
        {
            this.icollaboratorRL = icollaboratorRl;
        }
        public CollaboratorEntity AddCollaborator(long UserId, long NotesId, string CollaboratorEmail)
        {
           return  icollaboratorRL.AddCollaborator(UserId, NotesId, CollaboratorEmail);
        }

        public CollaboratorEntity UpdateCollaborator(long UserId, long CollaboratorId, string ColloboratorEmail)
        {
            return icollaboratorRL.UpdateCollaborator(UserId, CollaboratorId, ColloboratorEmail);
        }

        public object GetAllCollaborators()
        {
            return icollaboratorRL.GetAllCollaborators();
        }


        public object DeleteByCollaboratorId(long UserId, long CollaboratorId)
        {
            return icollaboratorRL.DeleteByCollaboratorId(UserId,CollaboratorId);
        }

        public object CollaboratorByNotesId(long NotesId)
        {
            return icollaboratorRL.CollaboratorByNotesId(NotesId);
        }

        public object CountCollaboratorsByUserId(long UserId)
        {
            return icollaboratorRL.CountCollaboratorsByUserId(UserId);
        }



    }
}
