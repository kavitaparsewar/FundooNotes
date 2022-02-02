using BusinessLayer.Interfaces;
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

        public bool AddColab(string Email)
        {
            try
            {
                return colabRL.AddColab(colab);
            }
            catch (Exception)
            {

                throw;
            }
        }

        //public bool DeleteColab(string Email)
        //{
        //    try
        //    {
        //        return colabRL.DeleteColab(colab);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //public bool GetColab(string Email)
        //{
        //    try
        //    {
        //        return colabRL.GetColab(colab);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
    }
}
