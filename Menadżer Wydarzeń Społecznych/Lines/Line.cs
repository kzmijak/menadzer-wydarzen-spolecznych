using System;
using System.Collections.Generic;
using System.Text;

namespace Menadżer_Wydarzeń_Społecznych.Lines
{
    abstract class Line
    {
        public int Index { get; set; }
        protected string Content { get; set; }
        public ConsoleColor Color { get; set; }
        public ConsoleColor Background { get; set; }


        public static int LastIndex { get; set; }



        public string Text()
        {
            return this.Content;
        }
        
    }
}
