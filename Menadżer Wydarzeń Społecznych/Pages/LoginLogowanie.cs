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

        public LoginLogowanie(_CoreObject caller, StaticLine note = null) : base(note)
        {
            this.caller = caller;

            Contents.Add(new StaticLine("LOGOWANIE UŻYTKOWNIKA"));
            login = new ActiveLine("Login: ", "Unikatowa nazwa użytkownika podawana podczas rejestracji");
            password = new ActiveLine("Haslo: ", "Osobiste hasło użytkownika podawane podczas rejestracji");
            Contents.Add(login);
            Contents.Add(password);
            Contents.Add(new ActiveLine("Zaloguj", "Wypełnij wszystkie pola informacjami podanymi podczas rejestracji"));
            Contents.Add(new ActiveLine("Zarejestruj", "Dokonaj rejestracji aby uzyskać pełny dostęp do aplikacji"));
            Contents.Add(new ActiveLine("Powrot", "Powrót do panelu wyboru profilu"));
            this.Contents.Add(Note);
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
                    log = Logowanie.CheckCredentials(log) as Logowanie;
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
