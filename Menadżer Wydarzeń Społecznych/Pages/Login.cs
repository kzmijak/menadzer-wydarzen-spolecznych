using System;
using System.Collections.Generic;
using System.Text;
using MWS.dbo;
using MWS.Lines;
using MWS.Pages;

namespace MWS.Pages
{
    class Login : _Page
    {
        public Login(StaticLine note = null) : base(note)
        {
            if (!DbHelper.CheckConnection())
            {
                DbHelper.InstallDatabase();
                DbHelper.InstallProcedures();
            }

            this.Contents.Add(new StaticLine("MENDAŻER WYDARZEŃ SPOŁECZNYCH"));
            this.Contents.Add(new StaticLine("Logowanie użytkownika"));
            this.Contents.Add(new ActiveLine("Zaloguj lub zarejestruj jako pracownik", "Logowanie użytkownika o roli pracownika lub organizatora."));
            this.Contents.Add(new ActiveLine("Zaloguj lub zarejestruj jako sponsor", "Logowanie użytkownika o roli sponsora"));
            this.Contents.Add(new ActiveLine("Zaloguj lub zarejestruj jako uczestnik", "Logowanie użytkownika o roli uczestnika"));
            this.Contents.Add(new ActiveLine("Konfiguracja bazy danych", "Przejdź do panelu konfiguracji połączenia z bazą danych"));
            this.Contents.Add(Note);
        }


        public override void React(_Line line)
        {
            switch (line.Index)
            {
                case 2:
                    DisplayAdapter.Display(new LoginLogowanie(new Pracownik()));
                    break;
                case 3:
                    DisplayAdapter.Display(new LoginLogowanie(new Sponsor()));
                    break;
                case 4:
                    DisplayAdapter.Display(new LoginLogowanie(new Uczestnik()));
                    break;
                case 5:
                    DisplayAdapter.Display(new DBConfig());
                    break;
            }
        }
    }
}