using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ML_Lib.DataType;
using System.Threading.Tasks;

namespace ML_Lib.Algorithm
{

    public class Knn<T> where T:Vector,new()
    {
        public delegate void OnClassifyHandler(T NewNode,IEnumerable<T> ClosestKPoints,int MostTag, IEnumerable<T> ClassifiedNodes);
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

        public void Classify(int k,T NewNode)
        {
            List<KeyValuePair<T, double>> NodeDistance = new List<KeyValuePair<T, double>>();
            
            foreach (var ClassifiedNode in ClassifiedNodes)
            {
                NodeDistance.Add(new KeyValuePair<T, double>(ClassifiedNode, ClassifiedNode.GetEuclideanDistance(NewNode)));
            }

            NodeDistance.Sort((x, y) => { return x.Value.CompareTo(y.Value); });

            var ClosestKPoints = NodeDistance.Take(k);

            int MostTag = ClosestKPoints
                .GroupBy(node => node.Key.Tag)
                .OrderByDescending(group => group.Count())
                .First().Key;
            
            NewNode.Tag = MostTag;
            ClassifiedNodes.Add(NewNode);

            OnClassify?.Invoke(NewNode, ClosestKPoints.Select(x => x.Key), MostTag, ClassifiedNodes);
        }

    }
}
