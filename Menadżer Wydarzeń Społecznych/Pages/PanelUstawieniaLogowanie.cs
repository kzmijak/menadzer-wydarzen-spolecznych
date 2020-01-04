using MWS.dbo;
using MWS.Lines;
using MWS.Pages;
using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.Pages
{
    class PanelUstawieniaLogowanie : _Panel
    {
        private Logowanie Update;
        private _CoreObject Core;
        private string Haslo1;
        private string Haslo2;
        private string Haslo3;


        public PanelUstawieniaLogowanie(Logowanie logowanie, StaticLine note=null, Logowanie update=null, _CoreObject core=null, string haslo1 = "", string haslo2 = "", string haslo3 = ""): base(logowanie, note)
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
            Contents.Add(new ActiveLine("Login: \t\t\t" + Update.login, "Zmień login wykorzystywany do logowania"));
            if (logowanie.owner is Sponsor)
                Contents.Add(new ActiveLine("Nazwa organizacji: \t" + (Core as Sponsor).nazwa, "Nazwa organizacji reprezentowanej przez sponsora"));
            else
                Contents.Add(new StaticLine(""));
            Contents.Add(new StaticLine("Zmiana hasła:"));
            Contents.Add(new ActiveLine("Obecne hasło (wymagane): " + encode(Haslo1), "Hasło obecnie wykorzystywane do logowania"));
            Contents.Add(new ActiveLine("Nowe hasło: \t\t" + encode(Haslo2), "Nowe hasło, które zastąpi obecne hasło do logowania"));
            Contents.Add(new ActiveLine("Potwierdź hasło: \t" + encode(Haslo3), "Potwierdź nowe hasło"));
            Contents.Add(new StaticLine(""));
            Contents.Add(new ActiveLine("Resetuj", "Cofnij zmiany do stanu poprzednio zatwierdzonego"));
            Contents.Add(new ActiveLine("Zapisz", "Wypełnij wszystkie pola by zapisać zmiany"));
            Contents.Add(new ActiveLine("Powrót", "Powróć do panelu ustawień"));
            
            Contents.Add(Note);
        }

        public override void React(_Line line)
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
                            if (Logowanie.CheckCredentials(Update) == null)
                            {
                                if(execsponsor)
                                    logowanie.sponsor = Core as Sponsor;
                                logowanie.owner.logowanie = Update;
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
                        if(Logowanie.CheckCredentials(Update)==null)
                        {
                            if (execsponsor)
                                logowanie.sponsor = Core as Sponsor;
                            logowanie.owner.logowanie = Update;
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
