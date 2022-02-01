using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.AppContext
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Collab> Collaborator { get; set; }
    }
}
