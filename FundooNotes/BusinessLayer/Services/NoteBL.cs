using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entities;
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

        public bool CreateNotes(NotesModel notesModel, long ID)
        {
            try
            {
                return noteRL.CreateNote(notesModel,ID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool UpdateNotes(long ID, NotesModel notesModel)
        {
            try
            {
                if (noteRL.UpdateNotes(ID, notesModel))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public bool DeleteNote(long ID)
        {
            try
            {
                if (noteRL.DeleteNote(ID))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public IEnumerable<Note> GetNote()
        {
            try
            {
                return noteRL.GetNote();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public IEnumerable<Note> GetNoteById(long id)
        {
            try
            {
                return noteRL.GetNoteById(id);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public bool IsArchieveNote(long ID)
        {
            try
            {
                if (noteRL.IsArchieveNote(ID))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool IsPin(long ID)
        {
            try
            {
                if (noteRL.IsPin(ID))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool IsTrash(long ID)
        {
            try
            {
                if (noteRL.IsTrash(ID))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Image(long userId, long ID, IFormFile file)
        {
            try
            {
                return noteRL.Image(userId, ID, file);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Colorchange(long userId, long ID, string color)
        {
            try
            {
                if (noteRL.color(userId, ID, color))
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
