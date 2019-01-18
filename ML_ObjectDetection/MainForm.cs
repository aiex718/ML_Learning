using Accord.Imaging.Filters;
using ML_Lib.BitmapFeaturesExtractor;
using ML_Lib.Classifier.Knn;
using ML_Lib.DataType;
using ML_Lib.DataType.Extensions;
using ML_Lib.Tools;
using ML_Lib.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accord.Video.DirectShow;
using System.IO;
using System.Reflection;

namespace ML_ObjectDetection
{
    public partial class MainForm : Form
    {
        //Webcam
        VideoCaptureDevice videoSource;

        //Parameter for knn
        static int k = 25;//20
        static float ConfidenceThreshold = 0.7F;//0.7

        //Parameter for point group
        static int MinimumPointGroupCount = 60;//60
        static float MaximumPointGroupDistance = 70;//70
        static double MinimumPointGroupArea = 900;//0.00115;

        SURFFeatureExtractor FeatureExtractor;
        KnnTrainResult<SURFFeature> TrainResult;
        Knn<SURFFeature> knn;

        Dictionary<string, Brush> TagToBrush;
        List<Brush> AllBrush;
        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(SURFFeatureExtractor featureExtractor, KnnTrainResult<SURFFeature> knnTrainResult)
        {
            InitializeComponent();
            FeatureExtractor = featureExtractor;
            TrainResult = knnTrainResult;
            knn = new Knn<SURFFeature>(TrainResult);
            TagToBrush = new Dictionary<string, Brush>();
            AllBrush = new List<Brush> { Brushes.Aqua, Brushes.Red, Brushes.Green, Brushes.Yellow, Brushes.Gray };
        }



        private void Conn_btn_Click(object sender, EventArgs e)
        {
            if (videoSource!=null)
            {
                Connstr_ComboBox.Enabled = true;
                videoSource.SignalToStop();
                videoSource.NewFrame -= VideoSource_NewFrame;
                videoSource = null;
            }
            else
            {
                Connstr_ComboBox.Enabled = false;
                videoSource = new VideoCaptureDevice(Connstr_ComboBox.Text);
                videoSource.NewFrame += VideoSource_NewFrame;
                videoSource.Start();
            }
        }

        private void VideoSource_NewFrame(object sender, Accord.Video.NewFrameEventArgs eventArgs)
        {
            Image OldBitmap = Webcam_Picturebox.Image;

            //lock (Webcam_Picturebox)
            Webcam_Picturebox.Image = (Bitmap)eventArgs.Frame.Clone();

            eventArgs.Frame.Dispose();
            OldBitmap?.Dispose();
        }

        private void Snap_btn_Click(object sender, EventArgs e)
        {
            Image OldBitmap = SamplePicturebox.Image;
            //lock (Webcam_Picturebox)
            using( Image img = (Image)Webcam_Picturebox.Image.Clone())
                SamplePicturebox.Image = new Bitmap(img, new Size(1024,768));
            
            OldBitmap?.Dispose();
        }

        private void LoadPath_btn_Click(object sender, EventArgs e)
        {
            string path = FolderPath_TextBox.Text;
            if (String.IsNullOrEmpty(path) ==false && Directory.Exists(path))
            {
                string ResultPath = path + "\\Result\\";
                Directory.CreateDirectory(ResultPath);
                BitmapReader bitmapReader = new BitmapReader(path);
                var bitmaps = bitmapReader.GetBitmaps();
                foreach (var b in bitmaps)
                {
                    string Filename = b.Tag as string;
                    var RecognizedBitmaps = DoRecognize(b);
                    foreach (var RecognizedBitmap in RecognizedBitmaps)
                    {
                        RecognizedBitmap.Save(ResultPath + Filename + (RecognizedBitmap.Tag as string) + ".png", System.Drawing.Imaging.ImageFormat.Png);

                        RecognizedBitmap.Dispose();
                    }
                }
            }
        }

        private void LoadImg_btn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                //openFileDialog.InitialDirectory = Environment.CurrentDirectory;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Image OldBitmap = SamplePicturebox.Image;
                    string filePath = openFileDialog.FileName;
                    Bitmap b = new Bitmap(filePath);
                    SamplePicturebox.Image = b;
                    OldBitmap?.Dispose();
                }
            }
        }

        private void Recognize_btn_Click(object sender, EventArgs e)
        {
            if (SamplePicturebox.Image != null)
            {
                using (Bitmap b = (Bitmap)SamplePicturebox.Image.Clone())
                {
                    var RecognizedBitmaps = DoRecognize(b);
                    foreach (var RecognizedBitmap in RecognizedBitmaps)
                    {
                        ImageViewForm imageViewForm = new ImageViewForm(RecognizedBitmap, RecognizedBitmap.Tag as string);
                        imageViewForm.Show();
                        RecognizedBitmap.Dispose();
                    }
                }
            }
        }


        private IEnumerable<Bitmap> DoRecognize(Bitmap b)
        {
            List<Bitmap> Result = new List<Bitmap>();

            var NotClassifiedFeatures = FeatureExtractor.ExtractFeatures(b, null);
            List<SURFFeature> FeatureRemain = new List<SURFFeature>();

            Parallel.ForEach(NotClassifiedFeatures, (NotClassifiedFeature) =>
            {
                var KnnResult = knn.Classify(k, NotClassifiedFeature);

                if (KnnResult.Confidence >= ConfidenceThreshold)
                {
                    lock (FeatureRemain)
                        FeatureRemain.Add(NotClassifiedFeature);
                }
            });


            var GroupedFeatureRemains = FeatureRemain.GroupBy(x => x.Tag);
            Bitmap FinalResult = (Bitmap)b.Clone();
            //Draw Features
            FeaturesMarker featuresMarker = new FeaturesMarker(FeatureRemain.Select(x => x.Original));
            Bitmap MarkedBitmap = featuresMarker.Apply(b);

            //Draw bounding box
            foreach (var GroupedFeatureRemain in GroupedFeatureRemains)
            {
                string Tag = GroupedFeatureRemain.Key;

                Point2DGrouper point2DGrouper = new Point2DGrouper(MaximumPointGroupDistance) { MinimumGroupCount = MinimumPointGroupCount };
                var BoundingBoxes = point2DGrouper
                    .GetGrouped2DPoints(GroupedFeatureRemain.Select(f => f.GetLocation()))
                    .GroupBy(p => p.GroupID)
                    .Select(group => group.GetBoundingBox(MinimumPointGroupArea))
                    .Where(bbox => bbox != null);
                var ObjectBox = BoundingBoxes.GetBoundingBox();

                Brush brush = SelectBrush(Tag);
                if (BoundingBoxes != null)
                    MarkedBitmap.DrawRect(BoundingBoxes.Select(boxes => boxes.ToRectangle()), brush, 3, "Bbox:" + Tag);
                if (ObjectBox != null)
                {
                    FinalResult.DrawRect(ObjectBox.ToRectangle(), brush, 3, "Object:" + Tag);
                }
            }

            FinalResult.Tag = "Final Result";
            MarkedBitmap.Tag = "Class Bounding Boxes";
            Result.Add(FinalResult);
            Result.Add(MarkedBitmap);

            return Result;
        }
        
        private Brush SelectBrush(string tag)
        {
            Brush brushGet;
            if (TagToBrush.TryGetValue(tag,out brushGet)==false)
            {
                brushGet = AllBrush.First();
                TagToBrush.Add(tag, brushGet);
                AllBrush.Remove(AllBrush.First());
            }
            return brushGet;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            Connstr_ComboBox.Items.AddRange(videoDevices.Select(x=>x.MonikerString).ToArray());
        }
    }
}
