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
        public string Login(UserLogin userlogin)
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
        public string GenerateJWTToken(string email)
        {
            try
            {
                return userRL.GenerateJwtToken(email);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string ForgetPassword(string email)
        {
            try
            {
                return userRL.ForgetPassword(email);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ResetPassword(string email, string password, string confirmpassword)
        {
            try
            {
                return userRL.ResetPassword(email,password, confirmpassword);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
