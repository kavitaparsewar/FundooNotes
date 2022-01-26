using CommonLayer.Models;
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

        public string GenerateJwtToken(string Email)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt : key"]));
            var credintials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]{
                new Claim(ClaimTypes.Email,Email)
            };

            var token = new JwtSecurityToken(configuration["jwt:Issuer"], Email,
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credintials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //public string GenerateJWTToken(string Email)
        //{
        //    throw new NotImplementedException();
        //}




        public string ForgetPassword(string EmailId)
        {
            try
            {

                var Email = context.Users.FirstOrDefault(e => e.Email == EmailId);

                if (Email != null)
                {
                    var token = GenerateJwtToken(Email.Email);

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

    }
}

