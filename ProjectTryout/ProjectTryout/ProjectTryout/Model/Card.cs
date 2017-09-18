using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProjectTryout.Model.Deserialization;
using Xamarin.Forms;

namespace ProjectTryout.Model
{
    public class Card
    {
        [JsonConverter(typeof(ActionConverter))]
        public IAction Action;

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("representation")]
        public string Representation { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("coordinatorId"), JsonIgnore]
        public int CoordinatorId { get; set; }

        [JsonProperty("cardCategoryName"),JsonIgnore]
        public string CardCategoryName { get; set; }
    }
}
