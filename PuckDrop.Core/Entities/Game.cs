using System.ComponentModel;

namespace NHLModel
{
    public class Game
    {
        public GameType GameType { get; set; }
        public DateTime GameDate { get; set; }
        public Teams Teams { get; set; }
        public Venue Venue { get; set; }

    }

    public enum GameType
    {
        [Description("Preseason")]
        PR,
        [Description("Regular Season")]
        R,
        [Description("Playoff")]
        P
    }
}