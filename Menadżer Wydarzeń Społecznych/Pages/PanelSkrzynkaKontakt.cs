using System;
using System.Collections.Generic;
using System.Text;
using MWS.dbo;
using MWS.Lines;

namespace MWS.Pages
{
    class PanelSkrzynkaKontakt : _Panel
    {
        _CoreObject user;
        string listingcode;
        private List<Wydarzenie> wydarzenia
        {
            get
            {
                return user.wydarzenia;
            }
        }
        private List<string> contact
        {
            get
            {
                if (user is Sponsor)
                    return null;
                else
                    return new List<string>
                    {
                        "Email: \t" + user.kontakt.email,
                        "Miejscowość: \t" + user.kontakt.miejscowosc,
                        "Numer domu: \t" + user.kontakt.nrdomu,
                        "Miasto: \t" + user.kontakt.miasto,
                        "Kod pocztowy: \t" + user.kontakt.poczta,
                        "Ulica: \t" + user.kontakt.ulica
                    };
            }
        }
        private string action;
        decimal salary = 0;

        public PanelSkrzynkaKontakt(Logowanie logowanie, _CoreObject user, string listingcode = "00", StaticLine note = null, decimal salary = 0) : base(logowanie)
        {
            if(salary != 0)
            {
                this.salary = salary;
            }
            else if (user is Pracownik)
            {
                this.salary = (user as Pracownik).wynagrodzenie;
            }


            this.user = user;
            this.listingcode = listingcode;
            string staticline;
            if (user is Sponsor)
            {
                staticline = (user as Sponsor).nazwa.ToUpper();
            }
            else
            {
                staticline = user.kontakt.imie.ToUpper() + " " + user.kontakt.nazwisko.ToUpper();
            }

            string pracownik_pracownik = "";
            if(logowanie.owner is Pracownik)
            {
                if(logowanie.pracownik.stanowisko.ToLower() == "organizator" && (user as Pracownik).stanowisko.ToLower() != "organizator")
                {
                    if((user as Pracownik).kadra.Count == 0 || (user as Pracownik).kadra[0].id != logowanie.pracownik.id)
                    {
                        pracownik_pracownik = "Zatrudnij";
                        action = "employHim";
                    }
                    else
                    {
                        pracownik_pracownik = $"Zmień wynagrodzenie (obecnie {this.salary}PLN)";
                        action = "changeHim";
                    }
                }
                else if (logowanie.pracownik.stanowisko.ToLower() != "organizator" && (user as Pracownik).stanowisko.ToLower() == "organizator")
                {
                    if (logowanie.pracownik.kadra.Count == 0 || logowanie.pracownik.kadra[0].id != user.id)
                    {
                        pracownik_pracownik = "Wyślij wniosek o zatrudnienie";
                        action = "employMe";
                    }
                    else
                    {
                        pracownik_pracownik = $"Zmień wynagrodzenie (obecnie {this.salary}PLN)";
                        action = "changeMe";
                    }
                }
            }

            Contents.Add(new StaticLine(staticline));
            Contents.Add(new ActiveLine("O użytkowniku"));
            Expand(contact, listingcode[0]);
            Contents.Add(new ActiveLine("Wydarzenia użytkownika"));
            Expand(wydarzenia, listingcode[1]);
            Contents.Add(new StaticLine(""));
            Contents.Add(new ActiveLine(pracownik_pracownik));
            Contents.Add(new ActiveLine("Wyślij wiadomość"));
            Contents.Add(new ActiveLine("Odebrane wiadomości"));
            Contents.Add(new ActiveLine("Wysłane wiadomości"));
            Contents.Add(new StaticLine(""));
            Contents.Add(new ActiveLine("Usuń z kontaktów"));
            Contents.Add(new ActiveLine("Powrót"));
            Contents.Add(note);

        }

        public override void React(_Line line)
        {
            if(line.Index == 1)
            {
                DisplayAdapter.Display(new PanelSkrzynkaKontakt(logowanie, user, Reverse(0)));
            }
            else if(line.Index == 2 + Expand(contact, listingcode[0], false))
            {
                DisplayAdapter.Display(new PanelSkrzynkaKontakt(logowanie, user, Reverse(1)));
            }
            else if(line.Index > 2 + Expand(contact, listingcode[0], false) && line.Index < 3 + Expand(contact, listingcode[0], false) + Expand(contact, listingcode[1], false))
            {
                DisplayAdapter.Display(new PanelWydarzenieInterakcjaOsoba(logowanie, wydarzenia[line.Index - 4 - Expand(contact, listingcode[0], false)], user));
            }
            else if (user is Pracownik && line.Index == Contents.Count - 8)
            {
                decimal salarytmp;
                Console.WriteLine("Wpisz proponowaną wypłatę:");
                if (!decimal.TryParse(Console.ReadLine(), out salarytmp))
                {
                    DisplayAdapter.Display(new PanelSkrzynkaKontakt(logowanie, user, listingcode, new StaticLine("Niepoprawny format liczbowy.", ConsoleColor.Red)));
                }

                if (logowanie.pracownik.stanowisko.ToLower() == "organizator" && (user as Pracownik).stanowisko.ToLower() != "organizator")
                {
                    if(action == "employHim")
                    {
                        Wniosek addition = new Wniosek
                        {
                            kwota = salarytmp,
                            akcja = action
                        };
                        Wiadomosc.Send("NOWA OFERTA PRACY",
                            $"Nadawca wiadomości oferuje pracę o wynagrodzeniu {salarytmp}PLN." +
                            $"\n(Wiadomość wygenerowana automatycznie)",
                            logowanie, user.logowanie, addition);

                    }
                    if(action == "changeHim")
                    {
                        Wiadomosc.Send("ZMIANA WYNAGRODZENIA",
                            "Twój menadżer zmienił twoje wynagrodzenie." +
                            $"\nKwota: {salary} -> {salarytmp}" +
                            "\n(Wiadomość wygenerowana automatycznie)",
                            logowanie, user.logowanie);
                        (user as Pracownik).wynagrodzenie = salarytmp;
                        DataAccess.Update(user, user);
                    }
                }
                if (logowanie.pracownik.stanowisko.ToLower() != "organizator" && (user as Pracownik).stanowisko.ToLower() == "organizator")
                {
                    if (action == "employMe")
                    {
                        Wniosek addition = new Wniosek
                        {
                            kwota = salarytmp,
                            akcja = action
                        };
                        Wiadomosc.Send("NOWY WNIOSEK O ZATRUDNIENIE",
                            $"Nadawca wiadomości wnioskuje o pracę o wynagrodzeniu {salarytmp}PLN." +
                            $"\n(Wiadomość wygenerowana automatycznie)",
                            logowanie, user.logowanie, addition);

                    }
                    if (action == "changeMe")
                    {
                        Wniosek addition = new Wniosek
                        {
                            kwota = salarytmp,
                            akcja = action
                        };
                        Wiadomosc.Send("NOWY WNIOSEK O ZMIANĘ WYNAGRODZENIA",
                            $"Nadawca wiadomości wnioskuje o zmianę wynagrodzenie na wysokość {salarytmp}PLN." +
                            $"\n(Wiadomość wygenerowana automatycznie)",
                            logowanie, user.logowanie, addition);
                    }
                }
                DisplayAdapter.Display(new PanelSkrzynkaKontakt(logowanie, user, listingcode, new StaticLine("Czynność została wykonana.", ConsoleColor.Red), salarytmp));
            }
            else if (line.Index == Contents.Count - 7)
            {
                // TO DO
            }
            else if (line.Index == Contents.Count - 6)
            {
                DisplayAdapter.Display(new PanelSkrzynkaWiadomosci(logowanie, "ODEBRANE", user.logowanie));
            }
            else if (line.Index == Contents.Count - 5)
            {
                DisplayAdapter.Display(new PanelSkrzynkaWiadomosci(logowanie, "WYSŁANE", user.logowanie));
            }
            else if (line.Index == Contents.Count - 3)
            {
                DataAccess.Delete(logowanie, user.logowanie);
                DataAccess.Delete(user.logowanie, logowanie);

                if(logowanie.owner is Pracownik && user is Pracownik)
                {
                    if(logowanie.pracownik.stanowisko.ToLower() == "organizator" && (user as Pracownik).stanowisko.ToLower() != "organizator")
                    {
                        Wiadomosc.Send("ZMIANA STATUSU ZATRUDNIENIA",
                            "Nadawca wiadomości nie jest już twoim menadżerem." +
                            "\nWiadomość wygenerowana automatycznie.",
                            logowanie, user.logowanie);
                        (user as Pracownik).wynagrodzenie = 0;
                        DataAccess.Update(user, user);
                        var db = user.wydarzenia;
                        foreach(Wydarzenie wydarzenie in db)
                        {
                            DataAccess.Delete(wydarzenie, user);
                        }
                        DataAccess.Delete(logowanie.pracownik, user);
                    }
                    if (logowanie.pracownik.stanowisko.ToLower() != "organizator" && (user as Pracownik).stanowisko.ToLower() == "organizator")
                    {
                        Wiadomosc.Send("ZMIANA STATUSU ZATRUDNIENIA",
                            "Nadawca wiadomości nie jest już przez ciebie zatrudniony." +
                            "\nWiadomość wygenerowana automatycznie.",
                            logowanie, user.logowanie);
                        logowanie.pracownik.wynagrodzenie = 0;
                        DataAccess.Update(logowanie.pracownik, logowanie.pracownik);
                        var db = logowanie.pracownik.wydarzenia;
                        foreach (Wydarzenie wydarzenie in db)
                        {
                            DataAccess.Delete(wydarzenie, logowanie.pracownik);
                        }
                        DataAccess.Delete(logowanie.pracownik, user);
                    }
                }
            }
            else if (line.Index == Contents.Count - 2)
            {
                DisplayAdapter.Display(new PanelSkrzynkaKontakty(logowanie));
            }
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

        private int Expand<T>(List<T> source, char check = '2', bool display = true)
        {

            int cnt = 0;
            if (check == '1' || check == '2')
            {
                if (source is null || source.Count == 0)
                {
                    if (check == '1' && display)
                        Contents.Add(new StaticLine(" Brak danych do wyświetlenia"));
                    cnt++;
                }
                else
                {
                    foreach (var obj in source)
                    {
                        if (obj is string)
                        {
                            if (check == '1' && display)
                                Contents.Add(new StaticLine(" " + obj as string));
                            cnt++;
                        }
                        else if (obj is Wydarzenie)
                        {
                            if (check == '1' && display)
                                Contents.Add(new ActiveLine($" {(obj as Wydarzenie).nazwa}"));
                            cnt++;
                        }
                    }
                }
            }
            return cnt;
        }
    }
}