using System;
using System.Collections.Generic;
using System.Text;
using MWS.dbo;
using MWS.Lines;
using MWS.Pages;

namespace MWS.Pages
{
    class Login : Page
    {
        public List<Line> Contents { get; set; }
        
        public Login()
        {
            this.Contents = new List<Line>(10);
            Line.LastIndex = 0;

            this.Contents.Add(new StaticLine("THIS IS THE INITIAL LOGIN PAGE"));
            this.Contents.Add(new ActiveLine("Add record to the database"));
            this.Contents.Add(new StaticLine("Update a record from the database"));
            this.Contents.Add(new ActiveLine("Delete a record from the database"));
        }
        
        public void React(Line line)
        {
            switch(line.Index)
            {
                case 1:
                    Wydarzenie w = new Wydarzenie();
                    this.Contents.Add(new StaticLine("Name: "));
                    w.nazwa = Console.ReadLine();
                    this.Contents.Add(new StaticLine("Description: "));
                    w.opis = Console.ReadLine();
                    this.Contents.Add(new StaticLine("Place: "));
                    w.miejsce = Console.ReadLine();
                    this.Contents.Add(new StaticLine("Day: "));
                    w.dzien = DateTime.Parse(Console.ReadLine());
                    this.Contents.Add(new StaticLine("Hour: "));
                    w.godzina = TimeSpan.Parse(Console.ReadLine());
                    this.Contents.Add(new StaticLine("Budget: "));
                    w.budzet = Int32.Parse(Console.ReadLine());
                    DataAccess.Wydarzenie.Insert(w);
                    Console.WriteLine("DONE");
                    break;
                case 2:
                    DisplayAdapter.Display(new OtherPage());
                    break;
                case 3:
                    Console.WriteLine("Temporary line. Write message. ");
                    string x = Console.ReadLine();
                    this.Contents.Add(new StaticLine("Done. Message: " + x));
                    DisplayAdapter.Refresh(DisplayAdapter.CurrentPage);
                    break;

            }
        }
        
        
    }
}
