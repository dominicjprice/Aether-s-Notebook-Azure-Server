using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace Aethers.Notebook.Web
{
    public class WebRole : RoleEntryPoint
    { 
        public override bool OnStart()
        {
            Aethers.Notebook.Configuration.configureDiagnostics();
            return base.OnStart();
        }
    }
}
