using ML_Lib.Algorithm.Kmeans;
using ML_Lib.Algorithm.Knn;
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

namespace HandwritingDigitRecognition
{
    public partial class MainForm : Form
    {
        const int PenSize = 12;
        bool isMouseDown = false;
        Point lastPoint;
        List<KmeansTrainedDataView> KmeansTrainedDataViews=null;

        const int DefaultKnn_K=1000;
        Knn<RawImage28x28> Knn=null;
        Kmeans<RawImage28x28>.TrainResult KmeansTrainResult = null;

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(Knn<RawImage28x28> knn=null, Kmeans<RawImage28x28>.TrainResult kmeansTrainResult=null)
        {
            InitializeComponent();

            if (knn != null)
            {
                Knn = knn;
                Knn.OnClassify += Knn_OnClassify;
                Knn_KValue_Textbox.Text = DefaultKnn_K.ToString();
            }

            if (kmeansTrainResult!=null)
            {
                KmeansTrainResult = kmeansTrainResult;
                KmeansTrainResult.OnClassify += KmeansTrainResult_OnClassify;
                KmeansTrainedDataViews = new List<KmeansTrainedDataView>();

                foreach (var TrainResult in kmeansTrainResult)
                {
                    var view = new KmeansTrainedDataView();
                    view.SetValue(TrainResult.GetCenter().ToBitmap(), TrainResult.ClassifiedTag,0);
                    KmeansTrainedDataViews.Add(view);
                    KmeansTrainedData_flowPanel.Controls.Add(view);
                }

            }
        }


        //Refresh results
        private void KmeansTrainResult_OnClassify(RawImage28x28 NewNode,IEnumerable<VectorCollection<RawImage28x28>> OrderedGroup, string MostTag)
        {
            Image OldBitmap = KmeansResultPictureBox.Image;
            KmeansResultPictureBox.Image = OrderedGroup.First().GetCenter().ToBitmap();
            KmeansResultNum_Label.Text = MostTag;

            var Group = OrderedGroup.GetEnumerator();
            foreach (var KmeansTrainedDataView in KmeansTrainedDataViews)
            {
                if (Group.MoveNext())
                {
                    var TrainResult = Group.Current;
                    KmeansTrainedDataView.SetValue(TrainResult.GetCenter().ToBitmap(), TrainResult.ClassifiedTag, TrainResult.GetCenter().GetEuclideanDistance(NewNode));
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
            var OrderedTags = ClosestKPoints.GroupBy(x => x.OriginalTag).OrderByDescending(group=>group.Count());
            foreach (var OrderedTag in OrderedTags)
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
                lastPoint = e.Location;
            }
        }

        private void InuptPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }



        //Buttons
        private void Load_KemansTrainedData_btn_Click(object sender, EventArgs e)
        {
            string FilePath = null;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.CurrentDirectory;
                openFileDialog.Filter = "Trained json files (*.json)|*.json|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    FilePath = openFileDialog.FileName;
                }
            }

            if (FilePath != null)
            {
                if (KmeansTrainResult == null)
                {
                    KmeansTrainResult = new Kmeans<RawImage28x28>.TrainResult(FilePath);
                    KmeansTrainResult.OnClassify += KmeansTrainResult_OnClassify;
                    KmeansTrainedDataViews = new List<KmeansTrainedDataView>();

                    foreach (var TrainResult in KmeansTrainResult)
                    {
                        var view = new KmeansTrainedDataView();
                        view.SetValue(TrainResult.GetCenter().ToBitmap(), TrainResult.ClassifiedTag, 0);
                        KmeansTrainedDataViews.Add(view);
                        KmeansTrainedData_flowPanel.Controls.Add(view);
                    }
                }
                else
                {
                    KmeansTrainResult = new Kmeans<RawImage28x28>.TrainResult(FilePath);
                    KmeansTrainResult.OnClassify += KmeansTrainResult_OnClassify;

                    var DataViewsEnum = KmeansTrainedDataViews.GetEnumerator();
                    foreach (var TrainResult in KmeansTrainResult)
                    {
                        DataViewsEnum.MoveNext();
                        var view = DataViewsEnum.Current;
                        view.SetValue(TrainResult.GetCenter().ToBitmap(), TrainResult.ClassifiedTag, 0);
                    }
                }
            }

        }

        private void Clear_btn_Click(object sender, EventArgs e)
        {
            ResetInput();
        }

        private void Classify_btn_Click(object sender, EventArgs e)
        {
            Bitmap ResizedInput = new Bitmap (InputPictureBox.Image,28,28);
            RawImage28x28 NewInput = new RawImage28x28(ResizedInput);
            NewInput.Print();

            Knn?.Classify(Convert.ToInt32(Knn_KValue_Textbox.Text), NewInput);
            KmeansTrainResult?.Classify(NewInput);

            ResizedInput.Dispose();
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
