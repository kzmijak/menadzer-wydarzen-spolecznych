using System;
using System.Collections.Generic;
using System.Text;
using MWS.dbo;
using MWS.Lines;

namespace MWS.Pages
{
    class PanelUstawieniaKontaktKarta : _Panel
    {
        private KartaPlatnicza karta = new KartaPlatnicza();
        public PanelUstawieniaKontaktKarta(Logowanie logowanie, KartaPlatnicza update, StaticLine note = null) : base(logowanie, note)
        {
            string saveOrUpdate = "Aktualizuj";
            if(logowanie.owner.kontakt.kartaPlatnicza is null)
            {
                saveOrUpdate = "Zapisz";
            }

            karta.kontakt = logowanie.owner.kontakt.id;
            if (update != null)
                karta = update;
            Contents.Add(new StaticLine("KARTA PŁATNICZA "));
            Contents.Add(new ActiveLine("Właściciel: " + '\t' + '\t' + karta.wlasciciel, "Właściciel podany na karcie płatniczej"));
            Contents.Add(new ActiveLine("Numer karty: " + '\t' + '\t' + karta.numer, "16-znakowy numer karty płatniczej"));
            Contents.Add(new ActiveLine("Rok wygaśnięcia: " + '\t' + karta.wygasniecie, "Rok wygaśnięcia karty płatniczej"));
            Contents.Add(new ActiveLine("Kod bezpieczeństwa: " + '\t' + karta.kbezpiecz, "Kod bezpieczeństwa podany na odwrocie karty płatniczej"));
            Contents.Add(new StaticLine(""));
            Contents.Add(new ActiveLine("Resetuj", "Cofa zmiany do stanu poprzednio zatwierdzonego"));
            Contents.Add(new ActiveLine(saveOrUpdate));
            Contents.Add(new ActiveLine("Powrót", "Powrót do panelu ustawień kontaktu"));
            Contents.Add(Note);
        }

        public override void React(_Line line)
        {
            switch(line.Index)
            {
                case 1:
                    karta.wlasciciel = Console.ReadLine();
                    break;
                case 2:
                    karta.numer = Console.ReadLine();
                    break;
                case 3:
                    karta.wygasniecie = Console.ReadLine();
                    break;
                case 4:
                    karta.kbezpiecz = Console.ReadLine();
                    break;
                case 6:
                    DisplayAdapter.Display(new PanelUstawieniaKontaktKarta(logowanie, null));
                    break;
                case 7:
                    if(DbHelper.IsAnyNullOrEmpty(karta))
                    {
                        DisplayAdapter.Display(new PanelUstawieniaKontaktKarta(logowanie, karta, new StaticLine("Wypełnij wszystkie pola.", ConsoleColor.Red)));
                    }
                    else
                    {
                        logowanie.owner.kontakt.kartaPlatnicza = karta;
                        DisplayAdapter.Display(new PanelUstawieniaKontaktKarta(logowanie, karta, new StaticLine("Karta płatnicza została zaktualizowana", ConsoleColor.Green)));
                    }
                    break;
                case 8:
                    DisplayAdapter.Display(new PanelUstawieniaKontakt(logowanie));
                    break;
            }
            DisplayAdapter.Display(new PanelUstawieniaKontaktKarta(logowanie, karta));
        }
    }
}
