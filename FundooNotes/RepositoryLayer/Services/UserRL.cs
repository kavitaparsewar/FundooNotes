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
        /// <summary>
        /// this is registration method.
        /// </summary>
        /// <param name="user">this is model object   </param>
        /// <returns>  it returns current updated object </returns>
        public User Registration(UserRegistration user)
        {
            try
            {
                User newuser = new User();
                newuser.FirstName = user.FirstName;
                newuser.LastName = user.LastName;
                newuser.Email = user.Email;
                newuser.Password = EncryptPassword(user.Password);

                context.Users.Add(newuser);
                int result = context.SaveChanges();//save all changes in database also
                if (result > 0)
                    return newuser;
                else
                    return newuser;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string EncryptPassword(string password)
        {
            try
            {
                byte[] encode = new byte[password.Length];
                encode = Encoding.UTF8.GetBytes(password);
                string encPassword = Convert.ToBase64String(encode);
                return encPassword;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string DecryptPassword(string encryptpwd)
        {
            try
            {
                UTF8Encoding encodepwd = new UTF8Encoding();
                Decoder Decode = encodepwd.GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
                int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string decryptpwd = new String(decoded_char);
                return decryptpwd;
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
                User user = new User();
                user = context.Users.Where(x => x.Email == userlogin.Email).FirstOrDefault();
                string decPass = DecryptPassword(user.Password);
                var id = user.Id;
                if (decPass == userlogin.Password && user != null)
                    return TokenForId(id);
                else
                    return null;

            }
            catch (Exception)
            {

                throw;
            }


            


        }

        public string TokenForId(long Id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("Id", Id.ToString()) }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
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
               // var chkemail = context.Users.FirstOrDefault(e => e.Email == email);

                if (email != null)
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
                    user.Password = EncryptPassword(confirmPassword);
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

