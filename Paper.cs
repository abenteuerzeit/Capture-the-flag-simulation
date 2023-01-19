using System;
using System.Numerics;

namespace Codecool.CaptureTheFlag.Actors
{
    /// <summary>
    ///     Paper player class
    /// </summary>
    public class Paper : Player
    {
        public Paper(string name, GameMap mapReference) : base(name, mapReference)
        {
        }

        public override PlayerTeam Team => PlayerTeam.Paper;
        public override void OnGameCycle()
        {
            // See Rock.OnGameCycle() for example implementation

            if (!Alive)
                return;

            // Get player's current position
            var myPosition = MapReference.GetPosition(this);
            // Get the position of the nearest flag
            var nearestFlagPosition = MapReference.GetNearestFlagPosition(this);
            // Get the direction to the nearest flag
            var targetDirection = GetMoveDirection(myPosition, nearestFlagPosition);





            MapReference.TryMovePlayer(this, myPosition, targetDirection);
        }

        public override int Fight(Player otherPlayer)
        {

            if (otherPlayer.Team == Team)
                return -1;

            if (otherPlayer is Rock)
                return 1;
            else if (otherPlayer is Scissors)
                return 0;
            else
                return -1;

        }
    }
}