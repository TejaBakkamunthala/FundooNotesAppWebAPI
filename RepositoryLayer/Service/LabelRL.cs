using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class LabelRL : ILabelRL
    {

        public readonly FundooContext fundooContext;
        public LabelRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        public LabelEntity AddLabel(long NotesId, long UserId, string LabelName)
        {

            try
            {
                var result = fundooContext.NotesTablee.FirstOrDefault(note => note.NotesId == NotesId && note.UserId == UserId);
                if (result != null)
                {
                    LabelEntity labelEntity = new LabelEntity();
                    labelEntity.NotesId = NotesId;
                    labelEntity.UserId = UserId;
                    labelEntity.LabelName = LabelName;
                    fundooContext.Add(labelEntity);
                    fundooContext.SaveChanges();
                    return labelEntity;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
      
        }
        

        public LabelEntity Updatelabel(long UserId,long LabelId,String LabelName)
        {
            try { 
            var result=fundooContext.LabelTablee.FirstOrDefault(lab=>lab.LabelId==LabelId && lab.UserId== UserId);
           
                if (result != null)
                {
                    result.LabelName = LabelName;
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

        public object GetAllLabels()
        {
            try
            {
                var result = fundooContext.LabelTablee.ToList();
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
        public object DeleteLabelByLabelId(long LabelId,long UserId)
        {
            try
            {
                var result = fundooContext.LabelTablee.FirstOrDefault(lab => lab.LabelId == LabelId && lab.UserId == UserId);
                if (result != null)
                {
                    fundooContext.Remove(result);
                    fundooContext.SaveChanges();
                    return result;
                }
                else
                {
                    return "Label Id is not present";
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        //-> find notes that belongs to a particular label name
        public object FindNotesParticularlabelName(string LabelName)
        {
            var result = fundooContext.LabelTablee.Where(label => label.LabelName == LabelName).ToList();

            if(result!= null)
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
