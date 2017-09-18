using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using ProjectTryout.Model.Exceptions;
using ProjectTryout.Model.HttpModule;
using ProjectTryout.Model.Services;
using Xamarin.Forms;

namespace ProjectTryout.Model.Action_Types
{
    public class SendCoordinates : IAction 
    {

        private IGeolocator _locator;
        private IEnableLocationService _locationService;

        [JsonProperty("actionTypeId")]
        public int Id { get; set; }

        [JsonProperty("actionTypeName")]
        public string Name { get; set; }

        [JsonProperty("actionId")]
        public int ActionId { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonIgnore]
        public int Representable { get { return 0; } set { } }

        public async Task<ActionResult> ExecuteAction()
        {
            HttpMiddleman httpMiddleman = new HttpMiddleman(new HttpConsumer());
            var position = await GetGeolocation();
            if (position == null)
            {
                return new ActionResult
                {
                    Message = "Not able to trace position",
                    Succeed = false,
                    Notify = true
                };
            }
            var coordinates = new {Latitude = position.Latitude, Longitude = position.Longitude};
        
            var data = new {Arguments = coordinates, ActionId = ActionId, TimeStamp = DateTime.Now};

            return await httpMiddleman.GetAuthorization(data, "Gps coordinates were sent", true);
        }

        private async Task<Position> GetGeolocation()
        {

            _locationService = DependencyService.Get<IEnableLocationService>();
            if (!_locationService.IsLocationServiceEnabled())
            {
                throw new GpsDisabledException();
            }

            try
            {
                _locator = CrossGeolocator.Current;
                _locator.DesiredAccuracy = 50;

                Position position = await _locator.GetPositionAsync(timeoutMilliseconds: 100000);
                return position;

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to get location, may need to increase timeout: " + ex);
            }
            return null;
        }


    }
}
