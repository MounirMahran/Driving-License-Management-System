using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsPersonDataAccess
    {
        public static bool GetPersonByID(int ID, ref string FirstName, ref string SecondName, ref string ThirdName,
                                                      ref string LastName, ref short Gender,
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
                    LastName = (string)reader["LastName"];
                    Phone = (string)reader["Phone"];
                    NationalNumber = (string)reader["NationalNo"];
                    Address = (string)reader["Address"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    NationalityCountryID = (int)reader["NationalityCountryID"];
                    Gender = (byte)reader["Gendor"];
                    //Gender = -1;

                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImgPath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        ImgPath = "";
                    }

                    if (reader["ThirdName"] != DBNull.Value)
                    {
                        ThirdName = (string)reader["ThirdName"];
                    }
                    else
                    {
                        ThirdName = "";
                    }

                    if (reader["Email"] != DBNull.Value)
                    {
                        Email = (string)reader["Email"];
                    }
                    else
                    {
                        Email = "";
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

        public static bool GetPersonByNationalNumber(string NationalNumber, ref int ID, ref string FirstName, ref string SecondName, ref string ThirdName,
                                                      ref string LastName, ref short Gender,
                                                      ref string Email, ref string Phone,
                                                      ref string Address, ref DateTime DateOfBirth,
                                                      ref int NationalityCountryID, ref string ImgPath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "SELECT * FROM People WHERE NationalNo = @NationalNumber;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNumber", NationalNumber);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    ID = (int)reader["PersonID"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    LastName = (string)reader["LastName"];
                    Phone = (string)reader["Phone"];
                    Address = (string)reader["Address"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    NationalityCountryID = (int)reader["NationalityCountryID"];
                    Gender = (byte)reader["Gendor"];
                    //Gender = -1;

                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImgPath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        ImgPath = "";
                    }

                    if (reader["ThirdName"] != DBNull.Value)
                    {
                        ThirdName = (string)reader["ThirdName"];
                    }
                    else
                    {
                        ThirdName = "";
                    }

                    if (reader["Email"] != DBNull.Value)
                    {
                        Email = (string)reader["Email"];
                    }
                    else
                    {
                        Email = "";
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

            string query = "SELECT People.PersonID, People.NationalNo, People.FirstName, People.SecondName,\r\n\t\tPeople.ThirdName, People.LastName, People.DateOfBirth, \r\n\t\tCASE \r\n\t\t\tWHEN People.Gendor = 0 THEN 'Male'\r\n\t\t\tELSE 'Female'\r\n\t\tEND AS GenderCaption,\r\n\t\tPeople.Address, People.Phone, People.Email, Countries.CountryName, People.ImagePath\r\nFROM People INNER JOIN Countries ON People.NationalityCountryID = Countries.CountryID;\r\n";

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
                //Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return People;


        }

        public static int AddNewPerson(string NationalNo, string FirstName, string SecondName, string ThirdName,
                                       string LastName, DateTime DateOfBirth, short Gender, string Address,
                                       string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            int PersonID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "INSERT INTO [dbo].[People] ([NationalNo], [FirstName], [SecondName], [ThirdName] " +
                ",[LastName], [DateOfBirth], [Gendor], [Address], [Phone] ,[Email] ,[NationalityCountryID] ,[ImagePath]) " +
                "VALUES (@NationalNo, @FirstName, @SecondName, @ThirdName, @LastName, @DateOfBirth, @Gender, @Address, @Phone, @Email, @NationalityCountryID, @ImagePath);" +
                "SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(@query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gender", Gender);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if (ThirdName != "")
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", DBNull.Value);

            if (Email != "")
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", DBNull.Value);

            if (ImagePath != "")
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", DBNull.Value);



            try
            {
                connection.Open();
                object Result = command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int InsertedIndex))
                {
                    PersonID = InsertedIndex;
                }
            }
            catch (Exception ex)
            {
                //Errors will be loged here
                //Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();
            }

            return PersonID;
        }


        public static bool UpdatePerson(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName,
                                       string LastName, DateTime DateOfBirth, short Gender, string Address,
                                       string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            int RowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"UPDATE [dbo].[People]
                           SET [NationalNo] = @NationalNo
                              ,[FirstName] = @FirstName
                              ,[SecondName] = @SecondName
                              ,[ThirdName] = @ThirdName
                              ,[LastName] = @LastName
                              ,[DateOfBirth] = @DateOfBirth
                              ,[Gendor] = @Gender
                              ,[Address] = @Address
                              ,[Phone] = @Phone
                              ,[Email] = @Email
                              ,[NationalityCountryID] = @NationalityCountryID
                              ,[ImagePath] = @ImagePath
                         WHERE PersonID = @PersonID ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gender", Gender);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if (ThirdName != "")
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", DBNull.Value);

            if (Email != "")
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", DBNull.Value);

            if (ImagePath != "")
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", DBNull.Value);

            try
            {
                connection.Open();

                RowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //Log Errors
                return false;
            }
            finally
            {
                connection.Close();
            }

            return RowsAffected > 0;
        }


        public static bool DeletePerson(int PersonID)
        {
            int RowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"DELETE FROM [dbo].[People]
                                WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();
            }
            catch
            {
                //Errors will be loged here
            }
            finally
            {
                connection.Close();
            }

            return RowsAffected > 0;
        }

        public static bool PersonExists(int PersonID)
        {
            bool PersonExist = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "SELECT FOUND = 1 FROM People WHERE PersonID = @PersonID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) PersonExist = true;

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

            return PersonExist;
        }

        public static bool PersonExistsbyNationalNumber(string NationalNumber)
        {
            bool PersonExist = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "SELECT FOUND = 1 FROM People WHERE NationalNo = @NationalNumber;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNumber", NationalNumber);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) PersonExist = true;

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

            return PersonExist;

        }















    }



}
