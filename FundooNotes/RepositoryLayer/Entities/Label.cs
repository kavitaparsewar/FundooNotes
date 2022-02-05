using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Entities
{
    public class Label
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long LabelId { get; set; }
        public string LabelName { get; set; }
        [ForeignKey("notes")]
        public long NoteId { get; set; }
        [ForeignKey("user")]
        public long Id { get; set; }
        public virtual User user { get; set; }
        public virtual Note notes { get; set; }
    }
}
