using LuckyDraw.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyDraw.Service
{
    public interface ICommonService
    {
        void UpdateIgnoredList(IEnumerable<Setting> settings);
        void UpdatePrize(IEnumerable<ViewPrize> viewPrizes);
        void UpdateRange(IEnumerable<Setting> settings);
        void UpdateRandomTime(IEnumerable<Setting> settings);
        void UpdateStartIndex(int startIndex);
        void StartNewGame();
        List<ViewPrize> GetPrizeList();
        List<Setting> GetSettingList();
        List<ViewResult> GetResultList();
        List<ViewLog> GetLogList();
    }
}
