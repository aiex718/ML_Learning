using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ML_Lib.DataType;

namespace ML_Lib.Classifier.Kmeans
{
    public class KmeansTrainer<T> where T : Vector, new()
    {
        public delegate void OnIterationHandler(IEnumerable<VectorCollection<T>> Groups, int IterationCount);
        public event OnIterationHandler OnIteration;
        
        int K , IterationLimit;
        float ConvDistance;

        IEnumerable<VectorCollection<T>> FirstGeneration;
        readonly IEnumerable<T> UntrainedData;

        public KmeansTrainer(IEnumerable<T> untrainedData,int k, float convDistance=5, int iterationLimit=100)
        {
            K = k;
            ConvDistance = convDistance;
            IterationLimit = iterationLimit;
            UntrainedData = untrainedData;
        }

        public KmeansTrainResult<T> Train()
        {
            if (FirstGeneration==null)
                FirstGeneration = GenRandomCenter();

            return Training();
        }

        public void SetFirstGen(IEnumerable<VectorCollection<T>> FirstGen)
        {
            FirstGeneration = FirstGen;
        }

        KmeansTrainResult<T> Training()
        {
            var ThisGen = FirstGeneration;

            for (int Iteration = 0; Iteration < IterationLimit; Iteration++)
            {
                ClassifyUntrainedData(ThisGen);

                var NextGen = GenerateNextIteration(ThisGen);
                OnIteration?.Invoke(ThisGen, Iteration);

                float LongestDistance = CalcLongestDistance(ThisGen, NextGen);

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
                vectorCollection.Tag = vectorCollection.GetMostTag();

            var trainResult = new KmeansTrainResult<T>(ThisGen.Select(x=>x.Center));

            return trainResult;
        }

        IEnumerable<VectorCollection<T>> GenRandomCenter()
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            VectorCollection<T>[] Groups = new VectorCollection<T>[K];
            //Initial random member as groups center
            for (int i = 0; i < K; i++)
            {
                Groups[i] = new VectorCollection<T>();
                Groups[i].Tag = i.ToString();
                Groups[i].Center = UntrainedData.ElementAt(random.Next(0, UntrainedData.Count()));
            }

            return Groups;
        }

        IEnumerable<VectorCollection<T>> GenerateNextIteration(IEnumerable<VectorCollection<T>> ThisGen)//return longest
        {
            VectorCollection<T>[] NextGen = new VectorCollection<T>[ThisGen.Count()];

            for (int i = 0; i < NextGen.Length; i++)
            {
                NextGen[i] = new VectorCollection<T>();
                NextGen[i].Tag = ThisGen.ElementAt(i).Tag;
                NextGen[i].Center = ThisGen.ElementAt(i).GetMean();
            }
            return NextGen;
        }

        void ClassifyUntrainedData(IEnumerable<VectorCollection<T>> Groups)
        {
            Parallel.ForEach(UntrainedData, Node =>
            {
                object DistanceLock = new object();
                float ShortestValue = float.MaxValue;
                VectorCollection<T> ShortestGroup = null;

                Parallel.ForEach(Groups, Group =>
                {
                    float DistanceGet = Group.Center.GetEuclideanDistance(Node);

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

        static float CalcLongestDistance(IEnumerable<VectorCollection<T>> GroupA, IEnumerable<VectorCollection<T>> GroupB)
        {
            if (GroupA.Count() != GroupB.Count())
                throw new Exception("Group length not equalvilent.");

            float LongestDistance = float.MinValue;

            var AEnum = GroupA.GetEnumerator();
            var BEnum = GroupB.GetEnumerator();


            while (AEnum.MoveNext() && BEnum.MoveNext())
            {
                var GroupA_Center = AEnum.Current.Center;
                var GroupB_Center = BEnum.Current.Center;

                float DistanceGet = GroupA_Center.GetEuclideanDistance(GroupB_Center);

                if (DistanceGet > LongestDistance)
                    LongestDistance = DistanceGet;
            }
            return LongestDistance;
        }
    }
}
