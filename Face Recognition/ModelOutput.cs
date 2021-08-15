using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face_Recognition
{
    class ModelOutput
    {
        [ColumnName("predictions")]
        public float[] PredictedLabels { get; set; }
    }
}
