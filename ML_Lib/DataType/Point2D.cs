

using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;
using System.Drawing;

namespace ML_Lib.DataType
{
    public class Point2D: Vector
    {
        [JsonIgnore]
        const int Length = 2;

        [JsonIgnore]
        public float x { get { return VectorData[0]; } set { VectorData[0] = value; } }
        [JsonIgnore]
        public float y { get { return VectorData[1]; } set { VectorData[1] = value; } }

        protected override int DimensionExpected { get { return Length; } }

        public Point2D():base(new float[Length],null)
        {

        }

        public Point2D(float X, float Y,string TagSet = null):base(new float[Length] {X,Y}, TagSet)
        {
            x = X;
            y = Y;
        }

        public virtual PointF ToPoint()
        {
            return new PointF(x,y);
        }

        public override string Print()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append('(');
            sb.Append(x);
            sb.Append(',');
            sb.Append(y);
            sb.Append(")");

            var str = sb.ToString();
            System.Console.WriteLine(str);

            return str;
        }
    }
}
