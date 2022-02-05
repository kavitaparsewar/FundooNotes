using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        public ILabelBL labelBL;
        public LabelController(ILabelBL userBL)
        {
            this.labelBL = userBL;
        }
        [Authorize]
        [HttpPost("CreateLabel")]       
        public IActionResult CreateLabel(long Id, long NoteId, string newlabelName)
        {
            try
            {
                long ID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = labelBL.CreateLabel(Id, NoteId, newlabelName);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Label added successfully"});
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Label already exist" });
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        [Authorize]
        [HttpPut("RenameLabel")]

        public IActionResult RenameLabel(long Id, string oldLabelName, string newlabelName)
        {
            try
            {
                long ID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = labelBL.RenameLabel(Id, oldLabelName,newlabelName);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Label renamed ", Response = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Unsuccessful" });
                }
            }
            catch (Exception)
            {

                throw;
            }

        }


        [Authorize]
        [HttpGet("GetLabelByNoteId")]
        public IEnumerable GetLabelByNoteId(long Id, long NoteId)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                return labelBL.GetLabelByNoteId(Id,NoteId);
            }
            catch (Exception)
            {

                throw;
            }
        }


        [Authorize]
        [HttpDelete("DeleteLabel")]
        public IActionResult DeleteLabel(long Id ,string labelName)
        {
            try
            {
                long ID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (labelBL.DeleteLabel(Id, labelName))
                {
                    return this.Ok(new { success = true, message = "Label removed successfully" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "User access denied" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        [Authorize]
        [HttpDelete("DeleteLabelById")]

        public IActionResult DeleteLabelByNoteId(long Id, long NoteId, string labelName)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);


                if (labelBL.DeleteLabelByNoteId(Id, NoteId,labelName))
                {
                    return this.Ok(new { success = true, message = "Label removed successfully" });
                }


                else
                {
                    return this.BadRequest(new { success = false, message = "User access denied" });
                }
            }


            catch (Exception)
            {

                throw;
            }
        }
    }
}
