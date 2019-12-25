using System;
using System.Collections.Generic;
using System.Text;
using MWS.dbo;
using MWS.Lines;

namespace MWS.Pages
{
    class PanelWydarzenieInterakcjaCzlonek : _Panel
    {
        private Wydarzenie wydarzenie;
        bool listing;
        List<Pracownik> pracownicy = new List<Pracownik>(9999);
        public PanelWydarzenieInterakcjaCzlonek(Logowanie logowanie, Wydarzenie wydarzenie, StaticLine note = null, bool listing = false) : base(logowanie)
        {
            this.wydarzenie = wydarzenie;
            if(listing && logowanie.uczestnik.bilety.Count != 0)
                this.listing = listing;

            Contents.Add(new StaticLine("ZARZĄDZANIE WYDARZENIEM"));
            if (logowanie.owner is Pracownik)
            {
                if(logowanie.pracownik.stanowisko.ToLower() == "organizator")
                {
                    ConstructorOrganizator();
                }
                else
                {
                    ConstructorPracownik();
                }
            }
            if(logowanie.owner is Uczestnik)
            {
                ConstructorUczestnik();
            }

            Contents.Add(new StaticLine(""));
            Contents.Add(new ActiveLine("Powrót"));
        }

        public override void React(_Line line)
        {
            if (line.Index == Contents.Count - 1)
                DisplayAdapter.Display(new PanelWydarzenieInterakcja(logowanie, wydarzenie));
            else
            {
                if (logowanie.owner is Pracownik)
                {
                    if (logowanie.pracownik.stanowisko.ToLower() == "organizator")
                    {
                        ReactOrganizator(line.Index);
                    }
                    else
                    {
                        ReactPracownik(line.Index);
                    }
                }
                if (logowanie.owner is Uczestnik)
                {
                    ReactUczestnik(line.Index);
                }
            }
        }

        private void ConstructorOrganizator()
        {
            Contents.Add(new ActiveLine("Ustawienia wydarzenia"));
            Contents.Add(new ActiveLine("Bilety"));
            Contents.Add(new ActiveLine("Histora płatności"));
            Contents.Add(new ActiveLine("Moja kadra"));
            if(listing is false)
            {
                foreach(Pracownik pra in logowanie.pracownik.kadra)
                {
                    Contents.Add(new ActiveLine($"{pra.stanowisko} {pra.kontakt.imie} {pra.kontakt.nazwisko}, {pra.wynagrodzenie}"));
                    pracownicy.Add(pra);
                }
            }
            Contents.Add(new ActiveLine("Zrezygnuj z wydarzenia"));
            Contents.Add(new ActiveLine("Odwołaj wydarzenie"));
        }
        private void ReactOrganizator(int index)
        {
            int cnt = 0;
            if (listing)
                cnt = logowanie.pracownik.kadra.Count;

            if(index == 1)
            {
                if(wydarzenie.organizatorzy[0].id == logowanie.owner.id)
                {
                    DisplayAdapter.Display(new PanelWydarzeniaDodaj(logowanie, null, wydarzenie, wydarzenie.id));
                }
                else
                {
                    DisplayAdapter.Display(new PanelWydarzenieInterakcjaCzlonek(logowanie, wydarzenie, new StaticLine("Tylko główny organizator może zmienić informacje o wydarzeniu.", ConsoleColor.Red)));
                }
            }
            if(index == 2)
            {
                // REDIRECT TO PanelWydarzenieBilety
            }
            if(index == 3)
            {
                DisplayAdapter.Display(new PanelWydarzenieInterakcjaCzlonek(logowanie, wydarzenie, null, true));
            }
            if(listing && index > 3 && index < 4 + cnt)
            {
                DisplayAdapter.Display(new PanelWydarzenieInterakcjaOsoba(logowanie, wydarzenie, pracownicy[index - 4]));
            }
            if(index == 4 + cnt)
            {
                if(wydarzenie.organizatorzy.Count>1 && wydarzenie.organizatorzy[0].id == logowanie.owner.id)
                {
                    Wiadomosc.Send($"{wydarzenie.nazwa.ToUpper()} - ZMIANA ORGANIZACJI", $"Zostałeś głównym organizatorem wydarzenia \"{wydarzenie.nazwa}\"", logowanie, wydarzenie.organizatorzy[1].logowanie);
                }
                DataAccess.Delete(wydarzenie, logowanie.pracownik);
                DisplayAdapter.Display(new PanelWydarzenieInterakcja(logowanie, wydarzenie, new StaticLine("Nie jesteś już organizatorem tego wydarzenia.", ConsoleColor.Green)));
            }
            if(index == 5 + cnt)
            {
                if (wydarzenie.organizatorzy.Count > 1 && wydarzenie.organizatorzy[0].id == logowanie.owner.id) 
                foreach(Wydarzenie_Pracownik jt in DataAccess.GetConnections<Wydarzenie_Pracownik>())
                {
                    if(jt.idwydarzenia == wydarzenie.id)
                    {
                        var sender = logowanie;
                        var receiver = (DataAccess.GetRecordById<Pracownik>(jt.idpracownika)).logowanie;
                        Wiadomosc.Send($"WYDARZENIE ODWOŁANE", $"Wydarzenie w którym brałeś udział (\"{wydarzenie.nazwa}\") zostało odwołane. \nW przypadku niedogodności skontaktuj się z organizatorem - {logowanie.pracownik.kontakt.imie} {logowanie.pracownik.kontakt.nazwisko}", sender, receiver);
                    }
                }
                foreach (Wydarzenie_Sponsor jt in DataAccess.GetConnections<Wydarzenie_Sponsor>())
                {
                    if (jt.idwydarzenia == wydarzenie.id)
                    {
                        var sender = logowanie;
                        var receiver = DataAccess.GetRecordById<Sponsor>(jt.idsponsora).logowanie;
                        Wiadomosc.Send($"WYDARZENIE ODWOŁANE", $"Wydarzenie w którym brałeś udział (\"{wydarzenie.nazwa}\") zostało anulowane. \nW przypadku niedogodności skontaktuj się z organizatorem - {logowanie.pracownik.kontakt.imie} {logowanie.pracownik.kontakt.nazwisko}", sender, receiver);
                    }
                }
                foreach (Wydarzenie_Uczestnik jt in DataAccess.GetConnections<Wydarzenie_Uczestnik>())
                {
                    if (jt.idwydarzenia == wydarzenie.id)
                    {
                        var sender = logowanie;
                        var receiver = DataAccess.GetRecordById<Uczestnik>(jt.iduczestnika).logowanie;
                        Wiadomosc.Send("WYDARZENIE ODWOŁANE", $"Wydarzenie w którym brałeś udział (\"{wydarzenie.nazwa}\") zostało anulowane. \nW przypadku niedogodności skontaktuj się z organizatorem - {logowanie.pracownik.kontakt.imie} {logowanie.pracownik.kontakt.nazwisko}", sender, receiver);
                    }
                }
            }
        }

        private void ConstructorPracownik()
        {
            Contents.Add(new ActiveLine("Zrezygnuj z wydarzenia"));
        }
        private void ReactPracownik(int index)
        {
            switch(index)
            {
                case 1:
                    DataAccess.Delete(wydarzenie, logowanie.pracownik);                    
                    Wiadomosc.Send("Pracownik zrezygnował z wydarzenia", $"Nadawca wiadomości zrezygnował się z wydarzenia: \"{wydarzenie.nazwa}\"", logowanie, logowanie.pracownik.kadra[0].logowanie);
                    DisplayAdapter.Display(new PanelWydarzenieInterakcja(logowanie, wydarzenie, new StaticLine("Nie bierzesz już udziału w tym wydarzeniu.\nTwój przełożony został poinformowany", ConsoleColor.Red)));
                    break;
            }
        }

        private void ConstructorUczestnik()
        {
            string msg = "";
            if (logowanie.uczestnik.bilety.Count == 0)
                msg = "(niedostępne)";

            Contents.Add(new ActiveLine("Kup bilet"));
            Contents.Add(new ActiveLine("Moje bilety" + msg));
            if(listing)
            {
                foreach(var bilet in logowanie.uczestnik.bilety)
                {
                    Contents.Add(new ActiveLine(" " + bilet.nazwa));
                }
            }
        }
        private void ReactUczestnik(int index)
        {
            int cnt = 0;
            if (listing)
            {
                cnt = logowanie.uczestnik.bilety.Count;
            }

            if(index == 1)
            {
                    // REDIRECT TO THE TICKETS TAB PanelWydarzenieInterakcjaBilety
            }
            if(index == 2)
            {
                if(listing is false)
                {
                    DisplayAdapter.Display(new PanelWydarzenieInterakcjaCzlonek(logowanie, wydarzenie, null, true));
                }
                if(listing is true)
                {
                    DisplayAdapter.Display(new PanelWydarzenieInterakcjaCzlonek(logowanie, wydarzenie, null, false));
                }
            }
            if(listing && index > 2 && index < 3 + cnt)
            {
                // REDIRECT TO THE TICKETS TAB PanelWydarzenieInterakcjaBilety
            }
        }
    }
}
