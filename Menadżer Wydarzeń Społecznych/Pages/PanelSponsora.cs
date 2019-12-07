using MWS.dbo;
using MWS.Lines;
using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.Pages
{
    class PanelSponsora: Panel
    {
        public PanelSponsora(Logowanie logowanie, StaticLine note): base(logowanie, note)
        {
        }

        public override void React(Line line)
        {
        }
    }
}
