using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyDraw.DBContexts
{
    public class LuckyDrawEntity : ILuckyDrawEntity
    {
        public LuckDrawDBContext GetDBContext()
        {
            return new LuckDrawDBContext();
        }
    }
}
