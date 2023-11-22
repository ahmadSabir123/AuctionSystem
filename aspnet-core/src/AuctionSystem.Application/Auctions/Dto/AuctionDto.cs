using AuctionSystem.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSystem.Auctions.Dto
{
    public class AuctionDto
    {
        public double? Bid { get; set; }
        public int? UserId { get; set; }
        public long? ProductId { get; set; }
        public string ProductName {  get; set; }
        public string UserName {  get; set; }
        
        
    }
}
