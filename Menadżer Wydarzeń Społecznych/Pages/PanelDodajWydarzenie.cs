using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using MWS.dbo;
using MWS.Lines;

namespace MWS.Pages
{
    class PanelDodajWydarzenie : Panel
    {
        public string nazwa { get; set; }
        public string opis { get; set; }
        public string miejsce { get; set; }
        public DateTime dzien { get; set; }
        public TimeSpan godzina { get; set; }
        public decimal budzet { get; set; }

        public PanelDodajWydarzenie(Logowanie logowanie, StaticLine note = null, PanelDodajWydarzenie update = null): base(logowanie)
        {
            if (update is null)
                update = this;

            nazwa = update.nazwa;
            opis = update.opis;
            miejsce = update.miejsce;
            dzien = update.dzien;
            godzina = update.godzina;
            budzet = update.budzet;

            string sgodzina = godzina.ToString("hh\\:mm"); 
            //opis
            //miejsce
            //dzien
            //godzina
            //budzet

            Contents.Add(new StaticLine("KREATOR WYDARZEŃ"));
            Contents.Add(new ActiveLine("Nazwa:\t\t" + update.nazwa));
            Contents.Add(new ActiveLine("Opis:\t\t" + update.opis));
            Contents.Add(new ActiveLine("Miejsce:\t" + update.miejsce));
            Contents.Add(new ActiveLine("Dzien:\t\t" + update.dzien.ToShortDateString()));
            Contents.Add(new ActiveLine("Godzina:\t" + sgodzina));
            Contents.Add(new ActiveLine("Budżet (PLN):\t" + update.budzet));
            Contents.Add(new StaticLine(""));
            Contents.Add(new ActiveLine("Opublikuj"));
            Contents.Add(new ActiveLine("Cofnij zmiany"));
            Contents.Add(new ActiveLine("Wróć"));

            if(note != null)
            {
                Contents.Add(note);
            }
        }

        public override void React(Line line)
        {
            switch (line.Index)
            {
                case 1:
                    nazwa = Console.ReadLine();
                    break;
                case 2:
                    opis = Console.ReadLine();
                    break;
                case 3:
                    miejsce = Console.ReadLine();
                    break;
                case 4:
                    DateTime date;
                    if (!DateTime.TryParse(Console.ReadLine(), out date))
                    {
                        DisplayAdapter.Display(new PanelDodajWydarzenie(logowanie, new StaticLine("Niepoprawny format daty. (DD.MM.RRRR)", ConsoleColor.Red), this));
                    }
                    dzien = date;
                    break;
                case 5:
                    DateTime dt;
                    if (!DateTime.TryParseExact(Console.ReadLine(), "HH:mm", CultureInfo.InvariantCulture,
                                              DateTimeStyles.None, out dt))
                    {
                        DisplayAdapter.Display(new PanelDodajWydarzenie(logowanie, new StaticLine("Niepoprawny format godziny. (GG:MM)", ConsoleColor.Red), this));
                    }
                    godzina = dt.TimeOfDay;
                    break;
                case 6:
                    decimal dc;
                    if (!decimal.TryParse(Console.ReadLine(), out dc))
                    {
                        DisplayAdapter.Display(new PanelDodajWydarzenie(logowanie, new StaticLine("Niepoprawny format liczbowy.", ConsoleColor.Red), this));
                    }
                    budzet = dc;
                    break;
                case 8:
                    if(!DbHelper.IsAnyNullOrEmpty(this))
                    {
                        Pracownik pracownik = logowanie.pracownik;
                        Wydarzenie wydarzenie = new Wydarzenie
                        {
                            nazwa = this.nazwa,
                            opis = this.opis,
                            miejsce = this.miejsce,
                            dzien = this.dzien,
                            godzina = this.godzina,
                            budzet = this.budzet
                        };
                        wydarzenie = DataAccess.Wydarzenie.Insert(wydarzenie) as Wydarzenie;
                        logowanie.pracownik.wydarzenia.Add(wydarzenie);
                        DataAccess.Wydarzenie_Pracownik.Insert(wydarzenie, pracownik);

                        DisplayAdapter.Display(new PanelWydarzenia(logowanie, new StaticLine("Dodawanie przebiegło pomyślnie.", ConsoleColor.Green)));
                    }
                    else
                        DisplayAdapter.Display(new PanelDodajWydarzenie(logowanie, new StaticLine("Wypełnij wszystkie pola.", ConsoleColor.Red), this));
                    break;
                case 9:
                    DisplayAdapter.Display(new PanelDodajWydarzenie(logowanie));
                    break;
                case 10:
                    DisplayAdapter.Display(new PanelWydarzenia(logowanie));
                    break;
            }
            DisplayAdapter.Display(new PanelDodajWydarzenie(logowanie, null, this));
        }
    }
}
