using BL;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PdfSharp.Drawing;
using ProjectKavLekav;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using PdfSharp.Pdf;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Windows.UI.Xaml.Media.Imaging;
using System.Xml.Linq;
using iTextSharp.text;

namespace KavLekavAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PictureControler : ControllerBase
    {
        PictureBL _Pbl = new PictureBL();
       
        [HttpGet("GetAllPictures")]
        public IActionResult GetAllPictures()
        {
            return Ok(_Pbl.GetAllPictures());
        }

        [HttpGet]
        public IActionResult GetPicture(int code)
        {

            return Ok(_Pbl.GetPicture(code));
        }
        [HttpPost]
        public IActionResult AddPicture([FromBody] PictureDTO p)
        {
            //if(p.RouteGoalPicture!=null)
            return Ok(_Pbl.AddPicture(p));//הוספת תמונה מוכנה ע"י המנהל

            //  imP = new ImageProccess(new System.Drawing.Bitmap(path+""+p.RouteSoursePicture));
            //  //יצירת קובץ תמונה חדש ולהכניס לתוכו את מה שיצה
            //  Bitmap newImage = new Bitmap();
            //  //  הקובץ החדש שווה ל:=
            //  imP.CreatePoints();
            ////p.RouteGoalPicture=

            return Ok(_Pbl.AddPicture(p));//הוספת חידון ע"י שליחה לאלגוריתם
        }



        [HttpPut("UppdatePicture/{id}")]
        public IActionResult UppdatePicture([FromBody] PictureDTO p, int id)
        {

            return Ok(_Pbl.UppdatePicture(p, id));
        }

        [HttpDelete("RemovePicture/{id}")]
        public IActionResult RemovePicture(int id)
        {
            return Ok(_Pbl.RemovePicture(id));
        }

        [HttpGet("GetAllPicturesInCategory/{codeCategory}")]
        public IActionResult GetAllPicturesInCategory(int codeCategory)
        {
            return Ok(_Pbl.GetAllPicturesInCategory(codeCategory));
        }
        [HttpGet("GetAllPicturesOfOneUser/{codeUser}")]
        public IActionResult GetAllPicturesOfOneUser(int codeUser)
        {
            return Ok(_Pbl.GetAllPicturesOfOneUser(codeUser));
        }




        [HttpGet("GetAllCountings")]
        public IActionResult GetAllCountings()
        {
            return Ok(_Pbl.GetAllCountings());
        }
        string p = "images\\QuizPictures\\";

     
        
        ///העלאת תמונה
        public static string fName { get; set; }//משתנה שיכיל את שם הקובץ שיעלה
        [HttpPost("upload"), DisableRequestSizeLimit]
        public IActionResult uploade()

        {
            try
            {
var file = Request.Form.Files[0];
              
            var   folderName = Path.Combine("wwwroot", "images");//)wwroot(הקובץ יאוחסן בתיקייה  שהכנתי מראש
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    fName = fileName.ToString();
                    //  XDocument d = XDocument.Load(@"C:\Users\user\Desktop\projectKavLekav_shira\ProjectKavLekav\ProjectKavLekav\wwwroot\images\" + fName);
                    string[] filePaths = Directory.GetFiles(@"C:\Users\user\Desktop\projectKavLekav_shira\ProjectKavLekav\ProjectKavLekav\wwwroot\images\");
                    List <ListItem> files = new List<ListItem>();
                    foreach (string filePath in filePaths)
                    {
                        string n = Path.GetFileName(filePath);
                        if (n == fName)
                            throw new Exception("קיימת תמונה באותו השם ניתן להליף את השם ולנסות שוב");
                    }
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { dbPath });

                }
                else
                {
                    return BadRequest();
                }

                
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");//שגיאה שתיזרק אם ההעלאה תיכשל
            }
        }
        [HttpPost("uploadMenneger"), DisableRequestSizeLimit]
        public IActionResult uploadMenneger()

        {
            try
            {
                var file = Request.Form.Files[0];

                var folderName = Path.Combine("wwwroot",p);//)wwroot(הקובץ יאוחסן בתיקייה  שהכנתי מראש
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    fName = fileName.ToString();
                    //  XDocument d = XDocument.Load(@"C:\Users\user\Desktop\projectKavLekav_shira\ProjectKavLekav\ProjectKavLekav\wwwroot\images\" + fName);
                    string[] filePaths = Directory.GetFiles(@"C:\Users\user\Desktop\projectKavLekav_shira\ProjectKavLekav\ProjectKavLekav\wwwroot\images\");
                    List<ListItem> files = new List<ListItem>();
                    foreach (string filePath in filePaths)
                    {
                        string n = Path.GetFileName(filePath);
                        if (n == fName)
                        {
                            throw new Exception("קיימת תמונה באותו השם ניתן להליף את השם ולנסות שוב");
                            return null;
                        }
                    }
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { dbPath });

                }
                else
                {
                    return BadRequest();
                }


            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");//שגיאה שתיזרק אם ההעלאה תיכשל
            }
        }





        //לחישוב הדירוג
        // public static int countAllDounlods { get; set; }
        [HttpPut("cascading")]
        public void cascading(int id,[FromBody] PictureDTO p)
        {
            
            _Pbl.cascading(p, id);
          //  p.CountOfDownloads += 1;//עדכון ההורדות של התמונה הנוכחית

            //return Ok();
        }

      

        }
    }

