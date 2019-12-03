using System;
using System.Collections.Generic;
using System.Text;
using MWS.Lines;

namespace MWS.Pages
{
    interface Page
    {
        List<Line> Contents { get; set; }
        void React(Line line);
    }
}
