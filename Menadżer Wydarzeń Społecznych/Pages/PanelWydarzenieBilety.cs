using System;
using System.Collections.Generic;
using System.Text;
using MWS.dbo;
using MWS.Lines;

namespace MWS.Pages
{
    class PanelWydarzenieBilety : _Panel
    {
        Wydarzenie wydarzenie;

        public PanelWydarzenieBilety(Logowanie logowanie, Wydarzenie wydarzenie, StaticLine note = null) : base(logowanie)
        {

            this.wydarzenie = wydarzenie;
            Contents.Add(new StaticLine("BILETY NA WYDARZENIE"));
            if(wydarzenie.bilety.Count == 0)
            {
                Contents.Add(new StaticLine("Na wybrane wydarzenie jeszcze nie zostały wydane bilety"));
            }
            foreach(Bilet bilet in wydarzenie.bilety)
            {
                Contents.Add(new ActiveLine(bilet.nazwa + ", " + bilet.cena));
            }
            Contents.Add(new StaticLine(""));

            if (logowanie.owner.IsOrganizer(wydarzenie))
            {
                Contents.Add(new ActiveLine("Nowy bilet"));
            }
            Contents.Add(new ActiveLine("Powrót"));
            Contents.Add(note);
        }

        public override void React(_Line line)
        {
            if(line.Index > 0 && line.Index < wydarzenie.bilety.Count + 1)
            {
                DisplayAdapter.Display(new PanelWydarzenieBilet(logowanie, wydarzenie, wydarzenie.bilety[line.Index - 1]));
            }
            else if(logowanie.owner.IsOrganizer(wydarzenie) && line.Index == Contents.Count - 3)
            {
                DisplayAdapter.Display(new PanelWydarzenieBilet(logowanie, wydarzenie));
            }
            else if(line.Index == Contents.Count - 2)
            {
                DisplayAdapter.Display(new PanelWydarzenieInterakcja(logowanie, wydarzenie));
            }
        }
    }
}
