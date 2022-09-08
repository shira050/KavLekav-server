using BL;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KavLekavAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KategoryControler : ControllerBase
    {
        KategoryBL _Kbl = new KategoryBL();
    

        [HttpGet("GetAllKategories")]
        public IActionResult GetAllCategories()
        {
            return Ok(_Kbl.GetAllCategories());
        }

        [HttpGet]
        public IActionResult GetKategory(int code)
        {
            return Ok(_Kbl.GetKategory(code));
        }

        [HttpPost]
        public IActionResult AddKategory([FromBody] KategoryDTO c)
        {

            return Ok(_Kbl.AddKategory(c));
        }
        [HttpPut("UppdateKategory/{id}")]
        public IActionResult UppdateKategory([FromBody] KategoryDTO cat, int id)
        {

            return Ok(_Kbl.UppdateKategory(cat, id));
        }

        [HttpDelete("RemoveKategory/{id}")]
        public IActionResult RemoveKategory(int id)
        {
            return Ok(_Kbl.RemoveKategory(id));
        }
    }
}
