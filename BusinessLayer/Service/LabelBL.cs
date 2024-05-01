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
    public class LabelBL : ILabelBL
    {

        private readonly ILabelRL ilabelRL;

        public LabelBL(ILabelRL ilabelRL)
        {
            this.ilabelRL = ilabelRL;
        }

        public LabelEntity AddLabel(long NotesId, long UserId, string LabelName)
        {
            return ilabelRL.AddLabel(NotesId, UserId, LabelName);
        }


        public LabelEntity Updatelabel(long UserId, long LabelId, String LabelName)
        {
            return ilabelRL.Updatelabel(UserId, LabelId, LabelName);
        }

        public object GetAllLabels()
        {
            return ilabelRL.GetAllLabels();
        }

        public object DeleteLabelByLabelId(long LabelId, long UserId)
        {
            return ilabelRL.DeleteLabelByLabelId(LabelId,UserId);
        }


        public object FindNotesParticularlabelName(string LabelName)
        {
            return ilabelRL.FindNotesParticularlabelName(LabelName);
        }


    }



}
