

using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;
using System.Drawing;

namespace ML_Lib.DataType
{
    public class GroupedPoint2D : Point2D
    {
        [JsonIgnore]
        const int Length = 2;
        protected override int DimensionExpected { get { return Length; } }

        public int? GroupID=null;

        public GroupedPoint2D() : base()
        {

        }

        public GroupedPoint2D(Point2D point2D, int? groupID) : base()
        {
            x = point2D.x;
            y = point2D.y;
            GroupID = groupID;
        }

        public GroupedPoint2D(float X, float Y, int groupID, string TagSet = null):base(X,Y, TagSet)
        {
            x = X;
            y = Y;
            GroupID = groupID;
        }
        
    }
}
