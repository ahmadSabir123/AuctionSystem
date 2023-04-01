using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using AuctionSystem.Authorization;

namespace AuctionSystem
{
    [DependsOn(
        typeof(AuctionSystemCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class AuctionSystemApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<AuctionSystemAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(AuctionSystemApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
