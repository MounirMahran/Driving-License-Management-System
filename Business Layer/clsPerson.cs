namespace BusinessLayer
{
    public class clsPerson
    {
        public enum enMode { Update, AddNew }
        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string NationalNumber { get; set; }

        public string Address { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int NationalityCountryID { get; set; }
        public int Gender { get; set; }

        public string ImgPath { get; set; }

        private enMode Mode { get; set; }


        public clsPerson()
        {
            this.PersonID = -1;
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.Email = "";
            this.Phone = "";
            this.NationalNumber = "";
            this.Address = "";
            this.DateOfBirth = DateTime.Now;
            this.NationalityCountryID = -1;
            this.Gender = -1;
            this.ImgPath = "";
            this.Mode = enMode.AddNew;
        }

        private clsPerson(int personID, string firstName, string secondName, string thirdName, string lastName, string email, string phone, string nationalNumber, string address, DateTime dateOfBirth, int nationalityCountryID, int gender, string imgPath)
        {
            PersonID = personID;
            FirstName = firstName;
            SecondName = secondName;
            ThirdName = thirdName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            NationalNumber = nationalNumber;
            Address = address;
            DateOfBirth = dateOfBirth;
            NationalityCountryID = nationalityCountryID;
            Gender = gender;
            ImgPath = imgPath;
            Mode = enMode.Update;
        }

        public static DataTable ListAllPeople()
        {
            return clsPersonDataAccess.ListAllPeople();
        }

        public static clsPerson Find(int ID)
        {
            string FirstName = "";
            string SecondName = "";
            string ThirdName = "";
            string LastName = "";
            string Email = "";
            string Phone = "";
            string NationalNumber = "";
            string Address = "";
            DateTime DateOfBirth = DateTime.Now;
            int NationalityCountryID = -1;
            int Gender = -1;
            string ImgPath = "";

            if (clsPersonDataAccess.GetPersonByID(ID, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref Gender, ref Email, ref Phone, ref NationalNumber, ref Address, ref DateOfBirth, ref NationalityCountryID, ref ImgPath))
            {
                return new clsPerson(ID, FirstName, SecondName, ThirdName, LastName, Email, Phone, NationalNumber, Address, DateOfBirth, NationalityCountryID, Gender, ImgPath);
            }
            else return null;
        }
    }
}
