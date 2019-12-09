using System;

namespace LuckyDraw.Model
{
    public class ViewLog
    {
        public int Flag { get; set; }
        public int PrizeIndex { get; set; }
        public string PrizeName { get; set; }
        public string Random { get; set; }
        public string RoundName { get; set; }
        public DateTime UpdatedTime { get; set; }
    }
}
