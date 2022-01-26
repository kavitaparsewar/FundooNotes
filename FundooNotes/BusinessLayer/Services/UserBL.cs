using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        IUserRL userRL;
        public UserBL(IUserRL userBL)
        {
            this.userRL = userBL;
        }
        public bool Registration(UserRegistration user)
        {
            try
            {
                return userRL.Registration(user);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public bool Login(UserLogin userlogin)
        {
            try
            {
                return userRL.Login(userlogin);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string GenerateJWTToken(string Email)
        {
            try
            {
                return userRL.GenerateJWTToken(Email);
            }
            catch (Exception)
            {

                throw;
            }
        }


       public string FogotPassword(string Email)
        {
            try
            {
                return userRL.FogotPassword(Email);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
