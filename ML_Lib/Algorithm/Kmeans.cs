using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ML_Lib.DataType;
using System.Threading.Tasks;
using ML_Lib.Interface;
using System.Collections;
using System.IO;

namespace ML_Lib.Algorithm.Kmeans
{

    public class Kmeans<T> where T : Vector, new()
    {
        public class TrainResult : List<VectorCollection<T>>
        {
            public delegate void OnClassifyHandler(T NewNode,IEnumerable<VectorCollection<T>> OrderedGroup, string MostTag);
            public event OnClassifyHandler OnClassify;

            public TrainResult(IEnumerable<VectorCollection<T>> result)
            {
                this.AddRange(result);
            }

            public TrainResult(string filepath)
            {
                StreamReader sr = new StreamReader(filepath);

                while (sr.EndOfStream == false)
                {
                    string json = sr.ReadLine();
                    this.Add(new VectorCollection<T>(json));
                }
            }

            public void SaveTo(string filepath)
            {
                StreamWriter sw = new StreamWriter(filepath, false);

                foreach (var result in this)
                {
                    string json = result.ConvertToJson();
                    sw.WriteLine(json);
                }

                sw.Flush(); sw.Close(); sw.Dispose();
            }

            public string Classify(T NewNods)
            {
                List<KeyValuePair<VectorCollection<T>, double>> GroupDistancePair = new List<KeyValuePair<VectorCollection<T>, double>>();
                
                foreach (var Group in this)
                {
                    double DistanceGet = Group.GetCenter().GetEuclideanDistance(NewNods);
                    GroupDistancePair.Add(new KeyValuePair<VectorCollection<T>, double>(Group, DistanceGet));
                }

                var OrderedGroupDistancePair = GroupDistancePair.OrderBy(x => x.Value).Select(x=>x.Key);
                VectorCollection<T> ShortestGroup = OrderedGroupDistancePair.First();

                OnClassify?.Invoke(NewNods,OrderedGroupDistancePair, ShortestGroup.ClassifiedTag);
                NewNods.ClassifiedTag = ShortestGroup.ClassifiedTag;

                return ShortestGroup.ClassifiedTag;
            }
        }

        public delegate void OnIterationHandler(IEnumerable<VectorCollection<T>> Groups,int IterationCount);
        public event OnIterationHandler OnIteration;

        VectorCollection<T> Nodes;

        public Kmeans(VectorCollection<T> nodes)
        {
            Nodes = nodes;
        }

        public TrainResult Training(IEnumerable<VectorCollection<T>> FirstGen, int IterationLimit, double ConvDistance)
        {
            var ThisGen = FirstGen;

            for (int Iteration = 0; Iteration < IterationLimit; Iteration++)
            {
                ClassifyNodes(ThisGen);

                var NextGen = GenerateNextIteration(ThisGen);
                OnIteration?.Invoke(ThisGen, Iteration);

                double LongestDistance = CalcLongestDistance(ThisGen, NextGen);

                if (LongestDistance < ConvDistance)
                {
                    Console.WriteLine("Iteration:{0}, LongestDistance:{1} < {2}, Done", Iteration, LongestDistance, ConvDistance);
                    break;
                }
                else
                {
                    Console.WriteLine("Iteration:{0}, LongestDistance:{1} > {2}, Continue...", Iteration, LongestDistance, ConvDistance);
                    ThisGen = NextGen;
                }
            }

            foreach (var vectorCollection in ThisGen)
                vectorCollection.AssignTagFromMostOriginalTag();

            var trainResult = new TrainResult(ThisGen);

            return trainResult;
        }

        public TrainResult Training(int k, int IterationLimit, double ConvDistance)
        {
            var FirstGen = InitialGroupsRandomly(k);

            return Training(FirstGen, IterationLimit, ConvDistance);
        }

        double CalcLongestDistance(IEnumerable<VectorCollection<T>> GroupA, IEnumerable<VectorCollection<T>> GroupB)
        {
            if (GroupA.Count()!= GroupB.Count())
                throw new Exception("Group length not equalvilent.");

            double LongestDistance = double.MinValue;

            var AEnum = GroupA.GetEnumerator();
            var BEnum = GroupB.GetEnumerator();


            while (AEnum.MoveNext() && BEnum.MoveNext())
            {
                var GroupA_Center = AEnum.Current.GetCenter();
                var GroupB_Center = BEnum.Current.GetCenter();

                double DistanceGet = GroupA_Center.GetEuclideanDistance(GroupB_Center);

                if (DistanceGet> LongestDistance)
                    LongestDistance = DistanceGet;
            }
            return LongestDistance;
        }

        IEnumerable<VectorCollection<T>> InitialGroupsRandomly(int k)
        {
            VectorCollection<T>[] Groups = new VectorCollection<T>[k];
            //Initial random member as groups center
            for (int i = 0; i < k; i++)
            {
                Groups[i] = new VectorCollection<T>();
                Groups[i].ClassifiedTag = i.ToString();
                Groups[i].SetCenter(Nodes.GetRandomMember());
            }

            return Groups;
        }

        IEnumerable<VectorCollection<T>> GenerateNextIteration(IEnumerable<VectorCollection<T>> ThisGen)//return longest
        {
            VectorCollection<T>[] NextGen = new VectorCollection<T>[ThisGen.Count()];

            for (int i = 0; i < NextGen.Length; i++)
            {
                NextGen[i] = new VectorCollection<T>();
                NextGen[i].SetCenter(ThisGen.ElementAt(i).GetMean());
                NextGen[i].ClassifiedTag = ThisGen.ElementAt(i).ClassifiedTag;
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

                Node.ClassifiedTag = ShortestGroup.ClassifiedTag;
                lock (ShortestGroup)
                {
                    ShortestGroup.Add(Node);
                }
            });
        }

    }
}