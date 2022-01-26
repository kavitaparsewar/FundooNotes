﻿using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        public bool Registration(UserRegistration user);

        public bool Login(UserLogin userlogin);

        public string GenerateJWTToken(string Email);

        public string FogotPassword(string Email);
    }
}
