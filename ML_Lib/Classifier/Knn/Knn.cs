using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ML_Lib.DataType;
using System.Threading.Tasks;
using ML_Lib.Interface;

namespace ML_Lib.Classifier.Knn
{
    public class KnnClassifyResult : ITag
    {
        public float Confidence;
        public string Tag { get; set; }
    }

    public class Knn<T> where T:Vector,new()
    {
        public delegate void OnClassifyHandler(T NewNode,IEnumerable<T> ClosestKPoints,string MostTag, IEnumerable<T> ClassifiedNodes);
        public event OnClassifyHandler OnClassify;

        KnnTrainResult<T> ClassifiedNodes;
        float? Threshold = null;

        public Knn(KnnTrainResult<T> knnTrainResult, float? threshold = null)
        {
            ClassifiedNodes = knnTrainResult;
            Threshold = threshold;
        }
        

        public KnnClassifyResult Classify(int k,T NewNode)
        {
            List<KeyValuePair<T, float>> NodeDistance = new List<KeyValuePair<T, float>>();
            
            foreach (var ClassifiedNode in ClassifiedNodes)
            {
                var distance = ClassifiedNode.GetEuclideanDistance(NewNode);

                if (Threshold!=null && distance<Threshold)
                    NodeDistance.Add(new KeyValuePair<T, float>(ClassifiedNode, distance));
                else
                    NodeDistance.Add(new KeyValuePair<T, float>(ClassifiedNode, distance));
            }

            var ClosestKPoints = NodeDistance.OrderBy(x => x.Value).Take(k);

            var MostElementsGroup = ClosestKPoints
                .GroupBy(node => node.Key.Tag)
                .OrderByDescending(group => group.Count())
                .First();

            float confidence = ((float)MostElementsGroup.Count()) / ((float)k);
            string MostTag = MostElementsGroup.Key;
            
            NewNode.Tag = MostTag;

            OnClassify?.Invoke(NewNode, ClosestKPoints.Select(x => x.Key), MostTag, ClassifiedNodes);

            return new KnnClassifyResult() { Tag = MostTag, Confidence = confidence };
        }

    }
}
