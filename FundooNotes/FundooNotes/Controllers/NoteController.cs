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
    [Route("api/[controller]/[action]")]
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
                //long Id = Note.Claims.First(e => e.Type == "Id").Value;
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);

                noteBL.CreateNotes(notemodel,userId);
                return Ok(new { message = "Note Created successfull" });
            }
            catch (Exception )
            {

                throw ;
            }
        }

        [Authorize]
        [HttpPut("UpdateNote")]     
        public IActionResult UpdateNotes(long ID, NotesModel notesModel)
        {
            try
            {
                 if (noteBL.UpdateNotes(ID, notesModel))
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


        public IActionResult DeleteNote(long ID)
        {
            try
            {
                if (noteBL.DeleteNote(ID))
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
        [HttpGet("GetNote")]

        public IEnumerable<NotesModel> GetNotes()
        {
            try
            {
                return noteBL.GetNote();
            }
            catch (Exception)
            {

                throw;
            }
        }



        [Authorize]
        [HttpPut("Archieve")]
        public IActionResult IsArchieveNote(long ID) 
        {
            try
            {
               bool result = this.noteBL.IsArchieveNote(ID);


                if (result == true)
                {

                    return this.Ok(new { Success = true, message = "Unarchieved" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Archieved" });
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }



        [Authorize]
        [HttpPut("Pin")]

        public IActionResult IsPin(long ID)
        {
            try
            {
                bool result = this.noteBL.IsPin(ID);


                if (result == true)
                {

                    return this.Ok(new { Success = true, message = "Note Pinned " });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Pin Unsuccessful" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpPut("Trash")]

        public IActionResult IsTrash(long ID)
        {
            try
            {
                bool result = this.noteBL.IsTrash(ID);


                if (result == true)
                {

                    return this.Ok(new { Success = true, message = "Now Your note in Trash" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Not in Trash" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpPut("Image")]

        public IActionResult Image(long ID, IFormFile image)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (noteBL.Image(ID, userId, image))
                {
                    return this.Ok(new { Success = true, message = "Image uploaded successfully" });

                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Fail" });
                    
                }
            }
            catch (Exception)
            {
                throw;
            }

        }


        [Authorize]
        [HttpPut("color")]

        public IActionResult Color(long NoteID, string Color)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (noteBL.Colorchange(userId, NoteID, Color))
                {
                    return this.Ok(new { Success = true, message = "Color Changed" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Fail" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
