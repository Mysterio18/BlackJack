using System;
using System.Collections.Generic;

namespace BlackJack
{
    public abstract class IUserInterface
    {
        
        public abstract void BeginInteractionWithLogic(ILogic Logic);

        protected abstract void ShowGreeting();

        protected abstract void ShowPlayerTookInf(Card Card);

        protected abstract void ShowComputerTookInf();

        protected abstract void ShowComputerOpenInf(ILogic Logic);

        protected abstract void ShowPlayerOpenInf(ILogic Logic);

        protected abstract void ShowParting();

        protected abstract void ShowStartingMenu();

        protected abstract void ShowPlayingMenu();

        protected abstract void ShowPlayerWin();

        protected abstract void ShowComputerWin();

        protected abstract void ClearScreen();

        protected abstract void ShowSetOfCards(List<Card> set);

    }
}
