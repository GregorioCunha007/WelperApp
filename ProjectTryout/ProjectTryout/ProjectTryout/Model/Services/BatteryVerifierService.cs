using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ProjectTryout.Model.HttpModule;

namespace ProjectTryout.Model.Services
{
    public class BatteryVerifierService : IAlertService
    {
        public async Task Alert(string message)
        {
            HttpMiddleman middleman = new HttpMiddleman(new HttpConsumer());
            var data = new { Battery = message, TimeStamp = DateTime.UtcNow };
            try
            {
                Result res = await middleman.LowBatteryWarning(data);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
