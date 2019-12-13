﻿using MWS.dbo;
using MWS.Lines;
using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.Pages
{
    class PanelSponsora: Panel
    {
        public PanelSponsora(Logowanie logowanie, StaticLine note = null): base(logowanie)
        {
            Contents.Add(new StaticLine("PANEL SPONSORA"));
            Contents.Add(new ActiveLine("Moje wydarzenia"));
            Contents.Add(new ActiveLine("Skrzynka odbiorcza"));
            Contents.Add(new ActiveLine("Ustawienia konta"));
            Contents.Add(new ActiveLine("Wyloguj"));
        }

        public override void React(Line line)
        {
            switch (line.Index)
            {
                case 1:
                    DisplayAdapter.Display(new PanelWydarzenia(logowanie));
                    break;
                case 2:
                    DisplayAdapter.Display(new SkrzynkaOdbiorcza(logowanie));
                    break;
                case 3:
                    DisplayAdapter.Display(new PanelUstawienia(logowanie));
                    break;
                case 4:
                    DisplayAdapter.Display(new Login(new StaticLine("Użytkownik został wylogowany", ConsoleColor.Green)));
                    break;
            }
        }
    }
}
