using BusinessLayer.Interfaces;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class LabelBL : ILabelBL
    {
        ILabelRL labelRL;
        public LabelBL(ILabelRL labelBL)
        {
            this.labelRL = labelBL;
        }

        public bool CreateLabel(long Id, long NoteId, string newlabelName)
        {
            try
            {
                return labelRL.CreateLabel(Id, NoteId, newlabelName);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Label> RenameLabel(long Id, string oldLabelName, string newlabelName)
        {
            try
            {
                return labelRL.RenameLabel(Id, oldLabelName, newlabelName);
            }
            catch (Exception)
            {

                throw;
            }
        }
       public IEnumerable<Label> GetLabelByNoteId(long Id, long NoteId)
        {
            try
            {
                return labelRL.GetLabelByNoteId(Id,NoteId);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public bool DeleteLabel(long Id, string labelName)
        {
            try
            {
                return labelRL.DeleteLabel(Id, labelName);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public bool DeleteLabelByNoteId(long Id, long NoteId, string labelName)
        {
            try
            {
                return labelRL.DeleteLabelByNoteId(Id,NoteId, labelName);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
