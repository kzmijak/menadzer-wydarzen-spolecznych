using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.Lines
{
    class StaticLine: _Line
    {
        public StaticLine(string Content, ConsoleColor color = ConsoleColor.Yellow)
        {
            this.Content = Content;
            this.Index = LastIndex;
            this.Color = color;
            this.Background = ConsoleColor.Black;
            _Line.LastIndex++;
        }
    }
}
