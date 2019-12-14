using System;
using System.Collections.Generic;
using System.Text;
using MWS.dbo;
using MWS.Lines;

namespace MWS.Pages
{
    class PanelSkrzynka : _Panel
    {
        public PanelSkrzynka(Logowanie logowanie, StaticLine note = null): base(logowanie)
        {
        }

        public override void React(_Line line)
        {
            throw new NotImplementedException();
        }
    }
}
