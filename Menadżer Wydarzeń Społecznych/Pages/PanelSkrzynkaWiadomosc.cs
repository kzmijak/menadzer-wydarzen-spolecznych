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

        public PanelSkrzynkaWiadomosc(Logowanie logowanie, Wiadomosc wiadomosc, StaticLine note = null) : base(logowanie)
        {
            this.wiadomosc = wiadomosc;
            string sender;
            string receiver;
            if (wiadomosc.adresat.owner is Sponsor)
            {
                sender = wiadomosc.adresat.sponsor.nazwa;
            }
            else
            {
                sender = wiadomosc.adresat.owner.kontakt.imie + " " + wiadomosc.adresat.owner.kontakt.nazwisko;
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
                    Contents.Add(new ActiveLine("Potwierdź"));
            }
            if(wiadomosc.idodbiorcy == logowanie.id)
            {
                Contents.Add(new ActiveLine("Odpowiedz"));
            }
            Contents.Add(new ActiveLine("Usuń wiadomość"));
            Contents.Add(new ActiveLine("Powrót"));
            Contents.Add(note);
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
                    Wydarzenie_Sponsor.Add(donation_new.wydarzenie, wiadomosc.adresat.sponsor);
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
                            logowanie, wiadomosc.adresat);
                DisplayAdapter.Display(new PanelSkrzynkaWiadomosc(logowanie, wiadomosc));
            }
            if(type=="gotoevent")
            {
                DisplayAdapter.Display(new PanelWydarzenieInterakcja(logowanie, DataAccess.GetRecordById<Wydarzenie>(id)));
            }
            if(type=="orgApp")
            {
                Pracownik organizator = DataAccess.GetRecordById<Pracownik>(wiadomosc.adresat.owner.id);
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
                            logowanie, wiadomosc.adresat, wniosek);
                DisplayAdapter.Display(new PanelSkrzynkaWiadomosc(logowanie, wiadomosc));
            }
            if(type=="contactsAdd")
            {
                try
                {
                    Logowanie_Logowanie.Add(logowanie, wiadomosc.adresat);
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
                    logowanie, wiadomosc.adresat
                    );
                DisplayAdapter.Display(new PanelSkrzynkaWiadomosc(logowanie, wiadomosc, new StaticLine("Zaakceptowałeś zaproszenie.", ConsoleColor.Green)));
            }
        }
    }
}
