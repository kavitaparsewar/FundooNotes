using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models
{
   public class NotesModel
    {
        //public long Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime Remainder { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
        public bool IsArchieve { get; set; }
        public bool IsPin { get; set; }
        public bool IsTrash { get; set; }
        public DateTime? Createst { get; set; }
        public DateTime? Modifiedat { get; set; }
    }
}
