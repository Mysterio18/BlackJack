using System;
using System.Collections.Generic;
using BlackJack.Enums;

namespace BlackJack
{
    public class LogicWith36Cards : ILogic
    {

        public override void GenerateStartingSetsOfCards()
        {
            LenghtOfDeck = NumOfCards.Short36;

            MinValueOfCard = 6;

            MaxValueOfCard = 11;

            for (int i = 0; i < 2; i++)
            {
                SetOfPlayer.Add(GenerateCard());
                SetOfComputer.Add(GenerateCard());
            }

        }

        protected override Rank CheckLowerLimit(int ValueOfCard)
        {
            return Rank.None;
        }

        protected override Rank CheckUpperLimit(int ValueOfCard)
        {
            return Rank.None;
        }
    }
}
