using System.CommandLine;
using System.CommandLine.Invocation;
using AlexSplit.Executor;

namespace AlexSplit
{
    class Program
    {
        static readonly Script script = new Script();

        static void Main(string[] args)
        {

            RootCommand rootCommand = new RootCommand(
                description: "Splits a log file into small text files with the specified number of lines each.");
            Option nameOption = new Option<string>(
                aliases: new string[] { "--name", "-n" }
                , description: "The log file path to be processed.");
            rootCommand.AddOption(nameOption);

            Option linesOption = new Option<int>(
                aliases: new string[] { "--lines", "-l" }
                , description: "The number of lines to write to each file." +
                               "The default number of lines if not provided is 2000",
                getDefaultValue: () => 2000);
            rootCommand.AddOption(linesOption);

            rootCommand.Handler =
                CommandHandler.Create<string, int>(ExecuteFileAndReturnExitCode);

            rootCommand.InvokeAsync(args).Wait();

        }

        private static int ExecuteFileAndReturnExitCode(string name, int lines)
        {
            var opts = new Options { Source = name, NumberOfLines = lines };

            script.ExecuteFile(opts.Source, opts.NumberOfLines);
            return 0;
        }
    }
}
