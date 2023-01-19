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

            var myPosition = MapReference.GetPosition(this);
            var nearestFlagPosition = MapReference.GetNearestFlagPosition(this);
            var targetDirection = GetMoveDirection(myPosition, nearestFlagPosition);
            MapReference.TryMovePlayer(this, myPosition, targetDirection);
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