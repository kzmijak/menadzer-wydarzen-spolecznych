using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.Lines
{
    class ActiveLine: _Line
    {
        public bool Selected { get; set; }
        public string Description { get; set; }

        public ActiveLine(string Content, string Description = "", ConsoleColor color = ConsoleColor.White)
        {
            this.Content = Content;
            this.Description = Description;
            this.Index = LastIndex;
            this.Selected = false;
            this.Color = color;
            this.Background = ConsoleColor.Black;
            _Line.LastIndex++;
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
