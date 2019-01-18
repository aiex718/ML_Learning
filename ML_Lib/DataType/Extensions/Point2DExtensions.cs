using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML_Lib.DataType.Extensions
{
    public static class Point2DExtensions
    {
        public static BoundingBox GetBoundingBox(this IEnumerable<Point2D> points)
        {
            if (points.Any())
            {
                float MinimumX, MinimumY;
                float MaximumX, MaximumY;

                var XCollection = points.Select(p => p.x);
                MinimumX = XCollection.Min();
                MaximumX = XCollection.Max();

                var YCollection = points.Select(p => p.y);
                MinimumY = YCollection.Min();
                MaximumY = YCollection.Max();

                return new BoundingBox(MinimumX, MinimumY, MaximumX, MaximumY);
            }
            else
                return null;
        }

        public static BoundingBox GetBoundingBox(this IEnumerable<Point2D> points,double MinimumAreaSize)
        {
            var Bbox = GetBoundingBox(points);
            if (Bbox!=null && Bbox.GetArea() > MinimumAreaSize)
                return Bbox;
            else
                return null;
        }
    }
}