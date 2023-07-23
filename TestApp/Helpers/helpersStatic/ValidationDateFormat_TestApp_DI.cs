using TestApp.Interfaces;
using TestApp.Models;

namespace TestApp.Helpers.helpersStatic
{
    public class ValidationDateFormat_TestApp_DI : IValidationDateFormat_TestApp_DI
    {
        public ResponseStatus checkValidateDateFormat(string date)
        {
            ResponseStatus responseData = new ResponseStatus();

            try
            {
                DateTime newDate = DateTime.Parse(date);
                responseData.status = true;
                return responseData;
            }
            catch (FormatException)
            {
                responseData.status = false;
                responseData.ErrorMessage = "Incorrect format";
                return responseData;
            }

        }
    }
}
