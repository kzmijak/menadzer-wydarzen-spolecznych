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
        public static PSetWydarzenie Wydarzenie { get; set; } = new PSetWydarzenie();
    }
}
