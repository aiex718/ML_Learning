using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ML_Lib.DataType;

namespace ML_Lib.Tools
{
    public class Random2DPoints
    {
        public delegate void OnGenerateHandler(Point2DCollection Nodes);
        public static event OnGenerateHandler OnGenerateRandomPointsGroup;

        public static Point2DCollection GenerateRandomPointsGroup(int maximum, int count, int groups, double FluctuationRatio)
        {
            int Tag = 0;
            Point2DCollection Result = new Point2DCollection();
            for (int i = 0; i < groups; i++)
                Result.AddRange(GenerateRandomPoints(maximum, count / groups, FluctuationRatio, Tag++));

            OnGenerateRandomPointsGroup?.Invoke(Result);
            return Result;
        }

        public static Point2DCollection GenerateRandomPoints(int maximum, int count, double FluctuationRatio=0, int Tag = -1)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            Point2DCollection Result = new Point2DCollection();

            if (FluctuationRatio > 0.5)
                FluctuationRatio = 0.5;
            else if (FluctuationRatio < 0)
                FluctuationRatio = 0;


            if (FluctuationRatio > 0)
            {
                double FluctuationRange = maximum * FluctuationRatio;
                int MidPointMinimum = (int)FluctuationRange;
                int MidPointMaximum = (int)(maximum - FluctuationRange);

                Point2D MidPoint = new Point2D(random.Next(MidPointMinimum, MidPointMaximum), random.Next(MidPointMinimum, MidPointMaximum));

                for (int i = 0; i < count;)
                {
                    Point2D p = new Point2D(random.Next(maximum), random.Next(maximum));
                    if (MidPoint.GetEuclideanDistance(p) <= FluctuationRange)
                    {
                        if (Tag >= 0)
                            p.Tag = Tag;

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


            return Result;
        }

    }
}
