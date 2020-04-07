//using System;

//namespace AlexSplit
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            Console.WriteLine("Hello World!");
//        }
//    }
//}


using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using CommandLine;
using log4net;
using log4net.Config;

namespace AlexSplit
{
    partial class Program
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            LoadConfiguration();

            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(RunOptions)
                .WithNotParsed(HandleParseError);

            const int lineSize = 1000;
            for (var counter = 0; counter <= lineSize; counter++)
            {
                string message = $"Hello logging world! {counter}";
                log.Info(message);
            }

            Console.ReadLine();
        }

        private static void LoadConfiguration()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        }

        static void RunOptions(Options opts)
        {
            //handle options
        }
        static void HandleParseError(IEnumerable<Error> errs)
        {
            //handle errors
        }
    }
}
