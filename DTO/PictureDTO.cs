using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
  public  class PictureDTO
    {
        public int CodePicture { get; set; }
        public int? CodeUser { get; set; }
        public int? CodeKategory { get; set; }
        public DateTime? UppDateToWeb { get; set; }
        public int? CascadingPicture { get; set; }
        public string RouteSoursePicture { get; set; }
        public string RouteGoalPicture { get; set; }
        public int? CountOfDownloads { get; set; }
        public int? codeCounting { get; set; }

    }
}
