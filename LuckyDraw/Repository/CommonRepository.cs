using LuckyDraw.DBContexts;
using LuckyDraw.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LuckyDraw.Repository
{
    public class CommonRepository : ICommonRepository
    {
        private readonly ILuckyDrawEntity luckyDrawEntity;

        public CommonRepository():this(null)
        {
        }

        public CommonRepository(ILuckyDrawEntity luckyDrawEntity)
        {
            this.luckyDrawEntity = luckyDrawEntity ?? new LuckyDrawEntity();
        }

        public void UpdatePrize(IEnumerable<ViewPrize> prizes) {
            using (var db = luckyDrawEntity.GetDBContext())
            {
                var allIds = prizes.Select(i => i.Id);
                var deletedPrizes = db.Prizes.Where(i => !allIds.Contains(i.Id));
                db.Prizes.RemoveRange(deletedPrizes);
                db.SaveChanges();

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

                    db.SaveChanges();
                }
            }
        }

        public void UpdateIgnoredList(IEnumerable<Setting> settings)
        {
            var setting = settings.First();
            using (var db = luckyDrawEntity.GetDBContext())
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
            using (var db = luckyDrawEntity.GetDBContext())
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

        public void UpdateRandomTime(IEnumerable<Setting> settings)
        {
            var setting = settings.First();
            using (var db = luckyDrawEntity.GetDBContext())
            {
                if (db.Settings.Count() != 0)
                {
                    var s = db.Settings.First();
                    s.RandomAnimationDuration = setting.RandomAnimationDuration;
                    s.RandomTransitionDuration = setting.RandomTransitionDuration;
                    db.SaveChanges();
                }
            }
        }

        public void UpdateRunningIndex(int runningIndex) {
            using (var db = luckyDrawEntity.GetDBContext())
            {
                if (db.Settings.Count() != 0) {
                    var s = db.Settings.First();
                    s.RunningIndex = runningIndex;
                    db.SaveChanges();
                }
            }
        }

        public void StartNewGame() {
            using (var db = luckyDrawEntity.GetDBContext())
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

        public void UpdateRandom(int id, string random) {
            using (var db = luckyDrawEntity.GetDBContext())
            {
                var r = db.Results.Find(id);
                r.Random = random;
                r.Status = 1;
                r.UpdatedTime = DateTime.Now;
                db.SaveChanges();
            }
        }

        public void WriteLog(int resultId, string random)
        {
            using (var db = luckyDrawEntity.GetDBContext())
            {
                var l = new Log
                {
                    ResultId = resultId,
                    Random = random
                };

                db.Logs.Add(l);
                db.SaveChanges();
            }
        }

        public void ResetRandom(int resultId)
        {
            using (var db = luckyDrawEntity.GetDBContext())
            {
                var r = db.Results.Find(resultId);
                r.Random = string.Empty;
                r.Status = 0;
                r.UpdatedTime = DateTime.Now;
                db.SaveChanges();
            }
        }

        public bool IsDBReady()
        {
            using (var db = luckyDrawEntity.GetDBContext())
            {
                return db.Results.Count() != 0 && db.Settings.Count() != 0;                
            }
        }

        public Setting GetSetting()
        {
            using (var db = luckyDrawEntity.GetDBContext())
            {
                return db.Settings.First();
            }
        }

        public List<Result> GetCurrentResultList()
        {
            using (var db = luckyDrawEntity.GetDBContext())
            {
                if (db.Results.Count() > 0) { 
                    var flag = db.Results.Max(i => i.Flag);
                    return db.Results.Where(i => i.Flag == flag).ToList();
                }
                else
                {
                    return new List<Result>();
                }
            }
        }

        public List<Prize> GetPrizeList()
        {
            using (var db = luckyDrawEntity.GetDBContext())
            {
               return db.Prizes.ToList();
            }
        }

        public List<Setting> GetSettingList()
        {
            using (var db = luckyDrawEntity.GetDBContext())
            {
                return db.Settings.ToList();
            }
        }

        public List<Result> GetResultList()
        {
            return GetCurrentResultList();
        }

        public List<Result> GetLogList() {
            using (var db = luckyDrawEntity.GetDBContext())
            {
                var logs = new List<Result>();
                var logData = db.Logs.ToList();
                foreach (var log in logData)
                {
                    var r = db.Results.Find(log.ResultId);
                    r.Random = log.Random;

                    var logResult = new Result
                    {
                        PrizeIndex = r.PrizeIndex,
                        PrizeName = r.PrizeName,
                        Random = r.Random,
                        RoundName = r.RoundName
                    };
                    logs.Add(logResult);
                }
                
                return logs;
            }
        }

        public void AddPrizes(List<Prize> prizes)
        {
            using (var db = luckyDrawEntity.GetDBContext())
            {
                db.Prizes.AddRange(prizes);
                db.SaveChanges();
            }
        }

        public bool IsEmptyPrize()
        {
            using (var db = luckyDrawEntity.GetDBContext())
            {
                return db.Prizes.Count() == 0;
            }
        }

        public void AddSetting(Setting setting)
        {
            using (var db = luckyDrawEntity.GetDBContext())
            {
                db.Settings.Add(setting);
                db.SaveChanges();
            }
        }

        public bool IsEmptySetting()
        {
            using (var db = luckyDrawEntity.GetDBContext())
            {
                return db.Settings.Count() == 0;
            }
        }
    }
}
