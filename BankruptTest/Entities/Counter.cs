using System;
using System.Collections.Generic;
using System.Text;

namespace BankruptTest.Entities
{
    class Counter
    {
        public int GameId { get; set; }
        public int TurnsToWin { get; set; }
        public bool TimeoutWin { get; set; }
        public BasePlayer Winner { get; set; }
    }
}
