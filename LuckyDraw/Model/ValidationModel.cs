using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyDraw.Model
{
    static class ValidationModel
    {
        public static string ValidatePrizes(IEnumerable<ViewPrize> prizes)
        {
            for (var i = 0; i < prizes.Count() - 1; i++) {
                for (var j = i + 1; j < prizes.Count(); j++) {
                    var p1 = prizes.ToList()[i];
                    var p2 = prizes.ToList()[j];

                    if ((p1.RoundIndex == p2.RoundIndex && p1.RoundName.Trim() != p2.RoundName.Trim()) ||
                        (p1.RoundIndex != p2.RoundIndex && p1.RoundName.Trim() == p2.RoundName.Trim())) {
                        return "Inconsistant Round Index and Round Name";
                    }
                }
            }

            return string.Empty;
        }
    }
}
