using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyDraw.Model
{
    public class SeedModel
    {
        public void GenerateSeedData()
        {
            GenerateSetting();
            GeneratePrizes();
        }

        private void GeneratePrizes() {
            using (var db = new LuckyDrawEntities())
            {
                if (db.Prizes.Count() == 0)
                {
                    var prizes = new List<Prize>
                    {
                        new Prize { PrizeName = "Gold", PrizeIndex = 1, RoundIndex = 1, RoundName = "Gold Prize" },
                        new Prize { PrizeName = "Gold", PrizeIndex = 2, RoundIndex = 1, RoundName = "Gold Prize" },
                        new Prize { PrizeName = "Gold", PrizeIndex = 3, RoundIndex = 1, RoundName = "Gold Prize" },
                        new Prize { PrizeName = "Gold", PrizeIndex = 4, RoundIndex = 1, RoundName = "Gold Prize" },
                        new Prize { PrizeName = "Gold", PrizeIndex = 5, RoundIndex = 1, RoundName = "Gold Prize" },
                        new Prize { PrizeName = "Iphone", PrizeIndex = 6, RoundIndex = 2, RoundName = "Silver Prize" },
                        new Prize { PrizeName = "Iphone", PrizeIndex = 7, RoundIndex = 2, RoundName = "Silver Prize" },
                        new Prize { PrizeName = "Iphone", PrizeIndex = 8, RoundIndex = 2, RoundName = "Silver Prize" },
                        new Prize { PrizeName = "Iphone", PrizeIndex = 9, RoundIndex = 2, RoundName = "Silver Prize" },
                        new Prize { PrizeName = "Iphone", PrizeIndex = 10, RoundIndex = 2, RoundName = "Silver Prize" },
                        new Prize { PrizeName = "5 Million", PrizeIndex = 11, RoundIndex = 3, RoundName = "Cash Prize" },
                        new Prize { PrizeName = "5 Million", PrizeIndex = 12, RoundIndex = 3, RoundName = "Cash Prize" },
                        new Prize { PrizeName = "5 Million", PrizeIndex = 13, RoundIndex = 3, RoundName = "Cash Prize" },
                        new Prize { PrizeName = "5 Million", PrizeIndex = 14, RoundIndex = 3, RoundName = "Cash Prize" },
                        new Prize { PrizeName = "5 Million", PrizeIndex = 15, RoundIndex = 3, RoundName = "Cash Prize" },
                        new Prize { PrizeName = "5 Million", PrizeIndex = 16, RoundIndex = 3, RoundName = "Cash Prize" },
                        new Prize { PrizeName = "5 Million", PrizeIndex = 17, RoundIndex = 3, RoundName = "Cash Prize" },
                        new Prize { PrizeName = "5 Million", PrizeIndex = 18, RoundIndex = 3, RoundName = "Cash Prize" },
                        new Prize { PrizeName = "5 Million", PrizeIndex = 19, RoundIndex = 3, RoundName = "Cash Prize" },
                        new Prize { PrizeName = "5 Million", PrizeIndex = 20, RoundIndex = 3, RoundName = "Cash Prize" }
                    };

                    foreach (var p in prizes)
                    {
                        db.Prizes.Add(p);
                    }

                    db.SaveChanges();
                }
            }
        }


        private void GenerateSetting() {
            using (var db = new LuckyDrawEntities())
            {
                if (db.Settings.Count() == 0)
                {
                    var setting = new Setting { RandomFrom = 100, RandomTo = 200, Excluded = "110,120,130,140", StartIndex = 0 };
                    db.Settings.Add(setting);

                    db.SaveChanges();
                }
            }
        }
    }
}
