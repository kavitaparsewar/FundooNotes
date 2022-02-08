using CommonLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IUserBL
    {
        public User Registration(UserRegistration user);
        public string Login(UserLogin userlogin);
        public string GenerateJWTToken(string email);
        public string ForgetPassword(string email);
        public bool ResetPassword(string email,string password, string confirmpassword);

       
    }
}
