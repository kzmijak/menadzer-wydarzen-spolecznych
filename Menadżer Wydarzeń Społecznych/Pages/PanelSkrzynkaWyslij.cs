using System;
using System.Collections.Generic;
using System.Text;
using MWS.dbo;
using MWS.Lines;

namespace MWS.Pages
{
    class PanelSkrzynkaWyslij : _Panel
    {
        private List<string> lines;
        public PanelSkrzynkaWyslij(Logowanie logowanie, List<string> lines = null, StaticLine note = null) : base(logowanie)
        {
            if(lines == null)
            {
                lines = new List<string>{ "", "", "", "", "", "", "", "", ""};
            }
            this.lines = lines;

            Contents.Add(new StaticLine("WYSYŁANIE WIADOMOŚCI"));
            Contents.Add(new ActiveLine("Tytuł: "+ this.lines[0]));
            foreach(string line in lines)
            {
                if(line == this.lines[0])
                {
                    continue;
                }
                Contents.Add(new ActiveLine("* " + line));
                if(line == "")
                {
                    break;
                }
            }
            Contents.Add(new StaticLine(""));
            Contents.Add(new ActiveLine("Wyślij"));
            Contents.Add(new ActiveLine("Wyczyść"));
            Contents.Add(new ActiveLine("Powrót"));
            Contents.Add(note);
        }

        public override void React(_Line line)
        {
            if(line.Index == 1)
            {
                lines[0] = Console.ReadLine();
            }
            if(line.Index > 1 && line.Index < Contents.Count - 5)
            {
                lines[line.Index - 1] = Console.ReadLine();
            }
            if(line.Index == Contents.Count - 4)
            {
                string title = "";
                string content = "";
                foreach(string l in lines)
                {
                    if(l == lines[0])
                    {
                        title = l;
                    }
                    else
                    {
                        content += l + '\n';
                    }
                }
                Wiadomosc wiadomosc = new Wiadomosc
                {
                    idadresata = logowanie.id,
                    tytul = title,
                    tresc = content
                };
                DisplayAdapter.Display(new PanelSkrzynkaKontakty(logowanie, "0000", wiadomosc, new StaticLine("Wybierz osobę do której wiadomość ma zostać wysłana.", ConsoleColor.Blue)));
            }
            if(line.Index == Contents.Count - 3)
            {
                DisplayAdapter.Display(new PanelSkrzynkaWyslij(logowanie));
            }
            if(line.Index == Contents.Count - 2)
            {
                DisplayAdapter.Display(new PanelSkrzynka(logowanie));
            }
            DisplayAdapter.Display(new PanelSkrzynkaWyslij(logowanie, lines));
        }
    }
}
