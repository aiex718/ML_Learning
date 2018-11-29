using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ML_Lib.Views;
using ML_Lib.DataType;
using ML_Lib.Algorithm;
using ML_Lib.Tools;

namespace KmeanPractice
{
    class Program
    {
        
        //Parameters for generate random points
        static int NodesMaxValueSet = 10000, NodesSet = 10000, DataGroupCount = 3;
        static double FluctuationRatio = 0.18;

        //Parameters for kmeans
        static int k = 3, IterationLimit = 100, ConvDistance = 5;

        //Parameters for retry
        static int Round = 0;
        static bool Retry = true;

        static void Main(string[] args)
        {
            Console.WriteLine("Using demo setup?[y/n]");
            string str = Console.ReadLine();
            if (str.ToLower().Contains("y")==false)
            {
                Console.WriteLine("Input node maximum value:(Default:10000 , Min:0)");
                NodesMaxValueSet = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Input node count:(Default:10000 , Min:0)");
                NodesSet = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Input k:( Default:3 , Min:1)");
                k = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Input iteration limit:(Default:100)");
                IterationLimit = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Input convergence distance:(Default:5)");
                ConvDistance = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Input groups count for random points to divid into:(Default:3 , Min:0)");
                DataGroupCount = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Input fluctuation ratio:(Default:0.18 , Min:0 , Max:0.5)");
                FluctuationRatio = Convert.ToDouble(Console.ReadLine());
            }

            //Gen grouped random nodes
            Random2DPoints.OnGenerateRandomPointsGroup += Random2DPoints_OnGenerate;
            Point2DCollection Nodes = Random2DPoints.GenerateRandomPointsGroup(NodesMaxValueSet, NodesSet, DataGroupCount, FluctuationRatio);
            Nodes.Print();

            while (Retry)
            {
                Kmeans<Point2D> kmeans = new Kmeans<Point2D>(Nodes);
                kmeans.OnIteration += Kmeans_OnIteration;
                kmeans.Classify(k, IterationLimit, ConvDistance);

                InputCommand();
            }
        }

        private static void InputCommand()
        {
            while (true)
            {
                System.Console.WriteLine("Input command, e.g. CR");
                System.Console.WriteLine("C to clear all views except last generation");
                System.Console.WriteLine("R to run again with current dataset");
                System.Console.WriteLine("I to set class count");
                System.Console.WriteLine("Q to exit");

                string s = Console.ReadLine();

                if (s.ToLower().Contains("c"))
                {
                    Points2DCollectionsViewer.CloseAllButLastView();
                }

                if (s.ToLower().Contains("i"))
                {
                    Console.WriteLine("Input class count:");
                    k = Convert.ToInt32(Console.ReadLine());
                }

                if (s.ToLower().Contains("r"))
                {
                    Round++;
                    Retry = true;
                    break;
                }

                if (s.ToLower().Contains("q"))
                {
                    Retry = false;
                    break;
                }
            }
        }

        private static void Random2DPoints_OnGenerate(Point2DCollection Nodes)
        {
            Points2DCollectionsViewer View = Points2DCollectionsViewer.BuildViewer(Nodes, "RawData Generated", 0, NodesMaxValueSet);
            Points2DCollectionsViewer.ShowViewer(View);
            Points2DCollectionsViewer.CloseAllButLastView();
        }

        private static void Kmeans_OnIteration(IEnumerable<VectorCollection<Point2D>> Groups, int IterationCount)
        {
            Point2DCollection point2Ds = new Point2DCollection();
            foreach (var Group in Groups)
                point2Ds.AddRange(new Point2DCollection(Group));

            Points2DCollectionsViewer View = Points2DCollectionsViewer.BuildViewer(point2Ds, "Round:"+ Round + ",Iteration:"+ IterationCount, 0, NodesMaxValueSet);
            foreach (var Group in Groups)
                View.AddMarkAt(Group.GetCenter(), 16, 3);
            Points2DCollectionsViewer.ShowViewer(View);
        }
    }


    // procedure for check the same coordinates
}