using BankruptTest.Config;
using BankruptTest.Entities;
using BankruptTest.Extensions;
using BankruptTest.Services;
using System;
using System.Collections.Generic;

namespace BankruptTest
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Counter> gameCounter = new List<Counter>();

            for (int i = 0; i < 300; i++)
            {
                BoardConfig board = new BoardConfig();
                PlayerConfig players = new PlayerConfig(
                    new BasePlayer(1, BehaviorList.Impulsive), new BasePlayer(2, BehaviorList.Demanding), new BasePlayer(3, BehaviorList.Impulsive), new BasePlayer(4, BehaviorList.Unpredictable)
                    );
                board.LoadBoard();
                if(board.boardProperties.Count == 0)
                {
                    Console.WriteLine(@"Não é possível executar o teste pois o arquivo de configuração não está presente.");
                    return;
                }
                Console.WriteLine("=#=#=#=#=#=#=#=#=#=#=#=#  Game Start #=#=#=#=#=#=#=#=#=#=#=#=");
                players.PlayerList.Shuffle();
                foreach (var p in players.PlayerList)
                {
                    Console.WriteLine(p.ToString());
                }
                GameService game = new GameService(board.boardProperties, players.PlayerList, gameCounter);
                game.GameFlow();
            }

            MetricsService metrics = new MetricsService(gameCounter);
            PlayerConfig playerList = new PlayerConfig(
                    new BasePlayer(1, BehaviorList.Impulsive), new BasePlayer(2, BehaviorList.Demanding), new BasePlayer(3, BehaviorList.Impulsive), new BasePlayer(4, BehaviorList.Unpredictable)
                    );

            var TimeOuts = metrics.TimeOutGames();
            Console.WriteLine($"O número de partidas finalizadas por limite de turnos é de: {TimeOuts}.");
            var TurnsToWin = metrics.TurnAverage();
            Console.WriteLine($"A média de turnos que uma partida demora para ser concluída é de: {TurnsToWin}");
            List<WinPercentage> WinPercentageByPlayer = metrics.WinPercentageAllPlayers(playerList.PlayerList);
            foreach(WinPercentage register in WinPercentageByPlayer)
            {
                Console.WriteLine($"O Jogador {register.Player.Id} venceu {Math.Round(register.WPercentage * 100,2)}% das vezes.");
            }
            BasePlayer mostWinner = metrics.MostWinningPlayer(WinPercentageByPlayer);
            Console.WriteLine($"O Jogador com mais vitórias foi o Jogador { mostWinner.Id } com o comportamento {mostWinner.Behavior}.");
        }
    }
}
