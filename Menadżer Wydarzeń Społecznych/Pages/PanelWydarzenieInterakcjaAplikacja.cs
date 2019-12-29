using System;
using System.Collections.Generic;
using System.Text;
using MWS.dbo;
using MWS.Lines;

namespace MWS.Pages
{
    class PanelWydarzenieInterakcjaAplikacja : _Panel
    {
        private string message;
        private decimal donation;
        private Wydarzenie wydarzenie;
        public PanelWydarzenieInterakcjaAplikacja(Logowanie logowanie, Wydarzenie wydarzenie, StaticLine note = null, string message = null, decimal donation = 0) : base(logowanie)
        {
            if (message is null)
                this.message = $"(Wiadomość do głównego organizatora - {wydarzenie.organizatorzy[0].kontakt.imie} {wydarzenie.organizatorzy[0].kontakt.imie})";
            else
                this.message = message;

            this.donation = donation;
            
            this.wydarzenie = wydarzenie;

            Contents.Add(new StaticLine("APLIKACJA DO WYDARZENIA"));
            Contents.Add(new StaticLine($"Wydarzenie: \"{wydarzenie.nazwa}\""));
            Contents.Add(new StaticLine($"Kandydat: {logowanie.pracownik.kontakt.imie} {logowanie.pracownik.kontakt.nazwisko}"));
            Contents.Add(new ActiveLine(this.message));
            Contents.Add(new ActiveLine("Dotacja: " + donation.ToString() + "PLN"));
            Contents.Add(new StaticLine($"{wydarzenie.miejsce}, dnia {DateTime.Now}"));
            Contents.Add(new StaticLine(""));
            Contents.Add(new ActiveLine("Wyślij"));
            Contents.Add(new ActiveLine("Powrót"));
            Contents.Add(note);
        }

        public override void React(_Line line)
        {
            switch(line.Index)
            {
                case 3:
                    string msg = Console.ReadLine();
                    DisplayAdapter.Display(new PanelWydarzenieInterakcjaAplikacja(logowanie, wydarzenie, null, msg, donation));
                    break;
                case 4:
                    decimal dc;
                    if(!decimal.TryParse(Console.ReadLine(), out dc))
                    {
                        DisplayAdapter.Display(new PanelWydarzenieInterakcjaAplikacja(logowanie, wydarzenie, new StaticLine("Niepoprawny format liczbowy.", ConsoleColor.Red), message, donation));
                    }
                    DisplayAdapter.Display(new PanelWydarzenieInterakcjaAplikacja(logowanie, wydarzenie, null, message, dc));
                    break;
                case 7:

                    string str = "";
                    for(int i = 0; i < 6; i++)
                    {
                        str += $"{Contents[i].Content}\n";
                    }
                    
                    var sender = logowanie.pracownik.logowanie;
                    var receiver = wydarzenie.organizatorzy[0].logowanie;
                    
                    Wniosek wniosek = new Wniosek
                    {
                        kwota = donation,
                        akcja = $"orgApp:{wydarzenie.id}"
                    };
                    Wiadomosc.Send($"APLIKACJA DO WYDARZENIA \"{wydarzenie.nazwa.ToUpper()}\"", str, sender, receiver, wniosek);
                    
                    DisplayAdapter.Display(new PanelWydarzenieInterakcja(logowanie, wydarzenie, new StaticLine("Aplikacja na zgłoszenie została wysłana.", ConsoleColor.Green)));
                    break;
                case 8:
                    DisplayAdapter.Display(new PanelWydarzenieInterakcja(logowanie, wydarzenie));
                    break;
            }
        }
    }
}
