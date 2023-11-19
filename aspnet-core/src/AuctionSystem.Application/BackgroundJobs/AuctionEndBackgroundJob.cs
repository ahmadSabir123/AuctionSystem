using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using Abp.Threading;
using AuctionSystem.Enums;
using AuctionSystem.MultiTenancy;
using AuctionSystem.Products.Dto;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSystem.BackgroundJobs
{
    public class AuctionEndBackgroundJob : AsyncBackgroundJob<ProductDto>, ITransientDependency
    {
        private readonly IRepository<AuctionSystem.Product.Product, long> _productRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IBackgroundJobManager _backgroundJobManager;

        public TenantManager TenantManager { get; set; }
        public IAbpSession AbpSession { get; set; }
        public AuctionEndBackgroundJob(IUnitOfWorkManager unitOfWorkManager, IBackgroundJobManager backgroundJobManager, IRepository<Product.Product, long> productRepository)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _backgroundJobManager = backgroundJobManager;
            _productRepository = productRepository;
        }
        [UnitOfWork]
        [AutomaticRetry(Attempts = 0)]
        public override async Task ExecuteAsync(ProductDto record)
        {
            var tenants = TenantManager.Tenants.ToList();
            foreach (var tenant in tenants)
            {
                AsyncHelper.RunSync(() => EndAuction(tenant.Id, record));
            }
        }
        public async Task EndAuction(int tenantId, ProductDto product)
        {
            using (_unitOfWorkManager.Current.SetTenantId(tenantId))
            {
                var record = await _productRepository.FirstOrDefaultAsync(x => x.Id == product.Id);
                record.Status = ProductStatus.AuctionDone;
                await _productRepository.UpdateAsync(record);
            }    
        }
    }
}
