using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PublicOpinion.DataModel
{
    public class PositionPomWord
    {
        public string TextContent;
        public int Start;
        public int End;
        public int Length;

        public PositionPomWord(string textContent, int start, int end, int length)
        {
            this.TextContent = textContent;
            this.Start = start;
            this.End = end;
            this.Length = length;
        }
    }
}
