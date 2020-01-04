using System;
using System.Collections.Generic;
using System.Text;
using MWS.Lines;

namespace MWS.Pages
{
    abstract class _Page
    {
        public List<_Line> Contents { get; set; }
        public StaticLine Note { get; set; } = new StaticLine("");
        public _Page(StaticLine Note = null)
        {
            if(Note != null)
                this.Note = Note;

            Contents = new List<_Line>(63);
            _Line.LastIndex = 0;
        }
        public virtual void React(_Line line) { }
    }
}
