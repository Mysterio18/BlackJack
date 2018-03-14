using System;
namespace BlackJack
{
    public class Game
    {

        protected IUserInterface UI;
        protected ILogic Logic;

        public void Start(ILogic TypeOfLogic, IUserInterface TypeOfUI)
        {
            SetStartingSettings(TypeOfLogic, TypeOfUI);

            Logic.GenerateStartingSetsOfCards();

            UI.BeginInteractionWithLogic( Logic );
        }

        private void SetStartingSettings(ILogic TypeOfLogic, IUserInterface TypeOfUI)
        {
            Logic = TypeOfLogic;
            UI = TypeOfUI;
        }


    }
}