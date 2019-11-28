using Autofac;
using ESFA.DC.SubmitLearnerData.API.Public.Service;
using ESFA.DC.SubmitLearnerData.API.Public.Service.Factory;
using ESFA.DC.SubmitLearnerData.API.Public.Service.Interface;
using ESFA.DC.SubmitLearnerData.API.Public.Service.Providers;
using ESFA.DC.SubmitLearnerData.API.Public.Utils.Modules;

namespace ESFA.DC.SubmitLearnerData.API.Public.Modules
{
    public class APIModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<AzureStorageRepositoryService>().As<IRepositoryService>();
            containerBuilder.RegisterType<ApplicationVersionsProvider>().As<IApplicationVersionsProvider>();
            containerBuilder.RegisterType<APICacheRetrievalService>().As<IAPICacheRetrievalService>();
            containerBuilder.RegisterType<AzureContainerService>().As<IAzureContainerService>();
            containerBuilder.RegisterType<CloudBlobContainerFactory>().As<ICloudBlobContainerFactory>();

            containerBuilder.RegisterModule<PollyModule>();
        }
    }
}
