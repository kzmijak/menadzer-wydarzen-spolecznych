using MWS.dbo;
using MWS.Lines;
using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.Pages
{
    class PanelWydarzenieInterakcjaOsoba : _Panel
    {
        public PanelWydarzenieInterakcjaOsoba(Logowanie logowanie, StaticLine note, Wydarzenie selectedEvent, _DatabaseObject selectedUser) : base(logowanie)
        {
            if (selectedUser is Sponsor)
            {
                Contents.Add(new StaticLine((selectedUser as Sponsor).nazwa.ToUpper()));
                Contents.Add(new ActiveLine("Dotacje sponsorskie"));
                Contents.Add(new ActiveLine("Inne wydarzenia"));

                /*
                foreach(var model in selectedEvent.dotacje)
                {
                    if(model.sponsor.id == selectedUser.id)
                    {
                        Contents.Add(new ActiveLine(model.kwota + ": " + model.oczekiwania));
                    }
                }*/
            }
            else if (selectedUser is Pracownik && (selectedUser as Pracownik).stanowisko.ToLower() == "organizator")
            {
                Contents.Add(new StaticLine((selectedUser as Pracownik).kontakt.imie.ToUpper() + " " + (selectedUser as Pracownik).kontakt.nazwisko.ToUpper()));
                Contents.Add(new StaticLine("Telefon: " + (selectedUser as Pracownik).kontakt.telefon));
                Contents.Add(new StaticLine(""));
                Contents.Add(new ActiveLine("Pomocnicy"));
                Contents.Add(new ActiveLine("Inne wydarzenia"));
                Contents.Add(new ActiveLine("Zaproś do kontaktów"));

                /*foreach(var model in (selectedUser as Pracownik).kadra)
                {
                    Contents.Add(new ActiveLine(model.stanowisko + " " + model.kontakt.imie + " " + model.kontakt.nazwisko));
                }*/
            }
            else if (selectedUser is Pracownik && (selectedUser as Pracownik).stanowisko.ToLower() != "organizator")
            {
                Contents.Add(new StaticLine((selectedUser as Pracownik).stanowisko.ToUpper() + " " + (selectedUser as Pracownik).kontakt.imie.ToUpper() + " " + (selectedUser as Pracownik).kontakt.nazwisko.ToUpper()));
                Contents.Add(new StaticLine("Telefon: " + (selectedUser as Pracownik).kontakt.telefon));
                Contents.Add(new StaticLine("Koordynator: " + (selectedUser as Pracownik).kadra[0].kontakt.imie + " " + (selectedUser as Pracownik).kadra[0].kontakt.nazwisko));
                Contents.Add(new StaticLine(""));
                Contents.Add(new ActiveLine("Inne wydarzenia"));
                Contents.Add(new ActiveLine("Zaproś do kontaktów"));
            }
            else if (selectedUser is Uczestnik)
            {
                Contents.Add(new StaticLine((selectedUser as Uczestnik).kontakt.imie.ToUpper() + " " + (selectedUser as Uczestnik).kontakt.nazwisko.ToUpper()));
                Contents.Add(new StaticLine("Telefon: " + (selectedUser as Uczestnik).kontakt.telefon));
                Contents.Add(new StaticLine(""));
                Contents.Add(new ActiveLine("Inne wydarzenia"));
                Contents.Add(new ActiveLine("Zaproś do kontaktów"));
            }
            Contents.Add(new StaticLine(""));
            Contents.Add(new ActiveLine("Powrót"));
            Contents.Add(note);
        }

        public override void React(_Line line)
        {
        }
    }
}
