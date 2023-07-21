﻿using ILogic_Currency_DINameSpace.Interfaces;
using Logic.Interfaces;
using Logic.XMLModel;
using System.Globalization;
using System.Xml.Linq;

namespace LogicNameSpace
{
    public class Logic_Currency_DI : ILogic_Currency_DI
    {
        private readonly IRequest_Currency_DI _request;
        private DateTime date;
        public Logic_Currency_DI(IRequest_Currency_DI request)
        {
            _request = request;
        }

        public async Task<List<ExchangeXMLModel>> processWithCurrencyFacade(string EXTERNAL_URL)
        {
            var responseData = await getResponseWithData(EXTERNAL_URL);
            var parsedData = parseData(responseData);
            return parsedData;
        }

        #region private
        private async Task<IEnumerable<XElement?>> getResponseWithData(string EXTERNAL_URL)
        {
            string format = "dd.MM.yyyy";
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = await _request.request(EXTERNAL_URL);

            }
            catch (Exception ex)
            {
                throw new ArgumentException("url error");
            }

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    string xmlContent = await response.Content.ReadAsStringAsync();


                    XDocument xmlDocument = XDocument.Parse(xmlContent);
                    var titleElement = xmlDocument.Descendants("item");

                    date = DateTime.ParseExact(xmlDocument.Element("rates").Element("date").Value, format, CultureInfo.InvariantCulture);
                    return titleElement;
                }
                else
                {
                    throw new ArgumentException("Response is Fail");
                }
            }
            catch (Exception ex)
            {

                throw new ArgumentException("XMl Parse error " + ex.Message);
            }
        }

        private List<ExchangeXMLModel> parseData(IEnumerable<XElement> titleElement)
        {
            List<ExchangeXMLModel> list = new List<ExchangeXMLModel>();
            foreach (var item in titleElement)
            {
                var oneItem = new ExchangeXMLModel()
                {
                    Code = item.Element("title").Value,
                    Title = item.Element("fullname").Value,
                    Value = double.Parse(item.Element("description").Value, CultureInfo.InvariantCulture),
                    Date = date
                };
                list.Add(oneItem);
            }
            return list;
        }
        #endregion


    }
}