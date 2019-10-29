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

            this.commonRepository.AddPrizes(prizes);
        }


        private void GenerateSetting()
        {
            var setting = new Setting { RandomFrom = 100, RandomTo = 200, Excluded = "110,120,130,140", StartIndex = 0 };
            this.commonRepository.AddSetting(setting);
        }
    }
}
