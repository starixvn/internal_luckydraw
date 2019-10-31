using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyDraw.DBContexts
{
    public interface ILuckyDrawEntity
    {
        LuckDrawDBContext GetDBContext();
    }
}
