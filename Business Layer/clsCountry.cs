using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsCountry
    {
        public short CountryId { get; set; }
        public string CountryName { get; set; }

        public clsCountry(short countryid, string countryname)
        {
            CountryId = countryid;
            CountryName = countryname;
        }
        public static DataTable ListAllCountries()
        {
            return clsCountriesDataAccess.ListAllCountries();
        }
        public static clsCountry Find(short CountryID)
        {
            string CountryName = "";

            bool isFound = clsCountriesDataAccess.GetCountryByID(CountryID, ref CountryName);

            if (isFound)
            {
                return new clsCountry(CountryID, CountryName);
            }

            return null;

        }
        public static clsCountry Find(string CountryName)
        {
            short CountryID = 0;

            bool isFound = clsCountriesDataAccess.GetCountryByName(CountryName, ref CountryID);

            if (isFound)
            {
                return new clsCountry(CountryID, CountryName);
            }

            return null;
        }
    }
}
