using System;
using System.Collections.Generic;
using BlackJack.Enums;

namespace BlackJack
{
    public class LogicWith52Cards : ILogic
    {
        public override void GenerateStartingSetsOfCards()
        {
            for (int i = 0; i < 2; i++)
            {
                SetOfComputer.Add(GenerateCard());
                SetOfPlayer.Add(GenerateCard());
            }

        }

        sealed protected override Rank CheckLowerLimit(int ValueOfCard)
        {
            if (ValueOfCard == 2)
            {
                return Rank.Two;
            }
            if (ValueOfCard == 3)
            {
                return Rank.Three;
            }
            if (ValueOfCard == 4)
            {
                return Rank.Four;
            }
            if (ValueOfCard == 5)
            {
                return Rank.Five;
            }

            return Rank.None;
        }

        sealed protected override Rank CheckUpperLimit(int ValueOfCard)
        {
            return Rank.None;
        }

        public override void ConfigDeck()
        {
            LenghtOfDeck = NumOfCards.Full52;

            MinValueOfCard = 2;

            MaxValueOfCard = 11;
        }
    }
}
