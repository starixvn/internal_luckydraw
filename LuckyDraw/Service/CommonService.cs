using LuckyDraw.Model;
using LuckyDraw.Repository;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LuckyDraw.Service
{
    public class CommonService : ICommonService
    {
        private readonly ICommonRepository commonRepository;

        public CommonService():this(null)
        {
        }

        public CommonService(ICommonRepository commonRepository)
        {
            this.commonRepository = commonRepository ?? new CommonRepository();
        }

        public void UpdateIgnoredList(IEnumerable<Setting> settings)
        {
            commonRepository.UpdateIgnoredList(settings);
        }

        public void UpdatePrize(IEnumerable<ViewPrize> viewPrizes)
        {
            commonRepository.UpdatePrize(viewPrizes);
        }

        public void UpdateRange(IEnumerable<Setting> settings)
        {
            commonRepository.UpdateRange(settings);
        }

        public void UpdateRandomTime(IEnumerable<Setting> settings)
        {
            commonRepository.UpdateRandomTime(settings);
        }

        public void UpdateStartIndex(int startIndex)
        {
            commonRepository.UpdateStartIndex(startIndex);
        }

        public void StartNewGame()
        {
            commonRepository.StartNewGame();
        }

        public ObservableCollection<ViewPrize> GetPrizeList()
        {
            var prizeList = commonRepository.GetPrizeList();
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

        public ObservableCollection<Setting> GetSettingList()
        {
            var settingList = commonRepository.GetSettingList();
            var collection = new ObservableCollection<Setting>();
            foreach (var h in settingList)
            {
                collection.Add(h);
            }

            return collection;
        }

        public ObservableCollection<ViewResult> GetResultList()
        {
            var resultList = commonRepository.GetResultList();
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

        public ObservableCollection<ViewLog> GetLogList()
        {
            var logList = commonRepository.GetLogList();
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
