﻿using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.AppContext;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        Context context;
        private readonly IConfiguration configuration;
        public UserRL(Context context, IConfiguration config)
        {
            this.context = context;//appcontext to for api
            this.configuration = config;//for startup file instance
        }
        public bool Registration(UserRegistration user)
        {
            try
            {
                User newuser = new User();
                newuser.FirstName = user.FirstName;
                newuser.LastName = user.LastName;
                newuser.Email = user.Email;
                newuser.Password = user.Password;

                context.Users.Add(newuser);
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
        public bool Login(UserLogin userlogin)
        {
            try
            {
                User newuser = new User();

                var result = context.Users.Where(x => x.Email == userlogin.Email && x.Password == userlogin.Password).FirstOrDefault();

                if (result != null)
                {
                    string token = "";
                    UserResponse loginResponse = new UserResponse();
                    token = GenerateJwtToken(userlogin.Email);
                    loginResponse.Email = userlogin.Email;
                    loginResponse.Token = token;

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string GenerateJwtToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {              
                Subject = new ClaimsIdentity(new[] { new Claim("Email", email) }),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public string ForgetPassword(string email)
        {
            try
            {
                var chkemail = context.Users.FirstOrDefault(e => e.Email == email);

                if (chkemail != null)
                {
                    var token = GenerateJwtToken(email);

                    new MSMQModel().MsmqSender(token);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }       
        public bool ResetPassword(string email, string password, string confirmPassword)
        {
            try
            {

                if (password.Equals(confirmPassword))
                {
                    User user = context.Users.Where(e => e.Email == email).FirstOrDefault();
                    user.Password = confirmPassword;
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

