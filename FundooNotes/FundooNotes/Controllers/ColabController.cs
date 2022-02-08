using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ColabController : ControllerBase
    {
        public IColabBL colabBL;
        public ColabController(IColabBL userBL)
        {
            this.colabBL = userBL;
        }
    
        [Authorize]
        [HttpPost]
        public IActionResult AddCollaborator(long NoteId, string Email)
        {
            try
            {
                long Id = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);

                ColabModel colabmodel = new ColabModel();

                colabmodel.Id = Id;
                colabmodel.NoteId = NoteId;
                colabmodel.Email = Email;
                var result = colabBL.AddColab(colabmodel);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Collaborator added successfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Failed" });
                }


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize]
        [HttpGet]
        public IEnumerable<Collab> GetCollaboratorsByID(long id, long NoteId)
        {
            try
            {
                long Id = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                return colabBL.GetColabById(id, NoteId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpDelete]
        public IActionResult DeleteColab(long Id, long NoteId, string Email)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (colabBL.DeleteColab(Id,NoteId,Email))
                {
                    return this.Ok(new { success = "true", message = "Collaborator Removed" });
                }
                else
                {
                    return this.BadRequest(new { success = "false", message = "Fail" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}





