using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Enums;

namespace BlackJack
{
    public abstract class ILogic
    {
        protected static Random Generator = new Random();

        protected NumOfCards LenghtOfDeck;
        protected int MinValueOfCard;
        protected int MaxValueOfCard;

        protected bool IsComputerGoing = false;

        protected List<Card> SetOfPlayer = new List<Card>();
        protected List<Card> SetOfComputer = new List<Card>();

        protected string GenerateSuite()
        {
            Suite RandomSuite = (Suite)Generator.Next(1, 4);

            if (RandomSuite == Suite.Club)
            {
                return "♣";
            }
            if (RandomSuite == Suite.Diamond)
            {
                return "♦";
            }
            if (RandomSuite == Suite.Heart)
            {
                return "♥";
            }

            return "♠";
        }

        protected Rank SpotRank(int ValueOfCard)
        {
            Rank RankOfCard = Rank.None;

            RankOfCard = CheckLowerLimit(ValueOfCard);

            if (RankOfCard == Rank.None)
                RankOfCard = CheckAverageLimit(ValueOfCard);

            if (RankOfCard == Rank.None)
                RankOfCard = CheckUpperLimit(ValueOfCard);

            return RankOfCard;

        }

        protected Rank CheckAverageLimit(int ValueOfCard)
        {

            if (ValueOfCard == 6)
            {
                return Rank.Six;
            }
            if (ValueOfCard == 7)
            {
                return Rank.Seven;
            }
            if (ValueOfCard == 8)
            {
                return Rank.Eight;
            }
            if (ValueOfCard == 9)
            {
                return Rank.Nine;
            }
            if (ValueOfCard == 10)
            {
                return (Rank)Generator.Next(10, 13);
            }
            if (ValueOfCard == 11)
            {
                return Rank.Ace;
            }


            return Rank.None;

        }

        protected Card GenerateCard()
        {
            string SuiteOfCard = GenerateSuite();

            int ValueOfCard = Generator.Next(MinValueOfCard, MaxValueOfCard);

            Rank RankOfCard = SpotRank(ValueOfCard);

            Card NewCard = new Card();

            NewCard.Suite = SuiteOfCard;

            NewCard.Value = ValueOfCard;

            NewCard.Rank = RankOfCard;

            return NewCard;
        }

        public void GetCardToPlayerSet()
        {
            SetOfPlayer.Add(GenerateCard());
        }

        public void GetCardToComputerSet()
        {
            SetOfComputer.Add(GenerateCard());
        }

        public List<Card> GetCardsOfPlayer()
        {
            return SetOfPlayer;
        }

        public List<Card> GetCardsOfComputer()
        {
            return SetOfComputer;
        }

      

        public bool CheckValidSum( List<Card> SetOfCards )
        {
            int Sum = CalculateSum(SetOfCards);

            if( Sum < 21 )
            {
                return true;
            }

            return false;
        }

        public int CalculateSum( List<Card> SetOfCards )
        {
            int Sum = 0;

            foreach (var Card in SetOfCards)
            {
                Sum += Card.Value;
            }

            return Sum;
        }

        public void ClearSets()
        {
            GetCardsOfComputer().Clear();
            GetCardsOfPlayer().Clear();
            GenerateStartingSetsOfCards();
        }

      
        public bool ComputerTakingCard()
        {
            int ComputerSum = CalculateSum(GetCardsOfComputer());
            if (ComputerSum > 15)
            {
                return false;
            }
            GetCardToComputerSet();
            return true;
        }

        public bool IsPlayerWinner()
        {
            int ComputerSum = CalculateSum(GetCardsOfComputer());
            int PlayerSum = CalculateSum(GetCardsOfPlayer());
            if(ComputerSum > PlayerSum)
            {
                return false;  
            }
            return true;
        }

        protected abstract Rank CheckLowerLimit(int ValueOfCard);

        protected abstract Rank CheckUpperLimit(int ValueOfCard);

        public abstract void GenerateStartingSetsOfCards();

    }

}
