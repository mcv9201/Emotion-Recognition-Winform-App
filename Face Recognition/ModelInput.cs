using Microsoft.ML.Transforms.Image;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face_Recognition
{
    class ModelInput
    {
        public struct InputModelConfiguration
        {
            public const int ImageHeight = 64;
            public const int ImageWidth = 64;
        }

        [ImageType(InputModelConfiguration.ImageHeight, InputModelConfiguration.ImageWidth)]
        public Bitmap Image { get; set; }
    }
}
