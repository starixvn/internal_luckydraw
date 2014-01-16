using System.Linq;
using System.Web.Mvc;
using YearEndParty.Core;
using YearEndParty.Web.Models;
using Zen.Barcode;
using System.Web.Configuration;
using System;

namespace YearEndParty.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            dynamic prizeTypeList;
            bool isCountAdhoc = Convert.ToBoolean(WebConfigurationManager.AppSettings["adhoc"]);

            using (var context = new bodb_luckyEntities())
            {
                ViewData["counter"] = context.Winners.Count();

                if (isCountAdhoc)
                {
                    prizeTypeList = context.PrizeSettings
                        .Where(t =>
                               t.PrizeType == 1
                            || t.PrizeType == 2
                            || t.PrizeType == 3
                            || t.PrizeType == 4
                            || t.PrizeType == 5
                            && (t.ParentType == 0 || t.PrizeType == t.ParentType))
                        .Select(t => new PrizeTypeModel
                        {
                            PrizeType = t.PrizeType,
                            Name = t.Name,
                            Quantity = t.Quantity,
                            CurrentPrize = t.CurrentPrize
                        }).ToList();
                }
                else
                {
                    prizeTypeList = context.PrizeSettings
                        .Where(t =>
                               t.PrizeType == 2
                            || t.PrizeType == 3
                            || t.PrizeType == 4
                            || t.PrizeType == 5
                            && (t.ParentType == 0 || t.PrizeType == t.ParentType))
                        .Select(t => new PrizeTypeModel
                        {
                            PrizeType = t.PrizeType,
                            Name = t.Name,
                            Quantity = t.Quantity,
                            CurrentPrize = t.CurrentPrize
                        }).ToList();
                }

                #region rem
                //prizeTypeList = (from t in context.PrizeSettings
                //             where t.PrizeType == 2
                //                || t.PrizeType == 3
                //                || t.PrizeType == 4
                //                || t.PrizeType == 6
                //                && (t.ParentType == 0 || t.PrizeType == t.ParentType)
                //             select new PrizeTypeModel
                //             {
                //                 PrizeType = t.PrizeType,
                //                 Name = t.Name,
                //                 //Quantity = t.PrizeType != 1 ? t.Quantity * 3 : t.Quantity,
                //                 Quantity = t.Quantity,
                //                 CurrentPrize = t.CurrentPrize
                //             }).ToList();
                #endregion
            }

            return View(prizeTypeList);
        }

        public ActionResult PartyProgram()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult PartyAgenda()
        {
            return View();
        }

        public ActionResult DinnerMenu()
        {
            return View();
        }

        public FileResult BarcodeImage(string barcode)
        {
            var barcodeDrawer = new Code128BarcodeDraw(Code128Checksum.Instance);
            var image = barcodeDrawer.Draw(barcode, 50, 2);
            return File(Utilities.ImageToBytes(image, true), "image/jpeg");
        }
    }
}