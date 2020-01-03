using System;
using System.Collections.Generic;
using System.Text;
using MWS.dbo;
using MWS.Lines;

namespace MWS.Pages
{
    class PanelWydarzenieBilet : _Panel
    {
        Wydarzenie wydarzenie;
        Bilet bilet;

        public PanelWydarzenieBilet(Logowanie logowanie, Wydarzenie wydarzenie, Bilet bilet = null, StaticLine note = null) : base(logowanie)
        {
            if (bilet is null)
                bilet = new Bilet();
            this.bilet = bilet;
            this.wydarzenie = wydarzenie;

            string BuyOrDelete = "";
            if (logowanie.owner is Uczestnik)
            {
                BuyOrDelete = "Kup bilet";
                foreach (var b in logowanie.uczestnik.bilety)
                {
                    if(b.id == bilet.id)
                    {
                        BuyOrDelete = "Sprzedaj bilet";
                    }
                }
            }
            else if(logowanie.owner.IsOrganizer())
            {
                if (DataAccess.GetRecordById<Bilet>(bilet.id) == null)
                {
                    BuyOrDelete = "Wydaj bilet";
                }
                else
                {
                    BuyOrDelete = "Odwołaj bilet";
                }
            }
            
            Contents.Add(new StaticLine("INFORMACJE O BILECIE"));
            Contents.Add(new ActiveLine("Nazwa: " + bilet.nazwa));
            Contents.Add(new ActiveLine("Cena:  " + bilet.cena + "PLN"));
            Contents.Add(new ActiveLine("Opis:  " + bilet.opis));
            Contents.Add(new StaticLine(""));
            if(logowanie.owner.IsOrganizer() || logowanie.owner is Uczestnik)
                Contents.Add(new ActiveLine(BuyOrDelete));
            Contents.Add(new ActiveLine("Powrót"));
            Contents.Add(note);
        }

        public override void React(_Line line)
        {
            if(logowanie.owner.IsOrganizer())
            {
                if(DataAccess.GetRecordById<Bilet>(bilet.id) is null)
                {
                    if(line.Index == 1)
                    {
                        bilet.nazwa = Console.ReadLine();
                    }
                    else if (line.Index == 2)
                    {
                        int price;
                        if(!int.TryParse(Console.ReadLine(), out price))
                        {
                            DisplayAdapter.Display(new PanelWydarzenieBilet(logowanie, wydarzenie, bilet, new StaticLine("Wpisz poprawną liczbę")));
                        }
                        bilet.cena = price;
                    }
                    else if (line.Index == 3)
                    {
                        bilet.opis = Console.ReadLine();
                    }
                }
            }
            if(line.Index == 5)
            {
                if (Contents[5].Content == "Kup bilet")
                {
                    if(logowanie.uczestnik.kontakt.kartaPlatnicza is null)
                    {
                        DisplayAdapter.Display(new PanelUstawieniaKontaktKarta(logowanie, null, new StaticLine("By dokonać kupna musisz dodać kartę płatniczą.")));
                    }
                    Uczestnik_Bilet.Add(logowanie.uczestnik, bilet);
                    Wydarzenie_Uczestnik.Add(wydarzenie, logowanie.uczestnik);
                    DataAccess.Insert<Platnosc>(new Platnosc
                    {
                        idkarty = logowanie.uczestnik.kontakt.kartaPlatnicza.id,
                        idnadawcy = logowanie.uczestnik.id,
                        kwota = bilet.cena,
                        dzien = DateTime.Now,
                        godzina = DateTime.Now.TimeOfDay
                    });
                }
                else if (Contents[5].Content == "Sprzedaj bilet")
                {
                    DataAccess.Delete(logowanie.uczestnik, bilet);
                    DataAccess.Delete(wydarzenie, logowanie.uczestnik);
                    DataAccess.Insert<Platnosc>(new Platnosc
                    {
                        idkarty = logowanie.uczestnik.kontakt.kartaPlatnicza.id,
                        idnadawcy = logowanie.uczestnik.id,
                        kwota = 0 - bilet.cena,
                        dzien = DateTime.Now,
                        godzina = DateTime.Now.TimeOfDay
                    });
                }
                else if (Contents[5].Content == "Wydaj bilet")
                {
                    bilet.idwydarzenia = wydarzenie.id;

                    if(DbHelper.IsAnyNullOrEmpty(bilet))
                    {
                        DisplayAdapter.Display(new PanelWydarzenieBilet(logowanie, wydarzenie, bilet, new StaticLine("Wypełnij wszystkie pola", ConsoleColor.Red)));
                    }

                    bilet = DataAccess.Insert(bilet);

                    Wniosek addition = new Wniosek
                    {
                        kwota = 0,
                        akcja = "checkTicket:" + bilet.id
                    };
                    foreach (var user in wydarzenie.czlonkowie)
                    {
                        Wiadomosc.Send($"NOWY BILET DLA WYDARZENIA \"{wydarzenie.nazwa.ToUpper()}\"",
                            $"Nazwa: {bilet.nazwa}" +
                            $"\nCena: {bilet.cena}" +
                            $"\nOpis: {bilet.opis}" +
                            $"\nPrzejść do biletu?" +
                            $"\n(Wiadomość wygenerowana automatycznie)",
                            logowanie, user.logowanie, addition);
                    }
                    DisplayAdapter.Display(new PanelWydarzenieBilet(logowanie, wydarzenie, bilet, new StaticLine("Bilet został wydany.", ConsoleColor.Green)));
                }
                else if (Contents[5].Content == "Odwołaj bilet")
                {
                    DataAccess.Delete(bilet);
                    foreach (var user in wydarzenie.czlonkowie)
                    {
                        if(user.IsOrganizer())
                        {
                            Wiadomosc.Send($"WYCOFANO BILET WYDARZENIA \"{wydarzenie.nazwa.ToUpper()}\"",
                                $"Nadawca wiadomości odwołał bilet" +
                                $"\nNazwa: {bilet.nazwa}" +
                                $"\nCena: {bilet.cena}" +
                                $"\nOpis: {bilet.opis}" +
                                $"\n(Wiadomość wygenerowana automatycznie)",
                                logowanie, user.logowanie);
                        }
                        else if(user is Uczestnik)
                        {
                            foreach(var b in (user as Uczestnik).bilety)
                            {
                                if(bilet.id == b.id)
                                {
                                    Wiadomosc.Send($"WYCOFANO BILET WYDARZENIA \"{wydarzenie.nazwa.ToUpper()}\"",
                                        $"Nadawca wiadomości odwołał bilet" +
                                        $"\nNazwa: {bilet.nazwa}" +
                                        $"\nCena: {bilet.cena}" +
                                        $"\nOpis: {bilet.opis}" +
                                        $"\n(Wiadomość wygenerowana automatycznie)",
                                        logowanie, user.logowanie);
                                }
                            }
                        }
                    }
                    DisplayAdapter.Display(new PanelWydarzenieBilety(logowanie, wydarzenie, new StaticLine("Bilet został odwołany.", ConsoleColor.Green)));
                }
            }
            else if(line.Index == 6)
            {
                DisplayAdapter.Display(new PanelWydarzenieBilety(logowanie, wydarzenie));
            }
            DisplayAdapter.Display(new PanelWydarzenieBilet(logowanie, wydarzenie, bilet));
        }
    }
}
