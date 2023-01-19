using System;
using System.Collections.Generic;
using System.Numerics;
using Codecool.CaptureTheFlag.Actors;

namespace Codecool.CaptureTheFlag
{
    /// <summary>
    ///     GameMap class
    /// </summary>
    public class GameMap
    {
        /// <summary>
        ///     A 2D matrix of all actor references (null if the field is empty)
        /// </summary>
        public Actor[,] ActorMatrix { get; }

        /// <summary>
        ///     Contains all players present on the map (also dead ones)
        /// </summary>
        public List<Player> Players { get; }

        /// <summary>
        ///     Contains all players present on the map (also captured ones)
        /// </summary>
        public List<Flag> Flags { get; }

        /// <summary>
        ///     Returns a new GameMap instance, constructed from given char matrix
        /// </summary>
        /// <param name="charMatrix"></param>
        public GameMap(string charMatrix)
        {
            var matrixRows = charMatrix.Split("\n");
            var rows = matrixRows.Length;
            var cols = matrixRows[0].Length;
            ActorMatrix = new Actor[rows, cols];
            Players = new List<Player>();
            Flags = new List<Flag>();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    var currentChar = matrixRows[i][j];
                    var actor = ActorFactory.CreateFromChar(currentChar, this);
                    if (actor != null)
                    {
                        ActorMatrix[i, j] = actor;
                        if (actor is Player)
                        {
                            Players.Add(actor as Player);
                        }
                        else if (actor is Flag)
                        {
                            Flags.Add(actor as Flag);
                        }
                    }
                }
            }
        }


        /// <summary>
        ///     Returns a char matrix of map's current state
        /// </summary>Regex.Split("\r\n|\r|\n");
        /// <returns></returns>
        public override string ToString()
        {
            var mapString = "";
            for (int i = 0; i < ActorMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < ActorMatrix.GetLength(1); j++)
                {
                    var currentActor = ActorMatrix[i, j];
                    if (currentActor == null)
                    {
                        mapString += "..";
                    }
                    else
                    {
                        mapString += currentActor.ToString();
                    }
                }
                mapString += "\n";
            }
            return mapString;
        }

        /// <summary>
        ///     Returns an actor instance present on given position
        ///     Should return null if no actor is present
        ///     Should throw an ArgumentException if the position is outside map's boundaries
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Actor GetActor((int x, int y) position)
        {

            if (position.x < 0 || position.x >= ActorMatrix.GetLength(1) || position.y < 0 || position.y >= ActorMatrix.GetLength(0))
            {
                throw new ArgumentException("Position is outside map's boundaries.");
            }

            return ActorMatrix[position.y, position.x];

        }

        /// <summary>
        ///     Returns a position of given actor instance
        ///     Should throw an ArgumentException if actor is not found or no actor is given
        /// </summary>
        /// <param name="actor"></param>
        /// <returns></returns>
        public (int x, int y) GetPosition(Actor actor)
        {
            for (int i = 0; i < ActorMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < ActorMatrix.GetLength(1); j++)
                {
                    if (ActorMatrix[i, j] == actor)
                    {
                        return (j, i);
                    }
                }
            }

            throw new ArgumentException("Actor is not found on the map.");
        }

        /// <summary>
        ///     Assignes given actor to given position
        ///     Should throw an ArgumentException if the position is occupied by an another actor
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="position"></param>
        public void SetPosition(Actor actor, (int x, int y) position)
        {
            if (GetActor(position) != null)
            {
                throw new ArgumentException("Position is already occupied by another actor.");
            }

            ActorMatrix[position.y, position.x] = actor;
        }

        /// <summary>
        ///     Attempts to move given player to a new position
        ///     If necessary, restricts movement or simulates fights between players (DO NOT MODIFY)
        /// </summary>
        /// <param name="player"></param>
        /// <param name="currentPosition"></param>
        /// <param name="dir"></param>
        public void TryMovePlayer(Player player, (int x, int y) currentPosition, Direction dir)
        {
            var (x, y) = dir.ToVector();
            (int x, int y) targetPosition = (currentPosition.x + x, currentPosition.y + y);

            if (!WithinBoundaries(targetPosition))
                return;

            var actorOnTargetPosition = GetActor(targetPosition);

            switch (actorOnTargetPosition)
            {
                case null:
                    {
                        // Empty field, player can move here
                        ActorMatrix[currentPosition.y, currentPosition.x] = null;
                        SetPosition(player, targetPosition);
                        break;
                    }

                case Flag flag:
                    {
                        // Capture the flag
                        ActorMatrix[currentPosition.y, currentPosition.x] = null;
                        ActorMatrix[targetPosition.y, targetPosition.x] = null;
                        SetPosition(player, targetPosition);

                        player.CapturedFlags++;
                        flag.Captured = true;
                        break;
                    }

                case Player otherPlayer:
                    {
                        var fightResult = player.Fight(otherPlayer);

                        if (fightResult == 1)
                        {
                            // Player has won, move to the target position
                            otherPlayer.Alive = false;
                            ActorMatrix[currentPosition.y, currentPosition.x] = null;
                            ActorMatrix[targetPosition.y, targetPosition.x] = null;
                            SetPosition(player, targetPosition);

                            player.KilledPlayers++;
                        }
                        else if (fightResult == 0)
                        {
                            // Other player has won
                            player.Alive = false;
                            ActorMatrix[currentPosition.y, currentPosition.x] = null;
                            ActorMatrix[targetPosition.y, targetPosition.x] = null;
                            SetPosition(otherPlayer, currentPosition);

                            otherPlayer.KilledPlayers++;
                        }

                        break;
                    }
            }
        }

        /// <summary>
        ///     Returns the position of an uncaptured flag that is closest to given player
        ///     Should throw ArgumentException if there are no uncaptured flags
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public (int x, int y) GetNearestFlagPosition(Player player)
        {
            var playerPosition = GetPosition(player);
            var nearestFlag = Flags.FirstOrDefault(f => !f.Captured);
            if (nearestFlag == null)
                throw new ArgumentException("There are no uncaptured flags.");
            var nearestFlagPosition = GetPosition(nearestFlag);

            // Use Manhattan Distance to calculate distance between player and nearest flag
            int distance = Math.Abs(playerPosition.x - nearestFlagPosition.x) + Math.Abs(playerPosition.y - nearestFlagPosition.y);
            foreach (var flag in Flags)
            {
                if (flag.Captured) continue;
                var flagPosition = GetPosition(flag);
                var newDistance = Math.Abs(playerPosition.x - flagPosition.x) + Math.Abs(playerPosition.y - flagPosition.y);
                if (newDistance < distance)
                {
                    nearestFlag = flag;
                    nearestFlagPosition = flagPosition;
                    distance = newDistance;
                }
            }
            return nearestFlagPosition;
        }

        /// <summary>
        ///     Returns true if given position is within the map's boundaries
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool WithinBoundaries((int x, int y) position)
        {
            return position.x >= 0 && position.x < ActorMatrix.GetLength(1) && position.y >= 0 && position.y < ActorMatrix.GetLength(0);
        }
    }
}