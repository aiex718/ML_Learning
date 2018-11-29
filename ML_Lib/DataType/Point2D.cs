

using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ML_Lib.DataType
{
    public class Point2D: Vector
    {
        public override int Dimension { get => 2; }

        public double x { get { return VectorData[0]; } set { VectorData[0] = value; } }
        public double y { get { return VectorData[1]; } set { VectorData[1] = value; } }
        
        public Point2D():base()
        {

        }

        public Point2D(double X, double Y,int tag=-1):base()
        {
            x = X;
            y = Y;

            if (tag>0)
                Tag = tag;
        }

        public void Print()
        {
            System.Console.WriteLine("({0},{1})", x, y);
        }
    }
    public class Point2DCollection : VectorCollection<Point2D>
    {
        public Point2DCollection(VectorCollection<Point2D> Parent)
        {
            this.Tag = Parent.Tag;
            this.AddRange(Parent);
        }
        public Point2DCollection(int tag = -1)
        {
            if (tag>0)
                Tag = tag;
        }

        public Point2DCollection(Point2D mid, int tag = -1)
        {
            if (tag > 0)
                Tag = tag;
        }

        public void Print()
        {
            foreach (var p in this)
                p.Print();
        }

    }

    


}
