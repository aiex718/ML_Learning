using ML_Lib.DataType;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ML_Lib.Classifier.Kmeans
{
    public class KmeansTrainResult<T> : TrainResult<T> where T : Vector, new()
    {
        public KmeansTrainResult(IEnumerable<T> TrainResult) : base(TrainResult)
        {
            ;
        }

        public KmeansTrainResult(string Filepath):base(Filepath)
        {
            ;
        }
    }
}
