using System;
using System.Collections.Generic;
using System.Text;
using MWS.dbo;
using MWS.Lines;

namespace MWS.Pages
{
    class PanelUczestnika: Panel
    {

        public PanelUczestnika(Logowanie logowanie, StaticLine note = null): base(logowanie, note)
        {
        }

        public override void React(Line line)
        {
            throw new NotImplementedException();
        }
       
    }
}
