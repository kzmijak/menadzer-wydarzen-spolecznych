using System;
using System.Collections.Generic;
using System.Text;
using MWS.dbo;
using MWS.Lines;

namespace MWS.Pages
{
    class PanelSkrzynkaWiadomosc : _Panel
    {
        Wiadomosc wiadomosc;
        string mode
        {
            get
            {
                if (logowanie.id == wiadomosc.idnadawcy)
                {
                    return "WYSŁANE";
                }
                else
                {
                    return "ODEBRANE";
                }
            }
        }

        public PanelSkrzynkaWiadomosc(Logowanie logowanie, Wiadomosc wiadomosc, StaticLine note = null) : base(logowanie, note)
        {
            this.wiadomosc = wiadomosc;
            string sender;
            string receiver;
            if (wiadomosc.nadawca.owner is Sponsor)
            {
                sender = wiadomosc.nadawca.sponsor.nazwa;
            }
            else
            {
                sender = wiadomosc.nadawca.owner.kontakt.imie + " " + wiadomosc.nadawca.owner.kontakt.nazwisko;
            }
            if (wiadomosc.odbiorca.owner is Sponsor)
            {
                receiver = wiadomosc.odbiorca.sponsor.nazwa;
            }
            else
            {
                receiver = wiadomosc.odbiorca.owner.kontakt.imie + " " + wiadomosc.odbiorca.owner.kontakt.nazwisko;
            }

            Contents.Add(new StaticLine(wiadomosc.tytul));
            Contents.Add(new StaticLine($"Data: {wiadomosc.dzien.ToShortDateString()}"));
            Contents.Add(new StaticLine($"Dzień: {wiadomosc.godzina.ToString("hh\\:mm")}"));
            Contents.Add(new StaticLine($"Od: {sender}"));
            Contents.Add(new StaticLine($"Do: {receiver}"));
            Contents.Add(new StaticLine("---"));
            Contents.Add(new StaticLine(wiadomosc.tresc, ConsoleColor.White));
            Contents.Add(new StaticLine("---"));
            Contents.Add(new StaticLine(""));
            if(wiadomosc.wniosek != null)
            {
                string tf;
                if (wiadomosc.wniosek.zatwierdzone == true)
                {
                    tf = "Tak";
                }
                else 
                {
                    tf = "Nie";
                }

                Contents.Add(new StaticLine("Załącznik:"));
                if(wiadomosc.wniosek.kwota > 0)
                {
                    Contents.Add(new StaticLine(" Kwota:       " + wiadomosc.wniosek.kwota));
                }
                Contents.Add(new StaticLine(" Zatwierdzone:" + tf));
                Contents.Add(new StaticLine(""));
                if (wiadomosc.idodbiorcy == logowanie.id && wiadomosc.wniosek.zatwierdzone == false)
                    Contents.Add(new ActiveLine("Potwierdź", "Potwierdź załączoną informację"));
            }
            if(wiadomosc.idodbiorcy == logowanie.id)
            {
                Contents.Add(new ActiveLine("Odpowiedz", "Odpowiedz nadawcy na tą wiadomość"));
            }
            Contents.Add(new ActiveLine("Usuń wiadomość", "Usuń tę wiadomość (UWAGA: Wiadomość jest usunięta dla obu użytkowników)"));
            Contents.Add(new ActiveLine("Powrót", "Powrót do listy wiadomości"));
            Contents.Add(Note);
        }

        public override void React(_Line line)
        {
            if(line.Index == Contents.Count - 5)
            {
                if (wiadomosc.idodbiorcy == logowanie.id && wiadomosc.wniosek.zatwierdzone == false)
                    Proceed(wiadomosc.wniosek.akcja);
            }
            else if (line.Index == Contents.Count - 4)
            {
                if(wiadomosc.idodbiorcy == logowanie.id)
                {
                    int tmp = wiadomosc.idnadawcy;
                    wiadomosc.idnadawcy = wiadomosc.idodbiorcy;
                    wiadomosc.idodbiorcy = tmp;
                    wiadomosc.tytul = "RE: " + wiadomosc.tytul;
                    DisplayAdapter.Display(new PanelSkrzynkaWyslij(logowanie, null, wiadomosc));
                }
            }
            else if (line.Index == Contents.Count - 3)
            {
                DataAccess.Delete(wiadomosc.wniosek);
                DataAccess.Delete(wiadomosc);
                DisplayAdapter.Display(new PanelSkrzynkaWiadomosci(logowanie, mode, null, new StaticLine("Wiadomość została usunięta.", ConsoleColor.Green)));
            }
            else if (line.Index == Contents.Count - 2)
            {
                
                DisplayAdapter.Display(new PanelSkrzynkaWiadomosci(logowanie, mode));
            }
        }

        private void Proceed(string action)
        {
            int div = action.IndexOf(":");
            if(div == -1)
            {
                div = action.Length;
                action += ":0";
            }

            string type = action.Substring(0, div);
            int id;

            if(!int.TryParse(action.Substring(div+1), out id))
            {
                id = 0;
            }

            if(wiadomosc.wniosek != null )
            {
                var addition_old = DataAccess.GetRecordById<Wniosek>(wiadomosc.wniosek.id);
                var addition_new = DataAccess.GetRecordById<Wniosek>(wiadomosc.wniosek.id);
                addition_new.zatwierdzone = true;
                DataAccess.Update(addition_old, addition_new);
            }

            if(type == "donation")
            {
                var donation_old = DataAccess.GetRecordById<Dotacja>(id);
                var donation_new = DataAccess.GetRecordById<Dotacja>(id);
                donation_new.zatwierdzone = true;
                try
                {
                    Wydarzenie_Sponsor.Add(donation_new.wydarzenie, wiadomosc.nadawca.sponsor);
                }
                catch (Exception e)
                {
                    DisplayAdapter.Display(new PanelSkrzynkaWiadomosc(logowanie, wiadomosc, new StaticLine(e.Message, ConsoleColor.Red)));
                }
                DataAccess.Update(donation_old, donation_new);
                Wiadomosc.Send(
                            "DOTACJA ZOSTAŁA ZAAKCEPTOWANA",
                            $"Wydarzenie:  {donation_new.wydarzenie.nazwa}"
                        + $"\nOrganizator: {logowanie.pracownik.kontakt.imie} {logowanie.pracownik.kontakt.nazwisko}"
                        + $"\nKwota:       {donation_new.kwota}"
                        + $"\nOczekiwania: {donation_new.oczekiwania},",
                            logowanie, wiadomosc.nadawca);
                DisplayAdapter.Display(new PanelSkrzynkaWiadomosc(logowanie, wiadomosc));
            }
            if(type=="gotoevent")
            {
                DisplayAdapter.Display(new PanelWydarzenieInterakcja(logowanie, DataAccess.GetRecordById<Wydarzenie>(id)));
            }
            if(type=="orgApp")
            {
                Pracownik organizator = DataAccess.GetRecordById<Pracownik>(wiadomosc.nadawca.owner.id);
                Wydarzenie wydarzenie = DataAccess.GetRecordById<Wydarzenie>(id);
                try
                {
                    Wydarzenie_Pracownik.Add(wydarzenie, organizator);
                }
                catch (Exception e)
                {
                    DisplayAdapter.Display(new PanelSkrzynkaWiadomosc(logowanie, wiadomosc, new StaticLine(e.Message, ConsoleColor.Red)));
                }
                Wniosek wniosek = new Wniosek
                {
                    kwota = 0,
                    akcja = $"gotoevent:{id}"
                };
                Wiadomosc.Send(
                            "ZOSTAŁEŚ ORGANIZATOREM WYDARZENIA",
                            $"Wydarzenie:  {wydarzenie.nazwa}"
                        + $"\nOrganizator: {logowanie.pracownik.kontakt.imie} {logowanie.pracownik.kontakt.nazwisko}"
                        + $"\nPrzejść do wydarzenia?",
                            logowanie, wiadomosc.nadawca, wniosek);
                DisplayAdapter.Display(new PanelSkrzynkaWiadomosc(logowanie, wiadomosc));
            }
            if(type=="contactsAdd")
            {
                try
                {
                    Logowanie_Logowanie.Add(logowanie, wiadomosc.nadawca);
                }
                catch(Exception e)
                {
                    DisplayAdapter.Display(new PanelSkrzynkaWiadomosc(logowanie, wiadomosc, new StaticLine(e.Message, ConsoleColor.Red)));
                }
                string msg = "";
                if (logowanie.owner is Sponsor)
                {
                    msg = $"Sponsor \"{logowanie.sponsor.nazwa}\" przyjął twoje zaproszenie do grona znajomych.";
                }
                else
                {
                    msg = $"Użytkownik {logowanie.owner.kontakt.imie} {logowanie.owner.kontakt.nazwisko} przyjął twoje zaproszenie do grona znajomych.";

                }
                Wiadomosc.Send("ZAPROSZENIE ZAAKCEPTOWANE",
                    msg,
                    logowanie, wiadomosc.nadawca
                    );
                DisplayAdapter.Display(new PanelSkrzynkaWiadomosc(logowanie, wiadomosc, new StaticLine("Zaakceptowałeś zaproszenie.", ConsoleColor.Green)));
            }
            if(type == "checkTicket")
            {
                Bilet bilet = DataAccess.GetRecordById<Bilet>(id);

                DisplayAdapter.Display(new PanelWydarzenieBilet(logowanie, bilet.wydarzenie, bilet));
            }
            if(type == "employHim")
            {
                if(logowanie.pracownik.kadra.Count != 0)
                {
                    Wiadomosc.Send("PRACOWNIK ZREZYGNOWAŁ Z ZATRUDNIENIA",
                        "Nadawca wiadomości nie jest już twoim pracownikiem" +
                        "\n(Wiadomośc wygenerowana automatycznie)",
                        logowanie, logowanie.pracownik.kadra[0].logowanie);
                    DataAccess.Delete(logowanie.pracownik, logowanie.pracownik.kadra[0]);
                }

                Pracownik_Pracownik.Add(logowanie.pracownik, wiadomosc.nadawca.pracownik);
                logowanie.pracownik.wynagrodzenie = wiadomosc.wniosek.kwota;
                DataAccess.Update(logowanie.pracownik, logowanie.pracownik);

                Wiadomosc.Send("OFERTA ZAAKCEPTOWANA",
                    "Nadawca wiadomości jest teraz twoim pracownikiem" +
                    "\n(Wiadomośc wygenerowana automatycznie)", 
                    logowanie, wiadomosc.nadawca);

                DisplayAdapter.Display(new PanelSkrzynkaWiadomosci(logowanie, "ODEBRANE", null, new StaticLine("Zaakceptowałeś propozycję zatrudenia.", ConsoleColor.Green)));
            }
            if (type == "employMe")
            {
                if (wiadomosc.nadawca.pracownik.kadra.Count != 0)
                {
                    Wiadomosc.Send("PRACOWNIK ZREZYGNOWAŁ Z ZATRUDNIENIA",
                        "Nadawca wiadomości nie jest już twoim pracownikiem" +
                        "\n(Wiadomośc wygenerowana automatycznie)",
                        wiadomosc.nadawca, wiadomosc.nadawca.pracownik.kadra[0].logowanie);
                    DataAccess.Delete(wiadomosc.nadawca, logowanie.pracownik.kadra[0]);
                }

                Pracownik_Pracownik.Add(wiadomosc.nadawca.pracownik, logowanie.pracownik);
                wiadomosc.nadawca.pracownik.wynagrodzenie = wiadomosc.wniosek.kwota;
                DataAccess.Update(wiadomosc.nadawca.pracownik, wiadomosc.nadawca.pracownik);

                Wiadomosc.Send("OFERTA ZAAKCEPTOWANA",
                    "Jesteś teraz zatrudniony u nadawcy wiadomości" +
                    "\n(Wiadomośc wygenerowana automatycznie)",
                    logowanie, wiadomosc.nadawca);

                DisplayAdapter.Display(new PanelSkrzynkaWiadomosci(logowanie, "ODEBRANE", null, new StaticLine("Zaakceptowałeś propozycję zatrudenia.", ConsoleColor.Green)));
            }
            if(type=="changeMe")
            {
                wiadomosc.nadawca.pracownik.wynagrodzenie = wiadomosc.wniosek.kwota;
                DataAccess.Update(wiadomosc.nadawca.pracownik, wiadomosc.nadawca.pracownik);

                Wiadomosc.Send("OFERTA ZAAKCEPTOWANA",
                    "Nadawca wiadomości zgodził się na zmianę wynagrodzenia." +
                    $"\nTwoje wynagrodzenie teraz wynosi {wiadomosc.wniosek.kwota}PLN." +
                    "\n(Wiadomośc wygenerowana automatycznie)",
                    logowanie, wiadomosc.nadawca);
            }
        }
    }
}
