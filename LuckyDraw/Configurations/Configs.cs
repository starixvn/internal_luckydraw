using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyDraw.Configurations
{
    public class Configs : IConfigs
    {
        public string Password
        {
            get
            {
                return ConfigurationManager.AppSettings["password"];
            }
        }
    }
}
