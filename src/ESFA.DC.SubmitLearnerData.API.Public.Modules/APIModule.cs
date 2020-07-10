using Autofac;
using ESFA.DC.FileService;
using ESFA.DC.FileService.Config;
using ESFA.DC.FileService.Config.Interface;
using ESFA.DC.FileService.Interface;
using ESFA.DC.SubmitLearnerData.API.Public.Interface;
using ESFA.DC.SubmitLearnerData.API.Public.Model;
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
            containerBuilder.RegisterType<ApplicationVersionsProvider>().As<IProvider<ApplicationVersions>>();
            containerBuilder.RegisterType<APICacheRetrievalService>().As<IAPICacheRetrievalService>();
            containerBuilder.RegisterType<CloudBlobContainerFactory>().As<ICloudBlobContainerFactory>();
            containerBuilder.RegisterType<FileProviderService>().As<IFileProviderService>();

            containerBuilder.RegisterModule<PollyModule>();

            containerBuilder.RegisterType<AzureStorageFileService>().As<IFileService>();

            containerBuilder.Register(context =>
            {
                var apiConfiguration = context.Resolve<IAPIConfiguration>();
                return new AzureStorageFileServiceConfiguration
                {
                    ConnectionString = apiConfiguration.AzureStorageConnectionString
                };
            }).As<IAzureStorageFileServiceConfiguration>();
        }
    }
}
