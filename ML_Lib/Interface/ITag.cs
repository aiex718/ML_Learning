using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ML_Lib.Interface
{
    interface ITag
    {
        string OriginalTag { get; set; }
        string ClassifiedTag { get; set; }
    }
}
