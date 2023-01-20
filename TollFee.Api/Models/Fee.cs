using System;
using System.Collections.Generic;

#nullable disable

namespace TollFee.Api.Models
{
    public partial class Fee
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public long FromMinute { get; set; }
        public long ToMinute { get; set; }
        public decimal Price { get; set; }
    }
}
