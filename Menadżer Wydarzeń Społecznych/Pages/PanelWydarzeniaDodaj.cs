using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using MWS.dbo;
using MWS.Lines;

namespace MWS.Pages
{
    class PanelWydarzeniaDodaj : _Panel
    {

        private Wydarzenie wydarzenie;
        int updatecaller;

        public PanelWydarzeniaDodaj(Logowanie logowanie, StaticLine note = null, Wydarzenie update = null, int updatecaller = 0): base(logowanie)
        {
            if (updatecaller != 0)
            {
                this.updatecaller = updatecaller;
            }
            else
                this.updatecaller = 0;
            if(update != null)
            {
                wydarzenie = update;
            }
            else
            {
                wydarzenie = new Wydarzenie
                {
                    nazwa = "",
                    opis = "",
                    miejsce = "",
                    dzien = DateTime.Now,
                    godzina = DateTime.Now.TimeOfDay,
                    budzet = 0
                };
            }

            string sgodzina = wydarzenie.godzina.ToString("hh\\:mm");

            Contents.Add(new StaticLine("KREATOR WYDARZEŃ"));
            Contents.Add(new ActiveLine("Nazwa:\t\t" + wydarzenie.nazwa));
            Contents.Add(new ActiveLine("Opis:\t\t" + wydarzenie.opis));
            Contents.Add(new ActiveLine("Miejsce:\t" + wydarzenie.miejsce));
            Contents.Add(new ActiveLine("Dzien:\t\t" + wydarzenie.dzien.ToShortDateString()));
            Contents.Add(new ActiveLine("Godzina:\t" + sgodzina));
            Contents.Add(new ActiveLine("Budżet (PLN):\t" + wydarzenie.budzet));
            Contents.Add(new StaticLine(""));
            Contents.Add(new ActiveLine("Opublikuj"));
            Contents.Add(new ActiveLine("Wyczyść pola"));
            Contents.Add(new ActiveLine("Wróć"));

            if(note != null)
            {
                Contents.Add(note);
            }
        }

        public override void React(_Line line)
        {
            switch (line.Index)
            {
                case 1:
                    wydarzenie.nazwa = Console.ReadLine();
                    break;
                case 2:
                    wydarzenie.opis = Console.ReadLine();
                    break;
                case 3:
                    wydarzenie.miejsce = Console.ReadLine();
                    break;
                case 4:
                    DateTime date;
                    if (!DateTime.TryParse(Console.ReadLine(), out date))
                    {
                        DisplayAdapter.Display(new PanelWydarzeniaDodaj(logowanie, new StaticLine("Niepoprawny format daty. (DD.MM.RRRR)", ConsoleColor.Red), wydarzenie, updatecaller));
                    }
                    wydarzenie.dzien = date;
                    break;
                case 5:
                    DateTime dt;
                    if (!DateTime.TryParseExact(Console.ReadLine(), "HH:mm", CultureInfo.InvariantCulture,
                                              DateTimeStyles.None, out dt))
                    {
                        DisplayAdapter.Display(new PanelWydarzeniaDodaj(logowanie, new StaticLine("Niepoprawny format godziny. (GG:MM)", ConsoleColor.Red), wydarzenie, updatecaller));
                    }
                    wydarzenie.godzina = dt.TimeOfDay;
                    break;
                case 6:
                    decimal dc;
                    if (!decimal.TryParse(Console.ReadLine(), out dc))
                    {
                        DisplayAdapter.Display(new PanelWydarzeniaDodaj(logowanie, new StaticLine("Niepoprawny format liczbowy.", ConsoleColor.Red), wydarzenie, updatecaller));
                    }
                    wydarzenie.budzet = dc;
                    break;
                case 8:
                    if(!DbHelper.IsAnyNullOrEmpty(wydarzenie))
                    {   
                        if(this.updatecaller == 0)
                        {
                            Wydarzenie_Pracownik.Add(wydarzenie, logowanie.pracownik);

                            DisplayAdapter.Display(new PanelWydarzenia(logowanie, new StaticLine("Dodawanie przebiegło pomyślnie.", ConsoleColor.Green)));
                        }
                        else 
                        {
                            var old = DataAccess.GetRecordById<Wydarzenie>(updatecaller);
                            DataAccess.Update(old, wydarzenie);

                            foreach(var jt in DataAccess.GetConnections<Wydarzenie_Pracownik>())
                            {
                                if((jt as Wydarzenie_Pracownik).idwydarzenia == old.id && (DataAccess.GetRecordById<Pracownik>((jt as Wydarzenie_Pracownik).idpracownika)).stanowisko.ToLower() == "organizator")
                                {
                                    Wniosek GoToWydarzenia = new Wniosek
                                    {
                                        kwota = 0,
                                        akcja = $"gotoevent:{wydarzenie.id}"
                                    };

                                    Wiadomosc.Send(
                                        $"WYDARZENIE \"{wydarzenie.nazwa.ToUpper()}\" ULEGŁO ZMIANIE", 
                                        $"Zostały dokonane zmiany w wydarzeniu \"{(old as Wydarzenie).nazwa}\"."
                                      + $"\nKorekty dokonał: {logowanie.pracownik.kontakt.imie} {logowanie.pracownik.kontakt.nazwisko}."
                                      + $"\nNazwa:   {(old as Wydarzenie).nazwa}   -> {wydarzenie.nazwa}"
                                      + $"\nMiejsce: {(old as Wydarzenie).miejsce} -> {wydarzenie.miejsce}"
                                      + $"\nDzień:   {(old as Wydarzenie).dzien}   -> {wydarzenie.dzien}"
                                      + $"\nGodzina: {(old as Wydarzenie).godzina} -> {wydarzenie.godzina}"
                                      + $"\nBudżet:  {(old as Wydarzenie).budzet}  -> {wydarzenie.budzet}", 
                                        logowanie, (DataAccess.GetRecordById<Pracownik>((jt as Wydarzenie_Pracownik).idpracownika)).logowanie, GoToWydarzenia);

                                }
                            }
                            DisplayAdapter.Display(new PanelWydarzenieInterakcjaCzlonek(logowanie, wydarzenie, null));
                        }
                    }
                    else
                        DisplayAdapter.Display(new PanelWydarzeniaDodaj(logowanie, new StaticLine("Wypełnij wszystkie pola.", ConsoleColor.Red), wydarzenie, updatecaller));
                    break;
                case 9:
                    DisplayAdapter.Display(new PanelWydarzeniaDodaj(logowanie, null, null, updatecaller));
                    break;
                case 10:
                    DisplayAdapter.Display(new PanelWydarzenia(logowanie));
                    break;
            }
            DisplayAdapter.Display(new PanelWydarzeniaDodaj(logowanie, null, wydarzenie, updatecaller));
        }
    }
}
