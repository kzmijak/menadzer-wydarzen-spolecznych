using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.Lines
{
    abstract class _Line
    {
        public int Index { get; set; }
        public string Content { get; set; }
        public ConsoleColor Color { get; set; }
        public ConsoleColor Background { get; set; }


        public static int LastIndex { get; set; }
        
        public string Text()
        {
            return this.Content;
        }
        
    }
}
