using LuckyDraw.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyDraw.Service
{
    public interface IValidationService
    {
        string ValidatePrizes(IEnumerable<ViewPrize> prizes);
    }
}
