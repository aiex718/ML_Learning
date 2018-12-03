using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HandwritingDigitRecognition
{
    public partial class KmeansTrainedDataView : UserControl
    {
        public KmeansTrainedDataView()
        {
            InitializeComponent();
        }

        public void SetValue(Image img,string tag,double distance)
        {
            Image OldImage = Trained_Picturebox.Image;
            Trained_Picturebox.Image = img;
            Tag_Label.Text = tag;
            Distance_Label.Text = distance.ToString("0000.000");

            OldImage?.Dispose();
        }
    }
}
