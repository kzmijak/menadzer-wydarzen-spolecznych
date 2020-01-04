using System;
using System.Collections.Generic;
using System.Text;
using MWS.dbo;
using MWS.Lines;

namespace MWS.Pages
{
    class Panel : _Panel
    {

        public Panel (Logowanie logowanie, StaticLine note = null): base(logowanie, note)
        {
            if(logowanie.owner is Pracownik && logowanie.pracownik.stanowisko == "Organizator")
                Contents.Add(new StaticLine("PANEL ORGANIZATORA"));
            if (logowanie.owner is Pracownik && logowanie.pracownik.stanowisko != "Organizator")
                Contents.Add(new StaticLine("PANEL PRACOWNIKA"));
            if (logowanie.owner is Sponsor)
                Contents.Add(new StaticLine("PANEL SPONSORA"));
            if (logowanie.owner is Uczestnik)
                Contents.Add(new StaticLine("PANEL UCZESTNIKA"));


            Contents.Add(new ActiveLine("Moje wydarzenia", "Lista dostępnych wydarzeń"));
            Contents.Add(new ActiveLine("Skrzynka odbiorcza", "Wysyłanie wiadomości, historia wiadomości, lista kontaktów"));
            Contents.Add(new ActiveLine("Ustawienia konta", "Edytuj formularz kontaktowy, dodaj kartę płatniczą (tylko dla uczestników)"));
            Contents.Add(new ActiveLine("Wyloguj", "Wyloguj użytkownika"));

            Contents.Add(Note);
        }

        public override void React(_Line line)
        {
            switch(line.Index)
            {
                case 1:
                    DisplayAdapter.Display(new PanelWydarzenia(logowanie));
                    break;
                case 2:
                    DisplayAdapter.Display(new PanelSkrzynka(logowanie));
                    break;
                case 3:
                    DisplayAdapter.Display(new PanelUstawienia(logowanie));
                    break;
                case 4:
                    DisplayAdapter.Display(new Login(new StaticLine("Użytkownik został wylogowany", ConsoleColor.Green)));
                    break;
            }
        }
    }
}
