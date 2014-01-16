using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using YearEndParty.Core;
using YearEndParty.Web.Models;

namespace YearEndParty.Web.Controllers
{
    public class LuckyDrawController : Controller
    {
        public ActionResult Index()
        {
            dynamic prizeType;
            int counter;
            using (var context = new bodb_luckyEntities())
            {
                counter = context.Winners.Count();

                prizeType = (from t in context.PrizeSettings
                             where t.PrizeType < 5 && (t.ParentType == 0 || t.PrizeType == t.ParentType)
                             select new PrizeTypeModel
                             {
                                 PrizeType = t.PrizeType,
                                 Name = t.Name,
                                 //Quantity = t.PrizeType != 1 ? t.Quantity * 3 : t.Quantity,
                                 Quantity = t.Quantity,
                                 CurrentPrize = t.CurrentPrize
                             }).ToList();
            }
            ViewData["counter"] = counter;
            return View("index", prizeType);
        }

        public ActionResult Lucky(int id)
        {
            var prizeType = id;
            var prizeList = new List<PrizeTypeModel>();
            var prizeName = string.Empty;

            if (prizeType > 0)
            {
                using (var context = new bodb_luckyEntities())
                {
                    var prizeTmp = context.PrizeSettings.SingleOrDefault(t => t.PrizeType == prizeType);
                    if (prizeTmp != null)
                    {
                        prizeName = prizeTmp.Name;
                    }

                    prizeList = (from t in context.PrizeSettings
                                 where t.ParentType == prizeType || t.PrizeType == prizeType
                                 select new PrizeTypeModel
                                 {
                                     PrizeType = t.PrizeType,
                                     Name = t.Name,
                                     Prizes = from p in context.Prizes
                                              where p.PrizeType == t.PrizeType
                                              select new LuckyModel
                                                {
                                                    PrizeId = p.PrizeId,
                                                    PrizeName = p.Name,
                                                    Winners =
                                                       from wi in context.Winners
                                                       from st in context.Staffs
                                                       from dept in context.Departments
                                                       where wi.StaffId == st.StaffId
                                                              && st.DepartmentId == dept.DepartmentId
                                                              && wi.PrizeId == p.PrizeId
                                                       select new WinnerModel
                                                       {
                                                           StaffId = st.StaffId,
                                                           Department = dept.ShortName,
                                                           Nick = st.Nick,
                                                           FullName = st.FullName
                                                       }
                                                }
                                 }).ToList();


                }

                ViewData.Add("prizeName", prizeName);
            }

            return View(prizeList);
        }

        public ActionResult Winner()
        {
            List<WinnerPrize> winnerPrize;
            using (var context = new bodb_luckyEntities())
            {
                winnerPrize = (from t in context.PrizeSettings
                               orderby (t.PrizeType)
                               select new WinnerPrize
                               {
                                   PrizeType = t.PrizeType,
                                   TypeName = t.Name,
                                   Winners = (from w in context.Winners
                                              from s in context.Staffs
                                              from p in context.Prizes
                                              from d in context.Departments
                                              where w.StaffId == s.StaffId
                                               && w.PrizeId == p.PrizeId
                                               && p.PrizeType == t.PrizeType
                                               && s.DepartmentId == d.DepartmentId
                                              select new WinnerModel
                                              {
                                                  StaffId = w.StaffId,
                                                  Code = s.StaffCode,
                                                  FullName = s.FullName,
                                                  PrizeName = p.Name,
                                                  Department = d.Name,
                                                  Nick = s.Nick,
                                                  UsignName = s.UnsignName
                                              })
                               }).ToList();

            }
            return View(winnerPrize);
        }
    }
}