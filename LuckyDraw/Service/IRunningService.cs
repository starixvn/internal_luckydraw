using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyDraw.Service
{
    public interface IRunningService
    {
        int CurrentRound { get; }
        List<Result> CurrentList { get; }
        int MaxRound { get; }
        bool IsValid { get; }
        Setting Setting { get; }
        string GetRoundName();
        void ResetModel();
        void LoadSetting();
        bool LoadModel();
        void UpdateRandom(string random, int index);
        bool MoveNext();
        bool MoveBack();
        bool IsExistRandom(string random);
        string CreateRandomNumber();
        string GetIgnoredListText();
        string GetRandomRangeText();
        bool ResetRandom(int index);
        bool IsRunnable(Result item);
        bool IsSkipped(Result item);
        bool IsFirstRound();
        bool IsLastRound();
        bool IsRoundFinishRun();
    }
}
