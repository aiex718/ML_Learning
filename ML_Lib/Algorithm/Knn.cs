using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ML_Lib.DataType;
using System.Threading.Tasks;

namespace ML_Lib.Algorithm.Knn
{

    public class Knn<T> where T:Vector,new()
    {
        public delegate void OnClassifyHandler(T NewNode,IEnumerable<T> ClosestKPoints,string MostTag, IEnumerable<T> ClassifiedNodes);
        public event OnClassifyHandler OnClassify;

        VectorCollection<T> ClassifiedNodes;

        public Knn(VectorCollection<T> ClassifiedDataSet)
        {
            ClassifiedNodes = ClassifiedDataSet;
        }

        public void Classify(int k,IEnumerable<T> NewNodes)
        {
            foreach (var NewNode in NewNodes)
                Classify(k, NewNode);
        }

        public string Classify(int k,T NewNode)
        {
            List<KeyValuePair<T, double>> NodeDistance = new List<KeyValuePair<T, double>>();
            
            foreach (var ClassifiedNode in ClassifiedNodes)
            {
                NodeDistance.Add(new KeyValuePair<T, double>(ClassifiedNode, ClassifiedNode.GetEuclideanDistance(NewNode)));
            }
            
            var ClosestKPoints = NodeDistance.OrderBy(x => x.Value).Take(k);

            string MostTag = ClosestKPoints
                .GroupBy(node => node.Key.OriginalTag)
                .OrderByDescending(group => group.Count())
                .First().Key;
            
            NewNode.ClassifiedTag = MostTag;

            OnClassify?.Invoke(NewNode, ClosestKPoints.Select(x => x.Key), MostTag, ClassifiedNodes);

            return MostTag;
        }

    }
}
