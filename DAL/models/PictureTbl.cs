using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.models
{
    public partial class PictureTbl
    {
        public int CodePicture { get; set; }
        public int? CodeUser { get; set; }
        public int? CodeKategory { get; set; }
        public DateTime? UppDateToWeb { get; set; }
        public int? CascadingPicture { get; set; }
        public string RouteSoursePicture { get; set; }
        public string RouteGoalPicture { get; set; }
        public int? CountOfDownloads { get; set; }
        public int? CodeCounting { get; set; }

        public virtual CountingTbl CodeCountingNavigation { get; set; }
        public virtual KategoryTbl CodeKategoryNavigation { get; set; }
        public virtual UserTbl CodeUserNavigation { get; set; }
    }
}
