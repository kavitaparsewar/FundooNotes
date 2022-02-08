using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.AppContext;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class ColabRL : IColabRL
    {
        Context context;
        private readonly IConfiguration configuration;
        public ColabRL(Context context, IConfiguration config)
        {
            this.context = context;//appcontext to for api
            this.configuration = config;//for startup file instance
        }

        public bool AddColab(ColabModel colabmodel)
        {
            try
            {
                Collab collaborator = new Collab();
                Note note = context.Notes.Where(e => e.NoteId == colabmodel.NoteId && e.Id == colabmodel.Id).FirstOrDefault();
                if (note != null)
                {
                    collaborator.NoteId = colabmodel.NoteId;
                    collaborator.Email = colabmodel.Email;
                    collaborator.Id = colabmodel.Id;
                    context.Collaborator.Add(collaborator);
                    var result = context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public IEnumerable<Collab> GetColabById(long id, long NoteId)
        {
            return context.Collaborator.Where(e => e.Id == id).ToList();
        }


        public bool DeleteColab(long Id, long NoteId, string Email)
        {
            try
            {
                var collaborator = context.Collaborator.Where(e => e.Email == Email && e.NoteId == NoteId).FirstOrDefault();
                if (collaborator != null)
                {
                    context.Collaborator.Remove(collaborator);
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
