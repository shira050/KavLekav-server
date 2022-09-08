using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.models
{
    public partial class KategoryTbl
    {
        public KategoryTbl()
        {
            PictureTbls = new HashSet<PictureTbl>();
        }

        public int CodeKategory { get; set; }
        public string NameKategory { get; set; }

        public virtual ICollection<PictureTbl> PictureTbls { get; set; }
    }
}
