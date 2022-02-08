using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.AppContext;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        public INoteBL noteBL;

        Context context;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        public NoteController(INoteBL notesBL, IMemoryCache memoryCache, IDistributedCache distributedCache, Context context)
        {
            this.noteBL = notesBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.context = context;
        }

        [Authorize]
        [HttpPost]
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

        [HttpGet("redis")]
        public async Task<IActionResult> GetAllNotesUsingRedisCache()
        {
            var cacheKey = "NotesList";
            string serializedNotesList;
            var notesList = new List<Note>();
            var redisNotesList = await distributedCache.GetAsync(cacheKey);
            if (redisNotesList != null)
            {
                serializedNotesList = Encoding.UTF8.GetString(redisNotesList);
                notesList = JsonConvert.DeserializeObject<List<Note>>(serializedNotesList);
            }
            else
            {
                notesList = await context.Notes.ToListAsync();
                serializedNotesList = JsonConvert.SerializeObject(notesList);
                redisNotesList = Encoding.UTF8.GetBytes(serializedNotesList);
            }
            return Ok(notesList);
        }


        [Authorize]
        [HttpPut]     
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
        [HttpDelete]


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
        [HttpGet]

        public IEnumerable<Note> GetNotes()
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
        [HttpGet]

        public IEnumerable<Note> GetNoteById(long id)
        {
            try
            {
                return noteBL.GetNoteById(id);
            }
            catch (Exception)
            {

                throw;
            }
        }


        [Authorize]
        [HttpPut]
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
        [HttpPut]

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
        [HttpPut]

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
        [HttpPut]

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
        [HttpPut]

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
