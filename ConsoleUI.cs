using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public class ConsoleUI: IUserInterface
    {
        class Button
        {
            public string Text { get; set; }
            public Action OnSelect { get; set; }
        }
        
        class MenuState
        {
            private static readonly Stack<MenuState> States = new Stack<MenuState>();
            
            public string Name { get; set; }
            public List<Button> Buttons { get; set; }
            
            public static MenuState GetCurrent()
            {
                States.TryPop(out var current);
                States.Push(current);
                return current;
            }
            
            public static void SetMenu(MenuState state)
            {
                States.Push(state);
            }
            
            public static void Back()
            {
                States.Pop();
            }
        }
        
        
        public override void BeginInteractionWithLogic(ILogic Logic)
        {
            ShowGreeting();
            WaitKey();
            ClearScreen();

            Logic.GenerateStartingSetsOfCards();

            MenuState StartingMenu = null; MenuState PlayingMenu = null;
            
            StartingMenu = new MenuState
            {
                Name = "------------------------------------------Starting Menu-------------------------",
                Buttons = new List<Button>
                {
                    new Button {Text = "Start the round;", OnSelect = () =>
                        {
                            StartingRoundInteraction( Logic );
                            MenuState.SetMenu(PlayingMenu);
                        }},
                    new Button {Text = "Exit;", OnSelect = () =>
                        {
                            ClearScreen();
                            ShowParting();
                            WaitKey();
                            Environment.Exit(0);
                        }
                    }
                }
            };
            PlayingMenu = new MenuState
            {
                Name = "\n------------------------------------------Playing Menu--------------------------",
                Buttons = new List<Button>
                {
                    new Button {Text = "Open cards;", OnSelect = () =>
                        {
                            ShowPlayerOpenInf( Logic );
                            WaitKey();
                            ShowComputerOpenInf(Logic);
                            WaitKey();
                            OpeningCardsInteraction( Logic );
                        }
                    },
                    new Button {Text = "Get card;", OnSelect = () => GettingCardInteraction( Logic ) },
                    new Button {Text = "Leave the game;", OnSelect = () =>
                        {
                            ClearScreen();
                            MenuState.Back();
                        }
                    }
                }
            };
            
            MenuState.SetMenu(StartingMenu);
            
            byte choice = 0;
            
            while (true)
            {
                MenuState currentMenu = MenuState.GetCurrent();
                DrawMenu(currentMenu);
                choice = ReadAndSaveCode();
                if( choice <= currentMenu.Buttons.Count() )
                currentMenu.Buttons[choice - 1].OnSelect();
                
            }
        }
        //-----------------------------------------------------------------------------Methods of UI Interaction with a Logic-----------------
        private void StartingRoundInteraction( ILogic Logic )
        {
            ClearScreen();
            ShowSetOfCards(Logic.GetCardsOfPlayer());
        }
        
        private void GettingCardInteraction( ILogic Logic )
        {
            Logic.GetCardToPlayerSet();
            
            ShowPlayerTookInf( Logic.GetCardsOfPlayer().Last() );
            WaitKey();
            
            if (!Logic.CheckValidSum(Logic.GetCardsOfPlayer()))
            {
                ShowComputerWin();
                WaitKey();
                Logic.ClearSets();
                ClearScreen();
                MenuState.Back();
                return;
            }
            if (!Logic.CheckValidSum(Logic.GetCardsOfComputer()))
            {
                ShowPlayerWin();
                WaitKey();
                Logic.ClearSets();
                ClearScreen();
                MenuState.Back();
                return;
            }
            
            if (Logic.ComputerTakingCard())
            {
                ShowComputerTookInf();
                WaitKey();
            }
            else
            {
                ShowComputerOpenInf(Logic);
                WaitKey();
                OpeningCardsInteraction(Logic);
            }
            
            ClearScreen();
            ShowSetOfCards(Logic.GetCardsOfPlayer());
        }
        
        private void OpeningCardsInteraction( ILogic Logic )
        {
            if (Logic.IsPlayerWinner())
            {
                ShowPlayerWin();
            }
            else
            ShowComputerWin();
            
            WaitKey();
            ClearScreen();
            Logic.ClearSets();
            MenuState.Back();
        }
        //------------------------------------------------------------------------------------Drawing Methods of UI----------------------------------
        private void DrawMenu( MenuState State )
        {
            Console.WriteLine(State.Name);
            Console.WriteLine();
            Console.WriteLine($"Press [1-{State.Buttons.Count}] to select:\n");
            for (int i = 0; i < State.Buttons.Count; i++)
            {
                Console.Write($"[{i + 1}]: ");
                Console.WriteLine(State.Buttons[i].Text, ConsoleColor.White);
            }
            Console.Write(":");
        }
        
        protected override void ShowGreeting()
        {
            Console.WriteLine("Hello! Welcome to *BlackJack* game!\nPress Enter to continue!");
        }
        
        protected override void ShowComputerTookInf()
        {
            Console.WriteLine("Computer took the card!\nPress Enter to continue!");
        }
        
        protected override void ShowPlayerTookInf( Card Card )
        {
            Console.WriteLine($"You took the card. It is {Card.Rank} {Card.Suite}! \nPress Enter to continue.");
        }
        
        protected override void ShowComputerOpenInf( ILogic Logic )
        {
            Console.Write($"Computer is opening the cards!\n");
            ShowSetOfCards(Logic.GetCardsOfComputer());
            Console.WriteLine("Press Enter to continue.");
        }
        
        protected override void ShowPlayerOpenInf( ILogic Logic )
        {
            Console.Write($"Player is opening the cards!\n");
            ShowSetOfCards(Logic.GetCardsOfPlayer());
            Console.WriteLine("Press Enter to continue.");
        }
        protected override void ShowParting()
        {
            Console.WriteLine("Goodbye!\nPress Enter to exit!");
            
        }
        
        protected override void ShowStartingMenu()
        {
            
            Console.Write("1.Start the round;\n2.Exit;\n:");
            
        }
        
        protected override void ShowPlayingMenu()
        {
            Console.Write("\n--------------------------------------------------------------------------------\n1.Open cards;\n2.Get card;\n3.Leave the game;\n:");
        }
        
        protected override void ShowPlayerWin()
        {
            
            Console.WriteLine("Player is winner!\nPress Enter to continue!");
            
        }
        
        protected override void ShowComputerWin()
        {
            Console.WriteLine("Computer is winner!\nPress Enter to continue!");
        }
        
        protected override void ClearScreen()
        {
            Console.Clear();
        }
        
        private byte ReadAndSaveCode()
        {
            return Byte.Parse( Console.ReadLine() );
        }
        
        private void WaitKey()
        {
            Console.ReadKey();
        }
        
        
        protected override void ShowSetOfCards(List<Card> set)
        {
            Console.Write("The deck is:" + " || ");
            foreach (var item in set)
            {
                Console.Write(item.Rank + " " + item.Suite + " ");
                
            }
            Console.Write($"|| \n");
            
        }
        
        
    }
    
}
