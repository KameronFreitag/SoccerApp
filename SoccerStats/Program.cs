using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace SoccerStats
{
    class Program
    {
        static void Main(string[] args)
        {
        //Assign currentDirectory to where we are in the file system
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDirectory);
        //Append our current directory to the document we want to read so we have the full file path
            var fileName = Path.Combine(directory.FullName, "SoccerGameResults.csv");
            var fileContents = ReadSoccerResults(fileName);
        //We have a JSON document in our files.  Let's DESERIALIZE it.
        //To do so we must read from the file.  Let's change that file name!
            fileName = Path.Combine(directory.FullName, "players.json");
            var players = DeserializePlayers(fileName);
            var topTenPlayers = GetTopTenPlayers(players);

            foreach(var player in topTenPlayers)
            {
                Console.WriteLine("{0} {1}\n\tTeam: {2}\n\tPoints: {3}", player.FirstName, player.SecondName, player.TeamName, player.PointsPerGame);
            }

            //We're going to serialize our output as a .json file
            fileName = Path.Combine(directory.FullName, "topten.json");
            SerializePlayersToFile(topTenPlayers, fileName);
        }

        public static string ReadFile(string fileName)
        {
            using(var reader = new StreamReader(fileName))
            {
                return reader.ReadToEnd();
            }
        }

        public static List<GameResult> ReadSoccerResults(string fileName)
        {
            //Make a list of string arrays
            var soccerResults = new List<GameResult>();

            //Read the from the file
            using (var reader = new StreamReader(fileName))
            {
                string line = "";
                reader.ReadLine();
                //while there is something to read in our file
                //line will be the current line we're at in the file
                while((line = reader.ReadLine()) != null)
                {
                    var gameResult = new GameResult();
                    //split the line using commas as the delimiter and add those to a string array
                    string[] values = line.Split(',');
                    DateTime gameDate;
                    if (DateTime.TryParse(values[0], out gameDate))
                    {
                        gameResult.GameDate = gameDate;
                    }
                    gameResult.TeamName = values[1];
                    HomeOrAway homeOrAway;
                    if(Enum.TryParse(values[2], out homeOrAway))
                    {
                        gameResult.HomeOrAway = homeOrAway;
                    }
                    int parseInt;
                    if(int.TryParse(values[3], out parseInt))
                    {
                        gameResult.Goals = parseInt;
                    }
                    if (int.TryParse(values[4], out parseInt))
                    {
                        gameResult.GoalAttempts = parseInt;
                    }
                    if (int.TryParse(values[5], out parseInt))
                    {
                        gameResult.ShotsOnGoal = parseInt;
                    }
                    if (int.TryParse(values[6], out parseInt))
                    {
                        gameResult.ShotsOffGoal = parseInt;
                    }

                    double posessionPercent;
                    if (double.TryParse(values[7], out posessionPercent))
                    {
                        gameResult.PosessionPercent = posessionPercent;
                    }
                    soccerResults.Add(gameResult);
                }
            }

            return soccerResults;
        }

        public static List<Player> DeserializePlayers(string fileName)
        {
            var players = new List<Player>();
            var serializer = new JsonSerializer();
            using (var reader = new StreamReader(fileName))
            using(var jsonReader = new JsonTextReader(reader))
            {
                //Deserialize<T> can return whatever info you pass it as whatever type you tell it to in T (The type parameter)
                //For example, we're giving it an array of strings but it's going to convert it to a List of Player objects
                players = serializer.Deserialize<List<Player>>(jsonReader);
            }
                
            return players;
        }

        public static List<Player> GetTopTenPlayers(List<Player> players)
        {
            //var topTenPlayers = new List<Player>();
            //players.Sort(new PlayerComparer());

            //Using a linq statement to return a sorted list of 10
            return (
                from player in players
                orderby player.PointsPerGame descending
                select player).Take(10).ToList();
        }

        public static void SerializePlayersToFile(List<Player> players, string fileName)
        {
            var serializer = new JsonSerializer();
            using (var writer = new StreamWriter(fileName))
            using (var jsonWriter = new JsonTextWriter(writer))
            {
                //Deserialize<T> can return whatever info you pass it as whatever type you tell it to in T (The type parameter)
                //For example, we're giving it an array of strings but it's going to convert it to a List of Player objects
                serializer.Serialize(jsonWriter, players);
            }
        }
    }
}
