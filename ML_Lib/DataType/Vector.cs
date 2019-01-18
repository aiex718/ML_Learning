using ML_Lib.Interface;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ML_Lib.DataType
{
    public abstract class Vector:ITag
    {
        [JsonIgnore]
        protected abstract int DimensionExpected { get; }

        [JsonIgnore]
        public int Dimension { get { return VectorData.Length; } }

        [JsonProperty]
        public float[] VectorData { get; internal set; } = null;
        [JsonProperty]
        public string Tag { get; set; } = null;

        public Vector(float[] vectorData, string TagSet)
        {
            VectorData = vectorData ?? throw new Exception("VectorData is null");
            if (Dimension != DimensionExpected)
                throw new Exception("VectorData length not valid.");

            Tag = TagSet;
        }

        public Vector(double[] vectorData, string TagSet)
        {
            if (vectorData==null)
                throw new Exception("VectorData is null");
            //if (Dimension != DimensionExpected)
            //    throw new Exception("VectorData length not valid.");
            VectorData = Array.ConvertAll(vectorData, x => (float)x);
            Tag = TagSet;
        }
        
        public float GetEuclideanDistance(Vector other)
        {
            return GetDistance(other, 2);
        }

        public float GetDistance(Vector other,int Norm)
        {
            if (this.Dimension != other.Dimension)
                throw new Exception("Vector dimension not equivalent");
            else
            {
                double sum = 0;
                
                for (int i = 0; i < VectorData.Length; i++)
                {
                    sum += Math.Pow(VectorData[i] - other.VectorData[i], Norm);
                }

                return (float)Math.Pow(sum, 1.0/ Norm);
            }
        }

        public virtual string Print()
        {
            StringBuilder sb = new StringBuilder();

            foreach (float d in this.VectorData)
            {
                sb.Append(d.ToString("0.000"));
                sb.Append(' ');
            }

            var str = sb.ToString();
            Console.WriteLine(str);

            return str;
        }
    }

    public class VectorCollection<T> : List<T>, ITag where T : Vector, new()
    {
        public T Center
        {
            get { return new T { Tag=this.Tag, VectorData = CenterValue }; }
            set { CenterValue = value.VectorData;}
        }
        float[] CenterValue;

        public string Tag { get; set; } = null;

        public VectorCollection()
        {
            ;
        }

        public VectorCollection(IEnumerable<T> items)
        {
            this.AddRange(items);
        }

        public T GetMean()
        {
            if (this.Any())
            {
                float[] Mean = new float[this.First().Dimension];

                for (int i = 0; i < Mean.Length; i++)
                    Mean[i] = this.Select(x=>x.VectorData[i]).Average();
                
                T Result = new T
                {
                    Tag = Tag,
                    VectorData = Mean
                };

                return Result;
            }
            else
                return null;
        }

        public string GetMostTag()
        {
            string MostTag = this
                .GroupBy(x => x.Tag)
                .OrderByDescending(group => group.Count())
                .First().Key;

            return MostTag;
        }

        public void Print()
        {
            foreach (var vector in this)
                vector.Print();
        }


    }
    
    

}
