using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Dapper;
using MWS.dbo;
using MWS.Procedures;

namespace MWS
{
    static class DataAccess
    {
        public static PSetBilet Bilet { get; set; } = new PSetBilet();
        public static PSetDotacja Dotacja { get; set; } = new PSetDotacja();
        public static PSetKartaPlatnicza KartaPlatnicza { get; set; } = new PSetKartaPlatnicza();
        public static PSetKontakt Kontakt { get; set; } = new PSetKontakt();
        public static PSetLogowanie Logowanie { get; set; } = new PSetLogowanie();
        public static PSetPlatnosc Platnosc { get; set; } = new PSetPlatnosc();
        public static PSetPracownik Pracownik { get; set; } = new PSetPracownik();
        public static PSetUczestnik Uczestnik { get; set; } = new PSetUczestnik();
        public static PSetSponsor Sponsor { get; set; } = new PSetSponsor();
        public static PSetWydarzenie Wydarzenie { get; set; } = new PSetWydarzenie();
        public static PSetWniosek Wniosek{ get; set; } = new PSetWniosek();
        public static PSetWiadomosc Wiadomosc { get; set; } = new PSetWiadomosc();

        public static JPSetPracownik_Pracownik Pracownik_Pracownik { get; set; } = new JPSetPracownik_Pracownik();
        public static JPSetUczestnik_Bilet Uczestnik_Bilet { get; set; } = new JPSetUczestnik_Bilet();
        public static JPSetWydarzenie_Pracownik Wydarzenie_Pracownik { get; set; } = new JPSetWydarzenie_Pracownik();
        public static JPSetWydarzenie_Sponsor Wydarzenie_Sponsor { get; set; } = new JPSetWydarzenie_Sponsor();
        public static JPSetWydarzenie_Uczestnik Wydarzenie_Uczestnik { get; set; } = new JPSetWydarzenie_Uczestnik();
        public static JPSetLogowanie_Logowanie Logowanie_Logowanie { get; set; } = new JPSetLogowanie_Logowanie();

    }
}
