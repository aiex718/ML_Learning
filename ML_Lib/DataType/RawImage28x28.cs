using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ML_Lib.DataType
{
    public class RawImage28x28 : Vector
    {
        [JsonIgnore]
        public const int Height = 28, Width = 28 , Length = Height * Width;

        protected override int DimensionExpected { get { return Length; } }

        public RawImage28x28():base(new float[Length], null)
        {
            ;
        }

        public RawImage28x28(Bitmap bitmap, string TagSet):base(BitmapToRawDouble(bitmap), TagSet)
        {
            
        }

        public RawImage28x28(byte[] rawData, string TagSet) : base(rawData.Select(x => (float)x).ToArray(), TagSet)
        {
            
        }

        public byte[] ToRawData()
        {
            return VectorData.Select(x => (byte)x).ToArray();
        }

        public Bitmap ToBitmap()
        {
            Bitmap b = new Bitmap(Width, Height);

            int cnt = 0;
            for (int h = 0; h < Height; h++)
            {
                for (int w = 0; w < Width; w++)
                {
                    int value = (int)VectorData[cnt++];
                    b.SetPixel(w, h, Color.FromArgb(value, value, value));
                }
            }

            return b;
        }

        static float[] BitmapToRawDouble(Bitmap bitmap)
        {
            if (bitmap.Height!= Height || bitmap.Width !=Width)
                throw new Exception("Bitmap length not equivalent");

            float[] ary = new float[Length];
            int cnt = 0;

            for (int h = 0; h < bitmap.Height; h++)
            {
                for (int w = 0; w < bitmap.Width; w++)
                {
                    var pix = bitmap.GetPixel(w, h);
                    //int GrayValue = (pix.R + pix.G + pix.B) / 3;
                    //ary[cnt++] = Convert.ToByte(GrayValue);
                    ary[cnt++] = pix.A;
                }
            }

            return ary;
        }

        public override string Print()
        {
            StringBuilder sb = new StringBuilder();

            int cnt = 0;
            var ary = this.ToRawData();
            for (int h = 0; h < Height; h++)
            {
                for (int w = 0; w < Width; w++)
                {
                    sb.Append(ary[cnt++].ToString("X2"));
                    sb.Append(' ');
                }
                sb.AppendLine();
            }

            var str = sb.ToString();
            Console.WriteLine(str);

            return str;
        }

    }
}
