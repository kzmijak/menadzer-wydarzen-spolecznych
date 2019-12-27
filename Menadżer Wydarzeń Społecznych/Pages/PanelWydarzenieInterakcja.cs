using System;
using System.Collections.Generic;
using System.Text;
using MWS.dbo;
using MWS.Lines;

namespace MWS.Pages
{
    class PanelWydarzenieInterakcja : _Panel
    {
        List<_CoreObject> objects = new List<_CoreObject>(9999);
        private string listing = "";
        private Wydarzenie wydarzenie = new Wydarzenie();
        private bool isIn = false;

        public PanelWydarzenieInterakcja(Logowanie logowanie, Wydarzenie wydarzenie, StaticLine note = null, string listing = null): base(logowanie)
        {
            this.wydarzenie = wydarzenie;

            this.listing = listing;
            Contents.Add(new StaticLine($"WYDARZENIE \"{wydarzenie.nazwa.ToUpper()}\""));
            Contents.Add(new StaticLine($"Opis: \t\t\t{wydarzenie.opis}"));
            Contents.Add(new StaticLine($"Miejsce: \t\t{wydarzenie.miejsce}"));
            Contents.Add(new StaticLine($"Data: \t\t\t{wydarzenie.dzien.ToShortDateString()} {wydarzenie.godzina.ToString().Substring(0, 5)}"));
            if(wydarzenie.organizatorzy.Count != 0)
                Contents.Add(new StaticLine($"Główny organizator: \t{wydarzenie.organizatorzy[0].kontakt.imie} {wydarzenie.organizatorzy[0].kontakt.nazwisko}"));
            else
                Contents.Add(new StaticLine($"Główny organizator: \tbrak"));
            Contents.Add(new StaticLine($"Budżet: \t\t{wydarzenie.budzet}PLN"));
            Contents.Add(new StaticLine(""));
            Contents.Add(new StaticLine("CZŁONKOWIE"));
            Contents.Add(new ActiveLine("Organizatorzy")); //8

            if (listing == "organizator")
            {
                if (wydarzenie.organizatorzy.Count != 0)
                {
                    foreach (Pracownik p in wydarzenie.organizatorzy)
                    {
                        objects.Add(p);
                        Contents.Add(new ActiveLine($" {p.kontakt.imie} {p.kontakt.nazwisko}"));
                    }
                }
                else
                    Contents.Add(new StaticLine(" Organizatorzy są chwilowo niedostępni."));
            }

            Contents.Add(new ActiveLine("Kadra"));

            if (listing == "pracownik")
            {
                if (wydarzenie.pracownicy.Count != 0)
                {
                    foreach (Pracownik p in wydarzenie.pracownicy)
                    {
                        Contents.Add(new ActiveLine($" {p.stanowisko} {p.kontakt.imie} {p.kontakt.nazwisko}"));
                        objects.Add(p);
                    }
                }
                else
                    Contents.Add(new StaticLine(" Informacje o kadrze są obecnie niedostępne."));
            }

            Contents.Add(new ActiveLine("Sponsorzy"));

            if (listing == "sponsor")
            {
                if (wydarzenie.sponsorzy.Count != 0)
                {
                    foreach (Sponsor p in wydarzenie.sponsorzy)
                    {
                        Contents.Add(new ActiveLine($" {p.nazwa}"));
                        objects.Add(p);
                    }
                }
                else
                    Contents.Add(new StaticLine(" Informacje o sponsorach nie są obecnie dostępne."));
            }

            Contents.Add(new ActiveLine("Uczestnicy"));

            if (listing == "uczestnik")
            {
                if (wydarzenie.uczestnicy.Count != 0)
                {
                    foreach (Uczestnik p in wydarzenie.uczestnicy)
                    {
                        Contents.Add(new ActiveLine($" {p.kontakt.imie} {p.kontakt.nazwisko}"));
                        objects.Add(p);
                    }
                }
                else
                    Contents.Add(new StaticLine(" Wydarzenie nie ma jeszcze zapisanych uczestników."));
            }

            Contents.Add(new StaticLine(""));
            if (logowanie.owner is Sponsor)
                Contents.Add(new ActiveLine("Wyślij dotację"));
            else
            {
                foreach(var ob in wydarzenie.czlonkowie)
                {
                    if (ob.GetType() == logowanie.owner.GetType())
                    {
                        if(ob.id == logowanie.owner.id)
                        {
                            Contents.Add(new ActiveLine("Szczegóły członkostwa"));
                            isIn = true;
                        }
                    }
                }
                if(!isIn)
                    Contents.Add(new ActiveLine("Dołącz"));
            }
            Contents.Add(new ActiveLine("Powrót"));
            if (note!=null)
            {
                Contents.Add(note);
            }
        }

        public override void React(_Line line)
        {
            int cnt = 0;

            if (line.Index == 8)     // Organizatorzy
            {
                if(listing != "organizator")
                    DisplayAdapter.Display(new PanelWydarzenieInterakcja(logowanie, wydarzenie, null, "organizator"));
                else
                    DisplayAdapter.Display(new PanelWydarzenieInterakcja(logowanie, wydarzenie));
            }
            if (listing == "organizator")
            {
                cnt = wydarzenie.organizatorzy.Count;
                if (wydarzenie.organizatorzy.Count == 0)
                    cnt++;

                if (line.Index > 8 && line.Index < 9 + cnt)
                {
                    DisplayAdapter.Display(new PanelWydarzenieInterakcjaOsoba(logowanie, wydarzenie, objects[line.Index - 9]));
                }
            }


            if (line.Index == 9 + cnt) // Pracownicy
            {
                if(listing != "pracownik")
                DisplayAdapter.Display(new PanelWydarzenieInterakcja(logowanie, wydarzenie, null, "pracownik"));
                else
                    DisplayAdapter.Display(new PanelWydarzenieInterakcja(logowanie, wydarzenie));
            }
            if (listing == "pracownik")
            {
                cnt = wydarzenie.pracownicy.Count;
                if (wydarzenie.pracownicy.Count == 0)
                    cnt++;

                if (line.Index > 9 && line.Index < 10 + cnt)
                {
                    DisplayAdapter.Display(new PanelWydarzenieInterakcjaOsoba(logowanie, wydarzenie, objects[line.Index - 10]));
                }
            }

            if (line.Index == 10 + cnt) // Sponsorzy
            {
                if(listing != "sponsor")
                    DisplayAdapter.Display(new PanelWydarzenieInterakcja(logowanie, wydarzenie, null, "sponsor"));
                else
                    DisplayAdapter.Display(new PanelWydarzenieInterakcja(logowanie, wydarzenie));
            }
            if (listing == "sponsor")
            {
                cnt = wydarzenie.sponsorzy.Count;
                if (wydarzenie.sponsorzy.Count == 0)
                    cnt++;

                if (line.Index > 10 && line.Index < 11 + cnt)
                {
                    DisplayAdapter.Display(new PanelWydarzenieInterakcjaOsoba(logowanie, wydarzenie, objects[line.Index - 11]));
                }
            }

            if (line.Index == 11 + cnt) // Członkowie
            {
                if(listing != "uczestnik")
                    DisplayAdapter.Display(new PanelWydarzenieInterakcja(logowanie, wydarzenie, null, "uczestnik"));
                else
                    DisplayAdapter.Display(new PanelWydarzenieInterakcja(logowanie, wydarzenie));
            }
            if (listing == "uczestnik")
            {
                cnt = wydarzenie.uczestnicy.Count;
                if (wydarzenie.uczestnicy.Count == 0)
                    cnt++;
                
                if(line.Index > 11 && line.Index < 13 + cnt)
                {
                    DisplayAdapter.Display(new PanelWydarzenieInterakcjaOsoba(logowanie, wydarzenie, objects[line.Index - 12]));
                }
            }
            if (line.Index == 13 + cnt)
            {
                if(logowanie.owner is Pracownik)
                {
                    if(logowanie.pracownik.stanowisko.ToLower() == "organizator")
                    {
                        if(!isIn)
                        {
                            if(wydarzenie.organizatorzy.Count == 0)
                            {
                                Wydarzenie_Pracownik.Add(wydarzenie, logowanie.owner);
                                DisplayAdapter.Display(new PanelWydarzenieInterakcja(logowanie, wydarzenie, new StaticLine("Jesteś teraz głównym organizatorem wydarzenia.", ConsoleColor.Green)));
                            }
                            else
                            {
                                DisplayAdapter.Display(new PanelWydarzenieInterakcjaAplikacja(logowanie, wydarzenie));
                            }
                        }
                        else
                        {
                            DisplayAdapter.Display(new PanelWydarzenieInterakcjaCzlonek(logowanie, wydarzenie));
                        }
                    }
                    else
                    {
                        if(!isIn)
                        {
                            bool hasOrg = false;
                            foreach(var org in wydarzenie.organizatorzy)
                            {
                                foreach(var pra in org.kadra)
                                {
                                    if (pra.id == logowanie.owner.id)
                                        hasOrg = true;
                                }
                            }
                            if(hasOrg)
                            {
                                Wydarzenie_Pracownik.Add(wydarzenie, logowanie.owner);
                                DisplayAdapter.Display(new PanelWydarzenieInterakcja(logowanie, wydarzenie, new StaticLine("Zostałeś zapisany do kadry pracowników.", ConsoleColor.Green)));
                            }
                            else
                            {
                                if(logowanie.pracownik.kadra.Count != 0)
                                    DisplayAdapter.Display(new PanelWydarzenieInterakcja(logowanie, wydarzenie, new StaticLine($"Twój nadzorca {logowanie.pracownik.kadra[0].kontakt.imie + " " + logowanie.pracownik.kadra[0].kontakt.nazwisko} nie bierze udziału w tym wydarzeniu.", ConsoleColor.Red)));
                                else
                                    DisplayAdapter.Display(new PanelWydarzenieInterakcja(logowanie, wydarzenie, new StaticLine($"Przed wzięciem udziału w wydarzeniu musisz się zatrudnić u jednego z organizatorów biorących udział w tym wydarzeniu.", ConsoleColor.Red)));
                            }
                        }
                        else
                        {
                            DisplayAdapter.Display(new PanelWydarzenieInterakcjaCzlonek(logowanie, wydarzenie));
                        }
                    }
                }
                if(logowanie.owner is Sponsor)
                {
                    // REDIRECT TO SEND DONATION TAB PanelWydarzenieInterakcjaDotacja
                    DisplayAdapter.Display(new PanelWydarzenieInterakcjaDotacja(logowanie, wydarzenie)); 
                }
                if (logowanie.owner is Uczestnik)
                {
                    if(wydarzenie.bilety.Count == 0)
                    {
                        DisplayAdapter.Display(new PanelWydarzenieInterakcja(logowanie, wydarzenie, new StaticLine("Organizator nie wydał jeszcze biletów na to wydarzenie.", ConsoleColor.Red)));
                    }
                    else
                    {
                        DisplayAdapter.Display(new PanelWydarzenieInterakcjaCzlonek(logowanie, wydarzenie));
                    }
                }

            }

            if(line.Index == 14 + cnt)
            {
                DisplayAdapter.Display(new PanelWydarzenia(logowanie));
            }


        }
    }
}
