using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Enums;

namespace BlackJack
{
    public class Card
    {
        public string Suite { set; get; }
        public Rank Rank{ set; get; }
        public int Value { set; get; }


    }
}
