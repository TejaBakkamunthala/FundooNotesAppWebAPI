using Microsoft.AspNetCore.Components.Forms;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class CollaboratorRL : ICollaboratorRL
    {

        private readonly FundooContext fundooContext;

        public CollaboratorRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }




    public CollaboratorEntity AddCollaborator(long UserId,long NotesId,string CollaboratorEmail)
        {
           var result=fundooContext.NotesTablee.FirstOrDefault(note=>note.UserId== UserId && note.NotesId==NotesId);

            if (result != null)
            {
                CollaboratorEntity collaboratorEntity=new CollaboratorEntity();
                collaboratorEntity.CollaboratorEmail= CollaboratorEmail;
                collaboratorEntity.UserId= UserId;
                collaboratorEntity.NotesId= NotesId;
                fundooContext.CollaboratorTablee.Add(collaboratorEntity);
                fundooContext.SaveChanges();
                return collaboratorEntity;
            }
            else
            {
                return null;
            }
        }


        public CollaboratorEntity UpdateCollaborator(long UserId,long CollaboratorId,string ColloboratorEmail)
        {
            var result=fundooContext.CollaboratorTablee.FirstOrDefault(col=>col.UserId==UserId && col.CollaboratorId==CollaboratorId);
            if (result != null)
            {
                result.CollaboratorEmail = ColloboratorEmail;
                fundooContext.SaveChanges() ;
                return result;
            }
            else
            {
                return null;
            }
        }


        public object GetAllCollaborators()
        {
            var result = fundooContext.CollaboratorTablee.ToList();
            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public object DeleteByCollaboratorId(long UserId,long CollaboratorId)
        {
            var result = fundooContext.CollaboratorTablee.FirstOrDefault(col => col.UserId==UserId && col.CollaboratorId==CollaboratorId) ;

            if(result != null)
            {
                fundooContext.CollaboratorTablee.Remove(result);
                fundooContext.SaveChanges();
                return result;

            }
            else
            {
                return null;
            }
        }


        // 2)-> find collaborator in notes and show details of collaboration
        public object CollaboratorByNotesId(long NotesId)
        {
            var result = fundooContext.CollaboratorTablee.Where(col => col.NotesId == NotesId).ToList();
            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }


        //-> find count of collaborators of a particular user
        public object CountCollaboratorsByUserId(long UserId)
        {
            var result = fundooContext.CollaboratorTablee.Where(col => col.UserId == UserId).Count();

            if (result != null)
            {
                return result;
            }
            else
            {
                return result;
            }
        }

    }
}
