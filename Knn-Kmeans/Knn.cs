using System;
using System.Collections.Generic;
using System.Text;
using ML_Lib.Views;
using ML_Lib.DataType;
using ML_Lib.Tools;
using ML_Lib.Classifier.Knn;

namespace Program
{
    public class ProgramKnn
    {
        //Parameters for generate random points
        static int NodesMaxValueSet = 10000, NodesSet = 150, DataGroupCount = 3;
        static float FluctuationRatio = 0.5F;

        //Parameters for knn
        static int k = 7, NewNodesCount=10;

        //Parameters for retry
        static int Round = 0;
        static bool Retry = true;

        public static void Knn()
        {
            Console.WriteLine("Using demo setup?[y/n]");
            string str = Console.ReadLine();
            if (str.ToLower().Contains("y") == false)
            {
                Console.WriteLine("Input node maximum value:(Default:10000)");
                NodesMaxValueSet = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Input node count:(Default:150)");
                NodesSet = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Input groups count for random points to divid into:(Default:3, Min:1)");
                DataGroupCount = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Input fluctuation ratio:(Default:0.5, Min:0.1)");
                FluctuationRatio = Convert.ToSingle(Console.ReadLine());

                Console.WriteLine("Input new node count:(Default:10)");
                NewNodesCount = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Input k set:(Default:7)");
                k = Convert.ToInt32(Console.ReadLine());
            }

            //Gen grouped random nodes
            Random2DPoints.OnGeneratePointGroups += Random2DPoints_OnGeneratePointGroups;
            var ClassifiedPoints = Random2DPoints.GenerateRandomPointsGroup(NodesMaxValueSet, NodesSet, DataGroupCount, FluctuationRatio);
            var Dataset = new VectorCollection<Point2D>(ClassifiedPoints);
            Dataset.Print();

            var knnTrainer = new KnnTrainer<Point2D>(Dataset);
            Knn<Point2D> knn = new Knn<Point2D>(knnTrainer.Train());
            knn.OnClassify += Knn_OnClassify;

            while (Retry)
            {
                var NewNodes = Random2DPoints.GenerateRandomPoints(NodesMaxValueSet, NewNodesCount);

                foreach (var NewNode in NewNodes)
                    knn.Classify(k, NewNode);
                
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
                System.Console.WriteLine("Q to exit");

                string s = Console.ReadLine();

                if (s.ToLower().Contains("c"))
                {
                    Points2DCollectionsViewer.CloseAllButLastView();
                }

                if (s.ToLower().Contains("r"))
                {
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

        private static void Knn_OnClassify(Point2D NewNode, IEnumerable<Point2D> ClosestKPoints, string MostTag, IEnumerable<Point2D> ClassifiedNodes)
        {
            Console.WriteLine("NewPoint,x:{0},y:{1}\t,closet points in {2}", NewNode.x, NewNode.y, k);

            foreach (var ClosestPoint in ClosestKPoints)
                Console.WriteLine("Point,x:{0},y:{1}\t,tag:{2},distance{3}", ClosestPoint.x, ClosestPoint.y, ClosestPoint.Tag, ClosestPoint.GetEuclideanDistance(NewNode));

            Console.WriteLine("MostTag:{0}", MostTag);
            Console.WriteLine("----------------------------------");

            List<Point2D> point2Ds = new List<Point2D>();
            point2Ds.AddRange(ClassifiedNodes);

            StringBuilder sb = new StringBuilder();
            sb.Append("Round:");
            sb.Append(Round++);
            sb.Append(" ,AddPoint At:");
            sb.Append(NewNode.x);
            sb.Append(",");
            sb.Append(NewNode.y);
            sb.Append(",Tag:");
            sb.Append(NewNode.Tag);
            Points2DCollectionsViewer View = Points2DCollectionsViewer.BuildViewer(point2Ds, sb.ToString(), 0, NodesMaxValueSet);
            View.AddMarkAt(NewNode, 20, 7);
            
            foreach (var ClosestPoint in ClosestKPoints)
                View.AddMarkAt(ClosestPoint, 6, 7);
            Points2DCollectionsViewer.ShowViewer(View);
        }



        private static void Random2DPoints_OnGeneratePointGroups(IEnumerable<Point2D> Nodes)
        {
            Points2DCollectionsViewer View = Points2DCollectionsViewer.BuildViewer(Nodes, "RawData Generated", 0, NodesMaxValueSet);
            Points2DCollectionsViewer.ShowViewer(View);
            Points2DCollectionsViewer.CloseAllButLastView();
        }
    }
}
