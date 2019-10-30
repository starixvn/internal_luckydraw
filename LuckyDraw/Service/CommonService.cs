using LuckyDraw.Model;
using LuckyDraw.Repository;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LuckyDraw.Service
{
    public static class CommonService
    {
        public static void UpdateIgnoredList(IEnumerable<Setting> settings)
        {
            CommonRepository.UpdateIgnoredList(settings);
        }

        public static void UpdatePrize(IEnumerable<ViewPrize> viewPrizes)
        {
            CommonRepository.UpdatePrize(viewPrizes);
        }

        public static void UpdateRange(IEnumerable<Setting> settings)
        {
            CommonRepository.UpdateRange(settings);
        }

        public static void UpdateStartIndex(int startIndex)
        {
            CommonRepository.UpdateStartIndex(startIndex);
        }

        public static void StartNewGame()
        {
            CommonRepository.StartNewGame();
        }

        public static ObservableCollection<ViewPrize> GetPrizeList()
        {
            var prizeList = CommonRepository.GetPrizeList();
                var collection = new ObservableCollection<ViewPrize>();
                foreach (var h in prizeList)
                {
                    var p = new ViewPrize();
                    p.Id = h.Id;
                    p.PrizeIndex = h.PrizeIndex;
                    p.PrizeName = h.PrizeName;
                    p.RoundIndex = h.RoundIndex;
                    p.RoundName = h.RoundName;

                    collection.Add(p);
                }

                return collection;
        }

        public static ObservableCollection<Setting> GetSettingList()
        {
            var settingList = CommonRepository.GetSettingList();
            var collection = new ObservableCollection<Setting>();
            foreach (var h in settingList)
            {
                collection.Add(h);
            }

            return collection;
        }

        public static ObservableCollection<ViewResult> GetResultList()
        {
            var resultList = CommonRepository.GetResultList();
                var collection = new ObservableCollection<ViewResult>();
                foreach (var h in resultList.ToList())
                {
                    var r = new ViewResult();
                    r.PrizeIndex = h.PrizeIndex;
                    r.PrizeName = h.PrizeName;
                    r.Random = h.Random;
                    r.RoundName = h.RoundName;

                    collection.Add(r);
                }

                return collection;
        }

        public static ObservableCollection<ViewLog> GetLogList()
        {
            var logList = CommonRepository.GetLogList();
            var collection = new ObservableCollection<ViewLog>();
            foreach (var h in logList)
            {
                var r = new ViewLog();
                r.Flag = h.Flag;
                r.PrizeIndex = h.PrizeIndex;
                r.PrizeName = h.PrizeName;
                r.Random = h.Random;
                r.RoundName = h.RoundName;
                r.UpdatedTime = h.UpdatedTime;

                collection.Add(r);
            }

            return collection;
        }
    }
}
