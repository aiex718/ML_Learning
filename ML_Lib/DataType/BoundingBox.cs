using ML_Lib.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML_Lib.DataType
{
    public class BoundingBox : ITag
    {
        public float MinimumX, MinimumY;
        public float MaximumX, MaximumY;

        public string Tag { get ; set ; }

        public BoundingBox(float MinX, float MinY, float MaxX, float MaxY)
        {
            MinimumX = MinX;
            MinimumY = MinY;
            MaximumX = MaxX;
            MaximumY = MaxY;
        }

        public RectangleF ToRectangleF()
        {
            return new RectangleF(MinimumX, MinimumY, MaximumX - MinimumX, MaximumY - MinimumY);
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle((int)MinimumX, (int)MinimumY, (int)MaximumX - (int)MinimumX, (int)MaximumY - (int)MinimumY);
        }

        public double GetArea()
        {
            var rect = ToRectangleF();
            return rect.Height * rect.Width;
        }
    }
}
