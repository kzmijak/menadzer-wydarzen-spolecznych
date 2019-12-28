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
                if (logowanie.id == wiadomosc.idadresata)
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
                    int tmp = wiadomosc.idadresata;
                    wiadomosc.idadresata = wiadomosc.idodbiorcy;
                    wiadomosc.idodbiorcy = tmp;
                    DisplayAdapter.Display(new PanelSkrzynkaWyslij(logowanie, null, wiadomosc));
                }
            }
            else if (line.Index == Contents.Count - 3)
            {
                DataAccess.Delete(wiadomosc.wniosek);
                DataAccess.Delete(wiadomosc);
                DisplayAdapter.Display(new PanelSkrzynkaWiadomosci(logowanie, mode, new StaticLine("Wiadomość została usunięta.", ConsoleColor.Green)));
            }
            else if (line.Index == Contents.Count - 2)
            {
                
                DisplayAdapter.Display(new PanelSkrzynkaWiadomosci(logowanie, mode));
            }
        }

        private void Proceed(string action)
        {
            int div = action.IndexOf(":");
            string type = action.Substring(0, div);
            int id;
            int.TryParse(action.Substring(div+1), out id);

            if(type == "donation")
            {
                var donation_old = DataAccess.GetRecordById<Dotacja>(id);
                var donation_new = DataAccess.GetRecordById<Dotacja>(id);
                var addition_old = DataAccess.GetRecordById<Wniosek>(id);
                var addition_new = DataAccess.GetRecordById<Wniosek>(id);
                donation_new.zatwierdzone = true;
                addition_new.zatwierdzone = true;
                DataAccess.Update(donation_old, donation_new);
                DataAccess.Update(addition_old, addition_new);
                Wydarzenie_Sponsor.Add(donation_new.wydarzenie, wiadomosc.adresat.sponsor);
                Wiadomosc.Send(
                            "DOTACJA ZOSTAŁA ZAAKCEPTOWANA",
                            $"Wydarzenie:  {donation_new.wydarzenie.nazwa}"
                        + $"\nOrganizator: {logowanie.pracownik.kontakt.imie} {logowanie.pracownik.kontakt.nazwisko}"
                        + $"\nKwota:       {donation_new.kwota}"
                        + $"\nOczekiwania: {donation_new.oczekiwania},",
                            logowanie, wiadomosc.adresat);
                DisplayAdapter.Display(new PanelSkrzynkaWiadomosci(logowanie, mode));
            }
        }
    }
}
