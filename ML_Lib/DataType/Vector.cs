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
        public abstract int Dimension { get; }
        public abstract string Print();
        
        public double[] VectorData { get; internal set; } = null;
        public string OriginalTag { get; set; } = null;
        public string ClassifiedTag { get; set; } = null;

        public Vector()
        {
            VectorData = new double[Dimension];
        }

        public T ConvertTo<T>() where T : Vector, new()
        {
            var t = new T();

            if (t.Dimension == this.Dimension)
            {
                t.VectorData = this.VectorData;
                t.OriginalTag = this.OriginalTag;
                t.ClassifiedTag = this.ClassifiedTag;
                return t;
            }
            else
                throw new Exception("Vector dimension not equivalent");
        }
        public double GetEuclideanDistance(Vector other)
        {
            return GetDistance(other, 2);
        }
        public double GetDistance(Vector other,int Norm)
        {
            if (this.Dimension != other.Dimension)
                throw new Exception("Vector dimension not equivalent");
            else
            {
                double sum = 0.0;

                for (int i = 0; i < VectorData.Length; i++)
                    sum += Math.Pow(VectorData[i] - other.VectorData[i], Norm);

                return Math.Pow(sum, 1.0/ Norm);
            }
        }
    }

    public class VectorFromArray : Vector
    {
        public override int Dimension { get { return VectorData == null ? 0 : VectorData.Length; } }
        public VectorFromArray(double[] vectorData = null, string originalTag = null, string classifiedTag = null)
        {
            VectorData = vectorData;
            OriginalTag = originalTag;
            ClassifiedTag = classifiedTag;
        }

        public override string Print()
        {
            StringBuilder sb = new StringBuilder();

            foreach (double d in this.VectorData)
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
        Random random = new Random(Guid.NewGuid().GetHashCode());
        
        T Center { get; set; } = null;
        public string OriginalTag { get; set; } = null;
        public string ClassifiedTag { get; set; } = null;

        public VectorCollection()
        {
            ;
        }

        public VectorCollection(string json)
        {
            JToken jToken = JToken.Parse(json);
            string originalTag = jToken.Value<string>("OriginalTag");
            string classifiedTag = jToken.Value<string>("ClassifiedTag");
            double[] vectorData = jToken.Value<JArray>("VectorData").ToObject<double[]>();

            this.ClassifiedTag = classifiedTag;
            this.OriginalTag = originalTag;

            Center = new T();
            Center.ClassifiedTag = classifiedTag;
            Center.OriginalTag = originalTag;
            Center.VectorData = vectorData;
        }

        public VectorCollection(IEnumerable<T> items)
        {
            this.AddRange(items);
        }

        public void SetCenter(Vector center)
        {
            Center = center.ConvertTo<T>();
        }

        public T GetCenter()
        {
            if(Center!=null)
            {
                Center.OriginalTag = this.OriginalTag;
                Center.ClassifiedTag = this.ClassifiedTag;
            }
                
            return Center;
        }

        public T GetMean()
        {
            if (this.Any())
            {
                double[] Result = new double[this.First().Dimension];

                for (int i = 0; i < Result.Length; i++)
                    Result[i] = this.Select(x=>x.VectorData[i]).Average();
                
                return new VectorFromArray(Result,OriginalTag,ClassifiedTag).ConvertTo<T>();
            }
            else
                return null;
        }
        
        public T GetRandomMember()
        {
            return this.ElementAt(random.Next(0, this.Count));
        }

        public void AssignTagFromMostOriginalTag()
        {
            string MostTag = this
                .GroupBy(x => x.OriginalTag)
                .OrderByDescending(group => group.Count())
                .First().Key;

            this.ClassifiedTag = MostTag;
        }

        public void Print()
        {
            foreach (var vector in this)
                vector.Print();
        }

        public string ConvertToJson()
        {
            var VectorData = GetCenter().VectorData;
            var json = JsonConvert.SerializeObject(new { OriginalTag, ClassifiedTag, VectorData });
            return json;
        }


    }
    
    

}
