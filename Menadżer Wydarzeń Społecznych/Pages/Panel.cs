using MWS.dbo;
using MWS.Lines;
using MWS.Pages;
using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.Pages
{
    abstract class Panel: Page
    {
        public Logowanie logowanie { get; set; }

        public Panel(Logowanie logowanie): base()
        {
            this.logowanie = logowanie;
        }
        
    }
}
