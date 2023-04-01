using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using AuctionSystem.EntityFrameworkCore;
using AuctionSystem.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace AuctionSystem.Web.Tests
{
    [DependsOn(
        typeof(AuctionSystemWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class AuctionSystemWebTestModule : AbpModule
    {
        public AuctionSystemWebTestModule(AuctionSystemEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AuctionSystemWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(AuctionSystemWebMvcModule).Assembly);
        }
    }
}