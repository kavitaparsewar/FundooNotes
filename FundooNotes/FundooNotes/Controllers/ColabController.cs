//using BusinessLayer.Interfaces;
//using CommonLayer.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace FundooNotes.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ColabController : ControllerBase
//    {
//        public IColabBL colabBL;
//        public ColabController(IColabBL userBL)
//        {
//            this.colabBL = userBL;
//        }

//        [Authorize]
//        [HttpPost("AddCOllaborator")]
//        public IActionResult AddUser(ColabModel collabmodel)
//        {
//            try
//            {
//                if (colabBL.AddColabb(collabmodel))
//                {
//                    return this.Ok(new { Success = true, message = "Done Collaborator" });
//                }
//                else
//                {
//                    return this.BadRequest(new { Success = false, message = "Fail" });
//                }
//            }
//            catch (Exception)
//            {

//                throw;
//            }
//        }
//    }
//}
