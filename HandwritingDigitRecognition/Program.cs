using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ML_Lib.Tools;
using ML_Lib.Algorithm.Knn;
using ML_Lib.Algorithm.Kmeans;
using ML_Lib.DataType;
using System.Windows.Forms;

namespace HandwritingDigitRecognition
{
    class Program
    {
        static string MnistPixelFilePath = Environment.CurrentDirectory + "\\..\\train-images.idx3-ubyte";
        static string MnistLabelFilePath = Environment.CurrentDirectory + "\\..\\train-labels.idx1-ubyte";
        static string KmeansTrainResultFilePath = Environment.CurrentDirectory + "\\..\\KmeansTrainResult.json";
        static VectorCollection<RawImage28x28> Dataset = null;
        static Knn<RawImage28x28> knn = null;
        static Kmeans<RawImage28x28>.TrainResult KmeansTrainResult = null;

        [STAThread]
        static void Main(string[] args)
        {
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
                knn = new Knn<RawImage28x28>(Dataset);
            }

            if (File.Exists(KmeansTrainResultFilePath))
            {
                KmeansTrainResult = new Kmeans<RawImage28x28>.TrainResult(KmeansTrainResultFilePath);
            }



            if (KmeansTrainResult==null && knn==null)
            {
                Console.WriteLine("Fatal error: Can not find any trained data.");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                return;
            }

            if (Dataset != null)
            {
                if (KmeansTrainResult == null)
                {
                    Console.WriteLine("Can't find Kmeans train data.");
                    AskKmeansTrainData();
                }
                else if (KmeansTrainResult != null)
                {
                    Console.WriteLine("Found Kmeans train data.");
                    Console.WriteLine("[U]se trained data or [t]rain again? [u/t]");
                    if (Console.ReadLine().ToLower().Contains('t'))
                    {
                        KmeansTrainResult = null;
                        AskKmeansTrainData();
                    }
                }
            }

            if (knn == null)
            {
                Console.WriteLine("Can't find Knn train data, ignored");
                Console.WriteLine("Press any key to confirm.");
                Console.ReadKey();
            }

            MainForm form = new MainForm(knn, KmeansTrainResult);
            Application.EnableVisualStyles();
            Application.Run(form);
        }

        private static void AskKmeansTrainData()
        {
            while (true)
            {
                Console.WriteLine("Input 't' to training, 'i' to ignore.");
                string input = Console.ReadLine();
                if (input.ToLower().Contains('t'))
                {
                    Kmeans<RawImage28x28> kmeans = new Kmeans<RawImage28x28>(Dataset);
                    Console.WriteLine("Input ConvDistance:(Default:3)");
                    double ConvDistance = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Input iteration limit:(Default:200)");
                    int IterationLimit = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Training......");
                    //List<VectorCollection<RawImage28x28>> PresetCenter = new List<VectorCollection<RawImage28x28>>();
                    //var GroupedTagDataset = Dataset.GroupBy(x => x.OriginalTag);
                    //foreach (var GroupedTagData in GroupedTagDataset)
                    //{
                    //    VectorCollection<RawImage28x28> vecotrCollect = new VectorCollection<RawImage28x28>(GroupedTagData);

                    //    VectorCollection<RawImage28x28> GroupSetCenter = new VectorCollection<RawImage28x28>();
                    //    GroupSetCenter.SetCenter(vecotrCollect.GetMean());
                    //    GroupSetCenter.ClassifiedTag = GroupedTagData.Key;
                    //    GroupSetCenter.OriginalTag = GroupedTagData.Key;

                    //    PresetCenter.Add(GroupSetCenter);
                    //}
                    //KmeansTrainResult = kmeans.Training(PresetCenter, IterationLimit, ConvDistance);

                    KmeansTrainResult = kmeans.Training(10, IterationLimit, ConvDistance);

                    Console.WriteLine("Save trained result?[y/n]");
                    if (Console.ReadLine().ToLower().Contains('y'))
                    {
                        KmeansTrainResult.SaveTo(KmeansTrainResultFilePath);
                        Console.WriteLine("Saved at {0}", KmeansTrainResultFilePath);
                    }

                    break;
                }
                else if (input.ToLower().Contains("i"))
                {
                    break;
                }
            }
            
        }
    }
}
