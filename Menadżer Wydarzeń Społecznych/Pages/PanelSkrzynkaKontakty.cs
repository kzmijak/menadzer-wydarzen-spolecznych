using System;
using System.Collections.Generic;
using System.Text;
using MWS.dbo;
using MWS.Lines;

namespace MWS.Pages
{
    class PanelSkrzynkaKontakty : _Panel
    {
        string expandcode;

        private List<Pracownik> organizatorzy
        {
            get
            {
                List<Pracownik> output = new List<Pracownik>(9999);
                foreach(Logowanie user in logowanie.kontakty)
                {
                    if(user.owner is Pracownik && user.pracownik.stanowisko.ToLower() == "organizator")
                    {
                        output.Add(user.pracownik);
                    }
                }
                return output;
            }
        }
        private List<Pracownik> pracownicy
        {
            get
            {
                List<Pracownik> output = new List<Pracownik>(9999);
                foreach (Logowanie user in logowanie.kontakty)
                {
                    if (user.owner is Pracownik && user.pracownik.stanowisko.ToLower() != "organizator")
                    {
                        output.Add(user.pracownik);
                    }
                }
                return output;
            }
        }
        private List<Sponsor> sponsorzy
        {
            get
            {
                List<Sponsor> output = new List<Sponsor>(9999);
                foreach (Logowanie user in logowanie.kontakty)
                {
                    if (user.owner is Sponsor)
                    {
                        output.Add(user.sponsor);
                    }
                }
                return output;
            }
        }
        private List<Uczestnik> uczestnicy
        {
            get
            {
                List<Uczestnik> output = new List<Uczestnik>(9999);
                foreach (Logowanie user in logowanie.kontakty)
                {
                    if (user.owner is Uczestnik)
                    {
                        output.Add(user.uczestnik);
                    }
                }
                return output;
            }
        }
        private Wiadomosc message;

        public PanelSkrzynkaKontakty(Logowanie logowanie, string expandcode = "0000", Wiadomosc message = null, StaticLine note = null) : base(logowanie)
        {
            this.message = message;
            this.expandcode = expandcode;
            Contents.Add(new StaticLine("KONTAKTY" + ' ' + expandcode));
            Contents.Add(new ActiveLine("Organizatorzy"));
            Expand(organizatorzy, expandcode[0]);
            Contents.Add(new ActiveLine("Pracownicy"));
            Expand(pracownicy, expandcode[1]);
            Contents.Add(new ActiveLine("Sponsorzy"));
            Expand(sponsorzy, expandcode[2]);
            Contents.Add(new ActiveLine("Uczestnicy"));
            Expand(uczestnicy, expandcode[3]);
            Contents.Add(new StaticLine(""));
            Contents.Add(new ActiveLine("Powrót"));
            Contents.Add(note);
        }

        public override void React(_Line line)
        {
            if(line.Index == 1)
            {
                if (organizatorzy.Count == 0)
                {
                    DisplayAdapter.Display(new PanelSkrzynkaKontakty(logowanie, expandcode, message, new StaticLine("Nie masz kontaktów w tej sferze.", ConsoleColor.Red)));
                }
                else
                {
                    DisplayAdapter.Display(new PanelSkrzynkaKontakty(logowanie, Reverse(0), message));
                }
            }
            else if (line.Index > 1 && line.Index < 2 + Expand(organizatorzy))
            {
                SendOrSelect(organizatorzy[line.Index - 2]);                
            }
            else if (line.Index == 2 + Expand(organizatorzy))
            {
                if (organizatorzy.Count == 0)
                {
                    DisplayAdapter.Display(new PanelSkrzynkaKontakty(logowanie, expandcode, message, new StaticLine("Nie masz kontaktów w tej sferze.", ConsoleColor.Red)));
                }
                else
                {
                    DisplayAdapter.Display(new PanelSkrzynkaKontakty(logowanie, Reverse(0), message));
                }
            }
            else if(line.Index > 2 + Expand(organizatorzy) && line.Index < 3 + Expand(organizatorzy) + Expand(pracownicy))
            {
                SendOrSelect(pracownicy[line.Index - 3 + Expand(organizatorzy)]);
            }
            else if(line.Index == 3 + Expand(organizatorzy) + Expand(pracownicy))
            {
                if (pracownicy.Count == 0)
                {
                    DisplayAdapter.Display(new PanelSkrzynkaKontakty(logowanie, expandcode, message, new StaticLine("Nie masz kontaktów w tej sferze.", ConsoleColor.Red)));
                }
                else
                {
                    DisplayAdapter.Display(new PanelSkrzynkaKontakty(logowanie, Reverse(2), message));
                }
            }
            else if(line.Index > 3 + Expand(organizatorzy) + Expand(pracownicy) && line.Index < 4 + Expand(organizatorzy) + Expand(pracownicy) + Expand(sponsorzy))
            {
                SendOrSelect(pracownicy[line.Index - 4 + Expand(organizatorzy) + Expand(pracownicy)]);
            }
            else if(line.Index == 4 + Expand(organizatorzy) + Expand(pracownicy) + Expand(sponsorzy))
            {
                if (sponsorzy.Count == 0)
                {
                    DisplayAdapter.Display(new PanelSkrzynkaKontakty(logowanie, expandcode, message, new StaticLine("Nie masz kontaktów w tej sferze.", ConsoleColor.Red)));
                }
                else
                {
                    DisplayAdapter.Display(new PanelSkrzynkaKontakty(logowanie, Reverse(3), message));
                }
            }
            else if(line.Index > 4 + Expand(organizatorzy) + Expand(pracownicy) + Expand(sponsorzy) && line.Index < 5 + Expand(organizatorzy) + Expand(pracownicy) + Expand(sponsorzy) + Expand(uczestnicy))
            {
                SendOrSelect(pracownicy[line.Index - 5 + Expand(organizatorzy) + Expand(pracownicy) + Expand(sponsorzy)]);
            }
            else if(line.Index == 6 + Expand(organizatorzy) + Expand(pracownicy) + Expand(sponsorzy) + Expand(uczestnicy))
            {
                DisplayAdapter.Display(new PanelSkrzynka(logowanie));
            }
        }

        private void SendOrSelect(_CoreObject user)
        {
            if (message is null)
            {
                DisplayAdapter.Display(new PanelWydarzenieInterakcjaOsoba(logowanie, null, user));
            }
            else
            {
                message.adresat = user.logowanie;
                message.godzina = DateTime.Now.TimeOfDay;
                message.dzien = DateTime.Now;
                DataAccess.Insert(message);
                DisplayAdapter.Display(new PanelSkrzynka(logowanie, new StaticLine("Wiadomość została wysłana.", ConsoleColor.Green)));
            }
        }

        private string Reverse(int position)
        {
            var expandcode = this.expandcode.ToCharArray();
            if (expandcode[position] == '0')
            {
                expandcode[position] = '1';
            }
            else
            {
                expandcode[position] = '0';
            }            
            return new string(expandcode);            
        }

        private int Expand<T>(List<T> users, int check = '2') where T: _CoreObject
        {
            int cnt = 0;
            if(check=='1' || check =='2' && users.Count != 0)
            {
                foreach(var user in users)
                {
                    if(typeof(T) == typeof(Sponsor))
                    {
                        if(check == '1')
                            Contents.Add(new ActiveLine(" " + (user as Sponsor).nazwa));
                        cnt++;
                    }
                    else
                    {
                        string role = "";
                        if(user is Pracownik && (user as Pracownik).stanowisko.ToLower() != "organizator")
                        {
                            role = $" ({(user as Pracownik).stanowisko})";
                        }
                        if (check == '1')
                            Contents.Add(new ActiveLine(" " + user.kontakt.imie + " " + user.kontakt.imie + role));
                        cnt++;
                    }
                }
            }
            return cnt;
        }

    }
}
