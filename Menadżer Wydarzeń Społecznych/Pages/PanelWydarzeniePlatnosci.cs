using System;
using System.Collections.Generic;
using System.Text;
using MWS.dbo;
using MWS.Lines;

namespace MWS.Pages
{
    class PanelWydarzeniePlatnosci : _Panel
    {
        Wydarzenie wydarzenie;
        public PanelWydarzeniePlatnosci(Logowanie logowanie, Wydarzenie wydarzenie, StaticLine note = null) : base(logowanie, note)
        {
            this.wydarzenie = wydarzenie;
            Contents.Add(new StaticLine("HISTORIA PŁATNOŚCI"));
            Contents.Add(new StaticLine("*"));
            foreach(Platnosc platnosc in wydarzenie.platnosci)
            {
                ConsoleColor clr = new ConsoleColor();
                if(platnosc.kwota > 0)
                {
                    clr = ConsoleColor.Green;
                }
                else
                {
                    clr = ConsoleColor.Red;
                }
                Contents.Add(new StaticLine(
                    $"nadawca: {platnosc.kartaPlatnicza.owner.imie} {platnosc.kartaPlatnicza.owner.nazwisko}" +
                    $"\nWydarzenie: {wydarzenie.nazwa}" +
                    $"\nData: {platnosc.dzien}" +
                    $"\nSaldo:", ConsoleColor.White));
                Contents.Add(new StaticLine($"{platnosc.kwota}", clr));
                Contents.Add(new StaticLine($"*", ConsoleColor.White));
            }
            Contents.Add(new StaticLine(""));
            Contents.Add(new ActiveLine("Powrót", "Powrót do poprzedniego panelu"));
            Contents.Add(Note);
        }

        public override void React(_Line line)
        {
            if (line.Index == Contents.Count - 2)
            {
                DisplayAdapter.Display(new PanelWydarzenieInterakcjaCzlonek(logowanie, wydarzenie));
            }
        }
    }
}
