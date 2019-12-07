using System;
using System.Collections.Generic;
using System.Text;
using MWS.dbo;
using MWS.Lines;

namespace MWS.Pages
{
    class SkrzynkaOdbiorcza : Panel
    {
        public SkrzynkaOdbiorcza(Logowanie logowanie, StaticLine note = null): base(logowanie)
        {
        }

        public override void React(Line line)
        {
            throw new NotImplementedException();
        }
    }
}
