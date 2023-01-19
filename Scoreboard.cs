using System;
using System.Collections.Generic;
using System.Numerics;
using Codecool.CaptureTheFlag.Actors;

namespace Codecool.CaptureTheFlag
{
    /// <summary>
    ///     Static class with extension methods for reports about players' performance during the game
    /// </summary>
    public static class Scoreboard
    {
        /// <summary>
        ///     Returns a collection of all players, sorted by their score (from highest to lowest)
        /// </summary>
        /// <param name="players"></param>
        /// <returns></returns>
        public static IEnumerable<Player> GetRankedPlayers(this IEnumerable<Player> players)
        {
            return players.OrderByDescending(player => player.CurrentScore);
        }


        /// <summary>
        ///     Returns a collection of all players, from given team, sorted by their score (from highest to lowest)
        /// </summary>
        /// <param name="players"></param>
        /// <param name="team"></param>
        /// <returns></returns>
        public static IEnumerable<Player> GetRankedPlayersInTeam(this IEnumerable<Player> players, PlayerTeam team)
        {
            return players.Where(player => player.Team == team).OrderByDescending(player => player.CurrentScore);
        }


        /// <summary>
        ///     Returns the team that has the greatest amount of points scored by its players
        /// </summary>
        /// <param name="players"></param>
        /// <returns></returns>
        public static PlayerTeam GetWinningTeam(this IEnumerable<Player> players)
        {
            var teamScores = players.GroupBy(player => player.Team)
                                    .Select(group => new { Team = group.Key, TotalScore = group.Sum(player => player.CurrentScore) })
                                    .OrderByDescending(team => team.TotalScore);

            return teamScores.First().Team;
        }


        /// <summary>
        ///     Returns amount of dead players
        /// </summary>
        /// <param name="players"></param>
        /// <returns></returns>
        public static int GetDeadPlayersAmount(this IEnumerable<Player> players)
        {
            return players.Count(player => !player.Alive);
        }


        /// <summary>
        ///     Returns full scoreboard string for current game (see the example)
        /// </summary>
        /// <param name="players"></param>
        /// <returns></returns>
        public static string GetScoreboard(this IEnumerable<Player> players)
        {
            // Example output:
            // Format: $"Team {player.Team} {player.Name} Points: {player.CurrentScore}";
            // Team Rock Adam Points: 20
            // Team Paper Eve Points: 10
            // Team Scissors Abel Points: 5 DEAD

            var scoreboard = "";

            // Sort the players by team with the highest score first and then each player in that team by highest score
            var sortedPlayers = players.GetRankedPlayers().GroupBy(player => player.Team)
                                                       .Select(group => group.OrderByDescending(player => player.CurrentScore))
                                                       .SelectMany(group => group);



            foreach (var player in players)
            {
                scoreboard += $"Team {player.Team} {player.Name} Points: {player.CurrentScore}";

                if (!player.Alive)
                {
                    scoreboard += " DEAD";
                }

                scoreboard += Environment.NewLine;
            }

            return scoreboard;
        }
    }
}
