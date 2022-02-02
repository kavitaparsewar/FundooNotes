using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.AppContext;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class ColabRL
    {
        Context context;
        private readonly IConfiguration configuration;
        public ColabRL(Context context, IConfiguration config)
        {
            this.context = context;//appcontext to for api
            this.configuration = config;//for startup file instance
        }

        public bool AddColab(ColabModel collabmodel)
        {
            try
            {
                Collab newcolab = new Collab();

                context.Collaborator.Add(newcolab);
                int result = context.SaveChanges();//save all changes in database also
                if (result > 0)
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
