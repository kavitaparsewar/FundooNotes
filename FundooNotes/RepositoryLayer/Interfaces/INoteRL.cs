using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface INoteRL
    {
        public bool CreateNote(NotesModel notemodel, long ID);
        public bool UpdateNotes(long ID, NotesModel notesModel);
        public bool DeleteNote(long ID);

        public IEnumerable<Note> GetNote();

        public IEnumerable<Note> GetNoteById(long id);


        public bool IsArchieveNote(long ID);
        public bool IsPin(long ID);
        public bool IsTrash(long ID);
        public bool Image(long userID, long ID, IFormFile file);
        public bool color(long userID, long ID, string color);
    }
}
