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
        public const int Height = 28, Width = 28;

        public override int Dimension { get { return Height*Width; } }

        public RawImage28x28():base()
        {
            ;
        }

        public RawImage28x28(Bitmap bitmap, string originalTag = null)
        {
            OriginalTag = originalTag;
            VectorData = BitmapToRawDouble(bitmap);
        }

        public RawImage28x28(byte[] rawData, string originalTag = null)
        {
            if (rawData.Length != Dimension)
                throw new Exception("RawData length not equivalent");

            OriginalTag = originalTag;
            VectorData = rawData.Select(x=>(double)x).ToArray();
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

        double[] BitmapToRawDouble(Bitmap bitmap)
        {
            if (bitmap.Height!= Height || bitmap.Width !=Width)
                throw new Exception("Bitmap length not equivalent");

            double[] ary = new double[Dimension];
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
