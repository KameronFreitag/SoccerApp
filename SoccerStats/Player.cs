using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerStats
{

    public class RootObject
    {
        public Player[] Players { get; set; }
    }

    public class Player
    {
        //This thing in the brackets is an attribute
        //On it's own, it does nothing
        //Coupled with the code below, it can describe what to do with some data
        //Consider it meta data
        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "points_per_game")]
        public double PointsPerGame { get; set; }
        [JsonProperty(PropertyName = "second_name")]
        public string SecondName { get; set; }
        [JsonProperty(PropertyName = "team_name")]
        public string TeamName { get; set; }
    }
}
