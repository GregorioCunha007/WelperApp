using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ModernHttpClient;
using Newtonsoft.Json;

namespace ProjectTryout.Model.HttpModule
{
    public class HttpConsumer : IHttpService<Result>
    {
        private readonly HttpClient _client = new HttpClient(new NativeMessageHandler());

        public async Task<Result> Get(string uriParam, object[] uriParameters)
        {
            var uri = string.Format(uriParam, uriParameters);
            HttpResponseMessage response = await _client.GetAsync(uri);
            var content = await response.Content.ReadAsStringAsync();
            return new Result {Content = content, StatusCode = response.StatusCode};
        }

        public async Task<Result> Post(string uriParam, object[] uriParameters, object data)
        {
            var uri = string.Format(uriParam, uriParameters);
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            try
            {
                response = await _client.PostAsync(uri, content);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            
            return new Result {StatusCode = response.StatusCode};
        }
    }
}
