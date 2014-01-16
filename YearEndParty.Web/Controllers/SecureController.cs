using System.Linq;
using System.Web.Mvc;
using YearEndParty.Core;
using YearEndParty.Web.Models;
using Zen.Barcode;

namespace YearEndParty.Web.Controllers
{
    public class SecureController : Controller
    {
        [NonAction]
        public ActionResult Label(int? id)
        {
            var context = new bodb_luckyEntities();
            var model = (from i in context.Staffs
                         where i.DepartmentId == id || id == null
                         orderby i.StaffCode
                         select new Models.LabelModel
                                    {
                                        Name = i.FullName,
                                        Barcode = i.StaffCode,
                                        Nick = i.Nick
                                    }).ToList();
            return View(model);
        }

        [NonAction]
        public ActionResult Label2(int? id)
        {
            var context = new bodb_luckyEntities();
            var model = (from i in context.Staffs
                         where i.DepartmentId == id || id == null
                         orderby i.StaffCode
                         select new Models.LabelModel
                         {
                             StaffId = i.StaffId,
                             Name = i.FullName,
                             Barcode = i.StaffCode

                         }).ToList();
            return View(model);
        }
    }
}