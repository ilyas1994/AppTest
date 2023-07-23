using AutoMapper;
using ILogic_Currency_DINameSpace.Interfaces;
using Logic.XMLModel;
using Microsoft.AspNetCore.Mvc;
using TestApp.Interfaces;
using TestApp.Models;

namespace TestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ILogic_Currency_DI _logic;
        private readonly IConfiguration _configuration;
        private readonly ICurrencyQueries_TestApp_DI _currencyQueries;
        private readonly IValidationDateFormat_TestApp_DI _validatationDateFormat;
        private readonly IMapper _mapper;
        public CurrencyController(
            ILogic_Currency_DI logic,
            IConfiguration configuration,
            ICurrencyQueries_TestApp_DI currencyQueries,
            IValidationDateFormat_TestApp_DI validatationDateFormat,
            IMapper mapper
            )
        {
            _logic = logic;
            _configuration = configuration;
            _currencyQueries = currencyQueries;
            _validatationDateFormat = validatationDateFormat;
            _mapper = mapper;
        }

        [HttpGet("save")]
        public async Task<IActionResult> currencySave([FromQuery] string date)
        {
            var validateDate = _validatationDateFormat.checkValidateDateFormat(date);
            if (validateDate.status == false)
            {
                return BadRequest(validateDate.ErrorMessage);
            }
            var data = await _logic.processWithCurrencyFacade(_configuration["EXTERNAL_API"] + date);
            Console.WriteLine(data);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ExchangeXMLModel, R_CURRENCY>()
                    .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date));

            });
            var myMapper = config.CreateMapper();
            var createMapp = myMapper.Map<List<R_CURRENCY>>(data);
            var createData = await _currencyQueries.create(createMapp);
            return Ok(createData);
        }

        [HttpGet("getCurrency")]
        public async Task<IActionResult> currency([FromQuery] string date, string? code)
        {
            try
            {
                var parseDate = DateTime.Parse(date);
                var data = await _currencyQueries.getCurrency(parseDate, code);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
