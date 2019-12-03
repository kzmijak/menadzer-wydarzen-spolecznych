using System;
using System.Collections.Generic;
using System.Text;
using MWS.Lines;

namespace MWS.Pages
{
    class Login_Empty : Page
    {
        public List<Line> Contents { get; set; }
        public ILogin caller { get; set; }

        public Login_Empty(ILogin caller)
        {
            Contents.Add(new StaticLine("User not found. Create new? "));
            Contents.Add(new ActiveLine("Yes"));
            Contents.Add(new ActiveLine("No"));
        }

        public void React(Line line)
        {
            switch(line.Index)
            {
                case 1:
                    DisplayAdapter.Display(caller);
                    break;
                case 2:
                    DisplayAdapter.Display(new Login());
                    break;
            }
        }
    }
}
