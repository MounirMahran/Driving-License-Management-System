using BusinessLayer;
using DVLD.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class ucPersonInformation : UserControl
    {
        public ucPersonInformation()
        {
            InitializeComponent();
        }

        private clsPerson _Person;
        public clsPerson PersonInformation
        {
            get
            {
                return _Person;
            }

        }

        private int _PersonID;
        public int PersonID
        {
            get
            {
                return _PersonID;
            }
        }

        public void LoadPersonInfo(int ID)
        {
            _Person = clsPerson.Find(ID);

            lblPersonID.Text = ID.ToString();
            lblName.Text = _Person.FirstName + " " + _Person.SecondName + " " + _Person.ThirdName + " " + _Person.LastName;
            lblNationalNumber.Text = _Person.NationalNumber;
            if(_Person.Gender == 0 ) { lblGender.Text = "Male"; } else { lblGender.Text = "Female"; }
            lblEmail.Text = _Person.Email;
            lblAddress.Text = _Person.Address;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToShortDateString();
            lblPhone.Text = _Person.Phone;
            lblCountry.Text = _Person.NationalityCountryID.ToString();
            _LoadPersonImage();
        }
        public void LoadPersonInfo(string NationalNumber)
        {
            _Person = clsPerson.Find(NationalNumber);
            lblPersonID.Text = _Person.PersonID.ToString();
            lblName.Text = _Person.FirstName + " " + _Person.SecondName + " " + _Person.ThirdName + " " + _Person.LastName;
            lblNationalNumber.Text = _Person.NationalNumber;
            if (_Person.Gender == 0) { lblGender.Text = "Male"; } else { lblGender.Text = "Female"; }
            lblEmail.Text = _Person.Email;
            lblAddress.Text = _Person.Address;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToShortDateString();
            lblPhone.Text = _Person.Phone;
            lblCountry.Text = _Person.NationalityCountryID.ToString();
            _LoadPersonImage();

        }
        private void _LoadPersonImage()
        {
            if(_Person.ImgPath == "")
                pbPersonImage.Image = _Person.Gender == 0 ? Resources.Male_512 : Resources.Female_512;
            else 
                pbPersonImage.ImageLocation = _Person.ImgPath;
        }
        public void FillIntialValues()
        {
            lblPersonID.Text = "????";
            lblName.Text = "????";
            lblNationalNumber.Text = "????";
            lblGender.Text = "????";
            lblEmail.Text = "????";
            lblAddress.Text = "????";
            lblDateOfBirth.Text = "????";
            lblPhone.Text = "????";
            lblCountry.Text = "????";
            pbPersonImage.Image = Resources.Male_512;

        }
        private void gbPersonInfo_Enter(object sender, EventArgs e)
        {

        }

        private void pbPersonImage_Click(object sender, EventArgs e)
        {

        }
    }
}
