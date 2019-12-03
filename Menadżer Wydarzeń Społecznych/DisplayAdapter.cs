using System;
using System.Collections.Generic;
using System.Text;
using MWS.Pages;
using MWS.Lines;

namespace MWS
{
    class DisplayAdapter
    {
        public static Page CurrentPage = null;
        public static Line CurrentLine = null;

        static void Main(string[] args)
        {
            Display(new Login());   
        }

        private static void Select(int v)
        {
            try
            {
                if (CurrentPage.Contents[CurrentLine.Index + v] is ActiveLine && CurrentPage.Contents[CurrentLine.Index + v]!=null)
                {
                    (CurrentLine as ActiveLine).Toggle();
                    CurrentLine = CurrentPage.Contents[CurrentLine.Index + v];
                    (CurrentLine as ActiveLine).Toggle();
                    Refresh(CurrentPage);
                }
                else if(CurrentPage.Contents[CurrentLine.Index + 2*v] is ActiveLine && CurrentPage.Contents[CurrentLine.Index + 2*v] != null)
                {
                    (CurrentLine as ActiveLine).Toggle();
                    CurrentLine = CurrentPage.Contents[CurrentLine.Index + 2*v];
                    (CurrentLine as ActiveLine).Toggle();
                    Refresh(CurrentPage);
                }
            }
            catch(ArgumentOutOfRangeException)
            {
                return;
            }
        }

        public static void Display(Page Page)
        {
            CurrentLine = null;
            CurrentPage = null;
            Refresh(Page);
            while (true)
            {
                var ch = Console.ReadKey(false).Key;
                switch (ch)
                {
                    case ConsoleKey.Enter:
                        Trigger();
                        break;

                    case ConsoleKey.UpArrow:
                        Select(-1);
                        break;

                    case ConsoleKey.DownArrow:
                        Select(1);
                        break;
                }
            }
        }

        public static void Refresh(Page Page)
        {
            Console.Clear();
            CurrentPage = Page;
            foreach(Line Line in Page.Contents)
            {
                if (Line is ActiveLine && CurrentLine == null)
                {
                    (Line as ActiveLine).Toggle();
                    CurrentLine = Line;
                }
                Console.BackgroundColor = Line.Background;
                Console.ForegroundColor = Line.Color;
                Console.WriteLine(Line.Text());
                

                Console.ResetColor();
            }
        }

        public static void GetCurrentLine()
        {
            foreach (Line line in CurrentPage.Contents)
            {
                if (line is ActiveLine)
                    if ((line as ActiveLine).Selected)
                    {
                        CurrentLine = line;
                    }
            }
        }

        public static void Trigger()
        {
            CurrentPage.React(CurrentLine);
        }
    }
}
