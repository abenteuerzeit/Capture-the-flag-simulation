using System;

namespace Codecool.CaptureTheFlag.Actors
{
    /// <summary>
    ///     Rock player class
    /// </summary>
    public class Rock : Player
    {
        public Rock(string name, GameMap mapReference) : base(name, mapReference)
        {
        }

        public override PlayerTeam Team => PlayerTeam.Rock;

        public override void OnGameCycle()
        {
            if (!Alive)
                return;

            // Rock movement logic is fully implemented as an example

            // Make next move
            var myPosition = MapReference.GetPosition(this);
            var nearestFlagPosition = MapReference.GetNearestFlagPosition(this);
            var targetDirection = GetMoveDirection(myPosition, nearestFlagPosition);

            MapReference.TryMovePlayer(this, myPosition, targetDirection);
        }

        public override int Fight(Player otherPlayer)
        {
            if (otherPlayer.Team == Team)
            {
                return -1;
            }
            else if (otherPlayer.Team == PlayerTeam.Scissors)
            {
                otherPlayer.Alive = false;
                KilledPlayers++;
                return 1;
            }
            else
            {
                Alive = false;
                return 0;
            }
        }
    }
}