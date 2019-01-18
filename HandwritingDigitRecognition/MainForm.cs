using ML_Lib.DataType;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HandwritingDigitRecognition.Views;
using ML_Lib.Classifier;
using ML_Lib.Classifier.Kmeans;
using ML_Lib.Classifier.Knn;

namespace HandwritingDigitRecognition
{
    public partial class MainForm : Form
    {
        const int PenSize = 12;
        bool isMouseDown = false;

        const int DefaultKnn_K = 100;
        Knn<RawImage28x28> Knn = null;
        Kmeans<RawImage28x28> Kmeans = null;
        List<KmeansTrainedDataView> KmeansTrainedDataViews = null;

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(Knn<RawImage28x28> knn, Kmeans<RawImage28x28> kmeansTrainResult)
        {
            InitializeComponent();

            if (knn != null)
            {
                Knn = knn;
                SetupKnn();
            }

            if (kmeansTrainResult!=null)
            {
                Kmeans = kmeansTrainResult;
                SetupKmeans();
            }
        }
        private void SetupKnn()
        {
            Knn.OnClassify += Knn_OnClassify;
            Knn_KValue_Textbox.Text = DefaultKnn_K.ToString();
        }

        private void SetupKmeans()
        {
            Kmeans.OnClassify += Kmeans_OnClassify;
             
            //Initial view if null
            if (KmeansTrainedDataViews == null)
            {
                KmeansTrainedDataViews = new List<KmeansTrainedDataView>();
                for (int i = 0; i < Kmeans.GetTrainResult().Count; i++)
                {
                    var view = new KmeansTrainedDataView();
                    KmeansTrainedDataViews.Add(view);
                    KmeansTrainedData_flowPanel.Controls.Add(view);
                }
            }

            //Refresh view
            var DataViewsEnum = KmeansTrainedDataViews.GetEnumerator();
            foreach (RawImage28x28 TrainResult in Kmeans.GetTrainResult())
            {
                if (DataViewsEnum.MoveNext())
                {
                    var CurrentView = DataViewsEnum.Current;
                    CurrentView.SetValue(TrainResult.ToBitmap(), TrainResult.Tag, 0);
                }
            }
        }




        //Refresh results
        private void Kmeans_OnClassify(RawImage28x28 NewNode,IEnumerable<RawImage28x28> OrderedCenter, string MostTag)
        {
            Image OldBitmap = KmeansResultPictureBox.Image;
            KmeansResultPictureBox.Image = OrderedCenter.First().ToBitmap();
            KmeansResultNum_Label.Text = MostTag;

            var TrainResultsEnumerator = OrderedCenter.GetEnumerator();
            foreach (var KmeansTrainedDataView in KmeansTrainedDataViews)
            {
                if (TrainResultsEnumerator.MoveNext())
                {
                    var TrainResult = TrainResultsEnumerator.Current;
                    KmeansTrainedDataView.SetValue(TrainResult.ToBitmap(), TrainResult.Tag, TrainResult.GetEuclideanDistance(NewNode));
                }
            }

            OldBitmap?.Dispose();
        }

        private void Knn_OnClassify(RawImage28x28 NewNode, IEnumerable<RawImage28x28> ClosestKPoints, string MostTag, IEnumerable<RawImage28x28> ClassifiedNodes)
        {
            Image OldBitmap = KnnResultPictureBox.Image;
            KnnResultPictureBox.Image = ClosestKPoints.First().ToBitmap();
            KnnResultNum_Label.Text = MostTag;

            StringBuilder sb = new StringBuilder();
            var OrderTags = ClosestKPoints.GroupBy(x => x.Tag).OrderByDescending(group=>group.Count());
            foreach (var OrderedTag in OrderTags)
            {
                sb.Append("Tag:");
                sb.Append(OrderedTag.Key);
                sb.Append("\tCount:");
                sb.AppendLine(OrderedTag.Count().ToString());
            }

            ClosestKCount_RichTextbox.Text = sb.ToString();

            OldBitmap?.Dispose();
        }


        //Mouse Drawing
        private void InuptPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
        }

        private void InuptPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown == true)
            {
                using (Graphics g = Graphics.FromImage(InputPictureBox.Image))
                {
                    g.FillEllipse(Brushes.Black,new RectangleF(e.X- PenSize/2, e.Y- PenSize/2, PenSize, PenSize));
                }
                InputPictureBox.Invalidate();
            }
        }

        private void InuptPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }



        //Buttons
        private void Load_KemansTrainedData_btn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.CurrentDirectory;
                openFileDialog.Filter = "Trained json files (*.json)|*.json|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var FilePath = openFileDialog.FileName;
                    var KmeansTrainResult = new KmeansTrainResult<RawImage28x28>(FilePath);
                    SetupKmeans();
                }
            }
        }

        private void Clear_btn_Click(object sender, EventArgs e)
        {
            ResetInput();
        }

        private void Classify_btn_Click(object sender, EventArgs e)
        {
            using (Bitmap ResizedInput = new Bitmap(InputPictureBox.Image, 28, 28))
            {
                RawImage28x28 NewInput = new RawImage28x28(ResizedInput, null);
                NewInput.Print();

                Knn?.Classify(Convert.ToInt32(Knn_KValue_Textbox.Text), NewInput);
                Kmeans?.Classify(NewInput);
            }
        }



        private void MainForm_Load(object sender, EventArgs e)
        {
            ResetInput();
        }
        private void ResetInput()
        {
            Image OldImage = InputPictureBox.Image;

            Bitmap bmp = new Bitmap(InputPictureBox.Width, InputPictureBox.Height);
            InputPictureBox.Image = bmp;

            OldImage?.Dispose();
        }
        
    }
}
