using LuckyDraw.Repository;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;

namespace LuckyDraw.Service
{
    public class SeedService : ISeedService
    {
        private readonly ICommonRepository commonRepository;

        public SeedService() : this(null)
        {
        }

        public SeedService(ICommonRepository commonRepository)
        {
            this.commonRepository = commonRepository ?? new CommonRepository();
        }

        public void GenerateSeedData()
        {
            if (commonRepository.IsEmptySetting())
            {
                GenerateSetting();
            }

            if (commonRepository.IsEmptyPrize())
            {
                GeneratePrizes();
            }
        }

        private void GeneratePrizes()
        {
            var path = String.Format("{0}\\AppData\\InitialData\\Lucky2020.xlsx", AppDomain.CurrentDomain.BaseDirectory);
            var prizes = new List<Prize>();
            using (MemoryStream ms = new MemoryStream(File.ReadAllBytes(path)))
            {
                IWorkbook wb = NPOI.SS.UserModel.WorkbookFactory.Create(ms);
                NPOI.SS.UserModel.ISheet sheet = wb.GetSheetAt(0);
                for (int row = 0; row <= sheet.LastRowNum; row++)
                {
                    var sheetRow = sheet.GetRow(row);
                    if (sheetRow != null)
                    {
                        var prize = new Prize
                        {
                            PrizeIndex = Convert.ToInt32(sheetRow.GetCell(0).NumericCellValue),
                            PrizeName = sheetRow.GetCell(1).StringCellValue,
                            RoundIndex = Convert.ToInt32(sheetRow.GetCell(2).NumericCellValue),
                            RoundName = sheetRow.GetCell(3).StringCellValue,
                        };
                        prizes.Add(prize);
                    }
                }
            }
            
            commonRepository.AddPrizes(prizes);
        }


        private void GenerateSetting()
        {
            var setting = new Setting { RandomFrom = 1, RandomTo = 700, Excluded = "110,120,130,140", RunningIndex = 0, RandomAnimationDuration = 1, RandomTransitionDuration = 1 };
            commonRepository.AddSetting(setting);
        }
    }
}
