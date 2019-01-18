using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML_Lib.DataType.Extensions
{
    public static class BoundingBoxExtensions
    {
        public static BoundingBox GetBoundingBox(this IEnumerable<BoundingBox> BoundingBoxes)
        {
            float MinimumX, MinimumY;
            float MaximumX, MaximumY;
            if (BoundingBoxes!=null && BoundingBoxes.Any())
            {
                MinimumX = BoundingBoxes.Select(box => box.MinimumX).Min();
                MinimumY = BoundingBoxes.Select(box => box.MinimumY).Min();

                MaximumX = BoundingBoxes.Select(box => box.MaximumX).Max();
                MaximumY = BoundingBoxes.Select(box => box.MaximumY).Max();
                return new BoundingBox(MinimumX, MinimumY, MaximumX, MaximumY);
            }
            else
                return null;
        }
    }
}
