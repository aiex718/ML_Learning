

using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;

namespace ML_Lib.DataType
{
    public class Point2D: Vector
    {
        [JsonIgnore]
        public override int Dimension { get { return 2; } }

        [JsonIgnore]
        public double x { get { return VectorData[0]; } set { VectorData[0] = value; } }
        [JsonIgnore]
        public double y { get { return VectorData[1]; } set { VectorData[1] = value; } }
        
        public Point2D():base()
        {

        }

        public Point2D(double X, double Y,string originalTag=null):base()
        {
            x = X;
            y = Y;
            OriginalTag = originalTag;
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
