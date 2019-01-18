using Accord.Imaging;
using ML_Lib.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ML_Lib.DataType
{
    public class SURFFeature : Vector
    {
        [JsonProperty]
        public SpeededUpRobustFeaturePoint Original;

        [JsonIgnore]
        protected const int Length = 64;

        [JsonIgnore]
        protected override int DimensionExpected { get { return Length; } }
        [JsonIgnore]
        public double X { get { return Original.X; } set { Original.X = value; } }
        [JsonIgnore]
        public double Y { get { return Original.Y; } set { Original.Y = value; } }
        [JsonIgnore]
        public double Scale { get { return Original.Scale; } set { Original.Scale = value; } }
        [JsonIgnore]
        public double Response { get { return Original.Response; } set { Original.Response = value; } }
        [JsonIgnore]
        public double Orientation { get { return Original.Orientation; } set { Original.Orientation = value; } }
        [JsonIgnore]
        public int Laplacian { get { return Original.Laplacian; } set { Original.Laplacian = value; } }

        [JsonIgnore]
        public float[] Descriptor { get { return VectorData; }  }

        public SURFFeature() : base(new float[Length], null)
        {
            Original = new SpeededUpRobustFeaturePoint(-1, -1, -1, -1, -1, -1,null);
        }

        public SURFFeature(SpeededUpRobustFeaturePoint AccordSURFFeaturePoint, string TagSet) : base(AccordSURFFeaturePoint.Descriptor, TagSet)
        {
            Original = AccordSURFFeaturePoint;
        }

        public Point2D GetLocation()
        {
            return new Point2D((float)X, (float)Y,Tag);
        }

    }
}
