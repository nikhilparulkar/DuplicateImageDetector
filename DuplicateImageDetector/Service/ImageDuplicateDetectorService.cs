using DuplicateImageDetector.Models;
using DuplicateImageDetector.Utils;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DuplicateImageDetector.Service
{
    public class ImageDuplicateDetectorService : IDetectImageDuplicator
    {
        Dictionary<string, string> original;
        List<string> duplicates;
        private readonly ILogger<IDetectImageDuplicator> _logger;

        public ImageDuplicateDetectorService(ILogger<IDetectImageDuplicator> logger)
        {
            original = new Dictionary<string, string>();
            duplicates = new List<string>();
            _logger = logger;

        }

        public ResultModel ImageDuplicates(string directorypath)
        {
            _logger.LogInformation("Beginning Image duplicate detector");
            ResultModel result =null;
            try
            {

                if (string.IsNullOrWhiteSpace(directorypath))
                {
                    result = (new ResultModel { Original = null, duplicates = null, Error = "Invalid directory path" });
                    _logger.LogError(result.Error);
                    return result;
                }

                var allfiles = Directory.GetFiles(directorypath, "*.*", SearchOption.AllDirectories);

                foreach (var File in allfiles)
                {
                    var thisFileMd5 = MD5HashCalc.GetMD5(File);
                    AddOrUpdateFileStore(File, thisFileMd5);

                }


                result = new ResultModel { Original = original.Values.ToList(), duplicates = this.duplicates };
                writeToConsole(result);


                return result ;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Occure during Processing" +ex.Message);
                return (new ResultModel { Original = null, duplicates = null, Error = "Error Occure during Processing" });
            }
            
        }

        private void writeToConsole(ResultModel result)
        {
            if (result == null)
                return;

            if (string.IsNullOrEmpty(result.Error))
            {
                Console.WriteLine("Writing Originals");
                _logger.LogInformation("Writing Originals");

                foreach (var File in result.Original)
                {
                    Console.WriteLine(File);
                    _logger.LogInformation(File);
                }

                Console.WriteLine("Writing Duplicates");
                _logger.LogInformation("Writing Duplicates");

                foreach (var File in result.duplicates)
                {
                    Console.WriteLine(File);
                    _logger.LogInformation(File);
                }
            }
            else
            {
                Console.WriteLine(result.Error);
                _logger.LogError(result.Error);
            }



        }

        private void AddOrUpdateFileStore(string file, string md5Hash)
        {
            if (original.ContainsKey(md5Hash))
            {
                duplicates.Add(file);
            }
            else
            {
                original.TryAdd(md5Hash, file);
            }

        }

        


    }
}
