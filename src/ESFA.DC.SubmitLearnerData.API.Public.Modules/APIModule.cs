using Autofac;
using ESFA.DC.FileService;
using ESFA.DC.FileService.Config;
using ESFA.DC.FileService.Config.Interface;
using ESFA.DC.FileService.Interface;
using ESFA.DC.SubmitLearnerData.API.Public.Config;
using ESFA.DC.SubmitLearnerData.API.Public.Interface;
using ESFA.DC.SubmitLearnerData.API.Public.Service;
using ESFA.DC.SubmitLearnerData.API.Public.Service.Providers;
using ESFA.DC.SubmitLearnerData.API.Public.Utils.Modules;

namespace ESFA.DC.SubmitLearnerData.API.Public.Modules
{
    public class APIModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<AzureStorageRepositoryService>().As<IRepositoryService>();
            containerBuilder.RegisterType<APICacheRetrievalService>().As<IAPICacheRetrievalService>();
            containerBuilder.RegisterType<FileProviderService>().As<IFileProviderService>();

            containerBuilder.RegisterType<ApplicationVersionsProvider>().As<IApplicationVersionsProvider>();
            containerBuilder.RegisterType<ReferenceDataVersionProvider>().As<IReferenceDataVersionProvider>();

            containerBuilder.RegisterModule<PollyModule>();

            containerBuilder.RegisterType<AzureStorageFileService>().As<IFileService>();

            containerBuilder.Register(context =>
            {
                var apiConfiguration = context.Resolve<APIConfiguration>();
                return new AzureStorageFileServiceConfiguration
                {
                    ConnectionString = apiConfiguration.AzureStorageConnectionString
                };
            }).As<IAzureStorageFileServiceConfiguration>();
        }
    }
}
