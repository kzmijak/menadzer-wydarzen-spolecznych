using System;
using System.Collections.Generic;
using System.Text;
using MWS.dbo;
using MWS.Lines;

namespace MWS.Pages
{
    class LoginLogowanie : _Page
    {
        public _CoreObject caller;
        public ActiveLine login { get; set; }
        public ActiveLine password { get; set; }

        public LoginLogowanie(_CoreObject caller, StaticLine note = null) : base()
        {
            this.caller = caller;

            Contents.Add(new StaticLine("LOGOWANIE PRACOWNIKA"));
            login = new ActiveLine("Login: ");
            password = new ActiveLine("Haslo: ");
            Contents.Add(login);
            Contents.Add(password);
            Contents.Add(new ActiveLine("Zaloguj"));
            Contents.Add(new ActiveLine("Zarejestruj"));
            Contents.Add(new ActiveLine("Powrot"));

            if (!(note is null))
                this.Contents.Add(note);
        }

        public override void React(_Line line)
        {
            switch (line.Index)
            {
                case 1:
                    if (login.Content != "Login: ")
                    {
                        login.Content = "Login: ";
                        DisplayAdapter.Refresh(this);
                    }
                    login.Content += Console.ReadLine();
                    DisplayAdapter.Refresh(this);
                    break;
                case 2:
                    if (password.Content != "Haslo: ")
                    {
                        password.Content = "Haslo: ";
                        DisplayAdapter.Refresh(this);
                    }
                    password.Content += Console.ReadLine();
                    DisplayAdapter.Refresh(this);
                    break;
                case 3:
                    Logowanie log = new Logowanie
                    {
                        login = login.Content.Substring(7),
                        haslo = password.Content.Substring(7)
                    };
                    log = DataAccess.Logowanie.CheckCredentials(log) as Logowanie;
                    if(caller is Pracownik)
                    {
                        if (log is null || !(log.owner is Pracownik))
                            DisplayAdapter.Display(new LoginLogowanie(caller, new StaticLine("Niepoprawne dane logowania. Spróbuj ponownie", ConsoleColor.Red)));
                        else
                        {
                            if (log.pracownik.stanowisko.ToLower() == "Organizator".ToLower())
                                DisplayAdapter.Display(new Panel(log));
                            else
                                DisplayAdapter.Display(new Panel(log));
                        }
                    }
                    if(caller is Sponsor)
                    {
                        if (log is null || !(log.owner is Sponsor))
                            DisplayAdapter.Display(new LoginLogowanie(new Sponsor(), new StaticLine("Niepoprawne dane logowania. Spróbuj ponownie", ConsoleColor.Red)));
                        else
                            DisplayAdapter.Display(new Panel(log));
                    }
                    if(caller is Uczestnik)
                    {
                        if (log is null || !(log.owner is Uczestnik))
                            DisplayAdapter.Display(new LoginLogowanie(new Uczestnik(), new StaticLine("Niepoprawne dane logowania. Spróbuj ponownie", ConsoleColor.Red)));
                        else
                            DisplayAdapter.Display(new Panel(log));
                    }
                    break;
                case 4:
                    DisplayAdapter.Display(new LoginRejestracja(caller));
                    break;
                case 5:
                    DisplayAdapter.Display(new Login());
                    break;
            }
        }
    }
}
