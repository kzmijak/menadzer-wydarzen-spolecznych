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
        public Login(StaticLine note = null): base()
        {
            this.Contents.Add(new StaticLine("MENDAŻER WYDARZEŃ SPOŁECZNYCH"));
            this.Contents.Add(new StaticLine("Logowanie użytkownika"));
            this.Contents.Add(new ActiveLine("Zaloguj lub zarejestruj jako pracownik"));
            this.Contents.Add(new ActiveLine("Zaloguj lub zarejestruj jako sponsor"));
            this.Contents.Add(new ActiveLine("Zaloguj lub zarejestruj jako uczestnik"));
            
            if (!(note is null))
                this.Contents.Add(note);
        }

        
        public override void React(_Line line)
        {
            switch(line.Index)
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
            }
        }
    }
}
