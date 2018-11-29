using ML_Lib.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ML_Lib.DataType
{
    public abstract class Vector:ITag
    {
        public double[] VectorData { get; internal set; }
        public abstract int Dimension { get; }
        public int Tag { get; set; } = -1;

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
                t.Tag = this.Tag;
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
        public VectorFromArray(double[] vectorData = null, int tag = -1)
        {
            VectorData = vectorData;
            Tag = tag;
        }
    }

    public class VectorCollection<T> : List<T>, ITag where T : Vector, new()
    {
        Random random = new Random(Guid.NewGuid().GetHashCode());

        protected T Center = null;
        public int Tag { get; set; }

        public VectorCollection()
        {
            ;
        }

        public void SetCenter(Vector center)
        {
            Center = center.ConvertTo<T>();
        }

        public T GetCenter()
        {
            if(Center!=null)
                Center.Tag = this.Tag;
            return Center;
        }


        public T GetMean()
        {
            if (this.Any())
            {
                double[] Result = new double[this.First().Dimension];

                for (int i = 0; i < Result.Length; i++)
                    Result[i] = this.Select(x=>x.VectorData[i]).Average();
                
                return new VectorFromArray(Result,Tag).ConvertTo<T>();
            }
            else
                return null;
        }
        
        public T GetRandomMember()
        {
            return this.ElementAt(random.Next(0, this.Count));
        }

    }
    
    

}
