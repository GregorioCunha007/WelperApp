using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProjectTryout.Model
{
    public class Archive
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("cardId")]
        public int CardId { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonIgnore]
        public DateTime TimeStamp { get; set; }
    }
}
