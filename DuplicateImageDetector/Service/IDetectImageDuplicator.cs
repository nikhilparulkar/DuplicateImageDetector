using DuplicateImageDetector.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DuplicateImageDetector.Service
{
   
    public interface IDetectImageDuplicator
    {
        ResultModel ImageDuplicates(string directorypath);
    }
}
