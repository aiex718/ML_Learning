using ML_Lib.DataType;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ML_Lib.Classifier
{
    public abstract class TrainResult<T> : List<T> where T : Vector, new()
    {
        public TrainResult()
        {

        }

        public TrainResult(IEnumerable<T> result)
        {
            this.AddRange(result);
        }

        public TrainResult(string Filepath)
        {
            this.AddRange(ReadFromFile(Filepath));
        }

        protected virtual IEnumerable<T> ReadFromFile(string Filepath)
        {
            StreamReader sr = new StreamReader(Filepath);
            string json = sr.ReadToEnd();
            sr.Close(); sr.Dispose();
            return JsonConvert.DeserializeObject<IEnumerable<T>>(json);
        }

        public virtual void SaveToFile(string Filepath)
        {
            string json = JsonConvert.SerializeObject(this);
            StreamWriter sw = new StreamWriter(Filepath, false);
            sw.WriteLine(json);
            sw.Flush(); sw.Close(); sw.Dispose();
        }
    }
}
