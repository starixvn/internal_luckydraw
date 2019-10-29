using LuckyDraw.Repository;
using System.Collections.Generic;

namespace LuckyDraw.Service
{
    class SeedService
    {
        private readonly CommonRepository commonRepository;

        public SeedService()
        {
            this.commonRepository = new CommonRepository();
        }

        public void GenerateSeedData()
        {
            if (this.commonRepository.IsEmptySetting())
            {
                GenerateSetting();
            }

            if (this.commonRepository.IsEmptyPrize())
            {
                GeneratePrizes();
            }
        }

        private void GeneratePrizes()
        {
            var prizeName1 = "Gold";
            var prizeName2 = "Iphone";
            var prizeName3 = "5 Million";
            var roundName1 = "Gold Prize";
            var roundName2 = "Silver Prize";
            var roundName3 = "Cash Prize";

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

            this.commonRepository.AddPrizes(prizes);
        }


        private void GenerateSetting()
        {
            var setting = new Setting { RandomFrom = 100, RandomTo = 200, Excluded = "110,120,130,140", StartIndex = 0 };
            this.commonRepository.AddSetting(setting);
        }
    }
}
