using System;

namespace Codecool.CaptureTheFlag.Actors
{
    /// <summary>
    ///     PlayerTeam enum
    /// </summary>
    public enum PlayerTeam
    {
        Rock,
        Paper,
        Scissors
    }

    /// <summary>
    ///     Base class for all Player
    /// </summary>
    public abstract class Player : Actor
    {
        /// <summary>
        ///     Returns true after player was killed
        /// </summary>
        public bool Alive { get; set; }

        /// <summary>
        ///     Returns current amount of flags captured by this player
        /// </summary>
        public int CapturedFlags { get; set; }

        /// <summary>
        ///     Returns current amount of players killed by this player
        /// </summary>
        public int KilledPlayers { get; set; }

        /// <summary>
        ///     Returns this player's name
        /// </summary>
        public string Name { get; }

        protected Player(string name, GameMap mapReference) : base(mapReference)
        {
            Name = name;
            Alive = true;
            CapturedFlags = 0;
            KilledPlayers = 0;
        }

        /// <summary>
        ///     Returns direction for the player's next move, depending on the nearest flag's position
        /// </summary>
        /// <returns></returns>
        public static Direction GetMoveDirection((int x, int y) playerPosition, (int x, int y) flagPosition)
        {
            if (playerPosition.x < flagPosition.x)
            {
                return Direction.Right;
            }
            else if (playerPosition.x > flagPosition.x)
            {
                return Direction.Left;
            }
            else if (playerPosition.y < flagPosition.y)
            {
                return Direction.Down;
            }
            else if (playerPosition.y > flagPosition.y)
            {
                return Direction.Up;
            }
            else
            {
                return Direction.Up;
            }
        }

        /// <summary>
        ///     Returns this player's current score
        /// </summary>
        public int CurrentScore => CapturedFlags * 10 + KilledPlayers * 20;

        /// <summary>
        ///     Returns this player's team
        /// </summary>
        public abstract PlayerTeam Team { get; }

        /// <summary>
        ///     Cycles this player's behavior
        /// </summary>
        public abstract void OnGameCycle();

        /// <summary>
        ///     Fight simulation between this and given player
        /// </summary>
        /// <returns>1 if this player won, 0 if the other player won, -1 if fight didn't happen (otherPlayer is a teammate)</returns>
        public abstract int Fight(Player otherPlayer);
    }
}