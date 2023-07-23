using TestApp.Interfaces;
using TestApp.Models;

namespace TestApp.Helpers.helpersStatic
{
    public class ValidationDateFormat_TestApp_DI : IValidationDateFormat_TestApp_DI
    {
        public ResponseStatus checkValidateDateFormat(string date)
        {
            ResponseStatus responseData = new ResponseStatus();


            string[] formats = { "dd.MM.yyyy" };
            DateTime newDate;

            if (DateTime.TryParseExact(date, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out newDate))
            {
                responseData.status = true;
                return responseData;
            }
            else
            {
                responseData.status = false;
                responseData.ErrorMessage = "Некорректный формат даты! Формат должен быть в таком порядке: dd.MM.yyyy или dd/MM/yyyy";
                return responseData;
            }

        }
    }
}
