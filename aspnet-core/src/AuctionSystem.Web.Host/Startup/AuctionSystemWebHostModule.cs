using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using AuctionSystem.Configuration;

namespace AuctionSystem.Web.Host.Startup
{
    [DependsOn(
       typeof(AuctionSystemWebCoreModule))]
    public class AuctionSystemWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public AuctionSystemWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AuctionSystemWebHostModule).GetAssembly());
        }
    }
}
