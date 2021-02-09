using BankruptTest.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankruptTest.Services
{
    class GameService
    {
        List<Propriedade> board;
        List<BasePlayer> playerList;
        DiceRollService dice;
        List<Counter> gameCounter;

        

        public GameService(List<Propriedade> board, List<BasePlayer> players, List<Counter> counter)
        {
            this.board = board;
            this.playerList = players;
            this.gameCounter = counter;
            dice = new DiceRollService();
        }

        public void GameFlow()
        {
            bool hasWinner = false;
            Counter thisGame = new Counter();
            if (gameCounter.Count == 0)
            {
                thisGame.GameId = 1;
            }
            else
            {
                thisGame.GameId = gameCounter.Count + 1;
            }
            //Game turns
            for (int i = 1; i <= 1000; i++)
            {
                
                
                foreach (BasePlayer player in playerList)
                {
                    if (!player.Bankrupt)
                    {
                        Console.WriteLine($"É a vez de: {player.Id}");
                        player.Active = true;
                        int spotsToMove = dice.Roll();
                        player.Move(spotsToMove);
                        CheckProperty(player);
                        player.CheckBehavior(board[player.CurrentPlaceOnBoard - 1]);
                        player.CheckBankrupt();
                    }
                    if((playerList[0].Bankrupt && playerList[1].Bankrupt  && playerList[2].Bankrupt) ||
                        (playerList[1].Bankrupt && playerList[2].Bankrupt && playerList[3].Bankrupt) ||
                        (playerList[0].Bankrupt && playerList[1].Bankrupt && playerList[3].Bankrupt) ||
                        (playerList[0].Bankrupt && playerList[2].Bankrupt && playerList[3].Bankrupt))
                    {
                        thisGame.TurnsToWin = i;
                        thisGame.TimeoutWin = false;
                        BasePlayer winner = playerList.Find(p => p.Bankrupt == false);
                        thisGame.Winner = winner;
                        Console.WriteLine($"=#=#=#=#=#=#=#=#=#=#=#=# The winner is: Player {winner.Id}!! #=#=#=#=#=#=#=#=#=#=#=#=");
                        hasWinner = true;
                        gameCounter.Add(thisGame);
                        break;
                    }
                }
                if (hasWinner) break;
            }

            if (!hasWinner)
            {
                BasePlayer winner = playerList[0];
                int winningCredits = playerList[0].CurrentCredit;
                foreach(BasePlayer p in playerList)
                {
                    if(p.CurrentCredit > winningCredits)
                    {
                        winningCredits = p.CurrentCredit;
                        winner = p;
                    }
                    else if(p.CurrentCredit == winningCredits)
                    {
                        if(p.Id > winner.Id)
                        {
                            winner = p;
                        }
                    }
                }
                thisGame.TimeoutWin = true;
                thisGame.TurnsToWin = 10;
                thisGame.Winner = winner;
                Console.WriteLine($"=#=#=#=#=#=#=#=#=#=#=#=# The winner is: Player {winner.Id}!!  #=#=#=#=#=#=#=#=#=#=#=#=");
                gameCounter.Add(thisGame);

            }
        }

        public void CheckProperty(BasePlayer activePlayer)
        {
            Propriedade prop = board[activePlayer.CurrentPlaceOnBoard - 1];
            BasePlayer playerToPay;
            BasePlayer playerPaying = activePlayer;
            if(prop.CurrentOwnerId != 0)
            {
                playerToPay = playerList.Find(p => p.Id == prop.CurrentOwnerId);
                playerPaying.PayRent(prop, playerToPay);
                
            }
        }
    }
}
