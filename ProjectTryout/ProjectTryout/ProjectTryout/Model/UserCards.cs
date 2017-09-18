using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProjectTryout.Model
{
    public class UserCards
    {
        public UserCards() { }

        [JsonProperty("add")]
        public List<Card> AddedCards { get; set; }
        [JsonProperty("delete")]
        public List<Archive> DeletedCards { get; set; }
    }
}
