using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML_Lib.DataType.Extensions
{
    public static class BitmapExtensions
    {

        const int FontSize=10;
        public static void DrawRect(this Bitmap bitmap, Rectangle rect, Brush brush, int Width, string Msg=null)
        {
            if (rect != null)
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                using (Pen pen = new Pen(brush, Width))
                using (Font font = new Font("Arial", FontSize))
                {
                    g.DrawRectangle(pen, rect);
                    if (String.IsNullOrEmpty(Msg) == false)
                        g.DrawString(Msg, font, brush, rect.Location);
                }
            }
        }
        

        public static void DrawRect(this Bitmap bitmap, IEnumerable<Rectangle> rects, Brush brush, int Width, string Msg = null)
        {
            if (rects != null && rects.Any())
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                using (Pen pen = new Pen(brush, Width))
                using (Font font = new Font("Arial", FontSize))
                {
                    g.DrawRectangles(pen, rects.ToArray());
                    if (String.IsNullOrEmpty(Msg) == false)
                    {
                        foreach (var rect in rects)
                        {
                            g.DrawString(Msg, font, brush, rect.Location);
                        }
                    }
                }
            }
        }
    }
}
