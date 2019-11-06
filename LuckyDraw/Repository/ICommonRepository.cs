using LuckyDraw.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyDraw.Repository
{
    public interface ICommonRepository
    {
        void UpdatePrize(IEnumerable<ViewPrize> prizes);
        void UpdateIgnoredList(IEnumerable<Setting> settings);
        void UpdateRange(IEnumerable<Setting> settings);
        void UpdateRandomTime(IEnumerable<Setting> settings);
        void UpdateStartIndex(int startIndex);
        void StartNewGame();
        void UpdateRandom(int id, string random);
        bool IsDBReady();
        Setting GetSetting();
        List<Result> GetCurrentResultList(int startIndex);
        List<Prize> GetPrizeList();
        List<Setting> GetSettingList();
        List<Result> GetResultList();
        List<Result> GetLogList();
        void AddPrizes(List<Prize> prizes);
        bool IsEmptyPrize();
        void AddSetting(Setting setting);
        bool IsEmptySetting();
    }
}
