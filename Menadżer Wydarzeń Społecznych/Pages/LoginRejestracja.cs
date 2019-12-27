using System;
using System.Collections.Generic;
using System.Text;
using MWS.dbo;
using MWS.Lines;

namespace MWS.Pages
{
    class LoginRejestracja : _Page
    {
        public int id { get; set; }
        public _CoreObject caller { get; set; }
        
        public string login { get; set; }
        public string haslo { get; set; }
        public string haslo2 { get; set; }
        public int idpracownika { get; set; }
        public int idsponsora { get; set; }
        public int iduczestnika { get; set; }
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public string telefon { get; set; }
        public string email { get; set; }
        public string miejscowosc { get; set; }
        public string nrdomu { get; set; }
        public string miasto { get; set; }
        public string poczta { get; set; }
        public string ulica { get; set; }
        public string stanowisko { get; set; }
        public string nazwa { get; set; }
        public int fid { get; set; } 

        LoginRejestracja() { }

        public LoginRejestracja(_CoreObject caller, StaticLine note = null, LoginRejestracja update = null): base()
        {
            if(update is null)
                update = new LoginRejestracja();
            
            this.caller = caller;

            login = update.login;
            haslo = update.haslo;
            haslo2 = update.haslo2;
            idpracownika = update.idpracownika;
            idsponsora = update.idsponsora;
            iduczestnika = update.iduczestnika;
            imie = update.imie;
            nazwisko = update.nazwisko;
            telefon = update.telefon;
            email = update.email;
            miejscowosc = update.miejscowosc;
            nrdomu = update.nrdomu;
            miasto = update.miasto;
            poczta = update.poczta;
            ulica = update.ulica;
            stanowisko = update.stanowisko;
            nazwa = update.nazwa;
            fid = update.fid;

            Contents.Add(new StaticLine("REJESTRACJA UŻYTKOWNIKA"));                   //0
            Contents.Add(new StaticLine("Dane logowania"));                            //1
            Contents.Add(new ActiveLine("Login: \t\t" + login));                  //2
            Contents.Add(new ActiveLine("Hasło: \t\t" + encode(haslo)));          //3
            Contents.Add(new ActiveLine("Powtórz hasło: \t" + encode(haslo2))); //4

            if(!(caller is Sponsor))
            {
                Contents.Add(new StaticLine("Dane kontaktowe"));                //5
                Contents.Add(new ActiveLine("Imię: \t\t" + imie));                //6
                Contents.Add(new ActiveLine("Nazwisko: \t" + nazwisko));        //7
                Contents.Add(new ActiveLine("Telefon: \t" + telefon));          //8
                Contents.Add(new ActiveLine("Email: \t\t" + email));              //9
                Contents.Add(new ActiveLine("Miejscowość: \t" + miejscowosc));  //10
                Contents.Add(new ActiveLine("Numer domu: \t" + nrdomu));        //11
                Contents.Add(new ActiveLine("Miasto: \t" + miasto));            //12
                Contents.Add(new ActiveLine("Kod pocztowy: \t" + poczta));            //13
                Contents.Add(new ActiveLine("Ulica: \t\t" + ulica));              //14
            }
            if(caller is Pracownik)
            {
                Contents.Add(new ActiveLine("Stanowisko: \t" + update.stanowisko));    //P:15
            }
            if(caller is Sponsor)
            {
                Contents.Add(new ActiveLine("Nazwa organizacji: \t" + update.nazwa));  //S:5
            }
            Contents.Add(new StaticLine(""));                                   //S:P :U
            Contents.Add(new ActiveLine("Zarejestruj"));                        //6:16:15
            Contents.Add(new ActiveLine("Cofnij zmiany"));                      //7:17:16
            Contents.Add(new ActiveLine("Anuluj"));                             //8:18:17
            this.Contents.Add(note);
        }
        
        public override void React(_Line line)
        {
            if (caller is Pracownik)
                pracownik(line.Index);
            if (caller is Sponsor)
                sponsor(line.Index);
            if (caller is Uczestnik)
                uczestnik(line.Index);
        }

        private static string encode(string s)
        {
            string coded = "";
            if(s != null)
                foreach (char c in s)
                    coded += "*";
            return coded;
        }

        private void pracownik(int i)
        {
            nazwa = "placeholder";

            switch(i)
            {
                case 2:
                    login = Console.ReadLine();
                    break;
                case 3:
                    haslo = Console.ReadLine();
                    break;
                case 4:
                    haslo2 = Console.ReadLine();
                    break;
                case 6:
                    imie = Console.ReadLine();
                    break;
                case 7:
                    nazwisko = Console.ReadLine();
                    break;
                case 8:
                    telefon = Console.ReadLine();
                    break;
                case 9:
                    email = Console.ReadLine();
                    break;
                case 10:
                    miejscowosc = Console.ReadLine();
                    break;
                case 11:
                    nrdomu = Console.ReadLine();
                    break;
                case 12:
                    miasto = Console.ReadLine();
                    break;
                case 13:
                    poczta = Console.ReadLine();
                    break;
                case 14:
                    ulica = Console.ReadLine();
                    break;
                case 15:
                    stanowisko = Console.ReadLine();
                    break;
                case 17:
                    if (haslo == haslo2 && DbHelper.IsAnyNullOrEmpty(this) == false)
                    {
                        Pracownik pracownik = new Pracownik
                        {
                            stanowisko = this.stanowisko
                        };

                        pracownik = DataAccess.Insert(pracownik) as Pracownik;
                        Console.WriteLine(pracownik.id);

                        Logowanie login = new Logowanie
                        {
                            login = this.login,
                            haslo = this.haslo,
                            idpracownika = pracownik.id
                        };

                        pracownik.logowanie = login;

                        Kontakt kontakt = new Kontakt
                        {
                            imie = this.imie,
                            nazwisko = this.nazwisko,
                            telefon = this.telefon,
                            email = this.email,
                            miejscowosc = this.miejscowosc,
                            nrdomu = this.nrdomu,
                            miasto = this.miasto,
                            poczta = this.poczta,
                            ulica = this.ulica,
                            idpracownika = pracownik.id
                        };
                        pracownik.kontakt = kontakt;

                        DisplayAdapter.Display(new LoginLogowanie(caller, new StaticLine("Rejestracja przebiegła pomyślnie. Możesz się teraz zalogować.", ConsoleColor.Green)));
                        
                    }
                    else if (DbHelper.IsAnyNullOrEmpty(this) == true)
                    {
                        StaticLine warning = new StaticLine("Uzupełnij brakujące pola tekstowe.", ConsoleColor.Red);
                        DisplayAdapter.Display(new LoginRejestracja(caller, warning, this));
                    }
                    else
                    {
                        StaticLine warning = new StaticLine("Wprowadzone hasła się nie zgadzają. Wpisz je ponownie.", ConsoleColor.Red);
                        haslo = null;
                        haslo2 = null;
                        DisplayAdapter.Display(new LoginRejestracja(caller, warning, this));
                    }
                    break;
                case 18:
                    DisplayAdapter.Display(new LoginRejestracja(caller, null, null));
                    break;
                case 19:
                    DisplayAdapter.Display(new Login());
                    break;
            }
            DisplayAdapter.Display(new LoginRejestracja(caller, null, this), DisplayAdapter.CurrentLine);
        }

        private void uczestnik(int i)
        {
            nazwa = "placeholder";
            stanowisko = "placeholder";

            switch (i)
            {
                case 2:
                    login = Console.ReadLine();
                    break;
                case 3:
                    haslo = Console.ReadLine();
                    break;
                case 4:
                    haslo2 = Console.ReadLine();
                    break;
                case 6:
                    imie = Console.ReadLine();
                    break;
                case 7:
                    nazwisko = Console.ReadLine();
                    break;
                case 8:
                    telefon = Console.ReadLine();
                    break;
                case 9:
                    email = Console.ReadLine();
                    break;
                case 10:
                    miejscowosc = Console.ReadLine();
                    break;
                case 11:
                    nrdomu = Console.ReadLine();
                    break;
                case 12:
                    miasto = Console.ReadLine();
                    break;
                case 13:
                    poczta = Console.ReadLine();
                    break;
                case 14:
                    ulica = Console.ReadLine();
                    break;
                case 16:
                    if (haslo == haslo2 && DbHelper.IsAnyNullOrEmpty(this) == false)
                    {
                        var rnd = new Random();
                        Uczestnik uczestnik = new Uczestnik { fid = rnd.Next(10000,99999) };

                        uczestnik = DataAccess.Insert(uczestnik) as Uczestnik;

                        Logowanie login = new Logowanie
                        {
                            login = this.login,
                            haslo = this.haslo,
                            iduczestnika = uczestnik.id
                        };

                        uczestnik.logowanie = login;

                        Kontakt kontakt = new Kontakt
                        {
                            imie = this.imie,
                            nazwisko = this.nazwisko,
                            telefon = this.telefon,
                            email = this.email,
                            miejscowosc = this.miejscowosc,
                            nrdomu = this.nrdomu,
                            miasto = this.miasto,
                            poczta = this.poczta,
                            ulica = this.ulica,
                            iduczestnika = uczestnik.id
                        };

                        uczestnik.kontakt = kontakt;

                        DisplayAdapter.Display(new LoginLogowanie(new Uczestnik(), new StaticLine("Rejestracja przebiegła pomyślnie. Możesz się teraz zalogować.", ConsoleColor.Green)));
                    }
                    else if (DbHelper.IsAnyNullOrEmpty(this) == true)
                    {
                        StaticLine warning = new StaticLine("Uzupełnij brakujące pola tekstowe.", ConsoleColor.Red);
                        DisplayAdapter.Display(new LoginRejestracja(caller, warning, this));
                    }
                    else
                    {
                        StaticLine warning = new StaticLine("Wprowadzone hasła się nie zgadzają. Wpisz je ponownie.", ConsoleColor.Red);
                        haslo = null;
                        haslo2 = null;
                        DisplayAdapter.Display(new LoginRejestracja(caller, warning, this));
                    }
                    break;
                case 17:
                    DisplayAdapter.Display(new LoginRejestracja(caller, null, null));
                    break;
                case 18:
                    DisplayAdapter.Display(new Login());
                    break;
            }
            DisplayAdapter.Display(new LoginRejestracja(caller, null, this), DisplayAdapter.CurrentLine);
        }

        private void sponsor(int i)
        {
            imie = "placeholder";
            nazwisko = "placeholder";
            telefon = "placeholder";
            email = "placeholder";
            miejscowosc = "placeholder";
            nrdomu = "placeholder";
            miasto = "placeholder";
            poczta = "placeholder";
            ulica = "placeholder";
            stanowisko = "placeholder";

            switch (i)
            {
                case 2:
                    login = Console.ReadLine();
                    break;
                case 3:
                    haslo = Console.ReadLine();
                    break;
                case 4:
                    haslo2 = Console.ReadLine();
                    break;
                case 5:
                    nazwa = Console.ReadLine();
                    break;
                case 7:
                    if (haslo == haslo2 && DbHelper.IsAnyNullOrEmpty(this) == false)
                    {
                        Sponsor sponsor = new Sponsor
                        {
                            nazwa = this.nazwa
                        };

                        sponsor = DataAccess.Insert(sponsor) as Sponsor;

                        Logowanie login = new Logowanie
                        {
                            login = this.login,
                            haslo = this.haslo,
                            idsponsora = sponsor.id
                        };
                        sponsor.logowanie = login;

                        DisplayAdapter.Display(new LoginLogowanie(new Sponsor(), new StaticLine("Rejestracja przebiegła pomyślnie. Możesz się teraz zalogować.", ConsoleColor.Green)));
                    }
                    else if (DbHelper.IsAnyNullOrEmpty(this) == false)
                    {
                        StaticLine warning = new StaticLine("Uzupełnij brakujące pola tekstowe.", ConsoleColor.Red);
                        DisplayAdapter.Display(new LoginRejestracja(caller, warning, this));
                    }
                    else
                    {
                        StaticLine warning = new StaticLine("Wprowadzone hasła się nie zgadzają. Wpisz je ponownie.", ConsoleColor.Red);
                        haslo = null;
                        haslo2 = null;
                        DisplayAdapter.Display(new LoginRejestracja(caller, warning, this));
                    }
                    break;
                case 8:
                    DisplayAdapter.Display(new LoginRejestracja(caller, null, null));
                    break;
                case 9:
                    DisplayAdapter.Display(new Login());
                    break;
            }
            DisplayAdapter.Display(new LoginRejestracja(caller, null, this), DisplayAdapter.CurrentLine);
        }
    }
}
