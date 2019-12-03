using System;
using System.Collections.Generic;
using System.Text;
using MWS.Lines;

namespace MWS.Pages
{
    class LoginError : Page
    {
        public List<Line> Contents { get; set; }
        public Page caller { get; set; }

        public LoginError(Page caller)
        {
            this.caller = caller;
            Contents = new List<Line>(4);
            Line.LastIndex = 0;
            Contents.Add(new StaticLine("Błąd logowania. Sprawdź login i hasło."));
            Contents.Add(new ActiveLine("Ponów"));
            Contents.Add(new ActiveLine("Rejestracja"));
            Contents.Add(new ActiveLine("Powrót"));
        }

        public void React(Line line)
        {
            switch(line.Index)
            {
                case 1:
                    DisplayAdapter.Display(caller);
                    break;
                case 2:
                    throw new NotImplementedException();
                case 3:
                    DisplayAdapter.Display(new Login());
                    break;
            }
        }
    }
}
