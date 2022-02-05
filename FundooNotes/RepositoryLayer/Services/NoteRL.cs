using CommonLayer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.AppContext;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class NoteRL : INoteRL
    {
        Context context;
        //[Obsolete]

        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IConfiguration configuration;
        public NoteRL(Context context, IConfiguration config, IHostingEnvironment hostingEnvironment)
        {
            this.context = context;//appcontext to for api
            this.configuration = config;//for startup file instance

            this.hostingEnvironment = hostingEnvironment;
        }


        public bool CreateNote(NotesModel notemodel, long ID)
        {
            try
            {
                Note notes = new Note();
                
                notes.Id = ID;
                notes.Title = notemodel.Title;
                notes.Message = notemodel.Message;
                notes.Remainder = notemodel.Remainder;
                notes.Color = notemodel.Color;
                notes.Image = notemodel.Image;
                notes.IsArchieve = notemodel.IsArchieve;
                notes.IsPin = notemodel.IsPin;
                notes.IsTrash = notemodel.IsTrash;

                context.Notes.Add(notes);
                var result = context.SaveChanges();//save all changes in database also
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

        
        public bool UpdateNotes(long ID, NotesModel notesModel)
        {
            Note notes = context.Notes.Where(e => e.NoteId == ID).FirstOrDefault();

            notes.Title = notesModel.Title;
            notes.Message = notesModel.Message;
            notes.Remainder = notesModel.Remainder;
            notes.Color = notesModel.Color;
            notes.Image = notesModel.Image;
            notes.IsArchieve = notesModel.IsArchieve;
            notes.IsPin = notesModel.IsPin;
            notes.IsTrash = notesModel.IsTrash;


            var result = context.SaveChanges();
            if (result > 0)
                return true;

            else
                return false;
        }


        public bool DeleteNote(long ID)
        {
            Note notes = context.Notes.Where(e => e.NoteId == ID).FirstOrDefault();


            if (notes != null)
            {
                context.Notes.Remove(notes);

                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }


        public IEnumerable<Note> GetNote()
        {
           return context.Notes.ToList();          
        }

        public IEnumerable<Note> GetNoteById(long id)
        {             
            return context.Notes.Where(e => e.Id == id).ToList();           
        }

        public bool IsArchieveNote(long ID)
        {
            try
            {

                Note notes = this.context.Notes.FirstOrDefault(e => e.NoteId == ID);
                if (notes.IsArchieve == true)
                {
                    notes.IsArchieve = false;
                    this.context.SaveChanges();
                    return true;
                }
                else
                {
                    notes.IsArchieve = true;
                    this.context.SaveChanges();
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }


        public bool IsPin(long ID)
        {
            try
            {

                Note notes = this.context.Notes.FirstOrDefault(e => e.NoteId == ID);
                if (notes.IsPin == true)
                {
                    notes.IsPin = false;
                    this.context.SaveChanges();
                    return true;
                }
                else
                {
                    notes.IsPin = true;
                    this.context.SaveChanges();
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool IsTrash(long ID)
        {
            try
            {

                Note notes = this.context.Notes.FirstOrDefault(e => e.NoteId == ID);
                if (notes.IsTrash == true)
                {
                    notes.IsTrash = false;
                    this.context.SaveChanges();
                    return true;
                }
                else
                {
                    notes.IsTrash = true;
                    this.context.SaveChanges();
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Image(long userID, long ID, IFormFile file)
        {
            try
            {
                var target = Path.Combine(hostingEnvironment.ContentRootPath, "Image");
                Directory.CreateDirectory(target);
                var filePath = Path.Combine(target, file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                Note note = context.Notes.FirstOrDefault(e => e.Id == userID && e.NoteId == ID);
                if (note != null)
                {
                    note.Image = file.FileName;
                    var result = context.SaveChanges();
                    
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


        public bool color(long ID, long noteID,string color)
        {
            
            Note note = context.Notes.FirstOrDefault(e => e.Id == ID && e.NoteId == ID);

            if (note != null)
            {

                note.Color = color;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
