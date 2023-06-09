namespace NHLModel
{
    public class Date
    {
        public DateTime GameDate { get; set; }
        public int TotalGames { get; set; }
        public Game[] Games { get; set; }
    }
}