using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ML_Lib.Views
{
    public partial class ImageViewForm : Form
    {
        public ImageViewForm()
        {
            InitializeComponent();
        }

        public ImageViewForm(Bitmap b, string Title = null)
        {
            InitializeComponent();
            pictureBox.Image = (Bitmap)b.Clone();
            if (Title != null)
                this.Text = Title;
        }
        

        private void ImageViewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            pictureBox.Image?.Dispose();
        }
    }
}
