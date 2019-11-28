using System;
using System.Collections.Generic;
using System.Text;
using Menadżer_Wydarzeń_Społecznych.Lines;
using Menadżer_Wydarzeń_Społecznych.Pages;

namespace Menadżer_Wydarzeń_Społecznych.Pages
{
    class Login : Page
    {
        public List<Line> Contents { get; set; }
        
        public Login()
        {
            this.Contents = new List<Line>(10);
            Line.LastIndex = 0;

            this.Contents.Add(new StaticLine("THIS IS THE INITIAL LOGIN PAGE"));
            this.Contents.Add(new ActiveLine("Test message"));
            this.Contents.Add(new ActiveLine("Open different page"));
            this.Contents.Add(new ActiveLine("Test user input"));
        }
        
        public void React(Line line)
        {
            switch(line.Index)
            {
                case 1:
                    Console.WriteLine("Success");
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
