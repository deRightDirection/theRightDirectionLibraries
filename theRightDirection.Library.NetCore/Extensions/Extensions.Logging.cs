using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using theRightDirection.Library;

namespace theRightDirection
{
    public static partial class Extensions
    {
        public static void Exception(this ILog logger, Exception exception)
        {
            logger.Error(exception.Message, exception);
        }

        public static void LogEnvironmentInformation(this ILog logger)
        {
            LogEnvironmentInformation(logger, new List<string>()); ;
        }

        public static void LogEnvironmentInformation(this ILog logger, List<string> additionalLinesWithInformation)
        {
            logger.Info("----- environment configuration -----");
            logger.Info(Assembly.GetEntryAssembly().DirectoryOfAssembly());
            var osInformation = new SystemInformationHelper();
            logger.Info($"{osInformation.WindowsVersionName}{Environment.NewLine}{osInformation.Architecture}{Environment.NewLine}build {osInformation.BuildNumber}");
            foreach (var lineWithInformation in additionalLinesWithInformation)
            {
                logger.Info(lineWithInformation);
            }
            logger.Info("-------------------------------------");
        }

        public static void LogApplicationSettings(this ILog logger)
        {
            logger.Info("----- application configuration -----");
            var applicationSettings = ConfigurationManager.AppSettings;
            if (applicationSettings != null)
            {
                foreach (string k in applicationSettings.Keys)
                {
                    logger.Info($"{k}={applicationSettings[k]}");
                }
            }
            logger.Info("-------------------------------------");
        }

        public static string GetStackTrace(this ILog logger)
        {
            var stackTrace = new StackTrace();
            var stackFrames = stackTrace.GetFrames();
            if (stackFrames.Length < 4)
            {
                return string.Empty;
            }
            var trace = stackFrames[3];
            var mb = trace.GetMethod();
            var t = mb.DeclaringType;
            return $"{t}.{mb.Name}";
        }
    }
}