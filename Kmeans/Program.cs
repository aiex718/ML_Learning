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
        static void Main(string[] args)
        {
            bool retry = true;
            int RetryCount = 0;
            int NodesMaxValueSet=10000, NodesSet=10000, ClassSet=3, IterationLimit=100, ConvDistance=5 , DataGroupCount = 3;
            double FluctuationRatio = 0.18;

            Console.WriteLine("Using demo setup?[y/n]");
            string str = Console.ReadLine();
            if (str.ToLower().Contains("y")==false)
            {
                Console.WriteLine("Input node maximum value:(Default:10000)");
                NodesMaxValueSet = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Input node count:(Default:10000)");
                NodesSet = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Input class count:(Default:3)");
                ClassSet = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Input iteration limit:(Default:100)");
                IterationLimit = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Input convergence distance:(Default:5)");
                ConvDistance = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Input groups count for random points to divid into:(Default:3)");
                DataGroupCount = Convert.ToInt32(Console.ReadLine());

                if (DataGroupCount>0)
                {
                    Console.WriteLine("Input fluctuation ratio:(Default:0.18)");
                    FluctuationRatio = Convert.ToDouble(Console.ReadLine());
                }
            }

            //Gen grouped random nodes
            Point2DCollection Nodes;

            if (DataGroupCount > 0)
            {
                Nodes = RandomPoints.GenerateRandomPointsGroup(NodesMaxValueSet,NodesSet,DataGroupCount, FluctuationRatio);

                //Show generated group data
                Points2DCollectionsViewer View = Points2DCollectionsViewer.BuildViewer(Nodes, "RawData Generated", 0, NodesMaxValueSet);
                Points2DCollectionsViewer.ShowViewer(View);
                Points2DCollectionsViewer.CloseAllButLastView();
            }
            else
            {
                Nodes = RandomPoints.GenerateRandomPoints(NodesMaxValueSet, NodesSet, 0);
            }

            Nodes.Print();


            while (retry)
            {
                //Initial first Gen
                List<Point2DCollection> NewGeneration_ClassCollection = new List<Point2DCollection>();
                List<Point2DCollection> LastGeneration_ClassCollection = new List<Point2DCollection>();
                
                for (int i = 0; i < ClassSet; i++)
                {
                    //Select random point as mid point
                    NewGeneration_ClassCollection.Add(new Point2DCollection(Nodes.GetRandomPoint()));
                }

                int Iteration_cnt = 0;
                double ThisGenLongestDistance = double.MaxValue;

                while (Iteration_cnt++ < IterationLimit && ThisGenLongestDistance > ConvDistance)
                {
                    Kmeans.Calculate(Nodes, NewGeneration_ClassCollection);

                    if (LastGeneration_ClassCollection.Any())
                    {
                        ThisGenLongestDistance = 0;
                        for (int i = 0; i < LastGeneration_ClassCollection.Count; i++)
                        {
                            Point2D NewMidPoint = LastGeneration_ClassCollection[i].CalcNewMidPoint();
                            double Distance_get = LastGeneration_ClassCollection[i].GetMidPoint().GetDistance(NewMidPoint);

                            if (Distance_get > ThisGenLongestDistance)
                                ThisGenLongestDistance = Distance_get;
                        }
                        System.Console.WriteLine("Iteration:{0},LongestDistance:{1}", Iteration_cnt, ThisGenLongestDistance);
                    }

                    //Iterating
                    LastGeneration_ClassCollection = NewGeneration_ClassCollection;
                    NewGeneration_ClassCollection = new List<Point2DCollection>();
                    foreach (var OldGen in LastGeneration_ClassCollection)
                    {
                        Point2DCollection NexGen = new Point2DCollection(OldGen.CalcNewMidPoint());
                        NewGeneration_ClassCollection.Add(NexGen);
                    }



                    //Show data
                    int tag = 0;
                    Point2DCollection PointsToShow = new Point2DCollection();
                    foreach (var PointCollection in LastGeneration_ClassCollection)
                    {
                        foreach (var Point in PointCollection)
                        {
                            Point.Tag = tag;
                        }
                        PointsToShow.AddRange(PointCollection);
                        tag++;
                    }
                    Points2DCollectionsViewer View = Points2DCollectionsViewer.BuildViewer(PointsToShow, "Round:" + RetryCount + ",Iteration:" + Iteration_cnt, 0, NodesMaxValueSet);
                    //Add mark
                    foreach (var PointCollection in LastGeneration_ClassCollection)
                    {
                        if (PointCollection.Any())
                        {
                            Point2D p = PointCollection.GetMidPoint();
                            p.Tag = PointCollection.First().Tag;
                            View.AddMarkAt(p, 15, 3);
                        }
                    }

                    Points2DCollectionsViewer.ShowViewer(View);

                }

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
                        ClassSet = Convert.ToInt32(Console.ReadLine());
                    }

                    if (s.ToLower().Contains("r"))
                    {
                        RetryCount++;
                        retry = true;
                        break;
                    }

                    if (s.ToLower().Contains("q"))
                    {
                        retry = false;
                        break;
                    }
                    
                }
            }

            
        }

    }


    // procedure for check the same coordinates
}