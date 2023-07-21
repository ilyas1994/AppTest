using Logic.XMLModel;

namespace ILogic_Currency_DINameSpace.Interfaces
{
    public interface ILogic_Currency_DI
    {
        public Task<List<ExchangeXMLModel>> processWithCurrencyFacade(string EXTERNAL_URL);
    }
}
