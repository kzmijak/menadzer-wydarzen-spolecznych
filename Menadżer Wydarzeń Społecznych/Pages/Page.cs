using System;
using System.Collections.Generic;
using System.Text;
using MWS.Lines;

namespace MWS.Pages
{
    abstract class Page
    {
        public List<Line> Contents { get; set; }
        public Page(StaticLine note = null)
        {
            Contents = new List<Line>(31);
            Line.LastIndex = 0;
        }
        public virtual void React(Line line) {}
    }
}
