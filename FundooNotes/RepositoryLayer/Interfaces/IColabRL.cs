using CommonLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IColabRL
    {
        public bool AddColab(ColabModel colabmodel);
        public IEnumerable<Collab> GetColabById(long id, long NoteId);
        public bool DeleteColab(long Id, long NoteId, string Email);
    }
}
