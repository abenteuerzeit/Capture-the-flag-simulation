using System;

namespace Codecool.CaptureTheFlag.Actors
{
    /// <summary>
    ///     Scissors player class
    /// </summary>
    public class Scissors : Player
    {
        public Scissors(string name, GameMap mapReference) : base(name, mapReference)
        {
        }

        public override PlayerTeam Team => PlayerTeam.Scissors;

        public override void OnGameCycle()
        {
            // See Rock.OnGameCycle() for example implementation

            if (!Alive)
                return;

                // Team Scissors - represented by letter S. If the chosen field is occupied by a Paper, fight him. If the chosen field is occupied by a Rock, then move in the opposite direction If that position is also occupied by an enemy, fight him If moving to the opposite direction would mean walking out of map’s boundaries, then don’t move

            var myPosition = MapReference.GetPosition(this);
            var myDirection = MapReference.GetMoveDirection(this);
            var myMap = MapReference.GetMap();
            if (myMap == null)
                return;

            if (myMap.IsOccupied(myPosition, myDirection))
            {
                var otherPlayer = myMap.GetPlayer(myPosition, myDirection);
                if (otherPlayer != null)
                {
                    if (otherPlayer.Team == PlayerTeam.Paper)
                    {
                        otherPlayer.Alive = false;
                        KilledPlayers++;
                    }
                    else if (otherPlayer.Team == PlayerTeam.Rock)
                    {
                        var oppositeDirection = GetOppositeDirection(myDirection);
                        if (myMap.IsOccupied(myPosition, oppositeDirection))
                        {
                            otherPlayer = myMap.GetPlayer(myPosition, oppositeDirection);
                            if (otherPlayer != null)
                            {
                                if (otherPlayer.Team == PlayerTeam.Paper)
                                {
                                    otherPlayer.Alive = false;
                                    KilledPlayers++;
                                }
                            }
                        }
                        else
                        {
                            MapReference.TryMovePlayer(this, myPosition, oppositeDirection);
                        }
                    }
                }
            }

        }

        public override int Fight(Player otherPlayer)
        {
            if (otherPlayer.Team == Team)
                return -1;

            if (otherPlayer is Rock)
                return 1;
            else
                return 0;
        }
    }
}