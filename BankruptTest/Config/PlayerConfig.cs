using BankruptTest.Entities;
using BankruptTest.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankruptTest.Config
{
    class PlayerConfig
    {
        public List<BasePlayer> PlayerList;

        public PlayerConfig(BasePlayer player1, BasePlayer player2, BasePlayer player3, BasePlayer player4)
        {
            PlayerList = new List<BasePlayer>();

            PlayerList.Add(player1);
            PlayerList.Add(player2);
            PlayerList.Add(player3);
            PlayerList.Add(player4);
        }
    }
}
