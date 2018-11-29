using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ML_Lib.DataType;
using System.Threading.Tasks;
using ML_Lib.Interface;

namespace ML_Lib.Algorithm
{
    public class Kmeans<T> where T : Vector, new()
    {
        public delegate void OnIterationHandler(IEnumerable<VectorCollection<T>> Groups,int IterationCount);
        public event OnIterationHandler OnIteration;

        VectorCollection<T> Nodes;

        public Kmeans(VectorCollection<T> nodes)
        {
            Nodes = nodes;
        }
        public IEnumerable<VectorCollection<T>> Classify(int k, int IterationLimit, double ConvDistance)
        {
            var ThisGen = InitialGroupsRandomly(k);
            
            for (int Iteration = 0; Iteration < IterationLimit; Iteration++)
            {
                ClassifyNodes(ThisGen);
                var NextGen = GenerateNextIteration(ThisGen);
                OnIteration?.Invoke(ThisGen, Iteration);

                double LongestDistance = CalcLongestDistance(ThisGen, NextGen);

                if (LongestDistance < ConvDistance)
                {
                    Console.WriteLine("Iteration:{0}, LongestDistance:{1} < {2}, Done", Iteration, LongestDistance , ConvDistance);
                    break;
                }
                else
                {
                    Console.WriteLine("Iteration:{0}, LongestDistance:{1} > {2}, Continue...", Iteration, LongestDistance, ConvDistance);
                    ThisGen = NextGen;
                }
            }

            return ThisGen;
        }

        double CalcLongestDistance(IEnumerable<VectorCollection<T>> GroupA, IEnumerable<VectorCollection<T>> GroupB)
        {
            double LongestDistance = double.MinValue;
            for (int i = 0; i < GroupA.Count(); i++)
            {
                var GroupA_Center = GroupA.ElementAt(i).GetCenter();
                var GroupB_Center = GroupB.ElementAt(i).GetCenter();

                double DistanceGet = GroupA_Center.GetEuclideanDistance(GroupB_Center);

                if (DistanceGet> LongestDistance)
                    LongestDistance = DistanceGet;
            }
            return LongestDistance;
        }

        VectorCollection<T>[] InitialGroupsRandomly(int k)
        {
            VectorCollection<T>[] Groups = new VectorCollection<T>[k];
            //Initial random member as groups center
            for (int i = 0; i < k; i++)
            {
                Groups[i] = new VectorCollection<T>();
                Groups[i].Tag = i;
                Groups[i].SetCenter(Nodes.GetRandomMember());
            }

            return Groups;
        }

        VectorCollection<T>[] GenerateNextIteration(VectorCollection<T>[] ThisGen)//return longest
        {
            VectorCollection<T>[] NextGen = new VectorCollection<T>[ThisGen.Length];
            for (int i = 0; i < NextGen.Length; i++)
            {
                NextGen[i] = new VectorCollection<T>();
                NextGen[i].SetCenter(ThisGen[i].GetMean());
                NextGen[i].Tag = ThisGen[i].Tag;
            }
            return NextGen;
        }

        void ClassifyNodes(IEnumerable<VectorCollection<T>> Groups)//return Longest
        {
            Parallel.ForEach(Nodes, Node =>
            {
                object DistanceLock = new object();
                double ShortestValue = double.MaxValue;
                VectorCollection<T> ShortestGroup = null;

                Parallel.ForEach(Groups, Group =>
                {
                    double DistanceGet = Group.GetCenter().GetEuclideanDistance(Node);

                    lock (DistanceLock)
                    {
                        if (DistanceGet < ShortestValue)
                        {
                            ShortestValue = DistanceGet;
                            ShortestGroup = Group;
                        }
                    }
                });

                Node.Tag = ShortestGroup.Tag;
                lock (ShortestGroup)
                {
                    ShortestGroup.Add(Node);
                }
            });
        }

    }
}