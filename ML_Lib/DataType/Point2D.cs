

using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ML_Lib.DataType
{
    public class Point2D
    {
        public int Tag;
        public double x = 0, y = 0;
        public int id = 0;

        public Point2D(double X, double Y,int tag=-1)
        {
            x = X;
            y = Y;

            if (tag>0)
                Tag = tag;
        }
        
        public double GetDistance(Point2D p)
        {
            return Math.Sqrt(Math.Pow((x - p.x), 2) + Math.Pow((y - p.y), 2));
        }

    }
    public class Point2DCollection : List<Point2D>
    {
        Random random = new Random(Guid.NewGuid().GetHashCode());
        readonly Point2D MidPoint;
        public int Tag { get; private set; }

        public Point2DCollection(int tag = -1):base()
        {
            if (tag>0)
                Tag = tag;
        }

        public Point2DCollection(Point2D mid, int tag = -1) : base()
        {
            MidPoint = mid;
            if (tag > 0)
                Tag = tag;
        }

        public Point2D GetMidPoint()
        {
            return MidPoint;
        }

        public Point2D CalcNewMidPoint()
        {
            double meanX = this.Select(Point => (double)(Point.x)).Average();
            double meanY = this.Select(Point => (double)(Point.y)).Average();
            return new Point2D((int)meanX, (int)meanY);
        }

        public Point2D GetRandomPoint()
        {
            return this.ElementAt(random.Next(0,this.Count));
        }

        public void Print()
        {
            foreach (var p in this)
            {
                System.Console.WriteLine("({0},{1})", p.x, p.y);
            }
        }

    }

    


}
