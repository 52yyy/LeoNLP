using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PublicOpinion.DataModel
{
    public class PositionPomPair
    {
        public int PairId { get; set; }

        public PositionPomWord Target { get; set; }

        public PositionPomWord Feature { get; set; }

        public PositionPomWord Modify { get; set; }

        public PositionPomWord Opinion { get; set; }

        public bool Invert { get; set; }

        public string Orient { get; set; }
    }
}
