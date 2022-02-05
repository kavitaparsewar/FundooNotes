using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class ColabBL : IColabBL
    {
        IColabRL colabRL;

        public ColabBL(IColabRL userBL)
        {
            this.colabRL = userBL;
        }

        public bool AddColab(ColabModel colabmodel)       
        {
            try
            {
                return colabRL.AddColab(colabmodel);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<Collab> GetColabById(long id, long NoteId)
        {
            try
            {
                return colabRL.GetColabById(id, NoteId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool DeleteColab(long Id, long NoteId, string Email)
        {
            try
            {
                return colabRL.DeleteColab(Id, NoteId,Email);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
