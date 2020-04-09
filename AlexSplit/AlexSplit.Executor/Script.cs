using System;
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
            if (numberOfLines <= 0) return;


            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            var logFolder = CreateLogFolder(fileName);
            var subDirectoryFileName = Path.Combine(logFolder, fileNameWithoutExtension);

            var splitSize = numberOfLines;
            using (var lineIterator = File.ReadLines(fileName).GetEnumerator())
            {
                bool stillGoing = true;
                for (int chunk = 0; stillGoing; chunk++)
                {
                    stillGoing = WriteChunk(lineIterator, splitSize, chunk, subDirectoryFileName);
                    Console.Write(" " + numberOfLines + " lines written" + "\n");
                }
            }

        }

        private static bool WriteChunk(IEnumerator<string> lineIterator, int splitSize, int chunk, string subDirectory)
        {
            var fileNameOnFolder = CreateFileNameOnFolder(chunk, subDirectory);

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

        private static string CreateLogFolder(string filepath)
        {
            var logFolder = filepath.Replace(".", "_");

            if (!Directory.Exists(logFolder))
                Directory.CreateDirectory(logFolder);

            return logFolder;
        }

        private static string CreateFileNameOnFolder(int chunk, string logFileName)
        {
            var newFileName = logFileName + "_" + chunk + ".txt";
            return newFileName;
        }
    }
}
