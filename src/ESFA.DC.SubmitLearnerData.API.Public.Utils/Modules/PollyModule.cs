using Autofac;
using ESFA.DC.SubmitLearnerData.API.Public.Utils.Polly;
using ESFA.DC.SubmitLearnerData.API.Public.Utils.Polly.Interface;

namespace ESFA.DC.SubmitLearnerData.API.Public.Utils.Modules
{
    public class PollyModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<PollyPolicies>().As<IPollyPolicies>();
        }
    }
}
