using System;
using System.Collections.Generic;
using System.Text;
using MWS.dbo;
using MWS.Lines;

namespace MWS.Pages
{
    class LoginPracownik : Page
    {
        public ActiveLine login { get; set; }
        public ActiveLine password { get; set; }

        public LoginPracownik(StaticLine note = null) : base(note)
        {
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

        public override void React(Line line)
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
                    if (log is null)
                        DisplayAdapter.Display(new LoginPracownik(new StaticLine("Niepoprawne dane logowania. Spróbuj ponownie", ConsoleColor.Red)));
                    else
                    {
                        if (log.pracownik.stanowisko.ToLower() == "Organizator".ToLower())
                            DisplayAdapter.Display(new PanelOrganizatora(log));

                    }
                    break;
                case 4:
                    DisplayAdapter.Display(new LoginRegister(new LoginPracownik()));
                    break;
                case 5:
                    DisplayAdapter.Display(new Login());
                    break;
            }
        }
    }
}
