using LuckyDraw.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LuckyDraw.Repository
{
    public static class CommonRepository
    {
        public static void UpdatePrize(IEnumerable<ViewPrize> prizes) {
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

        public static void UpdateIgnoredList(IEnumerable<Setting> settings)
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

        public static void UpdateRange(IEnumerable<Setting> settings) {
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

        public static void UpdateStartIndex(int startIndex) {
            using (var db = new LuckyDrawEntities())
            {
                if (db.Settings.Count() != 0) {
                    var s = db.Settings.First();
                    s.StartIndex = startIndex;
                    db.SaveChanges();
                }
            }
        }

        public static void StartNewGame() {
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

        public static void UpdateRandom(int id, string random) {
            using (var db = new LuckyDrawEntities())
            {
                var r = db.Results.Find(id);
                r.Random = random;
                r.Status = 1;
                r.UpdatedTime = DateTime.Now;
                db.SaveChanges();
            }
        }

        public static bool IsDBReady()
        {
            using (var db = new LuckyDrawEntities())
            {
                return db.Results.Count() != 0 && db.Settings.Count() != 0;                
            }
        }

        public static Setting GetSetting()
        {
            using (var db = new LuckyDrawEntities())
            {
                return db.Settings.First();
            }
        }

        public static List<Result> GetCurrentResultList(int startIndex)
        {
            using (var db = new LuckyDrawEntities())
            {
                if (db.Results.Count() > 0) { 
                    var flag = db.Results.Max(i => i.Flag);
                    return db.Results.Where(i => i.Flag == flag && i.PrizeIndex >= startIndex).ToList();
                }
                else
                {
                    return new List<Result>();
                }
            }
        }

        public static List<Prize> GetPrizeList()
        {
            using (var db = new LuckyDrawEntities())
            {
               return db.Prizes.ToList();
            }
        }

        public static List<Setting> GetSettingList()
        {
            using (var db = new LuckyDrawEntities())
            {
                return db.Settings.ToList();
            }
        }

        public static List<Result> GetResultList()
        {
            return GetCurrentResultList(1);
        }

        public static List<Result> GetLogList() {
            using (var db = new LuckyDrawEntities())
            {
                return db.Results.ToList();
            }
        }

        public static void AddPrizes(List<Prize> prizes)
        {
            using (var db = new LuckyDrawEntities())
            {
                db.Prizes.AddRange(prizes);
                db.SaveChanges();
            }
        }

        public static bool IsEmptyPrize()
        {
            using (var db = new LuckyDrawEntities())
            {
                return db.Prizes.Count() == 0;
            }
        }

        public static void AddSetting(Setting setting)
        {
            using (var db = new LuckyDrawEntities())
            {
                db.Settings.Add(setting);
                db.SaveChanges();
            }
        }

        public static bool IsEmptySetting()
        {
            using (var db = new LuckyDrawEntities())
            {
                return db.Settings.Count() == 0;
            }
        }
    }
}
