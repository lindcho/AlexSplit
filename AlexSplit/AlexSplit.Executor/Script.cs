using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AlexSplit.Executor
{
    public class Script
    {
        public void ExecuteFile(string fileName, int numberOfLines)
        {
            var extension = fileName.Split('.').Last();
            if (extension != "txt") return;

            if(numberOfLines<=0) return;

            var splitSize = numberOfLines;
            var currentDirectory = fileName;
            using (var lineIterator = File.ReadLines(currentDirectory).GetEnumerator())
            {
                bool stillGoing = true;
                for (int chunk = 0; stillGoing; chunk++)
                {
                    stillGoing = WriteChunk(lineIterator, splitSize, chunk, currentDirectory);
                }
            }

        }

        private static bool WriteChunk<T>(IEnumerator<T> lineIterator,
            int splitSize, int chunk, string filepath)
        {
            var logFolder = CreateLogFolder<T>(filepath, out var folderName);
            var fileNameOnFolder = CreateFileNameOnFolder<T>(chunk, logFolder, folderName);

            using (var writer = File.CreateText(fileNameOnFolder))
            {
                for (int i = 0; i < splitSize; i++)
                {
                    if (!lineIterator.MoveNext())
                    {
                        return false;
                    }

                    writer.WriteLine(lineIterator.Current);
                }
            }

            return true;
        }

        private static string CreateLogFolder<T>(string filepath, out string folderName)
        {
            FileInfo baseFile = new FileInfo(filepath);
            var logFileName = baseFile.Name.Split('.').First();
            folderName = Path.Combine(baseFile.DirectoryName, logFileName + ".log");

            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
                return logFileName;
            }
            else
            {
                return logFileName;
            }
        }

        private static string CreateFileNameOnFolder<T>(int chunk, string logFileName, string folderName)
        {
            var newFileName = logFileName + chunk + ".txt";
            var actualName = Path.Combine(folderName, newFileName);
            return actualName;
        }
    }
}
