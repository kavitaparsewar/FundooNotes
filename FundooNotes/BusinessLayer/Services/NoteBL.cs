using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
     public class NoteBL : INoteBL
    {
        INoteRL noteRL;
        public NoteBL(INoteRL noteBL)
        {
            this.noteRL = noteBL;
        }

        public bool CreateNotes(NotesModel notesModel)
        {
            try
            {
                return noteRL.CreateNote(notesModel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool UpdateNotes(int noteID, NotesModel notesModel)
        {
            try
            {
                if (noteRL.UpdateNotes(noteID, notesModel))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public bool DeleteNote(int noteID)
        {
            try
            {
                if (noteRL.DeleteNote(noteID))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
