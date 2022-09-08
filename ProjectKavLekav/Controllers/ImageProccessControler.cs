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
//using System.Reflection.Metadata;
using System.Threading.Tasks;
using PdfSharp.Pdf;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Windows.UI.Xaml.Media.Imaging;
using Syncfusion.Pdf.Graphics;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Image = System.Drawing.Image;
//using SelectPdf;
//using SelectPdf;
//using Syncfusion.Pdf.Graphics;
//using Syncfusion.Pdf;
//using PdfPage = PdfSharp.Pdf.PdfPage;

namespace ProjectKavLekav.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageProccessControler : ControllerBase
    {
        ImageProccess imP;
        string path = "C:\\Users\\user\\Desktop\\projectKavLekav_shira\\ProjectKavLekav\\ProjectKavLekav\\wwwroot\\images\\";
        [HttpPost("newGame")]
        public IActionResult CreatNewGame(int codeCounting, int a1, int d, string name)
        {
            //יצירת קובץ 
           ImageProccess imP = new ImageProccess(new Bitmap(path+name));
            Bitmap img = imP.CreatePoints(codeCounting,a1,d);
            if (img != null)
            {
                img.Save(path + "\\QuizPictures\\" + " חידון " + name);
                return Ok("1");
            }
                //שמירה בשרת


                // var pic = new Bitmap(path+name);
                //// Bitmap pic = new Bitmap(@"C:\Users\user\Desktop\projectKavLekav_shira\ProjectKavLekav\ProjectKavLekav\wwwroot\images\גלידה.jpg");
                // imP = new ImageProccess(pic);
                // Bitmap _newGame = new Bitmap(pic.Width, pic.Height);
                // _newGame = imP.CreatePoints(codeCounting, a1, d);
                // string[] n = name.Split('.');
                // name = n[0];
                // //SelectPdf.PdfDocument doc = new SelectPdf.PdfDocument();
                // //SelectPdf.PdfPage page1 = doc.AddPage();
                // //SelectPdf.PdfFont font = doc.AddFont(SelectPdf.PdfStandardFont.Helvetica);
                // //font.Size = 20;
                // //var codeBitmap = new Bitmap(_newGame);
                // Image image1 = (Image)_newGame;
                // image1.Save(path+name);

                // System.Drawing.Image image = System.Drawing.Image.FromFile(path + name);
                // Document doc = new Document(PageSize.A4);
                // PdfWriter.GetInstance(doc, new FileStream(path + " חידון " + name + ".pdf", FileMode.Create));
                // //PdfWriter.GetInstance(doc, new FileStream(@"C:\Users\user\Desktop\projectChanuca\פרויקט כולל כל ההתקנות mdb\ProjectChanucaREACT\MDB 2 React\src\assets\images", FileMode.Create));
                // doc.Open();
                // iTextSharp.text.Image pdfImage = iTextSharp.text.Image.GetInstance(image, System.Drawing.Imaging.ImageFormat.Jpeg);
                // doc.Add(pdfImage);
                // doc.Close();






            else


                return Ok("0");
        }



      
    }
}



