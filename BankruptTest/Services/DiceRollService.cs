using System;
using System.Collections.Generic;
using System.Text;

namespace BankruptTest.Services
{
    class DiceRollService
    {
        Random rnd = new Random();
        public int Roll()
        {
            return rnd.Next(1, 6);
        }
    }
}
