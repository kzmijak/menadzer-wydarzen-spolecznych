using System;
using System.Collections.Generic;
using System.Text;

namespace Menadżer_Wydarzeń_Społecznych.Lines
{
    class ActiveLine: Line
    {
        public bool Selected { get; set; }

        public ActiveLine(string Content)
        {
            this.Content = Content;
            this.Index = LastIndex;
            this.Selected = false;
            this.Color = ConsoleColor.White;
            this.Background = ConsoleColor.Black;
            Line.LastIndex++;
        }

        public void Toggle()
        {
            if(this.Selected)
            {
                this.Selected = false;
                this.Color = ConsoleColor.White;
                this.Background = ConsoleColor.Black;
            }
            else
            {
                this.Selected = true;
                this.Color = ConsoleColor.Black;
                this.Background = ConsoleColor.White;
            }
        }
    }
}
