using System;
using System.Collections.Generic;
using System.Text;
using MWS.dbo;
using MWS.Lines;

namespace MWS.Pages
{
    class PanelSkrzynka : _Panel
    {
        public PanelSkrzynka(Logowanie logowanie, StaticLine note = null): base(logowanie, note)
        {
            Contents.Add(new StaticLine("SKRZYNKA ODBIORCZA"));
            Contents.Add(new ActiveLine("Odebrane", "Lista wiadomości adresowanych do użytkownika"));
            Contents.Add(new ActiveLine("Wysłane", "Lista wiadomości wysłanych przez użytkownika"));
            Contents.Add(new ActiveLine("Wyślij wiadomość", "Napisz wiadomość i wybierz odbiorcę z listy kontaktów"));
            Contents.Add(new ActiveLine("Kontakty", "Lista znajomości zawartych między użytkownikami"));
            Contents.Add(new ActiveLine("Powrót", "Powrót do panelu użytkownika"));
            Contents.Add(Note);
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
