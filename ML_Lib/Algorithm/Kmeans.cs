using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ML_Lib.DataType;
using System.Threading.Tasks;

namespace ML_Lib.Algorithm
{
    public class Kmeans
    {
        public static void Calculate(Point2DCollection nodes, List<Point2DCollection> ClassCollection)
        {
            if (nodes.Any() == false || ClassCollection.Any() == false)
            {
                return;
            }

            foreach (var node in nodes)
            {
                object ShortestDistanceLock = new object();
                double ShortestDistance = ClassCollection.First().GetMidPoint().GetDistance(node);
                Point2DCollection shortest_class = ClassCollection.First();

                Parallel.ForEach(ClassCollection, item =>
                {
                    double distance = item.GetMidPoint().GetDistance(node);

                    lock (ShortestDistanceLock)
                    {
                        if (distance < ShortestDistance)
                        {
                            ShortestDistance = distance;
                            shortest_class = item;
                        }
                    }
                });

                shortest_class.Add(node);
            }
        }
    }
}
