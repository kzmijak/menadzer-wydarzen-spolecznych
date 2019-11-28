using System;
using System.Collections.Generic;
using System.Text;
using Menadżer_Wydarzeń_Społecznych.Lines;

namespace Menadżer_Wydarzeń_Społecznych.Pages
{
    interface Page
    {
        List<Line> Contents { get; set; }
        void React(Line line);
    }
}
