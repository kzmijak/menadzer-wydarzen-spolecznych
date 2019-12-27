using MWS.dbo;
using MWS.Lines;
using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.Pages
{
    class PanelWydarzenieInterakcjaOsoba : _Panel
    {
        private _CoreObject selectedUser { get; set; }
        private Wydarzenie selectedEvent { get; set; }
        private int listing;
        private int isOrganizer;
        private bool listingWydarzenie;
        private List<Wydarzenie> Wydarzenia = new List<Wydarzenie>(9999);


        public PanelWydarzenieInterakcjaOsoba(Logowanie logowanie, Wydarzenie selectedEvent, _CoreObject selectedUser, bool listingWydarzenie = false, StaticLine note = null) : base(logowanie)
        {
            this.selectedEvent = selectedEvent;
            this.selectedUser = selectedUser;
            this.listingWydarzenie = listingWydarzenie;
            isOrganizer = 0;
            if (logowanie.owner is Pracownik)
            {
                foreach (Pracownik organizer in selectedEvent.organizatorzy)
                {
                    if (organizer.id == logowanie.pracownik.id)
                    {
                        isOrganizer = 1;
                    }
                }
            }
            Constructor(selectedUser);

            
            Contents.Add(new ActiveLine("Inne wydarzenia"));
            if (listingWydarzenie)
            {
                if(Wydarzenia.Count > 0)
                {
                    DisplayAdapter.Display(new PanelWydarzenieInterakcjaOsoba(logowanie, selectedEvent, selectedUser, false));
                }

                foreach(Wydarzenie wydarzenie in selectedUser.wydarzenia)
                {
                    if(wydarzenie.id != selectedEvent.id)
                    {
                        Contents.Add(new ActiveLine(" " + wydarzenie.nazwa));
                        Wydarzenia.Add(wydarzenie);
                    }
                }
                if(Wydarzenia.Count == 0)
                {
                    Contents.Add(new StaticLine(" Użytkownik nie bierze udziału w żadnym innym wydarzeniu"));
                }
            }
            Contents.Add(new ActiveLine("Zaproś do kontaktów"));
            if(isOrganizer == 1)
                Contents.Add(new ActiveLine("Usuń z wydarzenia"));
            Contents.Add(new StaticLine(""));                       
            Contents.Add(new ActiveLine("Powrót"));                 
            Contents.Add(note);                                     
        }                                                           

        public override void React(_Line line)
        {
            int wcount = 0;
            if(listingWydarzenie)
            {
                wcount = Wydarzenia.Count;
                if (wcount == 0)
                    wcount++;
            }

            if (line.Index == Contents.Count - 4 - isOrganizer - wcount)
            {
                DisplayAdapter.Display(new PanelWydarzenieInterakcjaOsoba(logowanie, selectedEvent, selectedUser, true));
            }
            if(listingWydarzenie && line.Index > (Contents.Count - 5 - isOrganizer - wcount) && line.Index < (Contents.Count - 4 - isOrganizer))
            {
                DisplayAdapter.Display(new PanelWydarzenieInterakcja(logowanie, Wydarzenia[0]));
            }
            if (line.Index == Contents.Count - 4 - isOrganizer)
            {
                if(selectedUser.id == logowanie.owner.id && selectedUser.GetType() == logowanie.owner.GetType())
                {
                    DisplayAdapter.Display(new PanelWydarzenieInterakcjaOsoba(logowanie, selectedEvent, selectedUser, false, new StaticLine("Nie możesz wysłać zaproszenia samemu sobie.", ConsoleColor.Red)));
                }
                else
                {
                    Wniosek addition = new Wniosek
                    {
                        kwota = 0,
                        akcja = "FriendslistInvitation"
                    };

                    Wiadomosc.Send("ZAPROSZENIE DO GRONA ZNAJOMYCH",
                        $"Użytkownik {logowanie.owner.kontakt.imie} {logowanie.owner.kontakt.nazwisko} zaprasza cię do grona znajomych.",
                        logowanie, selectedUser.logowanie, addition
                        );
                    DisplayAdapter.Display(new PanelWydarzenieInterakcjaOsoba(logowanie, selectedEvent, selectedUser, false, new StaticLine("Zaproszenie zostało wysłane.", ConsoleColor.Green)));
                }
            }
            else if (isOrganizer == 1 && line.Index == Contents.Count - 4)
            {
                DataAccess.Delete(selectedEvent, selectedUser);                
                Wiadomosc.Send("NIE BIERZESZ JUŻ UDZIAŁU W WYDARZENIU",
                    $"Nie bierzesz już udziału w wydarzeniu \"{selectedEvent.nazwa}\"."
                  + $"\nDecyzja podjęta przez: {logowanie.pracownik.kontakt.imie} {logowanie.pracownik.kontakt.nazwisko}.",
                    logowanie, selectedUser.logowanie);
                DisplayAdapter.Display(new PanelWydarzenieInterakcja(logowanie, selectedEvent, new StaticLine("Użytkownik został usunięty z wydarzenia.", ConsoleColor.Green)));

            }
            else if (line.Index == Contents.Count - 2)
            {
                DisplayAdapter.Display(new PanelWydarzenieInterakcja(logowanie, selectedEvent));
            }
            else if (selectedUser is Sponsor)
            {
                ReactSponsor(line.Index);
            }
            else if (selectedUser is Pracownik && (selectedUser as Pracownik).stanowisko.ToLower() == "organizator")
            {
                ReactOrganizator(line.Index);
            }
            else if (selectedUser is Pracownik && (selectedUser as Pracownik).stanowisko.ToLower() != "organizator")
            {
                ReactPracownik(line.Index);
            }
        }

        private void ConstructorOrganizator()
        {
            Contents.Add(new StaticLine((selectedUser as Pracownik).kontakt.imie.ToUpper() + " " + (selectedUser as Pracownik).kontakt.nazwisko.ToUpper()));
            Contents.Add(new StaticLine("Telefon: " + (selectedUser as Pracownik).kontakt.telefon));
            Contents.Add(new ActiveLine("Pracownicy"));
            foreach(var model in (selectedUser as Pracownik).kadra)
            {
                Contents.Add(new ActiveLine(model.stanowisko + " " + model.kontakt.imie + " " + model.kontakt.nazwisko));
            }
        }

        private void ReactOrganizator(int index)
        {

        }

        private void ConstructorPracownik()
        {
            Contents.Add(new StaticLine((selectedUser as Pracownik).stanowisko.ToUpper() + " " + (selectedUser as Pracownik).kontakt.imie.ToUpper() + " " + (selectedUser as Pracownik).kontakt.nazwisko.ToUpper()));
            Contents.Add(new StaticLine("Telefon: " + (selectedUser as Pracownik).kontakt.telefon));
            Contents.Add(new ActiveLine("Koordynator: " + (selectedUser as Pracownik).kadra[0].kontakt.imie + " " + (selectedUser as Pracownik).kadra[0].kontakt.nazwisko));
            ContactForm();
        }

        private void ReactPracownik(int index)
        {

        }

        private void ConstructorSponsor()
        {
            Contents.Add(new StaticLine((selectedUser as Sponsor).nazwa.ToUpper()));
            Contents.Add(new ActiveLine("Dotacje sponsorskie"));
            listing = 0;
            foreach(var model in selectedEvent.dotacje)
            {
                if(model.sponsor.id == selectedUser.id && model.zatwierdzone == true)
                {
                    Contents.Add(new ActiveLine(model.kwota + ": " + model.oczekiwania));
                    listing++;
                }
                if(listing == 0)
                {
                    Contents.Add(new StaticLine("Wybrany sponsor nie ma zatwierdzonych dotacji"));
                }
            }
        }

        private void ReactSponsor(int index)
        {
            
        }

        private void ConstructorUczestnik()
        {
            Contents.Add(new StaticLine((selectedUser as Uczestnik).kontakt.imie.ToUpper() + " " + (selectedUser as Uczestnik).kontakt.nazwisko.ToUpper()));
            Contents.Add(new StaticLine("Telefon: " + (selectedUser as Uczestnik).kontakt.telefon));
            ContactForm();
        }

        private void ContactForm()
        {
            if(isOrganizer == 1)
            {
                Contents.Add(new StaticLine("Email: \t\t" + (selectedUser as Uczestnik).kontakt.email));
                Contents.Add(new StaticLine("Miejscowość: \t" + (selectedUser as Uczestnik).kontakt.miejscowosc));
                Contents.Add(new StaticLine("Numer domu: \t" + (selectedUser as Uczestnik).kontakt.nrdomu));
                Contents.Add(new StaticLine("Miasto: \t" + (selectedUser as Uczestnik).kontakt.miasto));
                Contents.Add(new StaticLine("Kod pocztowy: \t" + (selectedUser as Uczestnik).kontakt.poczta));
                Contents.Add(new StaticLine("Ulica: \t\t" + (selectedUser as Uczestnik).kontakt.ulica));
            }
        }

        private void Constructor(_CoreObject selectedUser) 
        {
            if (selectedUser is Sponsor)
            {
                ConstructorSponsor();
            }
            else if (selectedUser is Pracownik && (selectedUser as Pracownik).stanowisko.ToLower() == "organizator")
            {
                ConstructorOrganizator();
            }
            else if (selectedUser is Pracownik && (selectedUser as Pracownik).stanowisko.ToLower() != "organizator")
            {
                ConstructorPracownik();
            }
            else if (selectedUser is Uczestnik)
            {
                ConstructorUczestnik();
            }
        }
    }
}
