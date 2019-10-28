using LuckyDraw;
using LuckyDraw.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyDraw.Repository
{
    public class CommonRepository
    {
        public void UpdatePrize(IEnumerable<ViewPrize> prizes) {
            using (var db = new LuckyDrawEntities())
            {
                var allIds = prizes.Select(i => i.Id);
                var deletedPrizes = db.Prizes.Where(i => !allIds.Contains(i.Id));
                db.Prizes.RemoveRange(deletedPrizes);

                foreach (var item in prizes)
                {
                    var prize = db.Prizes.Find(item.Id);
                    if (prize == null)
                    {
                        prize = new Prize();
                        db.Prizes.Add(prize);
                    }

                    prize.PrizeIndex = item.PrizeIndex;
                    prize.PrizeName = item.PrizeName.Trim();
                    prize.RoundIndex = item.RoundIndex;
                    prize.RoundName = item.RoundName.Trim();
                }

                db.SaveChanges();
            }
        }

        public void UpdateIgnoredList(IEnumerable<Setting> settings)
        {
            var setting = settings.First();
            using (var db = new LuckyDrawEntities())
            {
                if (db.Settings.Count() != 0)
                {
                    var s = db.Settings.First();
                    s.Excluded = setting.Excluded.Trim();
                    db.SaveChanges();
                }
            }
        }

        public void UpdateRange(IEnumerable<Setting> settings) {
            var setting = settings.First();
            using (var db = new LuckyDrawEntities())
            {
                if (db.Settings.Count() != 0)
                {
                    var s = db.Settings.First();
                    s.RandomFrom = setting.RandomFrom;
                    s.RandomTo = setting.RandomTo;
                    db.SaveChanges();
                }
            }
        }

        public void UpdateStartIndex(int startIndex) {
            using (var db = new LuckyDrawEntities())
            {
                if (db.Settings.Count() != 0) {
                    var s = db.Settings.First();
                    s.StartIndex = startIndex;
                    db.SaveChanges();
                }
            }
        }

        public void StartNewGame() {
            using (var db = new LuckyDrawEntities())
            {
                if (db.Prizes.Count() == 0)
                {
                    return;
                }

                var flag = db.Results.Count() > 0 ? db.Results.Max(i => i.Flag) + 1 : 1;
                foreach (var item in db.Prizes) {
                    var r = new Result();
                    r.PrizeIndex = item.PrizeIndex;
                    r.PrizeName = item.PrizeName;
                    r.RoundIndex = item.RoundIndex;
                    r.RoundName = item.RoundName;
                    r.PrizeId = item.Id;
                    r.Flag = flag;
                    r.Status = 0;
                    r.Random = "000";

                    db.Results.Add(r);
                }

                db.SaveChanges();
            }
        }
    }
}
