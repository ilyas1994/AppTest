using AutoMapper;
using Logic.XMLModel;
using TestApp.Models;

namespace TestApp.Helpers
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ExchangeXMLModel, R_CURRENCY>();
                config.CreateMap<R_CURRENCY, ExchangeXMLModel>();
            });
            return mappingConfig;
        }
    }
}
