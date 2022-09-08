using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.models
{
    public partial class CountingTbl
    {
        public CountingTbl()
        {
            PictureTbls = new HashSet<PictureTbl>();
        }

        public int CodeCounting { get; set; }
        public string NameCounting { get; set; }

        public virtual ICollection<PictureTbl> PictureTbls { get; set; }
    }
}
