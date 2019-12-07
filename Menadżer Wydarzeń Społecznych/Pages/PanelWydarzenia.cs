using MWS.dbo;
using MWS.Lines;
using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.Pages
{
    class PanelWydarzenia : Panel
    {

        public PanelWydarzenia(Logowanie logowanie, StaticLine note = null): base(logowanie, note)
        {
        }

        public override void React(Line line)
        {
            throw new NotImplementedException();
        }
    }
}
