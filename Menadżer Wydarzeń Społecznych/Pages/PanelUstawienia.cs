using System;
using System.Collections.Generic;
using System.Text;
using MWS.dbo;
using MWS.Lines;

namespace MWS.Pages
{
    class PanelUstawienia : _Panel
    {
        public PanelUstawienia(Logowanie logowanie, StaticLine note = null): base(logowanie, note)
        {
            Contents.Add(new StaticLine("USTAWIENIA"));
            Contents.Add(new ActiveLine("Edytuj opcje logowania", "Edytuj dane, których używasz do logowania"));
            if(!(logowanie.owner is Sponsor))
                Contents.Add(new ActiveLine("Edytuj formularz kontaktowy", "Edytuj kontaktowy i dodaj kartę płatniczą (tylko dla uczestników)"));
            Contents.Add(new ActiveLine("Usuń konto", "Usuń konto i wszelkie powiązane z tobą informacje"));
            Contents.Add(new ActiveLine("Powrót", "Powrót do panelu użytkownika"));
            Contents.Add(Note);
        }

        public override void React(_Line line)
        {
            int sponsordependent = 0;
            if (logowanie.owner is Sponsor)
                sponsordependent++;
            if(line.Index == 1)
            {
                DisplayAdapter.Display(new PanelUstawieniaLogowanie(logowanie));
            }
            if(line.Index == 2 - sponsordependent)
            {
                DisplayAdapter.Display(new PanelUstawieniaKontakt(logowanie));
            }
            if(line.Index == 3 - sponsordependent)
            {
                string conf;
                Console.WriteLine("Wpisz imię i nazwisko by potwierdzić wybór: ");
                conf = Console.ReadLine();
                if (conf == (logowanie.pracownik.kontakt.imie + ' ' + logowanie.owner.kontakt.nazwisko))
                {
                    DataAccess.Delete(logowanie.pracownik);
                    DisplayAdapter.Display(new Login(new StaticLine("Użytkownik został usunięty", ConsoleColor.Red)));
                }
            }
            if(line.Index == 4 - sponsordependent)
            {
                DisplayAdapter.Display(new Panel(logowanie));
            }
        }
    }
}
