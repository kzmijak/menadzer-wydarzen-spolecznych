using MWS.dbo;
using MWS.Lines;
using MWS.Pages;
using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.Pages
{
    abstract class _Panel: _Page
    {
        public Logowanie logowanie { get; set; }

        public _Panel(Logowanie logowanie, StaticLine Note = null): base(Note)
        {
            this.logowanie = logowanie;
        }
        
    }
}
