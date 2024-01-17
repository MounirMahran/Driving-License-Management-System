using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.People.Controls
{
    public partial class ctrlPersonInfoWithFilter : UserControl
    {
        //Define a custom event handler with parameters
        public event Action<int> OnPersonSelected;

        //Create a protected method with parameter to raise the event
        protected virtual void PersonSelected(int PersonID)
        {
            Action <int> handler = OnPersonSelected;

            if (handler != null)
            {
                handler(PersonID);
            }
        }
        
        private bool _ShowAddPerson = true;
        public bool ShowAddPerson
        {
            get { return _ShowAddPerson; }
            set
            {
                _ShowAddPerson = value;
                btnAddNewPerson.Visible = _ShowAddPerson;
            }
        }
        private bool _EnableFilter = true;
        public bool EnableFilter
        {
            get { return _EnableFilter; }
            set 
            { 
                _EnableFilter = value; 
                gbFilter.Enabled = _EnableFilter;
            }
        }
        private int _PersonID;
        public int PersonID
        {
            get
            {
                return ucPersonInformation1.PersonID;
            }
        }

        private string _NationalNumber;
        public clsPerson Person
        {
            get { return ucPersonInformation1.PersonInformation; }
        }
        public ctrlPersonInfoWithFilter()
        {
            InitializeComponent();
        }
        enum _enSearchMode { PersonIDMode, NationalNumMode}

        _enSearchMode Mode;
        private void _Search()
        {
            Mode = cbFilterBy.SelectedIndex == 0 ? _enSearchMode.PersonIDMode : _enSearchMode.NationalNumMode;

            switch (Mode)
            {
                case _enSearchMode.PersonIDMode:
                    try
                    {
                        if (txtFilterBy.Text == "")
                        {
                            MessageBox.Show("Please enter a valid ID or Naitonal Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            _PersonID = int.Parse(txtFilterBy.Text);
                            if (!clsPerson.PersonExists(_PersonID))
                            {
                                MessageBox.Show("Person with that ID does not exist", "Person Does Not Exist", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                return;
                            }
                            else
                            {
                                ucPersonInformation1.LoadPersonInfo(_PersonID);
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Not a valid ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;

                case _enSearchMode.NationalNumMode:
                    try
                    {
                        if (txtFilterBy.Text == "")
                        {
                            MessageBox.Show("Please enter a valid ID or Naitonal Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            if (!clsPerson.PersonExistsByNationalNumber(_NationalNumber))
                            {
                                MessageBox.Show("Person with that National Number does not exist", "Person Does Not Exist", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                return;
                            }
                            else
                            {
                                ucPersonInformation1.LoadPersonInfo(_NationalNumber);
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Not a valid national number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;

            }

            if(OnPersonSelected != null && EnableFilter)
            {
                OnPersonSelected(ucPersonInformation1.PersonID);
            }
        }
        private void ucPersonInformation1_Load(object sender, EventArgs e)
        {

        }
        private void ctrlPersonInfoWithFilter_Load(object sender, EventArgs e)
        {
            ucPersonInformation1.FillIntialValues();
            cbFilterBy.SelectedIndex = 0;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            _Search();
        }
        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddNewPerson frm = new frmAddNewPerson();
            frm.DataBack += _DataBackEvent;
            frm.ShowDialog();
        }
        private void _DataBackEvent(object sender, int PersonID)
        {
            cbFilterBy.SelectedIndex = 0;
            cbFilterBy.Text = PersonID.ToString();
            ucPersonInformation1.LoadPersonInfo(PersonID);
        }
        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {

        }
        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                btnSearch.PerformClick();
            }

            if(cbFilterBy.Text == "Person ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }
    }
}
