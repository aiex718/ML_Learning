using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML_Lib.Tools
{
    public class BitmapReader: FileReader
    {
        static readonly string[] DefaultFilter = { ".jpg", ".png" };
        public BitmapReader(string Path, string[] Filters = null) :base(Path, Filters ?? DefaultFilter)
        {

        }

        public List<Bitmap> GetBitmaps()
        {
            List<Bitmap> Result = new List<Bitmap>();
            foreach (var item in FileList)
            {
                string FileName = item.Substring(item.LastIndexOf("\\") + 1, item.Length - item.LastIndexOf("\\") - 5);
                Bitmap b = new Bitmap(item);
                b.Tag = FileName;
                Result.Add(b);
            }
            return Result;
        }

    }
}
