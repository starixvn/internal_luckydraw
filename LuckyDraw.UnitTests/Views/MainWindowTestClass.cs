using LuckyDraw.Service;
using LuckyDraw.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyDraw.UnitTests.Views
{
    class MainWindowTestClass : MainWindow
    {        
        public MainWindowTestClass(
            ICommonService commonService,
            ISeedService seedService,
            IRunningService runningService,
            IValidationService validationService,
            IAuthenticationService authenticationService,
            IUtils utils) : base(commonService, seedService, runningService, validationService, authenticationService, utils)
        {
        }
    }
}
