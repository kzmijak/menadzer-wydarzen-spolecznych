using System;
using System.Collections.Generic;
using System.Text;
using MWS.dbo;
using MWS.Lines;

namespace MWS.Pages
{
    class PanelWydarzenieInterakcjaDotacja : _Panel
    {
        private Wydarzenie wydarzenie;
        private Dotacja dotacja;
        public PanelWydarzenieInterakcjaDotacja(Logowanie logowanie, Wydarzenie wydarzenie, Dotacja dotacja = null, StaticLine note = null) : base(logowanie)
        {
            this.wydarzenie = wydarzenie;

            if(dotacja is null)
            {
                this.dotacja = new Dotacja
                {
                    kwota = 0,
                    oczekiwania = ""
                };
            }
            else
                this.dotacja = dotacja;

            Contents.Add(new StaticLine("DOTACJA"));
            Contents.Add(new ActiveLine("Kwota dotacja:\t" + this.dotacja.kwota.ToString() + "PLN"));
            Contents.Add(new ActiveLine("Oczekiwania:\t" + this.dotacja.oczekiwania));
            Contents.Add(new StaticLine(""));
            Contents.Add(new ActiveLine("Wyślij"));
            Contents.Add(new ActiveLine("Powrót"));
        }

        public override void React(_Line line)
        {
            switch(line.Index)
            {
                case 1:
                    decimal tmp = 0;
                    if(!decimal.TryParse(Console.ReadLine(), out tmp))
                    {
                        DisplayAdapter.Display(new PanelWydarzenieInterakcjaDotacja(logowanie, wydarzenie, dotacja, new StaticLine("Niepoprawny format danych wejściowcyh.", ConsoleColor.Red)));
                    }
                    else
                    {
                        dotacja.kwota = tmp;
                        DisplayAdapter.Display(new PanelWydarzenieInterakcjaDotacja(logowanie, wydarzenie, dotacja));
                    }
                    break;
                case 2:
                    dotacja.oczekiwania = Console.ReadLine();
                    DisplayAdapter.Display(new PanelWydarzenieInterakcjaDotacja(logowanie, wydarzenie, dotacja));
                    break;
                case 4:
                    Dotacja dbo = new Dotacja
                    {
                        idwydarzenia = wydarzenie.id,
                        idsponsora = logowanie.sponsor.id,
                        oczekiwania = dotacja.oczekiwania,
                        kwota = dotacja.kwota,
                        zatwierdzone = false
                    };

                    if (!DbHelper.IsAnyNullOrEmpty(dbo))
                    {
                        //dbo = DataAccess.Dotacja.Insert(dbo) as Dotacja;
                        Wydarzenie_Sponsor.Add(wydarzenie, logowanie.sponsor);
                        Wniosek wniosek = new Wniosek
                        {
                            kwota = dotacja.kwota,
                            akcja = "DecideDotacja"
                        };
                        DataAccess.Wiadomosc.Send(
                            "NOWA DOTACJA OD SPONSORA", 
                            $"Wydarzenie:  {wydarzenie.nazwa}"
                          + $"\nSponsor:     {logowanie.sponsor.nazwa}"
                          + $"\nKwota:       {dotacja.kwota}"
                          + $"\nOczekiwania: {dotacja.oczekiwania},", 
                            logowanie, wydarzenie.organizatorzy[0].logowanie, wniosek);

                        DisplayAdapter.Display(new PanelWydarzenieInterakcja(logowanie, wydarzenie, new StaticLine("Dotacja została wysłana pomyślnie i oczekuje na potwierdzenie.", ConsoleColor.Green)));
                    }
                    else
                    {
                        DisplayAdapter.Display(new PanelWydarzenieInterakcjaDotacja(logowanie, wydarzenie, dotacja, new StaticLine("Nie wszystkie pola zostały wypełnione.", ConsoleColor.Red)));
                    }
                    break;
                case 5:
                    DisplayAdapter.Display(new PanelWydarzenieInterakcja(logowanie, wydarzenie));
                    break;
            }
        }
    }
}
