using System;
using System.Linq;

namespace Codecool.CaptureTheFlag
{
    /// <summary>
    ///     Game simulation class
    /// </summary>
    public class Game
    {
        /// <summary>
        ///     Reference to game's map
        /// </summary>
        public GameMap Map { get; }

        /// <summary>
        ///     Returns the amount of iterations (cycles) the simulation has made
        /// </summary>
        public int Iterations { get; private set; }

        /// <summary>
        ///     Returns true if the game simulation should still be ongoing
        /// </summary>
        public bool OngoingGame => throw new NotImplementedException();

        public Game(GameMap map)
        {
            Map = map;
        }

        /// <summary>
        ///     Invokes OnGameCycle on all players (DO NOT MODIFY)
        /// </summary>
        public void CyclePlayers()
        {
            foreach (var player in Map.Players)
            {
                if (Map.Flags.All(f => f.Captured))
                    break;

                player.OnGameCycle();
            }
        }

        /// <summary>
        ///     Simulates the whole game, giving output to the Console (DO NOT MODIFY)
        /// </summary>
        public void SimulateGame()
        {
            Iterations = 0;

            while (OngoingGame)
            {
                Iterations++;

                Console.Clear();

                Console.WriteLine(Map.ToString());
                Console.WriteLine();
                Console.WriteLine(Map.Players.GetScoreboard());

                Console.ReadLine();

                CyclePlayers();
            }

            Console.Clear();

            Console.WriteLine(Map.ToString());
            Console.WriteLine();
            Console.WriteLine(Map.Players.GetScoreboard());
            Console.WriteLine();
            Console.WriteLine($"Game Over [{Iterations} iterations]");
            Console.WriteLine($"Team {Map.Players.GetWinningTeam()} has won");

            Console.ReadLine();
        }

        /// <summary>
        ///     Simulates the whole game without output to the Console (used for testing) (DO NOT MODIFY)
        /// </summary>
        public void SimulateGameNoOutput()
        {
            Iterations = 0;

            while (OngoingGame)
            {
                Iterations++;

                CyclePlayers();
            }
        }
    }
}