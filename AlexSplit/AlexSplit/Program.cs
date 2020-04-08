using System;
using System.IO;
using System.Reflection;
using AlexSplit.Executor;
using CommandLine;
using log4net;
using log4net.Config;

namespace AlexSplit
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        static readonly Script script = new Script();

        static void Main(string[] args)
        {
            LoadConfiguration();
          
            var parserResult = Parser.Default.ParseArguments<Options>(args);
            if (parserResult == null)
            {
                Console.WriteLine("invalid Arguments");
                Console.WriteLine("Usage:");
                Console.WriteLine(Options.GetUsage());
                return;
            }

            parserResult
                .MapResult(
                    ExecuteFileAndReturnExitCode,
                    errs => 1);

        }

        private static void LoadConfiguration()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        }

        private static int ExecuteFileAndReturnExitCode(Options opts)
        {
            script.ExecuteFile(opts.Source, opts.NumberOfLines);
            return 0;
        }
    }
}
