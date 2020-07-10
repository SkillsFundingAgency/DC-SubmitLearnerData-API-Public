using Autofac;
using Microsoft.AspNetCore.Hosting;
using ESFA.DC.Api.Common.Ioc.Modules;
using ESFA.DC.SubmitLearnerData.API.Public.Modules;
using ESFA.DC.SubmitLearnerData.API.Public.Ioc;
using StartupBase = ESFA.DC.Api.Common.StartupBase;

namespace ESFA.DC.SubmitLearnerData.API.Public
{
    public class Startup : StartupBase
    {
        public Startup(IWebHostEnvironment env) : base(env)
        {
        }

        public override void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            // API Common
            containerBuilder.SetupConfigurations(Configuration);
            containerBuilder.RegisterModule<LoggerRegistrations>();

            //FIS API Registrations
            containerBuilder.SetupFisApiConfigurations(Configuration);
            containerBuilder.RegisterModule<APIModule>();
        }
    }
}
