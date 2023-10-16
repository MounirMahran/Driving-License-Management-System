namespace DataAccessLayer
{
    public class clsDataAccess
    {
        public static bool GetPersonByID(int ID, ref string FirstName, ref string SecondName, ref string ThirdName,
                                                      ref string LastName, ref int Gender,
                                                      ref string Email, ref string Phone, ref string NationalNumber,
                                                      ref string Address, ref DateTime DateOfBirth,
                                                      ref int NationalityCountryID, ref string ImgPath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "SELECT * FROM People WHERE PersonID = @PersonID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", ID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    ThirdName = (string)reader["ThirdName"];
                    LastName = (string)reader["LastName"];
                    Email = (string)reader["Email"];
                    Phone = (string)reader["Phone"];
                    NationalNumber = (string)reader["NationalNo"];
                    Address = (string)reader["Address"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    NationalityCountryID = (int)reader["NationalityCountryID"];
                    //Gender = (int)reader["Gendor"];
                    Gender = -1;

                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImgPath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        ImgPath = "";
                    }

                    reader.Close();
                }


            }
            catch (Exception ex)
            {
                // Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
        public static DataTable ListAllPeople()
        {
            DataTable People = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "SELECT * FROM People;";

            SqlCommand command = new SqlCommand(query, connection);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    People.Load(reader);
                }

                reader.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return People;


        }
    }
}