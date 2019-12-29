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
        private int isOrganizer;
        private int constructorLinesCount;
        private string listingcode;
        private List<Wydarzenie> wydarzenia
        {
            get
            {
                List<Wydarzenie> output = new List<Wydarzenie>(9999);
                foreach (Wydarzenie wydarzenie in selectedUser.wydarzenia)
                {
                    if (wydarzenie.id != selectedEvent.id)
                    {
                        output.Add(wydarzenie);
                    }
                }
                return output;
            }
        }
        private List<Dotacja> dotacje
        {
            get
            {
                return selectedEvent.dotacje;
            }
        }
        private List<Pracownik> pracownicy
        {
            get
            {
                List<Pracownik> output = new List<Pracownik>(9999);
                foreach(Pracownik pracownik in (selectedUser as Pracownik).kadra)
                {
                    foreach(Pracownik pracownikwydarzenie in selectedEvent.pracownicy)
                    {
                        if(pracownik.id == pracownikwydarzenie.id)
                        {
                            output.Add(pracownik);
                        }
                    }
                }
                return output;
            }
        }
        

        public PanelWydarzenieInterakcjaOsoba(Logowanie logowanie, Wydarzenie selectedEvent, _CoreObject selectedUser, string listingcode = "00", StaticLine note = null) : base(logowanie)
        {
            this.selectedEvent = selectedEvent;
            this.selectedUser = selectedUser;
            this.listingcode = listingcode;
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

            constructorLinesCount = Constructor(selectedUser);
            
            Contents.Add(new ActiveLine("Inne wydarzenia"));
            Expand(wydarzenia, listingcode[1]);
            Contents.Add(new ActiveLine("Zaproś do kontaktów"));
            if(isOrganizer == 1)
                Contents.Add(new ActiveLine("Usuń z wydarzenia"));
            Contents.Add(new StaticLine(""));                       
            Contents.Add(new ActiveLine("Powrót"));                 
            Contents.Add(note);                                     
        }                                                           

        public override void React(_Line line)
        {
            int cnt = Expand(wydarzenia,listingcode[1], false);
            if (line.Index == constructorLinesCount) // inne wydarzenia
            {
                DisplayAdapter.Display(new PanelWydarzenieInterakcjaOsoba(logowanie, selectedEvent, selectedUser, Reverse(1)));
            }
            else if(line.Index > constructorLinesCount && line.Index < constructorLinesCount + Expand(wydarzenia, listingcode[1], false) + 1) // wydarzenie
            {
                DisplayAdapter.Display(new PanelWydarzenieInterakcja(logowanie, wydarzenia[line.Index - constructorLinesCount - 1]));
            } // zaproś do kontaktów
            else if (line.Index == constructorLinesCount + Expand(wydarzenia, listingcode[1], false) + 1)
            {
                if(selectedUser.id == logowanie.owner.id && selectedUser.GetType() == logowanie.owner.GetType())
                {
                    DisplayAdapter.Display(new PanelWydarzenieInterakcjaOsoba(logowanie, selectedEvent, selectedUser, listingcode, new StaticLine("Nie możesz wysłać zaproszenia samemu sobie.", ConsoleColor.Red)));
                }
                else
                {
                    Wniosek addition = new Wniosek
                    {
                        kwota = 0,
                        akcja = "contactsAdd"
                    };

                    string msg = "";
                    if(logowanie.owner is Sponsor)
                    {
                        msg = $"Sponsor \"{logowanie.sponsor.nazwa}\" zaprasza cię do grona znajomych.";
                    }
                    else
                    {
                        msg = $"Użytkownik {logowanie.owner.kontakt.imie} {logowanie.owner.kontakt.nazwisko} zaprasza cię do grona znajomych.";

                    }
                    Wiadomosc.Send("ZAPROSZENIE DO GRONA ZNAJOMYCH",
                        msg,
                        logowanie, selectedUser.logowanie, addition
                        );
                    DisplayAdapter.Display(new PanelWydarzenieInterakcjaOsoba(logowanie, selectedEvent, selectedUser, listingcode, new StaticLine("Zaproszenie zostało wysłane.", ConsoleColor.Green)));
                }
            } // usuń z wydarzenia jeśli (isOrganizer == 1)
            else if (isOrganizer == 1 && line.Index == Contents.Count - 4)
            {
                DataAccess.Delete(selectedEvent, selectedUser);                
                Wiadomosc.Send("NIE BIERZESZ JUŻ UDZIAŁU W WYDARZENIU",
                    $"Nie bierzesz już udziału w wydarzeniu \"{selectedEvent.nazwa}\"."
                  + $"\nDecyzja podjęta przez: {logowanie.pracownik.kontakt.imie} {logowanie.pracownik.kontakt.nazwisko}.",
                    logowanie, selectedUser.logowanie);
                DisplayAdapter.Display(new PanelWydarzenieInterakcja(logowanie, selectedEvent, new StaticLine("Użytkownik został usunięty z wydarzenia.", ConsoleColor.Green)));

            }
            else if (line.Index == Contents.Count - 2) //powrót
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

        private int ConstructorOrganizator()
        {
            Contents.Add(new StaticLine((selectedUser as Pracownik).kontakt.imie.ToUpper() + " " + (selectedUser as Pracownik).kontakt.nazwisko.ToUpper()));
            Contents.Add(new StaticLine("Telefon: " + (selectedUser as Pracownik).kontakt.telefon));
            Contents.Add(new ActiveLine("Pracownicy"));
            return 3 + Expand(pracownicy, listingcode[0]);
        }
        private void ReactOrganizator(int index)
        {
            if(index == 2)
            {
                DisplayAdapter.Display(new PanelWydarzenieInterakcjaOsoba(logowanie, selectedEvent, selectedUser, Reverse(0)));
            }
            else
            {
                DisplayAdapter.Display(new PanelWydarzenieInterakcjaOsoba(logowanie, selectedEvent, pracownicy[index - 3]));
            }
        }

        private int ConstructorPracownik()
        {
            Contents.Add(new StaticLine((selectedUser as Pracownik).stanowisko.ToUpper() + " " + (selectedUser as Pracownik).kontakt.imie.ToUpper() + " " + (selectedUser as Pracownik).kontakt.nazwisko.ToUpper()));
            Contents.Add(new StaticLine("Telefon: " + (selectedUser as Pracownik).kontakt.telefon));
            Contents.Add(new ActiveLine("Koordynator: " + (selectedUser as Pracownik).kadra[0].kontakt.imie + " " + (selectedUser as Pracownik).kadra[0].kontakt.nazwisko));
            return ContactForm() + 3;
        }
        private void ReactPracownik(int index)
        {
            if(index == 2)
            {
                DisplayAdapter.Display(new PanelWydarzenieInterakcjaOsoba(logowanie, selectedEvent, (selectedUser as Pracownik).kadra[0]));
            }
        }

        private int ConstructorSponsor()
        {
            Contents.Add(new StaticLine((selectedUser as Sponsor).nazwa.ToUpper()));
            Contents.Add(new ActiveLine("Dotacje sponsorskie"));
            return Expand(dotacje, listingcode[0]) + 2;
        }
        private void ReactSponsor(int index)
        {
            if(index == 1)
            {
                DisplayAdapter.Display(new PanelWydarzenieInterakcjaOsoba(logowanie, selectedEvent, selectedUser, Reverse(0)));
            }
        }

        private int ConstructorUczestnik()
        {
            Contents.Add(new StaticLine((selectedUser as Uczestnik).kontakt.imie.ToUpper() + " " + (selectedUser as Uczestnik).kontakt.nazwisko.ToUpper()));
            Contents.Add(new StaticLine("Telefon: " + (selectedUser as Uczestnik).kontakt.telefon));
            return ContactForm() + 2;
        }

        private int ContactForm()
        {
            if(isOrganizer == 1)
            {
                Contents.Add(new StaticLine("Email: \t\t" + selectedUser.kontakt.email));
                Contents.Add(new StaticLine("Miejscowość: \t" + selectedUser.kontakt.miejscowosc));
                Contents.Add(new StaticLine("Numer domu: \t" + selectedUser.kontakt.nrdomu));
                Contents.Add(new StaticLine("Miasto: \t" + selectedUser.kontakt.miasto));
                Contents.Add(new StaticLine("Kod pocztowy: \t" + selectedUser.kontakt.poczta));
                Contents.Add(new StaticLine("Ulica: \t\t" + selectedUser.kontakt.ulica));
                return 6;
            }
            return 0;
        }
        private int Constructor(_CoreObject selectedUser) 
        {
            if (selectedUser is Sponsor)
            {
                return ConstructorSponsor();
            }
            else if (selectedUser is Pracownik && (selectedUser as Pracownik).stanowisko.ToLower() == "organizator")
            {
                return ConstructorOrganizator();
            }
            else if (selectedUser is Pracownik && (selectedUser as Pracownik).stanowisko.ToLower() != "organizator")
            {
                return ConstructorPracownik();
            }
            else if (selectedUser is Uczestnik)
            {
                return ConstructorUczestnik();
            }
            return 0;
        }
        private int Expand<T>(List<T> source, char check = '2', bool display = true) where T: _DatabaseObject
        {
            
            int cnt = 0;
            if(check == '1' || check == '2')
            {
                if (source.Count == 0)
                {
                    if(check == '1' && display)
                        Contents.Add(new StaticLine(" Brak danych do wyświetlenia"));
                    cnt++;
                }
                else
                { 
                    foreach (var obj in source)
                    {
                        if(obj is Wydarzenie)
                        {
                            if(check == '1' && display)
                                Contents.Add(new ActiveLine(" " + (obj as Wydarzenie).nazwa));
                            cnt++;
                        }
                        if(obj is Dotacja)
                        {
                            if (check == '1' && display)
                                Contents.Add(new StaticLine(" " + (obj as Dotacja).kwota + ": " + (obj as Dotacja).oczekiwania));
                            cnt++;
                        }
                        if(obj is Pracownik)
                        {
                            if (check == '1' && display)
                                Contents.Add(new ActiveLine(" " + (obj as Dotacja).kwota + ": " + (obj as Dotacja).oczekiwania));
                            cnt++;
                        }
                    }
                }
            }
            return cnt;
        }
        private string Reverse(int position)
        {
            var expandcode = this.listingcode.ToCharArray();
            if (expandcode[position] == '0')
            {
                expandcode[position] = '1';
            }
            else
            {
                expandcode[position] = '0';
            }
            return new string(expandcode);
        }
    }
}
