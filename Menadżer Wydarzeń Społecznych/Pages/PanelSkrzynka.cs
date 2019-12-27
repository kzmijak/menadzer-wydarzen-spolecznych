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
            Contents.Add(new ActiveLine("Odebrane"));
            Contents.Add(new ActiveLine("Wysłane"));
            Contents.Add(new ActiveLine("Wyślij wiadomość"));
            Contents.Add(new ActiveLine("Kontakty"));
            Contents.Add(new ActiveLine("Powrót"));
        }

        public override void React(_Line line)
        {
            switch (line.Index)
            {
                case 1:
                    DisplayAdapter.Display(new PanelSkrzynkaWiadomosci(logowanie, "ODEBRANE"));
                    break;
                case 2:
                    DisplayAdapter.Display(new PanelSkrzynkaWiadomosci(logowanie, "WYSŁANE"));
                    break;
                case 3:
                    DisplayAdapter.Display(new PanelSkrzynkaWyslij(logowanie));
                    break;
                case 4:
                    DisplayAdapter.Display(new PanelSkrzynkaKontakty(logowanie));
                    break;
                case 5:
                    DisplayAdapter.Display(new Panel(logowanie));
                    break;
            }
        }
    }
}
