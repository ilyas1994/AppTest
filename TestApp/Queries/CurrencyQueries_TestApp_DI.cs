﻿using Microsoft.EntityFrameworkCore;
using TestApp.AppDbContext;
using TestApp.Interfaces;
using TestApp.Models;

namespace TestApp.Queries
{
    public class CurrencyQueries_TestApp_DI : ICurrencyQueries_TestApp_DI
    {
        private readonly ApplicationDbContext _dbContext;
        public CurrencyQueries_TestApp_DI(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }
        public async Task<object> create(List<R_CURRENCY> exchangeXMLModel)
        {
            try
            {
                await _dbContext.Set<R_CURRENCY>().AddRangeAsync(exchangeXMLModel);
                int numRecordSaved = await _dbContext.SaveChangesAsync();
                var result = new { count = numRecordSaved };
                return result;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception(ex.InnerException.Message);
            }
        }

        public async Task<IEnumerable<R_CURRENCY>> getCurrency(DateTime date, string code)
        {
            try
            {
                if (code != null)
                {
                    var query = await _dbContext.Set<R_CURRENCY>()
                    .Where(x => x.Date == date && x.Code == code).ToListAsync();
                    return query;
                }
                else
                {
                    var query = await _dbContext.Set<R_CURRENCY>()
              .Where(x => x.Date == date).ToListAsync();
                    return query;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
