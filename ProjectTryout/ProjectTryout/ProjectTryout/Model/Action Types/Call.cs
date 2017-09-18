using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Messaging;
using ProjectTryout.Model.HttpModule;

namespace ProjectTryout.Model.Action_Types
{
    public class Call : IAction
    {
        [JsonProperty("actionTypeId")]
        public int Id { get; set; }

        [JsonProperty("actionTypeName")]
        public string Name { get; set; }

        [JsonProperty("actionId")]
        public int ActionId { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonIgnore]
        public int Representable
        {
            get
            {
                return 0;
            }
            set {}
        }

        public async Task<ActionResult> ExecuteAction()
        {
            HttpMiddleman httpMiddleman = new HttpMiddleman(new HttpConsumer());
 
            var phoneDialer = CrossMessaging.Current.PhoneDialer;
            
            var data = new { ActionId = ActionId, TimeStamp = DateTime.Now };
            ActionResult result = await httpMiddleman.GetAuthorization(
                data,
                "Wants to make a call", 
                false);

            if (result.Succeed)
            {
                if (phoneDialer.CanMakePhoneCall)
                {
                    phoneDialer.MakePhoneCall(Number);
                }
            }
            
            return result;
        }
    }
}
