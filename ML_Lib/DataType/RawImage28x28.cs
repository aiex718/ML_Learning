using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ML_Lib.DataType
{
    class RawImage28x28 : Vector
    {
        public byte[,] RawData;
        public int Height, Width;

        public override int Dimension { get => RawData.Length; }

        public RawImage28x28(Bitmap bitmap, int tag)
        {
            Height = bitmap.Height;
            Width = bitmap.Width;
            Tag = tag;

            RawData = BitmapToRaw(bitmap);
        }

        public RawImage28x28(int height, int width, int tag)
        {
            Height = height;
            Width = width;
            Tag = tag;

            RawData = new byte[Height, Width];
        }

        public RawImage28x28(byte[,] rawData, int height, int width, int tag)
        {
            Height = height;
            Width = width;
            Tag = tag;

            RawData = rawData;
        }

        Bitmap ToBitmap()
        {
            Bitmap b = new Bitmap(Width, Height);

            for (int h = 0; h < Height; h++)
            {
                for (int w = 0; w < Width; w++)
                {
                    int value = RawData[h, w];
                    b.SetPixel(w, h, Color.FromArgb(value, value, value));
                }
            }

            return b;
        }


        public string ToHexString()
        {
            StringBuilder sb = new StringBuilder();
            for (int h = 0; h < Height; h++)
            {
                for (int w = 0; w < Width; w++)
                {
                    sb.Append(RawData[h, w].ToString("X"));
                    sb.Append(' ');
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }


        static byte[,] BitmapToRaw(Bitmap bitmap)
        {
            byte[,] ary = new byte[bitmap.Height, bitmap.Width];

            for (int h = 0; h < bitmap.Height; h++)
            {
                for (int w = 0; w < bitmap.Width; w++)
                {
                    var pix = bitmap.GetPixel(w, h);
                    int GrayValue = (pix.R + pix.G + pix.B) / 3;

                    ary[h, w] = Convert.ToByte(GrayValue);
                }
            }

            return ary;
        }
    }
}
