using System;
using System.Collections.Generic;
using System.Text;
using Menadżer_Wydarzeń_Społecznych.Lines;

namespace Menadżer_Wydarzeń_Społecznych.Pages
{
    class OtherPage : Page
    {
        public List<Line> Contents { get; set; }

        public OtherPage()
        {
            this.Contents = new List<Line>(10);
            Line.LastIndex = 0;

            this.Contents.Add(new StaticLine("THIS IS SOME OTHER PAGE"));
            this.Contents.Add(new ActiveLine("Hi"));
            this.Contents.Add(new ActiveLine("Return"));
        }

        public void React(Line line)
        {
            switch(line.Index)
            {
                case 1:
                    Console.WriteLine("Yes hello");
                    break;
                case 2:
                    DisplayAdapter.Display(new Login());
                    break;
            }
        }
    }
}
