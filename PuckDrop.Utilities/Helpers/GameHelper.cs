using NHLModel;
using PuckDrop.Model;
using System.ComponentModel;
using System.Reflection;

namespace PuckDrop
{
    public class GameHelper
    {

        public GameHelper() { }


        public string ToDescription(GameType type)
        {
            FieldInfo fi = type.GetType().GetField(type.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return type.ToString();
        }
        public bool ValidateGame(GameInfo game)
        {
            var gameInfo = game.Dates?.FirstOrDefault()?.Games?.FirstOrDefault();

            if (gameInfo == null)
            {
                return false;
            }

            var gameType = gameInfo?.GameType;
            var awayTeam = gameInfo?.Teams?.Away?.Team?.Name;
            var homeTeam = gameInfo?.Teams?.Home?.Team?.Name;
            var arena = gameInfo?.Venue?.Name;
            var gameTime = gameInfo?.GameDate;

            // Check if all the required fields have values
            return gameType != null && awayTeam != null && homeTeam != null && arena != null && gameTime != null;
        }

    }
}
