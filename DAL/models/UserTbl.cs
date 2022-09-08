using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.models
{
    public partial class UserTbl
    {
        public UserTbl()
        {
            PictureTbls = new HashSet<PictureTbl>();
        }

        public int CodeUser { get; set; }
        public string UserName { get; set; }
        public string UserLastName { get; set; }
        public string UserMail { get; set; }
        public string UserPassword { get; set; }
        public string UserClueForPass { get; set; }

        public virtual ICollection<PictureTbl> PictureTbls { get; set; }
    }
}
