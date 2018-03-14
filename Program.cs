
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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