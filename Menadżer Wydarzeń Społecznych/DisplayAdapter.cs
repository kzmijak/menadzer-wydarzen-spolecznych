using System;
using System.Collections.Generic;
using System.Text;
using MWS.Pages;
using MWS.Lines;

namespace MWS
{
    class DisplayAdapter
    {
        public static _Page CurrentPage = null;
        public static _Line CurrentLine = null;

        static void Main(string[] args)
        {
            Display(new Login());   
        }

        public static void FindNextValidLine(_Line currentline, int direction)
        {
            int lineChecked = currentline.Index;
            do
            {
                if (lineChecked + direction < 0)
                {
                    if (CurrentPage.Contents[CurrentPage.Contents.Count - 1] is ActiveLine)
                    {
                        CurrentLine = CurrentPage.Contents[CurrentPage.Contents.Count - 1];
                        (CurrentLine as ActiveLine).Toggle();                       
                        Refresh(CurrentPage);
                        break;
                    }
                    else
                        lineChecked = CurrentPage.Contents.Count;
                }
                else if (lineChecked + direction == CurrentPage.Contents.Count)
                {
                    if (CurrentPage.Contents[0] is ActiveLine)
                    {
                        CurrentLine = CurrentPage.Contents[0];
                        (CurrentLine as ActiveLine).Toggle();
                        Refresh(CurrentPage);
                        break;
                    }
                    else
                        lineChecked = -1;
                }
                else if (CurrentPage.Contents[lineChecked + direction] is ActiveLine)
                {
                    CurrentLine = CurrentPage.Contents[lineChecked + direction];
                    (CurrentLine as ActiveLine).Toggle();
                    Refresh(CurrentPage);
                    break;
                }
                lineChecked += direction;
            }
            while (true);
        }

        public static void Display(_Page Page, _Line cl = null)
        {
            
            CurrentPage = Page;
            CurrentLine = cl;
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
                        (CurrentLine as ActiveLine).Toggle();
                        FindNextValidLine(CurrentLine, -1);
                        break;

                    case ConsoleKey.DownArrow:
                        (CurrentLine as ActiveLine).Toggle();
                        FindNextValidLine(CurrentLine, 1);
                        break;
                }
            }
        }

        public static void Refresh(_Page Page)
        {
            Console.Clear();
            CurrentPage = Page;
            foreach(_Line Line in Page.Contents)
            {
                if (Line is ActiveLine && CurrentLine is null)
                {
                    (Line as ActiveLine).Toggle();
                    CurrentLine = Line;
                    
                }

                if (CurrentLine != null && Page.Contents[Page.Contents.Count - 1].Content != (CurrentLine as ActiveLine).Description && CurrentPage.Note.Content == "")
                {
                    Page.Contents[Page.Contents.Count - 1] = new StaticLine((CurrentLine as ActiveLine).Description, ConsoleColor.Blue);
                    Refresh(Page);
                    break;
                }

                if (Line != null)
                {
                    Console.BackgroundColor = Line.Background;
                    Console.ForegroundColor = Line.Color;
                    Console.WriteLine(Line.Text());

                }
                Console.ResetColor();
            }
            
        }

        public static void Trigger()
        {
            CurrentPage.React(CurrentLine);
        }
    }
}
