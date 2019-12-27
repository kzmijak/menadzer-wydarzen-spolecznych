using System;
using System.Collections.Generic;
using System.Text;
using MWS.dbo;
using MWS.Lines;

namespace MWS.Pages
{
    class PanelSkrzynkaWiadomosci : _Panel
    {
        private string mode;
        public PanelSkrzynkaWiadomosci(Logowanie logowanie, string mode, StaticLine note = null) : base(logowanie)
        {
            this.mode = mode;
            Contents.Add(new StaticLine(mode + " WIADOMOŚCI"));
            foreach(Wiadomosc wiadomosc in DataAccess.GetCollection<Wiadomosc>())
            {
                if((mode == "ODEBRANE" && wiadomosc.idodbiorcy == logowanie.id) || (mode == "WYSŁANE" && wiadomosc.idadresata == logowanie.id))
                {
                    Contents.Add(new ActiveLine($"({wiadomosc.dzien.ToShortDateString()} {wiadomosc.godzina.ToString("hh\\:mm")}) {wiadomosc.tytul}"));
                }
            }
            Contents.Add(new StaticLine(""));
            Contents.Add(new ActiveLine("Usuń wszystkie"));
            Contents.Add(new ActiveLine("Powrót"));
            Contents.Add(note);
        }

        public override void React(_Line line)
        {
            if(line.Index > 1 && line.Index < Contents.Count - 4)
            {
                // GO TO PanelSkrzynkaWiadomosc
            }
            if(line.Index == Contents.Count - 3)
            {
                foreach(Wiadomosc wiadomosc in DataAccess.GetCollection<Wiadomosc>())
                {
                    if((mode == "ODEBRANE" && wiadomosc.idodbiorcy == logowanie.id) || (mode == "WYSŁANE" && wiadomosc.idadresata == logowanie.id))
                    {
                        DataAccess.Delete(wiadomosc.wniosek);
                        DataAccess.Delete(wiadomosc);
                    }
                }
                DisplayAdapter.Display(new PanelSkrzynkaWiadomosci(logowanie, mode, new StaticLine("Wszystkie wiadomości zostały usunięte po obu stronach.", ConsoleColor.Green)));
            }
            if(line.Index == Contents.Count - 2)
            {
                DisplayAdapter.Display(new PanelSkrzynka(logowanie));
            }
        }
    }
}
