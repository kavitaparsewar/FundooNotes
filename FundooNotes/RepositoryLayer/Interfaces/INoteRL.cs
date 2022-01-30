using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface INoteRL
    {
        public bool CreateNote(NotesModel notemodel);
        public bool UpdateNotes(int noteID, NotesModel notesModel);
        public bool DeleteNote(int notesID);
    }
}
