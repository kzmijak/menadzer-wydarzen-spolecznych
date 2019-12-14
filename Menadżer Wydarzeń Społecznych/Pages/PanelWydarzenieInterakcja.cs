using System;
using System.Collections.Generic;
using System.Text;
using MWS.dbo;
using MWS.Lines;

namespace MWS.Pages
{
    class PanelWydarzenieInterakcja : _Panel
    {
        public PanelWydarzenieInterakcja(Logowanie logowanie, Wydarzenie wydarzenie, StaticLine note = null): base(logowanie)
        {

        }

        public override void React(_Line line)
        {
        }
    }
}
