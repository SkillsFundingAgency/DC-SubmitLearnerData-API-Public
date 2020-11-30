using Autofac;
using ESFA.DC.SubmitLearnerData.API.Public.Config;
using Microsoft.Extensions.Configuration;

namespace ESFA.DC.SubmitLearnerData.API.Public.Ioc
{
    public static class ConfigurationRegistration
    {
        public static void SetupFisApiConfigurations(this ContainerBuilder builder, IConfiguration configuration)
        {
            var config = configuration.GetSection("ApplicationConfig").Get<APIConfiguration>();
            builder.RegisterInstance<APIConfiguration>(config);
        }
    }
}
