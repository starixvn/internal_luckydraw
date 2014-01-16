using System.Linq;

namespace YearEndParty.Web.Models
{
    public class WinnerPrize
    {
        public int PrizeType { get; set; }
        public string TypeName { get; set; }
        public IQueryable<WinnerModel> Winners { get; set; }
    }

    public class WinnerModel
    {
        public int StaffId { get; set; }
        public string Code { get; set; }
        public string FullName { get; set; }
        public string PrizeName { get; set; }
        public string PrizeType { get; set; }
        public string Nick { get; set; }
        public string Department { get; set; }
        public string UsignName { get; set; }
    }
}