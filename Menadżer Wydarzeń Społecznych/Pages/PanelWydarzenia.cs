using MWS.dbo;
using MWS.Lines;
using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.Pages
{
    class PanelWydarzenia : _Panel
    {
        private bool organizatorCheck = false;
        private int notedependent = 1;
        public PanelWydarzenia(Logowanie logowanie, StaticLine note = null): base(logowanie)
        {
            Contents.Add(new StaticLine("WYDARZENIA"));
            if (logowanie.owner is Pracownik)
                if (logowanie.pracownik.stanowisko.ToLower() == "organizator")
                {
                    Contents.Add(new ActiveLine("Zorganizuj wydarzenie"));
                    this.organizatorCheck = true;
                }
            Contents.Add(new StaticLine("Aktywne wydarzenia:"));
            foreach (_DatabaseObject wydarzenie in DataAccess.GetCollection<Wydarzenie>())
            {
                Contents.Add(new ActiveLine((wydarzenie as Wydarzenie).nazwa));
            }
            Contents.Add(new StaticLine(""));
            Contents.Add(new ActiveLine("Powrót"));
            if (note != null)
            {
                Contents.Add(note);
                notedependent++;
            }
        }

        public override void React(_Line line)
        {
            if (organizatorCheck == true && line.Index == 1)
            {
                DisplayAdapter.Display(new PanelWydarzeniaDodaj(logowanie));
            }
            else if(line.Index == Contents.Count - notedependent )
            {
                DisplayAdapter.Display(new Panel(logowanie));
            }
            else
            {
                int organizatordependent = 2;
                if (organizatorCheck == true)
                    organizatordependent = 3;
                Wydarzenie wydarzenie = DataAccess.GetCollection<Wydarzenie>()[DisplayAdapter.CurrentLine.Index - organizatordependent] as Wydarzenie;
                DisplayAdapter.Display(new PanelWydarzenieInterakcja(logowanie, wydarzenie));
            }
        }
    }
}
