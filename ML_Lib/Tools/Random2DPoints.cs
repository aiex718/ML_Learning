using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ML_Lib.DataType;

namespace ML_Lib.Tools
{
    public class Random2DPoints
    {
        public delegate void GeneratePointGroupsHandler(IEnumerable<Point2D> Nodes);
        public static event GeneratePointGroupsHandler OnGeneratePointGroups;

        public delegate void GeneratePointsHandler(IEnumerable<Point2D> Nodes);
        public static event GeneratePointsHandler OnGeneratePoints;

        public static IEnumerable<Point2D> GenerateRandomPointsGroup(int maximum, int count, int groups, float FluctuationRatio)
        {
            int Tag = 0;
            List<Point2D> Result = new List<Point2D>();
            for (int i = 0; i < groups; i++)
                Result.AddRange(GenerateRandomPoints(maximum, count / groups, FluctuationRatio, (Tag++).ToString()));

            OnGeneratePointGroups?.Invoke(Result);
            return Result;
        }

        public static IEnumerable<Point2D> GenerateRandomPoints(int maximum, int count, float FluctuationRatio=0, string TagSet = null)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            List<Point2D> Result = new List<Point2D>();

            if (FluctuationRatio > 0.5F)
                FluctuationRatio = 0.5F;
            else if (FluctuationRatio < 0.0F)
                FluctuationRatio = 0.0F;


            if (FluctuationRatio > 0)
            {
                float FluctuationRange = maximum * FluctuationRatio;
                int MidPointMinimum = (int)FluctuationRange;
                int MidPointMaximum = (int)(maximum - FluctuationRange);

                Point2D MidPoint = new Point2D(random.Next(MidPointMinimum, MidPointMaximum), random.Next(MidPointMinimum, MidPointMaximum));

                for (int i = 0; i < count;)
                {
                    Point2D p = new Point2D(random.Next(maximum), random.Next(maximum), TagSet);
                    if (MidPoint.GetEuclideanDistance(p) <= FluctuationRange)
                    {
                        Result.Add(p);
                        i++;
                    }
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    Point2D p = new Point2D(random.Next(maximum), random.Next(maximum));
                    Result.Add(p);
                }
            }

            OnGeneratePoints?.Invoke(Result);
            return Result;
        }

    }
}
