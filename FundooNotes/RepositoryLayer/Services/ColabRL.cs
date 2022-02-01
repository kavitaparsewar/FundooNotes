//using Microsoft.Extensions.Configuration;
//using RepositoryLayer.AppContext;
//using RepositoryLayer.Entities;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace RepositoryLayer.Services
//{
//    public class ColabRL
//    {
//        Context context;
//        private readonly IConfiguration configuration;
//        public ColabRL(Context context, IConfiguration config)
//        {
//            this.context = context;//appcontext to for api
//            this.configuration = config;//for startup file instance
//        }

//        public bool AddColabb(ColabModel collabmodel)
//        {
//            try
//            {
//                Collab newuser = new Collab();

//                context.Collaborator.Add(newuser);
//                int result = context.SaveChanges();//save all changes in database also
//                if (result > 0)
//                    return true;
//                else
//                    return false;

//            }

//            catch (Exception)
//            {

//                throw;
//            }
//        }
//    }
//}
