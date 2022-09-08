using BL;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace KavLekavAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserControler : ControllerBase
    {
        UserBL _Ubl = new UserBL();
       
      

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            return Ok(_Ubl.GetAllUsers());
        }

        [HttpGet]
        public IActionResult GetUser(string name, string password)
        {
            return Ok(_Ubl.GetUser(name, password));
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] UserDTO user)
        {

            return Ok(_Ubl.AddUser(user));
        }
        [HttpPut("UppdateUser/{id}")]
        public IActionResult UppdateUser([FromBody] UserDTO user, int id)
        {
            return Ok(_Ubl.UppdateUser(user, id));
        }

        [HttpDelete("RemoveUser/{id}")]
        public IActionResult RemoveUser(int id)
        {
            return Ok(_Ubl.RemoveUser(id));
        }
      
        
        [HttpGet("ReadMenegerPassword")]
        public IActionResult ReadMenegerPassword()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("C:\\Users\\user\\Desktop\\projectKavLekav_shira\\ProjectKavLekav\\ProjectKavLekav\\wwwroot\\Password.xml");
            XDocument doc = XDocument.Load("C:\\Users\\user\\Desktop\\projectKavLekav_shira\\ProjectKavLekav\\ProjectKavLekav\\wwwroot\\Password.xml");
            return Ok(xmlDoc.InnerText);
        }
    }
}
