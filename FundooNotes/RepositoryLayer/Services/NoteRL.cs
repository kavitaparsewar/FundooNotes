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
    public class NoteRL : INoteRL
    {
        Context context;
        private readonly IConfiguration configuration;
        public NoteRL(Context context, IConfiguration config)
        {
            this.context = context;//appcontext to for api
            this.configuration = config;//for startup file instance
        }


        public bool CreateNote(NotesModel notemodel)
        {
            try
            {
                Note notes = new Note();

                notes.Id = notemodel.Id;
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

        //public string GenerateJwtToken(string Id)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(configuration["Jwt:key"]);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new[] { new Claim("Email", Id) }),
        //        Expires = DateTime.Now.AddHours(1),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}


        public bool UpdateNotes(int noteID, NotesModel notesModel)
        {
            Note notes = context.Notes.Where(e => e.NoteId == noteID).FirstOrDefault();
            
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


        public bool DeleteNote(int noteID)
        {
            Note notes = context.Notes.Where(e => e.NoteId == noteID).FirstOrDefault();


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
    }
}
