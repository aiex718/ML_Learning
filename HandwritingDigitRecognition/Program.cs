using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ML_Lib.Tools;
using ML_Lib.DataType;
using System.Windows.Forms;
using ML_Lib.Classifier;
using ML_Lib.Classifier.Kmeans;
using ML_Lib.Classifier.Knn;

namespace HandwritingDigitRecognition
{
    class Program
    {
        static string MnistPixelFilePath = Environment.CurrentDirectory + "\\..\\train-images.idx3-ubyte";
        static string MnistLabelFilePath = Environment.CurrentDirectory + "\\..\\train-labels.idx1-ubyte";
        static string KmeansTrainResultFilePath = Environment.CurrentDirectory + "\\..\\KmeansTrainResult.json";

        [STAThread]
        static void Main(string[] args)
        {
            VectorCollection<RawImage28x28> Dataset = null;
            Knn<RawImage28x28> knn = null;
            KmeansTrainResult<RawImage28x28> KmeansTrainResult = null;
            Kmeans<RawImage28x28> kmeans = null;

            //CheckFiles
            if (File.Exists(MnistPixelFilePath)==false)
                MnistPixelFilePath = Environment.CurrentDirectory + "\\train-images.idx3-ubyte";

            if (File.Exists(MnistLabelFilePath) == false)
                MnistLabelFilePath = Environment.CurrentDirectory + "\\train-labels.idx1-ubyte"; 

            if (File.Exists(KmeansTrainResultFilePath) == false)
                KmeansTrainResultFilePath = Environment.CurrentDirectory + "\\KmeansTrainResult.json";

            
            //LoadFiles
            if (File.Exists(MnistPixelFilePath) && File.Exists(MnistLabelFilePath))
            {
                var MnistDataSet = MnistDataSetLoader.LoadData(MnistPixelFilePath, MnistLabelFilePath);
                Dataset = new VectorCollection<RawImage28x28>(MnistDataSet);
            }

            if (File.Exists(KmeansTrainResultFilePath))
            {
                KmeansTrainResult = new KmeansTrainResult<RawImage28x28>(KmeansTrainResultFilePath);
            }

            if (Dataset != null)
            {
                var knnTrainer = new KnnTrainer<RawImage28x28>(Dataset);
                knn = new Knn<RawImage28x28>(knnTrainer.Train());

                if (KmeansTrainResult == null)
                {
                    Console.WriteLine("Can't find Kmeans train data.");
                    KmeansTrainResult = AskKmeansTrainData(false);
                }
                else if (KmeansTrainResult != null)
                {
                    Console.WriteLine("Found Kmeans train data.");
                    Console.WriteLine("[U]se trained data or [t]rain again? [u/t]");
                    if (Console.ReadLine().ToLower().Contains('t'))
                    {
                        KmeansTrainResult = null;
                        KmeansTrainResult = AskKmeansTrainData(true);
                    }
                }
            }
            else
            {
                Console.WriteLine("Can't find Knn train data, ignored");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            }

            if (KmeansTrainResult!=null)
            {
                kmeans = new Kmeans<RawImage28x28>(KmeansTrainResult);
            }

            if (kmeans == null && knn == null)
            {
                Console.WriteLine("Fatal error: Can not find any trained data.");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                return;
            }

            MainForm form = new MainForm(knn, kmeans);
            Application.EnableVisualStyles();
            Application.Run(form);
        }

        private static KmeansTrainResult<RawImage28x28> AskKmeansTrainData(bool SkipConfirm)
        {
            while (true)
            {
                string input;

                if (SkipConfirm)
                    input = "t";
                else
                {
                    Console.WriteLine("Input 't' to start training, 'i' to ignore.");
                    input = Console.ReadLine();
                }

                if (input.ToLower().Contains('t'))
                {
                    Console.WriteLine("Input ConvDistance:(Default:3)");
                    float ConvDistance = Convert.ToSingle(Console.ReadLine());
                    Console.WriteLine("Input iteration limit:(Default:200)");
                    int IterationLimit = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Training......");
                    var MnistDataSet = MnistDataSetLoader.LoadData(MnistPixelFilePath, MnistLabelFilePath);
                    KmeansTrainer<RawImage28x28> kmeansTrainer = new KmeansTrainer<RawImage28x28>(MnistDataSet, 10, ConvDistance, IterationLimit);
                    var TrainResult = kmeansTrainer.Train();

                    Console.WriteLine("Save trained result?[y/n]");
                    if (Console.ReadLine().ToLower().Contains('y'))
                    {
                        TrainResult.SaveToFile(KmeansTrainResultFilePath);
                        Console.WriteLine("Saved at {0}", KmeansTrainResultFilePath);
                    }

                    return TrainResult;
                }
                else if (input.ToLower().Contains("i"))
                {
                    return null;
                }
            }
            
        }
    }
}
