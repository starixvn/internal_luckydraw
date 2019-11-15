using LuckyDraw.Constants;
using LuckyDraw.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace LuckyDraw.Service
{
    public class RunningService : IRunningService
    {
        private readonly ICommonRepository commonRepository;
        private List<List<Result>> runningList { get; set; }
        private List<string> roundList { get; set; }
        public int CurrentRound { get; private set; }
        public List<Result> CurrentList { get; private set; }
        public int MaxRound { get; private set; }        
        public bool IsValid { get; private set; }
        public Setting Setting { get; private set; }

        public RunningService():this(null)
        {           
        }

        public RunningService(ICommonRepository commonRepository)
        {
            this.commonRepository = commonRepository ?? new CommonRepository();
            this.CurrentList = new List<Result>();
            this.runningList = new List<List<Result>>();
            this.roundList = new List<string>();
        }

        public string GetRoundName()
        {
            return this.roundList[this.CurrentRound].ToUpper(CultureInfo.CurrentCulture);
        }

        public string GetIgnoredListText()
        {
            return string.Join(" - ", this.Setting.Excluded.Replace(" ", "").Split(','));
        }

        public string GetRandomRangeText()
        {
            return string.Format("{0}-{1}", this.Setting.RandomFrom.ToString("000"), this.Setting.RandomTo.ToString("000"));
        }

        public void ResetModel()
        {
            this.IsValid = false;
        }

        public void LoadSetting()
        {
            this.Setting = commonRepository.GetSetting();
        }

        public bool LoadModel()
        {
            if (this.IsValid || !commonRepository.IsDBReady())
            {
                return false;
            }

            this.Setting = commonRepository.GetSetting();
            var resultList = commonRepository.GetCurrentResultList();
            var rounds = resultList.Select(i => new { i.RoundName, i.RoundIndex }).Distinct().OrderBy(i => i.RoundIndex).Select(i => i.RoundName).ToList();

            if (rounds.Count > 0)
            {
                this.MaxRound = rounds.Count - 1;
                this.CurrentRound = 0;
                this.runningList = new List<List<Result>>();
                for (var i = 0; i < rounds.Count; i++)
                {
                    var list = resultList.Where(r => r.RoundName == rounds[i]).ToList();
                    this.runningList.Add(list);
                }
                this.CurrentList = this.runningList[this.CurrentRound];
                this.roundList = rounds;
                this.IsValid = true;

                return true;
            }

            return false;
        }

        public void UpdateRandom(string random, int index)
        {
            if (random == "000")
            {
                return;
            }

            this.CurrentList[index].Random = random;
            this.CurrentList[index].Status = 1;
            commonRepository.UpdateRandom(this.CurrentList[index].Id, random);
            commonRepository.WriteLog(this.CurrentList[index].Id, random);
        }

        public bool MoveNext()
        {
            if (IsValid)
            {
                var nRound = this.CurrentRound + 1;
                if (nRound <= this.MaxRound)
                {
                    this.CurrentRound = nRound;
                    this.CurrentList = this.runningList[nRound];
                    return true;
                }
            }

            return false;
        }

        public bool MoveBack()
        {
            if (IsValid)
            {
                var pRound = this.CurrentRound - 1;
                if (pRound >= 0)
                {
                    this.CurrentRound = pRound;
                    this.CurrentList = this.runningList[pRound];
                    return true;
                }
            }

            return false;
        }

        public bool IsExistRandom(string random)
        {
            var existingRandoms = this.runningList.SelectMany(l1 => l1.Select(l2 => l2.Random)).ToList();
            var excludedRandoms = this.Setting.Excluded.Split(',').Select(i => i.Trim()).ToList();
            return existingRandoms.Contains(random) || excludedRandoms.Contains(random);
        }

        public string CreateRandomNumber()
        {
            Random rnd = new Random();
            for (var i = 0; i < Consts.MaxRandomTrials; i++)
            {
                var random = rnd.Next(this.Setting.RandomFrom, this.Setting.RandomTo + 1).ToString("000");
                if (!this.IsExistRandom(random))
                {
                    return random;
                }
            }

            return "000";
        }

        public bool ResetRandom(int index)
        {            
            var resultList = this.runningList.SelectMany(r => r).ToList();
            if ((index-1) < resultList.Count)
            {
                var result = resultList.ToList()[index-1];
                this.commonRepository.ResetRandom(result.Id);
                return true;
            }

            return false;
        }

        public bool IsSkipped(Result item)
        {
            var resultList = this.runningList.SelectMany(r => r).ToList();
            var index = resultList.FindIndex(r => r.Id == item.Id) + 1;

            return index < this.Setting.RunningIndex;
        }

        public bool IsRunnable(Result item)
        {
            return !IsSkipped(item) && item.Status == 0;
        }             
    }
}
