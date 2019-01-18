using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ML_Lib.DataType;
using System.Threading.Tasks;
using ML_Lib.Interface;
using System.Collections;
using System.IO;

namespace ML_Lib.Classifier.Kmeans
{
    public class Kmeans<T> where T : Vector, new()
    {
        public delegate void OnClassifyHandler(T NewNode, IEnumerable<T> TrainedResult, string MostTag);
        public event OnClassifyHandler OnClassify;

        KmeansTrainResult<T> TrainedResult;

        public Kmeans(KmeansTrainResult<T> trainResult)
        {
            TrainedResult = trainResult;
        }

        public string Classify(T NewNods)
        {
            List<KeyValuePair<T, float>> TrainedResultDistancePair = new List<KeyValuePair<T, float>>();

            foreach (T Center in TrainedResult)
            {
                float DistanceGet = Center.GetEuclideanDistance(NewNods);
                TrainedResultDistancePair.Add(new KeyValuePair<T, float>(Center, DistanceGet));
            }

            var ShortestGroup = TrainedResultDistancePair.OrderBy(x => x.Value).Select(x=>x.Key);
            var ShortestGroupTag = ShortestGroup.First().Tag;

            OnClassify?.Invoke(NewNods, ShortestGroup, ShortestGroupTag);
            NewNods.Tag = ShortestGroupTag;

            return ShortestGroupTag;
        }

        public KmeansTrainResult<T> GetTrainResult()
        {
            return TrainedResult;
        }
    }
}