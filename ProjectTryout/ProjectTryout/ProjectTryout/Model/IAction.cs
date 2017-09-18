using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProjectTryout.Model
{
    public interface IAction
    {
        [JsonProperty("actionTypeId")]
        int Id { get; set; }

        [JsonProperty("actionTypeName")]
        string Name { get; set; }

        [JsonProperty("actionId")]
        int ActionId { get; set; }

        [JsonIgnore]
        int Representable { get; set; }

        Task<ActionResult> ExecuteAction();
    }
}
