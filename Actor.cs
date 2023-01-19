using Codecool.CaptureTheFlag.Actors;

namespace Codecool.CaptureTheFlag.Actors
{
    /// <summary>
    ///     Base class for all Actors - Players and Flags
    /// </summary>
    public abstract class Actor
    {
        /// <summary>
        ///     Reference to Game's map
        /// </summary>
        protected GameMap MapReference;

        protected Actor(GameMap mapReference)
        {
            MapReference = mapReference;
        }
    }
}
