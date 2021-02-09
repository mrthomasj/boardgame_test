using BankruptTest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankruptTest.Services
{
    class MetricsService
    {
        List<Counter> gameCounter = new List<Counter>();

        public MetricsService(List<Counter> counter)
        {
            this.gameCounter = counter;
        }

        public int TimeOutGames()
        {
            int TimeOuts = 0;
            foreach(Counter game in gameCounter)
            {
                if (game.TimeoutWin)
                {
                    TimeOuts++;
                }
            }
            return TimeOuts;
        }

        public double TurnAverage()
        {
            
            List<int> TurnsCounterList = new List<int>();
            foreach(Counter game in gameCounter)
            {
                TurnsCounterList.Add(game.TurnsToWin);
            }
            string turnAverage = TurnsCounterList.Average().ToString();
            double result = Math.Round(double.Parse(turnAverage), 0);
            return result;
        }

        public double PlayerWinPercentage(BasePlayer player, int numberOfGames)
        {
            int wins = 0;
            foreach(Counter game in gameCounter)
            {
                if (game.Winner.Id == player.Id)
                    wins++;
            }
            double result = double.Parse(wins.ToString()) / double.Parse(numberOfGames.ToString());
            return result;
        }

        public List<WinPercentage> WinPercentageAllPlayers(List<BasePlayer> players)
        {
            List<WinPercentage> winPercentage = new List<WinPercentage>();

            foreach(BasePlayer player in players)
            {
                double playerWinPercentage = PlayerWinPercentage(player, gameCounter.Count);
                winPercentage.Add(new WinPercentage(){Player = player,  WPercentage = playerWinPercentage});
            }

            return winPercentage;

        }

        public BasePlayer MostWinningPlayer(List<WinPercentage> winList)
        {
            WinPercentage mostWinning = winList.Find(p => p.WPercentage == winList.Max(p => p.WPercentage));
            return mostWinning.Player;
        }
    }
}
