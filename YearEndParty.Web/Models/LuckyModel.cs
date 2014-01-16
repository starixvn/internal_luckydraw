using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YearEndParty.Web.Models
{
    public class LuckyModel
    {
        public int PrizeId { get; set; }
        public string PrizeName { get; set; }
        public IQueryable<WinnerModel> Winners { get; set; }
    }
}