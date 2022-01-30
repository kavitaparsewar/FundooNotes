using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface INoteBL
    {     
        public bool CreateNotes(NotesModel notemodel);
        public bool UpdateNotes(int noteID, NotesModel notesModel);
        public bool DeleteNote(int notesID);
    }
}
