using LuckyDraw.Repository;
using System.Collections.Generic;

namespace LuckyDraw.Service
{
    public static class SeedService
    {
        public static void GenerateSeedData()
        {
            if (CommonRepository.IsEmptySetting())
            {
                GenerateSetting();
            }

            if (CommonRepository.IsEmptyPrize())
            {
                GeneratePrizes();
            }
        }

        private static void GeneratePrizes()
        {
            const string prizeName1 = "Gold";
            const string prizeName2 = "Iphone";
            const string prizeName3 = "5 Million";
            const string roundName1 = "Gold Prize";
            const string roundName2 = "Silver Prize";
            const string roundName3 = "Cash Prize";

            var prizes = new List<Prize>
            {
                new Prize { PrizeName = prizeName1, PrizeIndex = 1, RoundIndex = 1, RoundName = roundName1 },
                new Prize { PrizeName = prizeName1, PrizeIndex = 2, RoundIndex = 1, RoundName = roundName1 },
                new Prize { PrizeName = prizeName1, PrizeIndex = 3, RoundIndex = 1, RoundName = roundName1 },
                new Prize { PrizeName = prizeName1, PrizeIndex = 4, RoundIndex = 1, RoundName = roundName1 },
                new Prize { PrizeName = prizeName1, PrizeIndex = 5, RoundIndex = 1, RoundName = roundName1 },
                new Prize { PrizeName = prizeName2, PrizeIndex = 6, RoundIndex = 2, RoundName = roundName2 },
                new Prize { PrizeName = prizeName2, PrizeIndex = 7, RoundIndex = 2, RoundName = roundName2 },
                new Prize { PrizeName = prizeName2, PrizeIndex = 8, RoundIndex = 2, RoundName = roundName2 },
                new Prize { PrizeName = prizeName2, PrizeIndex = 9, RoundIndex = 2, RoundName = roundName2 },
                new Prize { PrizeName = prizeName2, PrizeIndex = 10, RoundIndex = 2, RoundName = roundName2 },
                new Prize { PrizeName = prizeName3, PrizeIndex = 11, RoundIndex = 3, RoundName = roundName3 },
                new Prize { PrizeName = prizeName3, PrizeIndex = 12, RoundIndex = 3, RoundName = roundName3 },
                new Prize { PrizeName = prizeName3, PrizeIndex = 13, RoundIndex = 3, RoundName = roundName3 },
                new Prize { PrizeName = prizeName3, PrizeIndex = 14, RoundIndex = 3, RoundName = roundName3 },
                new Prize { PrizeName = prizeName3, PrizeIndex = 15, RoundIndex = 3, RoundName = roundName3 },
                new Prize { PrizeName = prizeName3, PrizeIndex = 16, RoundIndex = 3, RoundName = roundName3 },
                new Prize { PrizeName = prizeName3, PrizeIndex = 17, RoundIndex = 3, RoundName = roundName3 },
                new Prize { PrizeName = prizeName3, PrizeIndex = 18, RoundIndex = 3, RoundName = roundName3 },
                new Prize { PrizeName = prizeName3, PrizeIndex = 19, RoundIndex = 3, RoundName = roundName3 },
                new Prize { PrizeName = prizeName3, PrizeIndex = 20, RoundIndex = 3, RoundName = roundName3 }
            };

            CommonRepository.AddPrizes(prizes);
        }


        private static void GenerateSetting()
        {
            var setting = new Setting { RandomFrom = 100, RandomTo = 200, Excluded = "110,120,130,140", StartIndex = 0 };
            CommonRepository.AddSetting(setting);
        }
    }
}
