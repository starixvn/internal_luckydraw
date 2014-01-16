using System.Linq;
using System.Web.Mvc;
using YearEndParty.Core;
using Zen.Barcode;

namespace YearEndParty.Web.Controllers
{
    public class ImageController : Controller
    {
        [OutputCache(Duration = 43200, VaryByParam = "*", Location = System.Web.UI.OutputCacheLocation.ServerAndClient)]
        public FileResult Handler(string type, int id)
        {
            byte[] buffer = null;
            using (var context = new bodb_luckyEntities())
            {
                if (type.ToLower().Equals("prize"))
                {
                    var prize = context.Prizes.SingleOrDefault(pr => pr.PrizeId == id);
                    buffer = prize.Image;
                }
                else if (type.ToLower().Equals("staff"))
                {
                    var staff = context.Staffs.SingleOrDefault(s => s.StaffId == id);
                    buffer = staff.Avatar;
                }
            }
            if (buffer != null)
            {
                var image = Utilities.ScaleImage(buffer, 80, 80);
                var imageConvert = Utilities.ImageToBytes(image);
                return File(imageConvert, "image/jpeg");

            }

            return null;
        }

        public FileResult Barcode(string barcode)
        {
            var zen = new Code128BarcodeDraw(Code128Checksum.Instance);
            var image = zen.Draw(barcode, 50, 120);
            return File(Utilities.ImageToBytes(image, true), "image/jpeg");
        }
    }
}