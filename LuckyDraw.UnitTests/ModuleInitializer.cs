using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyDraw.UnitTests
{
    internal static class ModuleInitializer
    {
        internal static void Run()
        {
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
        }
    }
}
