﻿using Abp.Application.Services.Dto;
using AuctionSystem.Authorization.Users;
using AuctionSystem.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSystem.Products.Dto
{
    public class ProductDto : EntityDto<long?>
    {
        public string Name { get; set; }
        public int TenantId { get; set; }
        public double? BasePrice { get; set; }
        public byte[] Image { get; set; }
        public ProductStatus Status { get; set; }
        public bool IsProductSale { get; set; }
        public DateTime? AuctionStartAt { get; set; }
        public DateTime? AuctionEndAt { get; set; }
        public long? OwnerId { get; set; }
        public long? LocationId { get; set; }
        public long? CategoryId { get; set; }
        public string ImageBase64 { get; set; }
        public string LocationName {  get; set; }
        public string CategoryName { get; set; }
        public string SoldTo { get; set; }
        public string BuyFrom { get; set; }
        public double? SoldPrice { get; set; }
    }
}
