using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YearEndParty.Web.Models
{
    public class PrizeTypeModel
    {
        public int PrizeType { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int CurrentPrize { get; set; }
        public IQueryable<LuckyModel> Prizes { get; set; }
    }
}