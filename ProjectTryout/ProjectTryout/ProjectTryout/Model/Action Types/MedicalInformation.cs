using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProjectTryout.Model.HttpModule;

namespace ProjectTryout.Model.Action_Types
{
    public class MedicalInformation : IAction
    {
        [JsonProperty("actionTypeId")]
        public int Id { get; set; }

        [JsonProperty("actionTypeName")]
        public string Name { get; set; }

        [JsonProperty("actionId")]
        public int ActionId { get; set; }

        [JsonProperty("bloodType")]
        public string BloodType { get; set; }

        [JsonProperty("allergies")]
        public string Allergies { get; set; }

        [JsonProperty("medicalHistory")]
        public string MedicalHistory { get; set; }

        public int Representable
        {
            get { return 1; }
            set { }
        }

        public async Task<ActionResult> ExecuteAction()
        {
            HttpMiddleman httpMiddleman = new HttpMiddleman(new HttpConsumer());
            var data = new { ActionId = ActionId, TimeStamp = DateTime.Now };
            ActionResult result = await httpMiddleman.GetAuthorization(data, null, false);
            return result;
        }
    }
}
