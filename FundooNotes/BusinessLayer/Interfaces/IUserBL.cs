using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IUserBL
    {
        public bool Registration(UserRegistration user);
        public bool Login(UserLogin userlogin);
        public string GenerateJWTToken(string Email);
       public string ForgetPassword(string Email);

        public string ResetPassword(string Email);
    }
}
