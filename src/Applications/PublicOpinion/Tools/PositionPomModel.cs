using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PublicOpinion.DataModel
{
    public class PositionPomModel
    {
        public int SentenceId { get; set; }

        public string Content { get; set; }

        public List<PositionPomPair> Pairs { get; set; } 
    }
}
