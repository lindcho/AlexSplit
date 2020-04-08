using System.Text;
using CommandLine;

namespace AlexSplit.Executor
{
    public class Options
    {
        [Option('n', "name", Required = true, HelpText = "log file path")]
        public string Source { get; set; }

        [Option('l', "lines", Required = true, HelpText = "no of lines per file")]
        public int NumberOfLines { get; set; }


        private static string GetUsage()
        {
            var usage = new StringBuilder();
            usage.AppendLine("ApplicationName.exe -n <source> -l <numberOfLines>");
            usage.AppendLine("Read user manual for usage instructions...");
            return usage.ToString();
        }
    }
}
