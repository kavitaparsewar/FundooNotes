using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Entities
{
    public class Collab
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CollabId { get; set; }
        [ForeignKey("note")]

        public long NoteId { get; set; }
        public string Email { get; set; }
        [ForeignKey("user")]

        public long Id { get; set; }
        public virtual User user { get; set; }

        public virtual Note note { get; set; }            

    }
}
