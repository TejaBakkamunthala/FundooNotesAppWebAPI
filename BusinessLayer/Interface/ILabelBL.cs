using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ILabelBL
    {
        public LabelEntity AddLabel(long NotesId, long UserId, string LabelName);

        public LabelEntity Updatelabel(long UserId, long LabelId, String LabelName);

        public object GetAllLabels();

        public object DeleteLabelByLabelId(long LabelId, long UserId);

        public object FindNotesParticularlabelName(string LabelName);




    }
}
