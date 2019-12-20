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
            Contents.Add(new StaticLine("SKRZYNKA ODBIORCZA"));
            Contents.Add(new ActiveLine("Odebrane wiadomości"));
            Contents.Add(new ActiveLine("Wyślij wiadomość"));
            Contents.Add(new ActiveLine("Kontakty"));
            Contents.Add(new ActiveLine("Powrót"));

        }

        public override void React(_Line line)
        {
            switch (line.Index)
            {
                case 4:
                    DisplayAdapter.Display(new Panel(logowanie));
                    break;
            }
        }
    }
}
