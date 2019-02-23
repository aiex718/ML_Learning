using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accord.Imaging;
using Accord.Imaging.Filters;
using Accord.Math;
using ML_Lib.BitmapFeaturesExtractor;
using ML_Lib.Classifier;
using ML_Lib.Classifier.Knn;
using ML_Lib.DataType;
using ML_Lib.DataType.Extensions;
using ML_Lib.Tools;
using ML_Lib.Views;

namespace ML_ObjectDetection
{
    class Program
    {
        static string CurrentDirectory = Environment.CurrentDirectory;

        static List<KeyValuePair<string, string>> SamplePathTag_Pairs = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string,string>(FindImageFolder("ankylosaurus") , "ankylosaurus" ),
            new KeyValuePair<string,string>(FindImageFolder("brontosaurus") , "brontosaurus" ),
            new KeyValuePair<string,string>(FindImageFolder("stegosaurus") , "stegosaurus" ),
            new KeyValuePair<string,string>(FindImageFolder("triceratops") , "triceratops" ),
        };

        //Parameter for SURF
        static float threshold = 0.0002F;
        static int octaves = 4,initial = 1;
        static double minimumScale = 1.8F;

        [STAThread]
        static void Main(string[] args)
        {
            KnnTrainResult<SURFFeature> TrainedFeatures = new KnnTrainResult<SURFFeature>();
            SURFFeatureExtractor FeatureExtractor = new SURFFeatureExtractor(threshold, octaves, initial)
            { ExtractNegativeOnly = true, MinimumScale = minimumScale };

            //Training
            foreach (var SamplePathTag_Pair in SamplePathTag_Pairs)
            {
                string Path = SamplePathTag_Pair.Key;
                string Tag = SamplePathTag_Pair.Value;

                BitmapReader TestSampleBitmapReader = new BitmapReader(Path);
                var Bitmaps = TestSampleBitmapReader.GetBitmaps();

                Console.WriteLine("Extract features from tag:{0}, path:{1}", Tag, Path);
                
                var features = FeatureExtractor.ExtractFeatures(Bitmaps, Tag);
                Console.WriteLine("Features extract ,Count:{0}", features.Count());

                var knnTrainer = new KnnTrainer<SURFFeature>(features);
                var knnTrainResult = knnTrainer.Train();
                TrainedFeatures.AddRange(knnTrainResult);

                foreach (var b in Bitmaps)
                    b.Dispose();

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            Application.EnableVisualStyles();
            Application.Run(new MainForm(FeatureExtractor, TrainedFeatures));
        }


        static string FindImageFolder(string FolderName)
        {
            string FindPath = CurrentDirectory + @"\Images\TrainingSample\"+ FolderName;
            if (Directory.Exists(FindPath))
                return FindPath;
            else
                return CurrentDirectory + @"\..\..\..\Images\TrainingSample\"+FolderName;
        }

    }
}
