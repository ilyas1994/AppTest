using Logic.Interfaces;

namespace Logic
{
    public class Request_Currency_DI : IRequest_Currency_DI
    {
        private readonly HttpClient httpClient;
        public Request_Currency_DI()
        {
            httpClient = new HttpClient();
        }

        public async Task<HttpResponseMessage> request(string url)
        {
            return await httpClient.GetAsync(url);
        }
    }
}
