using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
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
    public class ColabController : ControllerBase
    {
        public IColabBL colabBL;
        public ColabController(IColabBL userBL)
        {
            this.colabBL = userBL;
        }

        [Authorize]
        [HttpPost("AddCOllaborator")]
        public IActionResult AddColab(ColabModel collabmodel)
        {
            try
            {
                //long Id = Note.Claims.First(e => e.Type == "Id").Value;
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);

                colabBL.AddColab(collabmodel, userId);
                return Ok(new { message = "Colaboration successfull" });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
