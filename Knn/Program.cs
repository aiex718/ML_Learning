using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ML_Lib.Views;
using ML_Lib.DataType;
using ML_Lib.Algorithm;
using ML_Lib.Tools;

namespace ProgramKnn
{
    class Program
    {
        static void Main(string[] args)
        {
            int Round = 0;

            bool retry=true;
            int NodesMaxValueSet = 10000, NodesSet = 150, NewNodes = 10, k = 7, DataGroupCount = 5;
            double FluctuationRatio = 0.2;

            Console.WriteLine("Using demo setup?[y/n]");
            string str = Console.ReadLine();
            if (str.ToLower().Contains("y") == false)
            {
                Console.WriteLine("Input node maximum value:(Default:10000)");
                NodesMaxValueSet = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Input node count:(Default:100)");
                NodesSet = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Input new node count:(Default:10)");
                NewNodes = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Input k set:(Default:7)");
                k = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Input groups count for random points to divid into:(Default:4)");
                DataGroupCount = Convert.ToInt32(Console.ReadLine());

                if (DataGroupCount > 0)
                {
                    Console.WriteLine("Input fluctuation ratio:(Default:0.1)");
                    FluctuationRatio = Convert.ToDouble(Console.ReadLine());
                }
            }


            //Gen grouped random nodes
            Point2DCollection ClassifiedPoints =null;

            if (DataGroupCount > 0)
            {
                ClassifiedPoints = RandomPoints.GenerateRandomPointsGroup(NodesMaxValueSet, NodesSet, DataGroupCount, FluctuationRatio);

                //Show generated group data
                Points2DCollectionsViewer View = Points2DCollectionsViewer.BuildViewer(ClassifiedPoints, "RawData Generated", 0, NodesMaxValueSet);
                Points2DCollectionsViewer.ShowViewer(View);
                
                Points2DCollectionsViewer.CloseAllButLastView();
            }
            else
            {
                ClassifiedPoints = RandomPoints.GenerateRandomPoints(NodesMaxValueSet, NodesSet, 0);
            }

            ClassifiedPoints.Print();

            while (retry)
            {
                Point2DCollection NewPoints = RandomPoints.GenerateRandomPoints(NodesMaxValueSet, NewNodes, 0);

                while (NewPoints.Any())
                {
                    Console.WriteLine("Round:{0},Start", Round);
                    var NewPoint = NewPoints.First();
                    NewPoints.Remove(NewPoint);
                    var ClosetPoints = Knn.Classify(k, NewPoint, ClassifiedPoints);
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Round:");
                    sb.Append(Round++);
                    sb.Append(" ,AddPoint At:");
                    sb.Append(NewPoint.x);
                    sb.Append(",");
                    sb.Append(NewPoint.y);
                    sb.Append(",Tag:");
                    sb.Append(NewPoint.Tag);
                    //Show generated group data
                    Points2DCollectionsViewer View = Points2DCollectionsViewer.BuildViewer(ClassifiedPoints, sb.ToString(), 0, NodesMaxValueSet);
                    View.AddMarkAt(NewPoint, 20, 1);

                    foreach (var ClosetPoint in ClosetPoints)
                    {
                        View.AddMarkAt(ClosetPoint, 6, 1);
                    }
                    Points2DCollectionsViewer.ShowViewer(View);
                }


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
                        retry = true;
                        break;
                    }

                    if (s.ToLower().Contains("q"))
                    {
                        retry = false;
                        break;
                    }

                }
            }// while retry
        }
    }
}
