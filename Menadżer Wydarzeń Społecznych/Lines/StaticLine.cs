using System;
using System.Collections.Generic;
using System.Text;

namespace Menadżer_Wydarzeń_Społecznych.Lines
{
    class StaticLine: Line
    {
        public StaticLine(string Content)
        {
            this.Content = Content;
            this.Index = LastIndex;
            this.Color = ConsoleColor.Yellow;
            this.Background = ConsoleColor.Black;
            Line.LastIndex++;
        }
    }
}
