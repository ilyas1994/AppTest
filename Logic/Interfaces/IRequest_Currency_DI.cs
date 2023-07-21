namespace Logic.Interfaces
{
    public interface IRequest_Currency_DI
    {
        public Task<HttpResponseMessage> request(string url);
    }
}
