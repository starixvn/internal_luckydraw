using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyDraw.Model
{
    class ViewLog
    {
        public int Flag { get; set; }
        public int PrizeIndex { get; set; }
        public string PrizeName { get; set; }
        public string Random { get; set; }
        public string RoundName { get; set; }
        public DateTime? UpdatedTime { get; set; }
    }
}
