using MWS.dbo;
using MWS.Lines;
using MWS.Pages;
using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.Pages
{
    class PanelUstawieniaLogowanie : Panel
    {
        private Logowanie Update;
        private CoreObject Core;
        private string Haslo1;
        private string Haslo2;
        private string Haslo3;


        public PanelUstawieniaLogowanie(Logowanie logowanie, StaticLine note=null, Logowanie update=null, CoreObject core=null, string haslo1 = "", string haslo2 = "", string haslo3 = ""): base(logowanie)
        {
            if (update != null)
                Update = update;
            else
                Update = logowanie.owner.logowanie;
            if (core != null)
                Core = core;
            else
                Core = logowanie.owner;
            
            Haslo1 = haslo1;
            Haslo2 = haslo2;
            Haslo3 = haslo3;

            Contents.Add(new StaticLine("DANE LOGOWANIA - EDYCJA"));
            Contents.Add(new ActiveLine("Login: \t\t\t" + Update.login));
            if (logowanie.owner is Sponsor)
                Contents.Add(new ActiveLine("Nazwa organizacji: \t" + (Core as Sponsor).nazwa));
            else
                Contents.Add(new StaticLine(""));
            Contents.Add(new StaticLine("Zmiana hasła:"));
            Contents.Add(new ActiveLine("Stare hasło (wymagane): " + encode(Haslo1)));
            Contents.Add(new ActiveLine("Nowe hasło: \t\t" + encode(Haslo2)));
            Contents.Add(new ActiveLine("Potwierdź hasło: \t" + encode(Haslo3)));
            Contents.Add(new StaticLine(""));
            Contents.Add(new ActiveLine("Resetuj"));
            Contents.Add(new ActiveLine("Zapisz"));
            Contents.Add(new ActiveLine("Powrót"));
            
            if (note != null)
            {
                Contents.Add(note);
            }
        }

        public override void React(Line line)
        {
            switch(line.Index)
            {
                case 1:
                    Update.login = Console.ReadLine();
                    DisplayAdapter.Display(new PanelUstawieniaLogowanie(logowanie, null, Update, Core, Haslo1, Haslo2, Haslo3));
                    break;
                case 2:
                    (Core as Sponsor).nazwa = Console.ReadLine();
                    DisplayAdapter.Display(new PanelUstawieniaLogowanie(logowanie, null, Update, Core, Haslo1, Haslo2, Haslo3));
                    break;
                case 4:
                    Haslo1 = Console.ReadLine();
                    DisplayAdapter.Display(new PanelUstawieniaLogowanie(logowanie, null, Update, Core, Haslo1, Haslo2, Haslo3));
                    break;
                case 5:
                    Haslo2 = Console.ReadLine();
                    DisplayAdapter.Display(new PanelUstawieniaLogowanie(logowanie, null, Update, Core, Haslo1, Haslo2, Haslo3));
                    break;
                case 6:
                    Haslo3 = Console.ReadLine();
                    DisplayAdapter.Display(new PanelUstawieniaLogowanie(logowanie, null, Update, Core, Haslo1, Haslo2, Haslo3));
                    break;
                case 8:
                    DisplayAdapter.Display(new PanelUstawieniaLogowanie(logowanie));
                    break;
                case 9:
                    bool execsponsor = false;
                    if(Haslo2 != "" && Haslo3 != "")
                    {
                        if(Haslo1 == logowanie.owner.logowanie.haslo && Haslo2 == Haslo3 && Update.login != "")
                        {
                            if(Core is Sponsor)
                            {
                                if ((Core as Sponsor).nazwa != "")
                                    execsponsor = true;
                                else break;
                            }
                            Update.haslo = Haslo3;
                            if (DataAccess.Logowanie.CheckCredentials(Update) == null)
                            {
                                if(execsponsor)
                                    DataAccess.Sponsor.Update(logowanie.sponsor, Core);
                                DataAccess.Logowanie.Update(logowanie.owner.logowanie, Update);
                                DisplayAdapter.Display(new PanelUstawieniaLogowanie(logowanie, new StaticLine("Zmiany zostały zapisane. Hasło zostało zmienione.", ConsoleColor.Green)));
                            }
                        }
                    }
                    else if(Haslo2 == "" && Haslo3 == "" && Haslo1 == logowanie.owner.logowanie.haslo && Update.login != "")
                    {
                        if (Core is Sponsor)
                        {
                            if ((Core as Sponsor).nazwa != "")
                                execsponsor = true;
                            else break;
                        }
                        Update.haslo = Haslo1;
                        if(DataAccess.Logowanie.CheckCredentials(Update)==null)
                        {
                            if(execsponsor)
                                DataAccess.Sponsor.Update(logowanie.sponsor, Core);
                            DataAccess.Logowanie.Update(logowanie.owner.logowanie, Update);
                            DisplayAdapter.Display(new PanelUstawieniaLogowanie(logowanie.owner.logowanie, new StaticLine("Zmiany zostały zapisane.", ConsoleColor.Green)));
                        }

                    }
                    DisplayAdapter.Display(new PanelUstawieniaLogowanie(logowanie.owner.logowanie, new StaticLine("Zmiany zostały odrzucone. Upewnij się, że wpisałeś poprawne hasła.", ConsoleColor.Red)));
                    break;
                case 10:
                    DisplayAdapter.Display(new PanelUstawienia(logowanie));
                    break;
            }
        }

        private static string encode(string s)
        {
            string coded = "";
            if (s != null)
                foreach (char c in s)
                    coded += "*";
            return coded;
        }

    }
}
