using CommonLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL  
    {
        public User Registration(UserRegistration user);
        public string Login(UserLogin userlogin);            
        public string GenerateJwtToken(string email);
        public  string ForgetPassword(string email);
        public bool ResetPassword(string email,string password, string confirmpassword);
    }
}
