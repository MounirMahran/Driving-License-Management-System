using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsCountriesDataAccess
    {
        public static DataTable ListAllCountries()
        {
            DataTable Countries = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "SELECT * FROM Countries;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) Countries.Load(reader);

                reader.Close();
            }
            catch (Exception ex)
            {
                //Errors will be loged here
            }
            finally
            {
                connection.Close();
            }

            return Countries;
        }

        public static bool GetCountryByID(short CountryID, ref string CountryName)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "SELECT * FROM Countries WHERE CountryID = @CountryID;";

            SqlCommand command = new SqlCommand(@query, connection);

            command.Parameters.AddWithValue("@CountryID", CountryID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    CountryName = (string)reader["CountryName"];

                    reader.Close();
                }

            }
            catch (Exception ex)
            {
                //Errors will be logged here
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool GetCountryByName(string CountryName, ref short CountryID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "SELECT * FROM Countries WHERE CountryName = @CountryName;";

            SqlCommand command = new SqlCommand(@query, connection);

            command.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    CountryID = (short)reader["CountryID"];

                    reader.Close();
                }

            }
            catch (Exception ex)
            {
                //Errors will be logged here
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
    }
}
