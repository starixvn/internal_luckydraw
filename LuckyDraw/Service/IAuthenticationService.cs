using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyDraw.Service
{
    public interface IAuthenticationService
    {
        bool Authenticate(string password);
    }
}
