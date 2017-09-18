using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProjectTryout.Model.HttpModule;
using ProjectTryout.Model.Services;

namespace ProjectTryout.Model.Action_Types
{
    public class ToText : IAction 
    {
        [JsonProperty("actionTypeId")]
        public int Id { get; set; }

        [JsonProperty("actionTypeName")]
        public string Name { get; set; }

        [JsonProperty("actionId")]
        public int ActionId { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonIgnore]
        public int Representable { get { return 1; } set {} }

        public async Task<ActionResult> ExecuteAction()
        {
            HttpMiddleman httpMiddleman = new HttpMiddleman(new HttpConsumer());
            var message = new { Message = Message};
            var data = new { Arguments = message, ActionId = ActionId, TimeStamp = DateTime.Now };
            ActionResult result = await httpMiddleman.GetAuthorization(data, Message, false);

            return result;
        }

    }
}
