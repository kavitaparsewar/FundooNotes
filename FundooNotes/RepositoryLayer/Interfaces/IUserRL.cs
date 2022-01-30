using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL  
    {
        public bool Registration(UserRegistration user);
        public bool Login(UserLogin userlogin);            
        public string GenerateJwtToken(string email);
        public  string ForgetPassword(string email);
        public bool ResetPassword(string email,string password, string confirmpassword);
    }
}
