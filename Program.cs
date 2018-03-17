using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack
{
    public class Program
    {
        static void Main(string[] args)
        {

            Game Session = new Game();

            Session.Start( new LogicWith52Cards(), new ConsoleUI() );

        }

    }

}