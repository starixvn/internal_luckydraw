using LuckyDraw.Constants;
using LuckyDraw.Model;
using LuckyDraw.Resources;
using System.Collections.Generic;
using System.Linq;

namespace LuckyDraw.Service
{
    public class ValidationService : IValidationService
    {
        public string ValidatePrizes(IEnumerable<ViewPrize> prizes)
        {
            var duplicateRound = ValidateDuplicateRound(prizes);
            if (!string.IsNullOrEmpty(duplicateRound))
            {
                return duplicateRound;
            }

            var maxRoundSize = ValidateMaxRoundSize(prizes);
            if (!string.IsNullOrEmpty(maxRoundSize))
            {
                return maxRoundSize;
            }

            return string.Empty;
        }

        private static string ValidateDuplicateRound(IEnumerable<ViewPrize> prizes)
        {
            for (var i = 0; i < prizes.Count() - 1; i++)
            {
                for (var j = i + 1; j < prizes.Count(); j++)
                {
                    var p1 = prizes.ToList()[i];
                    var p2 = prizes.ToList()[j];

                    if ((p1.RoundIndex == p2.RoundIndex && p1.RoundName.Trim() != p2.RoundName.Trim()) ||
                        (p1.RoundIndex != p2.RoundIndex && p1.RoundName.Trim() == p2.RoundName.Trim()))
                    {
                        return Labels.InconsistantRoundNames;
                    }
                }
            }

            return string.Empty;
        }

        private static string ValidateMaxRoundSize(IEnumerable<ViewPrize> prizes)
        {
            var maxRoundSize = prizes
                .GroupBy(p => p.RoundIndex)
                .Select(group => new
                {
                    Metric = group.Key,
                    Count = group.Count()
                })
                .Max(x => x.Count);
            if(maxRoundSize > Consts.MaxRoundItems)
            {
                return Labels.MaxRoundSizeError;
            }

            return string.Empty;
        }
    }
}
