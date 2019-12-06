using System;
using System.Collections.Generic;
using System.Text;
using MWS.dbo;
using MWS.Lines;
using MWS.Pages;

namespace MWS.Pages
{
    class Login : Page
    {
        public List<Line> Contents { get; set; }
        
        public Login(StaticLine note = null)
        {
            this.Contents = new List<Line>(10);
            Line.LastIndex = 0;

            this.Contents.Add(new StaticLine("MENDAŻER WYDARZEŃ SPOŁECZNYCH"));
            this.Contents.Add(new StaticLine("Logowanie użytkownika"));
            this.Contents.Add(new ActiveLine("Zaloguj lub zarejestruj jako pracownik"));
            this.Contents.Add(new ActiveLine("Zaloguj lub zarejestruj jako sponsor"));
            this.Contents.Add(new ActiveLine("Zaloguj lub zarejestruj jako uczestnik"));
            
            if (!(note is null))
                this.Contents.Add(note);
        }

        
        public void React(Line line)
        {
            switch(line.Index)
            {
                case 2:
                    DisplayAdapter.Display(new LoginPracownik());
                    break;
                case 3:
                    DisplayAdapter.Display(new LoginSponsor());
                    break;
                case 4:
                    DisplayAdapter.Display(new LoginUczestnik());
                    break;
            }
        }
        
    }
}
