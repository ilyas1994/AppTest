using TestApp.Models;

namespace TestApp.Interfaces
{
    public interface ICurrencyQueries_TestApp_DI
    {
        public Task<IEnumerable<R_CURRENCY>> getCurrency(DateTime date, string code);
        public Task<object> create(List<R_CURRENCY> exchangeXMLModel);
    }
}
