using System.Collections.Generic;
using Autofac;
using ESFA.DC.Logging;
using ESFA.DC.Logging.Config;
using ESFA.DC.Logging.Config.Interfaces;
using ESFA.DC.Logging.Enums;
using ESFA.DC.Logging.Interfaces;
using ESFA.DC.SubmitLearnerData.API.Public.Interface;

namespace ESFA.DC.SubmitLearnerData.API.Public.Modules
{
    public class LoggerModule : Module
    {
        private readonly IAPIConfiguration _configuration;

        public LoggerModule(IAPIConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new ApplicationLoggerSettings
            {
                ApplicationLoggerOutputSettingsCollection = new List<IApplicationLoggerOutputSettings>()
                {
                    new MsSqlServerApplicationLoggerOutputSettings()
                    {
                        MinimumLogLevel = LogLevel.Verbose,
                        ConnectionString = _configuration.LoggerConnectionString
                    },
                    new ConsoleApplicationLoggerOutputSettings()
                    {
                        MinimumLogLevel = LogLevel.Verbose
                    }
                }
            }).As<IApplicationLoggerSettings>().SingleInstance();

            builder.RegisterType<ExecutionContext>().As<IExecutionContext>().InstancePerLifetimeScope();
            builder.RegisterType<SerilogLoggerFactory>().As<ISerilogLoggerFactory>().InstancePerLifetimeScope();
            builder.RegisterType<SeriLogger>().As<ILogger>().InstancePerLifetimeScope();
        }
    }
}

