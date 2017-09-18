using System;
using System.Net;
using System.Threading.Tasks;
using ProjectTryout.Helpers;
using ProjectTryout.Model.Exceptions;
using ProjectTryout.Model.HttpModule;

namespace ProjectTryout.Model
{
    public class HttpMiddleman
    {
        private readonly IHttpService<Result> _httpService;

        // Uri for Api
        private static readonly string AuthorizationUri = "http://api-welp3r.azurewebsites.net/users/{0}/authorize_action";
        private static readonly string UpdateCardsUri = "http://api-welp3r.azurewebsites.net/update/user/{0}/cards?timeStampPrev={1}&timeStampCurr={2}";
        private static readonly string VerifyPasswordUri = "http://api-welp3r.azurewebsites.net/verify/users/{0}";
        private static readonly string LowBatteryWarningUri ="http://api-welp3r.azurewebsites.net/users/{0}/low-battery";

        public HttpMiddleman(IHttpService<Result> httpService)
        {
            _httpService = httpService;
        }

        public async Task<ActionResult> GetAuthorization(object data, string onSucessMessage, bool notifiable)
        {
            var id = Settings.UserId;
            try
            {
                Result res = await _httpService.Post(AuthorizationUri, new object[] { id }, data);
                if (res.StatusCode != HttpStatusCode.OK)
                {
                    throw new GeneralNetworkException(res.StatusCode.ToString());
                }
                return ActionResult.CreateActionSuccessfull(onSucessMessage, notifiable);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Result> GetCards()
        {
            var id = Settings.UserId;
            var timestampPrev = Settings.TimeStampPrev;
            var timestampCurr = DateTime.UtcNow;
            try
            {
                Result res;
                if (timestampPrev.Equals(default(DateTime)))
                {
                    res = await _httpService.Get(UpdateCardsUri, new object[] {id, null ,EncodeTime(timestampCurr)});
                }
                else
                {
                    res = await _httpService.Get(UpdateCardsUri, new object[] { id, EncodeTime(timestampPrev), EncodeTime(timestampCurr) });
                }

                Settings.TimeStampPrev = timestampCurr;

                if (res.StatusCode != HttpStatusCode.OK)
                {
                    throw new GeneralNetworkException(res.StatusCode.ToString());
                }
                return new JsonResult(res.Content);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Result> VerifyPassword(int id, object data)
        {
            
            try
            {
                Result res = await _httpService.Post(VerifyPasswordUri, new object[] { id }, data);

                if(res.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new PasswordMismatchException("Password don't match");
                }
                
                if (res.StatusCode != HttpStatusCode.OK)
                {
                    throw new GeneralNetworkException(res.StatusCode.ToString());
                }

                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Result> LowBatteryWarning(object data)
        {
            var id = Settings.UserId;
            try
            {
                Result res = await _httpService.Post(LowBatteryWarningUri, new object[] {id}, data);
                if (res.StatusCode != HttpStatusCode.OK)
                {
                    throw new GeneralNetworkException(res.StatusCode.ToString());
                }
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private string EncodeTime(DateTime dateTime)
        {
            string dt = dateTime.ToString("yyyy-MM-dd HH':'mm':'ss");
            dt = dt.Replace(" ", "_");
            return dt;
        }
    }
}
