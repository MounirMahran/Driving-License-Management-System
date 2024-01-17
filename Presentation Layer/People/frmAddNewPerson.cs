using BusinessLayer;
using DVLD.GeneralClasses;
using DVLD.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace DVLD
{
    public partial class frmAddNewPerson : Form
    {
        public delegate void DataBackEventHandler(object sender, int PersonID);
        public event DataBackEventHandler DataBack;

        private enum enMode { AddNewMode, UpdateMode };

        enMode _Mode;
        clsPerson _Person = new clsPerson();
        int _PersonID = -1;
        public frmAddNewPerson()
        {
            InitializeComponent();
            _Mode = enMode.AddNewMode;
        }
        public frmAddNewPerson(int PersonID)
        {
            InitializeComponent();

            _Mode = enMode.UpdateMode;
            _PersonID = PersonID;
        }
        private void _savePerson()
        {
            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.SecondName = txtSecondName.Text.Trim();
            _Person.ThirdName = txtThirdName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();
            _Person.Email = txtEmail.Text.Trim();
            _Person.Phone = txtPhone.Text.Trim();
            _Person.NationalNumber = txtNationalNumber.Text.Trim();
            _Person.Address = txtAddress.Text.Trim();
            _Person.DateOfBirth = dtpDateOfBirth.Value;
            _Person.NationalityCountryID = cbCountries.SelectedIndex + 1;
            if (rbMale.Checked) _Person.Gender = 0;
            else _Person.Gender = 1;
            if (pbPersonImage.ImageLocation != null)
                _Person.ImgPath = pbPersonImage.ImageLocation;
            else _Person.ImgPath = "";
            _Mode = enMode.UpdateMode;

            if (_Person.Save())
            {
                _Mode = enMode.UpdateMode;
                lblEditPersonTitle.Text = "Update Person";
                lblPersonID.Text = _Person.PersonID.ToString();
                MessageBox.Show("Person saved successfully", "Add New Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataBack?.Invoke(this, _Person.PersonID);
            }
            else
            {
                MessageBox.Show("Failed to save person", "Add New Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void _FillCountryCB()
        {
            /*cbCountries.DataSource = clsCountry.ListAllCountries();
            cbCountries.DisplayMember = "CountryName";
            cbCountries.SelectedIndex = cbCountries.FindStringExact("Egypt");*/

            DataTable Countries = clsCountry.ListAllCountries();

            foreach (DataRow Country in Countries.Rows)
            {
                cbCountries.Items.Add(Country["CountryName"]);
            }

        }
        private void _FillIntitialValues()
        {
            if (_Mode == enMode.AddNewMode) lblEditPersonTitle.Text = "Add New Person";
            else lblEditPersonTitle.Text = "Update Person";
            lblPersonID.Text = "";
            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";
            txtNationalNumber.Text = "";
            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);
            rbMale.Checked = true;
            _FillCountryCB();
            cbCountries.SelectedIndex = cbCountries.FindStringExact("Egypt");
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
            pbGender.Image = Resources.Male_512;
            btnRemoveImage.Visible = (pbPersonImage.ImageLocation != null);

        }   
        private void _LoadPersonData()
        {
            _Person = clsPerson.Find(_PersonID);

            
            lblPersonID.Text = _PersonID.ToString();
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.LastName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;
            txtNationalNumber.Text = _Person.NationalNumber;
            dtpDateOfBirth.Value = _Person.DateOfBirth;
            if (_Person.Gender == 0)
            {
                rbMale.Checked = true;
                pbGender.Image = Resources.Male_512;
            }
            else 
            {
                rbFemale.Checked = true;
                pbGender.Image = Resources.Female_512;
            }

            if(_Person.ImgPath != "")
            {
                pbPersonImage.ImageLocation = _Person.ImgPath;
                btnRemoveImage.Visible = true;
            }

            cbCountries.SelectedIndex = _Person.NationalityCountryID - 1;
            txtPhone.Text = _Person.Phone;
            txtEmail.Text = _Person.Email;
            txtAddress.Text = _Person.Address;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some Fields are not valid, please check them out", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }

            if (!_HandlePersonImage()) MessageBox.Show("Failed to handle person image", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            _savePerson();
        }
        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            pbPersonImage.Image = Properties.Resources.Female_512;
        }
        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {

            pbPersonImage.Image = Properties.Resources.Male_512;

        }
        private void frmAddNewPerson_Load(object sender, EventArgs e)
        {
            _FillIntitialValues();

            if(_Mode == enMode.UpdateMode)
                _LoadPersonData();
            
        }
        private bool _IsValidEmail(string EmailAddress)
        {
            // Define a regular expression pattern for a simple email validation
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // Create a Regex object with the pattern
            Regex regex = new Regex(pattern);

            // Use the regex.IsMatch method to check if the email matches the pattern
            return regex.IsMatch(EmailAddress);
        }
        private void _ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox Temp = ((TextBox)sender);

                if (string.IsNullOrEmpty(Temp.Text.Trim()))
                {
                    e.Cancel = true;
                    errorprovider.SetError(Temp, "This field is required");
                    return;
                }
                else
                {
                    errorprovider.SetError(Temp, null);
                }
            }

            else if(sender is RichTextBox)
            {
                RichTextBox Temp = ((RichTextBox)sender);
                
                if (string.IsNullOrEmpty(Temp.Text.Trim()))
                {
                    e.Cancel = true;
                    errorprovider.SetError(Temp, "This field is required");
                    return;
                }
                else
                {
                    errorprovider.SetError(Temp, null);
                }

            }
        } 
        private void dtpDateOfBirth_DropDown(object sender, EventArgs e)
        {

            dtpDateOfBirth.MaxDate = DateTime.Today.AddYears(-18);
            //dtpDateOfBirth.Value = MinDate;

        }
        private void dtpDateOfBirth_ValueChanged(object sender, EventArgs e)
        {
        }
        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            //Don't apply it if the email is not provided
            if (txtEmail.Text == "") return;

            if (!_IsValidEmail(txtEmail.Text.Trim()))
            {
                e.Cancel = true;
                errorprovider.SetError(txtEmail, "Please enter a valid email");

            }
            else
            {
                errorprovider.SetError(txtEmail, null);
            }
        }
        private void txtNationalNumber_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txtNationalNumber.Text.Trim()))
            {
                e.Cancel = true;
                errorprovider.SetError(txtNationalNumber, "This field is required!");
                return;
            }
            else
            {
                errorprovider.SetError(txtNationalNumber, null);
            }

            if (txtNationalNumber.Text.Trim() != _Person.NationalNumber && clsPerson.PersonExistsByNationalNumber(txtNationalNumber.Text.Trim()))
            {
                e.Cancel = true;
                errorprovider.SetError(txtNationalNumber, "National number exists, enter a valid one");
            }
            else
            {
                errorprovider.SetError(txtNationalNumber, null);
            }
        }
        private void txtFirstName_Validating(object sender, CancelEventArgs e)
        {
            txtFirstName.Validating += _ValidateEmptyTextBox;
        }
        private void txtSecondName_Validating(object sender, CancelEventArgs e)
        {
            txtSecondName.Validating += _ValidateEmptyTextBox;
        }
        private void txtLastName_Validating(object sender, CancelEventArgs e)
        {
            txtLastName.Validating += _ValidateEmptyTextBox;

        }
        private void txtPhone_Validating(object sender, CancelEventArgs e)
        {
            txtPhone.Validating += _ValidateEmptyTextBox;

        }
        private void txtAddress_Validating(object sender, CancelEventArgs e)
        {
            txtAddress.Validating += _ValidateEmptyTextBox;

        }
        private void btnSetImage_Click(object sender, EventArgs e)
        {

            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string SelectedFilePath = openFileDialog1.FileName;
                pbPersonImage.Load(SelectedFilePath);
                btnRemoveImage.Visible = true;
            }


        } 
        private bool _HandlePersonImage()
        {
            string OldImageLocation = _Person.ImgPath;
            string NewImageLocation = pbPersonImage.ImageLocation;

            if (OldImageLocation != NewImageLocation)
            {
                if(OldImageLocation != "") File.Delete(OldImageLocation);
                
                if (NewImageLocation == null) return true;
                
                if (clsUtility.CopyImageToFolder(ref NewImageLocation))
                {
                    pbPersonImage.ImageLocation = NewImageLocation;
                    _Person.ImgPath = NewImageLocation;
                }
            }

            return true;

        }
        private void btnRemoveImage_Click(object sender, EventArgs e)
        {
            pbPersonImage.ImageLocation = null;

            if (rbMale.Checked) pbPersonImage.Image = Resources.Male_512;
            else pbPersonImage.Image = Resources.Female_512;

            btnRemoveImage.Visible = false;
        }
    }
}
