using ML_Lib.DataType;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ML_Lib.Classifier.Knn
{
    public class KnnTrainResult<T> : TrainResult<T> where T : Vector, new()
    {
        public KnnTrainResult() : base()
        {
            ;
        }

        public KnnTrainResult(IEnumerable<T> TrainResult) : base(TrainResult)
        {
            ;
        }

        public KnnTrainResult(string Filepath) : base(Filepath)
        {
            ;
        }
    }
}
