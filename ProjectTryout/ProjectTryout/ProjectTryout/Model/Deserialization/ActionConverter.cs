using System;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjectTryout.Model.Action_Types;

namespace ProjectTryout.Model.Deserialization
{
    internal  class ActionConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            var action = default(IAction);

            switch (jsonObject["actionTypeId"].Value<int>())
            {
                case 1:
                    action = new SendCoordinates();
                    break;
                case 2:
                    action = new Call();
                    break;
                case 3:
                    action = new ToText();
                    break;
                case 4:
                    action = new MedicalInformation();
                    break;
                case 6:
                    action = new CallRequest();
                    break;
            }
            serializer.Populate(jsonObject.CreateReader(), action);
            return action;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IAction);
        }
    }
}
