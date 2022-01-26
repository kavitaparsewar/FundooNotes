﻿using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class UserController : ControllerBase
    {
        public IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }
        [HttpPost("Register")]
        public IActionResult AddUser(UserRegistration user)
        {
            try
            {
                if (userBL.Registration(user))
                {
                    return this.Ok(new { Success = true, message = "Registration successfull" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Registration Unsuccessfull" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost("Login")]
        public IActionResult AddLogin(UserLogin userlogin)
        {
            try
            {
                string token = userBL.GenerateJWTToken(userlogin.Email);
                if (userBL.Login(userlogin))
                {
                   
                    return this.Ok(new { Success = true, message = "login successful",Token=token });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "something Wrong" });
                }
            }
            catch (Exception)
            {

                throw;
            }          
        }
       [HttpPost("FogotPassword")]
        public IActionResult FogotPassword(string EmailId)
        {
            try
            {
                if (userBL.FogotPassword(Email))
                {
                    string token = userBL.FogotPassword(EmailId.Email);
                    return this.Ok(new { Success = true, message = "login successful" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "something Wrong" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
