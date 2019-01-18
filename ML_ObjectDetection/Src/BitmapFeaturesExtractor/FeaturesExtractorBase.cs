using ML_Lib.DataType;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML_Lib.BitmapFeaturesExtractor
{
    public abstract class FeaturesExtractorBase<T> where T : Vector
    {
        public IEnumerable<T> ExtractFeatures(IEnumerable<Bitmap> bitmaps, string TagSet)
        {
            List<T> Result = new List<T>();

            Parallel.ForEach(bitmaps, bitmap =>
            //foreach (var bitmap in bitmaps)
            {
                var FeaturesGet = ExtractFeatures(bitmap, TagSet);
                lock (Result)
                {
                    Result.AddRange(FeaturesGet);
                }
            }
            );
            return Result;
        }
        public abstract IEnumerable<T> ExtractFeatures(Bitmap bitmap, string TagSet);

    }
}
