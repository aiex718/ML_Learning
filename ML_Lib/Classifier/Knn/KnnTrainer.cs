using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ML_Lib.DataType;

namespace ML_Lib.Classifier.Knn
{
    public class KnnTrainer<T> where T : Vector, new()
    {
        List<T> UntrainedData;
        public double? MinimumDifferentDistance;

        public KnnTrainer(IEnumerable<T> untrainedData)
        {
            foreach (var item in untrainedData)
            {
                if (item.Tag == null)
                    throw new Exception("Data for KnnTrainer must have tag");
            }

            UntrainedData = new List<T>();
            UntrainedData.AddRange(untrainedData);
        }

        public KnnTrainResult<T> Train()
        {
            if (MinimumDifferentDistance!=null)
            {
                RemoveSimilarNode();
            }
            return new KnnTrainResult<T>(UntrainedData);
        }

        public void RemoveSimilarNode()
        {
            Vector[] NodeArray = UntrainedData.ToArray();
            UntrainedData.Clear();

            object SyncLock = new object();
            Parallel.For(0, NodeArray.Length, CurrentNodeIdx =>
            {
                bool AddThisValue = true;
                for (int SearchNodeIdx = CurrentNodeIdx + 1; SearchNodeIdx < NodeArray.Length; SearchNodeIdx++)
                {
                    if (NodeArray[CurrentNodeIdx].GetEuclideanDistance(NodeArray[SearchNodeIdx]) < MinimumDifferentDistance)
                    {
                        AddThisValue = false;
                        break;
                    }
                }

                if (AddThisValue)
                {
                    lock (SyncLock)
                    {
                        UntrainedData.Add(new T { VectorData = NodeArray[CurrentNodeIdx].VectorData, Tag = NodeArray[CurrentNodeIdx].Tag });
                    }
                }
            });
        }


    }
}
