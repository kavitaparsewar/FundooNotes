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
    public class NoteController : ControllerBase
    {
        public INoteBL noteBL;
        public NoteController(INoteBL noteBL)
        {
            this.noteBL = noteBL;
        }


        [Authorize]
        [HttpPost("CreateNote")]
        public IActionResult CreateNotes(NotesModel notemodel)
        {
            try
            {
                if (noteBL.CreateNotes(notemodel))
                {
                    return this.Ok(new { Success = true, message = "Created successfull" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Unsuccessfull" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        [Authorize]
        [HttpPut("UpdateNote")]     
        public IActionResult UpdateNotes(int noteID, NotesModel notesModel)
        {
            try
            {
                 if (noteBL.UpdateNotes(noteID, notesModel))
                    {
                    return this.Ok(new { Success = true, message = "Record Updated" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Unsuccessfull" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpDelete("DeleteNote")]


        public IActionResult DeleteNote(int noteID)
        {
            try
            {
                if (noteBL.DeleteNote(noteID))
                {
                    return this.Ok(new { Success = true, message = "Record Deleted" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Unsuccessfull" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpGet("UserId")]

        [Authorize]
        [HttpPut("Archieve")]

    }
}
