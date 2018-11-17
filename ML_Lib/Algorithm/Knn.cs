using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ML_Lib.DataType;
using System.Threading.Tasks;

namespace ML_Lib.Algorithm
{
    class PointWithDistance : Point2D
    {
        public PointWithDistance(Point2D p , double distance) : base(p.x, p.y, p.Tag)
        {
            Distance = distance;
        }

        public double Distance;
    }


    public class Knn
    {
        public static IEnumerable<Point2D> Classify(int k,Point2D NewPoint,Point2DCollection PointsWithTag)
        {
            List<PointWithDistance> PointDistance = new List<PointWithDistance>();
            
            foreach (var ClassifiedPoint in PointsWithTag)
            {
                PointDistance.Add(new PointWithDistance(ClassifiedPoint, ClassifiedPoint.GetDistance(NewPoint)));
            }
            

            PointDistance.Sort((x, y) => { return x.Distance.CompareTo(y.Distance); });

            var ClosestKPoints = PointDistance.Take(k);
            int MostTag = ClosestKPoints.GroupBy(p => p.Tag)
            .OrderByDescending(group => group.Count())
            .First().Key;
            Console.WriteLine("NewPoint,x:{0},y:{1},closet points in {2}", NewPoint.x, NewPoint.y,k);

            foreach (var ClosestPoint in ClosestKPoints)
            {
                Console.WriteLine("Point,x:{0},y:{1},tag:{2},distance{3}", ClosestPoint.x, ClosestPoint.y, ClosestPoint.Tag, ClosestPoint.Distance);
            }

            Console.WriteLine("MostTag:{0}", MostTag);
            Console.WriteLine("----------------------------------");

            NewPoint.Tag = MostTag;
            PointsWithTag.Add(NewPoint);

            return ClosestKPoints;
        }

    }
}
