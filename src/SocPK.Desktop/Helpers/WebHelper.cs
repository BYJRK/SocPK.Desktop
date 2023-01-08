using RestSharp;
using System.Net;
using System.Threading.Tasks;

namespace SocPK.Desktop.Helpers
{
    public class WebHelper
    {
        private readonly RestClient client;

        public WebHelper(RestClient client)
        {
            this.client = client;
        }

        public async Task<string> GetStringAsync(string url, int retry = 10)
        {
            var request = new RestRequest(url, Method.Get);
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return response.Content;

            if (retry > 0)
            {
                await Task.Delay(1000);
                return await GetStringAsync(url, retry - 1);
            }
            throw new WebException("Web request failed.");
        }
    }
}
