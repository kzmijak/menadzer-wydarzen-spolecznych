using System;
using System.Collections.Generic;
using System.Text;
using MWS.dbo;
using MWS.Lines;

namespace MWS.Pages
{
    class PanelUstawieniaKontakt : _Panel
    {
        _CoreObject Core = null;
        Kontakt Update = new Kontakt();

        public PanelUstawieniaKontakt(Logowanie logowanie, StaticLine note = null, Kontakt update = null, _CoreObject core = null): base(logowanie, note)
        {
            if (update == null)
                Update = logowanie.owner.kontakt;
            else
                Update = update;
            if (core == null)
                Core = logowanie.owner;
            else
                Core = core;

            Contents.Add(new StaticLine("FORMULARZ KONTAKTOWY - EDYCJA"));
            Contents.Add(new StaticLine("Dane kontaktowe"));
            Contents.Add(new ActiveLine("Imię: \t\t" + Update.imie, "Pierwsze imię użytkownika (do 16 znaków)"));            
            Contents.Add(new ActiveLine("Nazwisko: \t" + Update.nazwisko, "Nazwisko użytkownika (do 16 znaków)"));      
            Contents.Add(new ActiveLine("Telefon: \t" + Update.telefon, "Telefon kontaktowy (do 16 znaków)"));        
            Contents.Add(new ActiveLine("Email: \t\t" + Update.email, "Adres poczty e-mail (do 32 znaków)"));              
            Contents.Add(new ActiveLine("Miejscowość: \t" + Update.miejscowosc, "Aktualna miejscowość zameldowania użytkownika (do 16 znaków)"));    
            Contents.Add(new ActiveLine("Numer domu: \t" + Update.nrdomu, "Aktualny numer domu użytkownika (do 8 znaków)"));          
            Contents.Add(new ActiveLine("Miasto: \t" + Update.miasto, "Miasto w którym urzęduje poczta (do 32 znaków)"));              
            Contents.Add(new ActiveLine("Kod pocztowy: \t" + Update.poczta, "Kod pocztowy użytkownika w formacie XX-YYY (do 6 znaków)"));              
            Contents.Add(new ActiveLine("Ulica: \t\t" + Update.ulica, "Aktualna ulica zameldowania użytkownika (do 16 znaków)"));
            if(logowanie.owner is Pracownik)
            {
                Contents.Add(new ActiveLine("Stanowisko: \t" + (Core as Pracownik).stanowisko, "Stanowisko, na którym użytkownik chce podjąć pracę.\n\"Organizator\" odblokowuje zawartość menadżerską."));
            }
            else if (logowanie.owner is Uczestnik)
            {
                Contents.Add(new ActiveLine("Karta płatnicza", "Dodaj lub zmień kartę płatniczą (niezbędne by zakupić bilet na wydarzenie)"));
            }
            Contents.Add(new StaticLine(""));
            Contents.Add(new ActiveLine("Resetuj", "Cofnij zmiany do stanu poprzednio zaakceptowanego"));
            Contents.Add(new ActiveLine("Zapisz", "Zapisz zmiany do bazy danych"));
            Contents.Add(new ActiveLine("Powrót", "Powróć do panelu ustawień"));
            
            Contents.Add(Note);
        }

        public override void React(_Line line)
        {
            switch(line.Index)
            {
                case 2:
                    Update.imie = Console.ReadLine();
                    DisplayAdapter.Display(new PanelUstawieniaKontakt(logowanie, null, Update, Core));
                    break;
                case 3:
                    Update.nazwisko = Console.ReadLine();
                    DisplayAdapter.Display(new PanelUstawieniaKontakt(logowanie, null, Update, Core));
                    break;
                case 4:
                    Update.telefon = Console.ReadLine();
                    DisplayAdapter.Display(new PanelUstawieniaKontakt(logowanie, null, Update, Core));
                    break;
                case 5:
                    Update.email = Console.ReadLine();
                    DisplayAdapter.Display(new PanelUstawieniaKontakt(logowanie, null, Update, Core));
                    break;
                case 6:
                    Update.miejscowosc = Console.ReadLine();
                    DisplayAdapter.Display(new PanelUstawieniaKontakt(logowanie, null, Update, Core));
                    break;
                case 7:
                    Update.nrdomu = Console.ReadLine();
                    DisplayAdapter.Display(new PanelUstawieniaKontakt(logowanie, null, Update, Core));
                    break;
                case 8:
                    Update.miasto = Console.ReadLine();
                    DisplayAdapter.Display(new PanelUstawieniaKontakt(logowanie, null, Update, Core));
                    break;
                case 9:
                    Update.poczta = Console.ReadLine();
                    DisplayAdapter.Display(new PanelUstawieniaKontakt(logowanie, null, Update, Core));
                    break;
                case 10:
                    Update.ulica = Console.ReadLine();
                    DisplayAdapter.Display(new PanelUstawieniaKontakt(logowanie, null, Update, Core));
                    break;
                case 11:
                    if(logowanie.owner is Pracownik)
                    {
                        Core = new Pracownik
                        {
                            stanowisko = Console.ReadLine()
                        };
                    }
                    if (logowanie.owner is Sponsor)
                    {
                        Core = new Sponsor
                        {
                            nazwa = Console.ReadLine()
                        };
                    }
                    if(logowanie.owner is Uczestnik)
                    {
                        DisplayAdapter.Display(new PanelUstawieniaKontaktKarta(logowanie, logowanie.owner.kontakt.kartaPlatnicza));
                    }
                    DisplayAdapter.Display(new PanelUstawieniaKontakt(logowanie, null, Update, Core));
                    break;
                case 13:
                    DisplayAdapter.Display(new PanelUstawieniaKontakt(logowanie, null, null, null));
                    break;
                case 14:
                    bool allOk = false;
                    if(logowanie.owner is Pracownik)
                    {
                        if (DbHelper.IsAnyNullOrEmpty(Core as Pracownik) == false)
                        {
                            logowanie.owner = Core;                         
                            allOk = true;
                        }
                    }
                    else if(logowanie.owner is Uczestnik)
                    {
                        allOk = true;
                    }
                    else if(allOk is false )
                        DisplayAdapter.Display(new PanelUstawieniaKontakt(logowanie, new StaticLine("Żadne pola nie mogą być puste.", ConsoleColor.Red), null, null));

                    if (DbHelper.IsAnyNullOrEmpty(Update) == false && allOk)
                    {
                        logowanie.owner.kontakt = Update;
                        DisplayAdapter.Display(new PanelUstawieniaKontakt(logowanie, new StaticLine("Zmiany zostały pomyślnie zapisane.", ConsoleColor.Green), Update, Core));
                    }
                    else
                        DisplayAdapter.Display(new PanelUstawieniaKontakt(logowanie, new StaticLine("Żadne pola nie mogą być puste.", ConsoleColor.Red), null, Core));
                    break;
                case 15:
                    DisplayAdapter.Display(new PanelUstawienia(logowanie));
                    break;
            }
        }
    }
}
