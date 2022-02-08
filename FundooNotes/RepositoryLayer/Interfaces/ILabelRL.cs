using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface ILabelRL
    {
        public bool CreateLabel(long Id, long NoteId, string newlabelName);
        public IEnumerable<Label> RenameLabel(long Id, string oldLabelName, string newlabelName);
        public IEnumerable<Label> GetLabelByNoteId(long Id, long NoteId);
        public bool DeleteLabel(long Id, string labelName);
        public bool DeleteLabelByNoteId(long Id, long NoteId, string labelName);
    }
}