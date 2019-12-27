using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Dapper;
using MWS.dbo;
using MWS.Procedures;
using System.Linq;

namespace MWS
{
    static class DataAccess
    {
        private static PSetBilet bilet { get; set; } = new PSetBilet();
        private static PSetDotacja dotacja { get; set; } = new PSetDotacja();
        private static PSetKartaPlatnicza kartaPlatnicza { get; set; } = new PSetKartaPlatnicza();
        private static PSetKontakt kontakt { get; set; } = new PSetKontakt();
        private static PSetLogowanie logowanie { get; set; } = new PSetLogowanie();
        private static PSetPlatnosc platnosc { get; set; } = new PSetPlatnosc();
        private static PSetPracownik pracownik { get; set; } = new PSetPracownik();
        private static PSetUczestnik uczestnik { get; set; } = new PSetUczestnik();
        private static PSetSponsor sponsor { get; set; } = new PSetSponsor();
        private static PSetWydarzenie wydarzenie { get; set; } = new PSetWydarzenie();
        private static PSetWniosek wniosek{ get; set; } = new PSetWniosek();
        private static PSetWiadomosc wiadomosc { get; set; } = new PSetWiadomosc();

        private static JPSetPracownik_Pracownik jpPracownik_Pracownik { get; set; } = new JPSetPracownik_Pracownik();
        private static JPSetUczestnik_Bilet jpUczestnik_Bilet { get; set; } = new JPSetUczestnik_Bilet();
        private static JPSetWydarzenie_Pracownik jpWydarzenie_Pracownik { get; set; } = new JPSetWydarzenie_Pracownik();
        private static JPSetWydarzenie_Sponsor jpWydarzenie_Sponsor { get; set; } = new JPSetWydarzenie_Sponsor();
        private static JPSetWydarzenie_Uczestnik jpWydarzenie_Uczestnik { get; set; } = new JPSetWydarzenie_Uczestnik();
        private static JPSetLogowanie_Logowanie jpLogowanie_Logowanie { get; set; } = new JPSetLogowanie_Logowanie();

        public static _DatabaseObject Insert(_DatabaseObject obj, _DatabaseObject jpobj = null)
        {
            if (obj is Bilet)
            {
                return bilet.Insert(obj);
            }
            else if (obj is Dotacja)
            {
                return dotacja.Insert(obj);
            }
            else if (obj is KartaPlatnicza)
            {
                return kartaPlatnicza.Insert(obj);
            }
            else if (obj is Kontakt)
            {
                return kontakt.Insert(obj);
            }
            else if (obj is Logowanie)
            {
                if (jpobj is null)
                {
                    return logowanie.Insert(obj);
                }
                else if (jpobj is Logowanie)
                {
                    jpLogowanie_Logowanie.Insert(obj, jpobj);
                    return null;
                }
            }
            else if (obj is Platnosc)
            {
                return platnosc.Insert(obj);
            }
            else if (obj is Pracownik)
            {
                if (jpobj is null)
                {
                    return pracownik.Insert(obj);
                }
                else if (jpobj is Pracownik)
                {
                    jpPracownik_Pracownik.Insert(obj, jpobj);
                    return null;
                }
            }
            else if (obj is Uczestnik)
            {
                if (jpobj is null)
                {
                    return uczestnik.Insert(obj);
                }
                else if (jpobj is Bilet)
                {
                    jpUczestnik_Bilet.Insert(obj, jpobj);
                    return null;
                }
            }
            else if (obj is Sponsor)
            {
                return sponsor.Insert(obj);
            }
            else if (obj is Wydarzenie)
            {
                if (jpobj is null)
                {
                    return wydarzenie.Insert(obj);
                }
                else if (jpobj is Pracownik)
                {
                    jpWydarzenie_Pracownik.Insert(obj, jpobj);
                    return null;
                }
                else if (jpobj is Sponsor)
                {
                    jpWydarzenie_Sponsor.Insert(obj, jpobj);
                    return null;
                }
                else if (jpobj is Uczestnik)
                {
                    jpWydarzenie_Uczestnik.Insert(obj, jpobj);
                    return null;
                }
            }
            else if (obj is Wniosek)
            {
                return wniosek.Insert(obj);
            }
            else if (obj is Wiadomosc)
            {
                return wiadomosc.Insert(obj);
            }
            return null;
        }
        public static void Update(_DatabaseObject @old, _DatabaseObject @new)
        {
            if (@old is Bilet && @new is Bilet)
            {
                bilet.Update(@old, @new);
            }
            else if (@old is Dotacja && @new is Dotacja)
            {
                dotacja.Update(@old, @new);
            }
            else if (@old is KartaPlatnicza && @new is KartaPlatnicza)
            {
                kartaPlatnicza.Update(@old, @new);
            }
            else if (@old is Kontakt && @new is Kontakt)
            {
                kontakt.Update(@old, @new);
            }
            else if (@old is Logowanie && @new is Logowanie)
            {
                logowanie.Update(@old, @new);                
            }
            else if (@old is Platnosc && @new is Platnosc)
            {
                platnosc.Update(@old, @new);
            }
            else if (@old is Pracownik && @new is Pracownik)
            {
                pracownik.Update(@old, @new);                
            }
            else if (@old is Uczestnik && @new is Uczestnik)
            {
                uczestnik.Update(@old, @new);                
            }
            else if (@old is Sponsor && @new is Sponsor)
            {
                sponsor.Update(@old, @new);
            }
            else if (@old is Wydarzenie && @new is Wydarzenie)
            {
                wydarzenie.Update(@old, @new);                
            }
            else if (@old is Wniosek && @new is Wniosek)
            {
                wniosek.Update(@old, @new);
            }
            else if (@old is Wiadomosc && @new is Wiadomosc)
            {
                wiadomosc.Update(@old, @new);
            }
        }
        public static void Delete(_DatabaseObject obj, _DatabaseObject jpobj = null)
        {
            if (obj is Bilet)
            {
                bilet.Delete(obj);
            }
            else if (obj is Dotacja)
            {
                dotacja.Delete(obj);
            }
            else if (obj is KartaPlatnicza)
            {
                kartaPlatnicza.Delete(obj);
            }
            else if (obj is Kontakt)
            {
                kontakt.Delete(obj);
            }
            else if (obj is Logowanie)
            {
                if (jpobj is null)
                {
                    logowanie.Delete(obj);
                }
                else if (jpobj is Logowanie)
                {
                    jpLogowanie_Logowanie.Delete(obj, jpobj);
                }
            }
            else if (obj is Platnosc)
            {
                platnosc.Delete(obj);
            }
            else if (obj is Pracownik)
            {
                if (jpobj is null)
                {
                    pracownik.Delete(obj);
                }
                else if (jpobj is Pracownik)
                {
                    jpPracownik_Pracownik.Delete(obj, jpobj);
                }
            }
            else if (obj is Uczestnik)
            {
                if (jpobj is null)
                {
                    uczestnik.Delete(obj);
                }
                else if (jpobj is Bilet)
                {
                    jpUczestnik_Bilet.Delete(obj, jpobj);
                }
            }
            else if (obj is Sponsor)
            {
                sponsor.Delete(obj);
            }
            else if (obj is Wydarzenie)
            {
                if (jpobj is null)
                {
                    wydarzenie.Delete(obj);
                }
                else if (jpobj is Pracownik)
                {
                    jpWydarzenie_Pracownik.Delete(obj, jpobj);
                }
                else if (jpobj is Sponsor)
                {
                    jpWydarzenie_Sponsor.Delete(obj, jpobj);
                }
                else if (jpobj is Uczestnik)
                {
                    jpWydarzenie_Uczestnik.Delete(obj, jpobj);
                }
            }
            else if (obj is Wniosek)
            {
                wniosek.Delete(obj);
            }
            else if (obj is Wiadomosc)
            {
                wiadomosc.Delete(obj);
            }
        }
        public static T GetRecord<T>(_DatabaseObject obj) where T: _DatabaseObject
        {
            if (obj is Bilet)
            {
                return bilet.GetRecord(obj) as T;
            }
            else if (obj is Dotacja)
            {
                return dotacja.GetRecord(obj) as T;
            }
            else if (obj is KartaPlatnicza)
            {
                return kartaPlatnicza.GetRecord(obj) as T;
            }
            else if (obj is Kontakt)
            {
                return kontakt.GetRecord(obj) as T;
            }
            else if (obj is Logowanie)
            {
                return logowanie.GetRecord(obj) as T;
            }
            else if (obj is Platnosc)
            {
                return platnosc.GetRecord(obj) as T;
            }
            else if (obj is Pracownik)
            {
                return pracownik.GetRecord(obj) as T;
            }
            else if (obj is Uczestnik)
            {
                return uczestnik.GetRecord(obj) as T;
            }
            else if (obj is Sponsor)
            {
                return sponsor.GetRecord(obj) as T;
            }
            else if (obj is Wydarzenie)
            {
                return wydarzenie.GetRecord(obj) as T;
            }
            else if (obj is Wniosek)
            {
                return wniosek.GetRecord(obj) as T;
            }
            else if (obj is Wiadomosc)
            {
                return wiadomosc.GetRecord(obj) as T;
            }
            else
            {
                throw new InvalidCastException("Data type not precised.");
            }
        }
        public static T GetRecordById<T>(int id) where T: _DatabaseObject
        {
            if (typeof(T) == typeof(Bilet))
            {
                return bilet.GetRecordById(id) as T;
            }
            else if (typeof(T) == typeof(Dotacja))
            {
                return dotacja.GetRecordById(id) as T;
            }
            else if (typeof(T) == typeof(KartaPlatnicza))
            {
                return kartaPlatnicza.GetRecordById(id) as T;
            }
            else if (typeof(T) == typeof(Kontakt))
            {
                return kontakt.GetRecordById(id) as T;
            }
            else if (typeof(T) == typeof(Logowanie))
            {
                return logowanie.GetRecordById(id) as T;
            }
            else if (typeof(T) == typeof(Platnosc))
            {
                return platnosc.GetRecordById(id) as T;
            }
            else if (typeof(T) == typeof(Pracownik))
            {
                return pracownik.GetRecordById(id) as T;
            }
            else if (typeof(T) == typeof(Uczestnik))
            {
                return uczestnik.GetRecordById(id) as T;
            }
            else if (typeof(T) == typeof(Sponsor))
            {
                return sponsor.GetRecordById(id) as T;
            }
            else if (typeof(T) == typeof(Wydarzenie))
            {
                return wydarzenie.GetRecordById(id) as T;
            }
            else if (typeof(T) == typeof(Wniosek))
            {
                return wniosek.GetRecordById(id) as T;
            }
            else if (typeof(T) == typeof(Wiadomosc))
            {
                return wiadomosc.GetRecordById(id) as T;
            }
            return null;
        }
        public static List<T> GetCollection<T>() where T: _DatabaseObject
        {
            if (typeof(T) == typeof(Bilet))
            {
                return bilet.GetCollection().Cast<T>().ToList();
            }
            else if (typeof(T) == typeof(Dotacja))
            {
                return dotacja.GetCollection().Cast<T>().ToList();
            }
            else if (typeof(T) == typeof(KartaPlatnicza))
            {
                return kartaPlatnicza.GetCollection().Cast<T>().ToList();
            }
            else if (typeof(T) == typeof(Kontakt))
            {
                return kontakt.GetCollection().Cast<T>().ToList();
            }
            else if (typeof(T) == typeof(Logowanie))
            {
                return logowanie.GetCollection().Cast<T>().ToList();
            }
            else if (typeof(T) == typeof(Platnosc))
            {
                return platnosc.GetCollection().Cast<T>().ToList();
            }
            else if (typeof(T) == typeof(Pracownik))
            {
                return pracownik.GetCollection().Cast<T>().ToList();
            }
            else if (typeof(T) == typeof(Uczestnik))
            {
                return uczestnik.GetCollection().Cast<T>().ToList();
            }
            else if (typeof(T) == typeof(Sponsor))
            {
                return sponsor.GetCollection().Cast<T>().ToList();
            }
            else if (typeof(T) == typeof(Wydarzenie))
            {
                return wydarzenie.GetCollection().Cast<T>().ToList();
            }
            else if (typeof(T) == typeof(Wniosek))
            {
                return wniosek.GetCollection().Cast<T>().ToList();
            }
            else if (typeof(T) == typeof(Wiadomosc))
            {
                return wiadomosc.GetCollection().Cast<T>().ToList();
            }
            return null;
        }
        public static List<T> GetConnections<T>() where T: _JoiningTable
        {
            if (typeof(T) == typeof(Logowanie_Logowanie))
            {
                return jpLogowanie_Logowanie.GetCollection().Cast<T>().ToList();
            }
            else if (typeof(T) == typeof(Pracownik_Pracownik))
            {
                return jpPracownik_Pracownik.GetCollection().Cast<T>().ToList();
            }
            else if (typeof(T) == typeof(Uczestnik_Bilet))
            {
                return jpUczestnik_Bilet.GetCollection().Cast<T>().ToList();
            }
            else if (typeof(T) == typeof(Wydarzenie_Pracownik))
            {
                return jpWydarzenie_Pracownik.GetCollection().Cast<T>().ToList();
            }
            else if (typeof(T) == typeof(Wydarzenie_Sponsor))
            {
                return jpWydarzenie_Sponsor.GetCollection().Cast<T>().ToList();
            }
            else if (typeof(T) == typeof(Wydarzenie_Uczestnik))
            {
                return jpWydarzenie_Uczestnik.GetCollection().Cast<T>().ToList();
            }
            return null;
        }
    }
}
