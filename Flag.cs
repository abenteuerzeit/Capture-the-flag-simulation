namespace Codecool.CaptureTheFlag.Actors
{
    /// <summary>
    ///     Flag actor class
    /// </summary>
    public class Flag : Actor
    {
        public Flag(GameMap mapReference) : base(mapReference)
        {
            Captured = false;
        }

        /// <summary>
        ///     Returns true after being captured by a player
        /// </summary>
        public bool Captured { get; set; }

    }
}