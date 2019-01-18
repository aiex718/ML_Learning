using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML_Lib.Tools
{
    public class FileReader
    {
        readonly protected string TargetPath;
        readonly protected List<string> FileList;

        public FileReader(string Path,string[] Filters=null)
        {
            TargetPath = Path;
            FileList = new List<string>();

            var Files = Directory.GetFiles(TargetPath);

            if (Filters==null)
            {
                FileList.AddRange(Files);
            }
            else
            {
                foreach (var filter in Filters)
                {
                    FileList.AddRange(Files.Where(x => x.ToLower().Contains(filter)));
                }
            }
        }

        public List<string> GetFilepathList()
        {
            return FileList;
        }


    }
}
