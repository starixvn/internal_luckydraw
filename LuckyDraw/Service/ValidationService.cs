using LuckyDraw.Model;
using LuckyDraw.Resources;
using System.Collections.Generic;
using System.Linq;

namespace LuckyDraw.Service
{
    public static class ValidationService
    {
        public static string ValidatePrizes(IEnumerable<ViewPrize> prizes)
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
    }
}
