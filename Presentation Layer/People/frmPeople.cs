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

namespace DVLD
{
    public partial class frmPeople : Form
    {

        public frmPeople()
        {
            InitializeComponent();
        }

        private static DataTable _dtable = clsPerson.ListAllPeople();

        private DataTable Peoplelist = _dtable.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                       "FirstName", "SecondName", "ThirdName", "LastName",
                                                       "GenderCaption", "DateOfBirth", "CountryName",
                                                       "Phone", "Email");
        private void _RefreshPeopleList()
        {

            _dtable = clsPerson.ListAllPeople();

            Peoplelist = _dtable.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                       "FirstName", "SecondName", "ThirdName", "LastName",
                                                       "GenderCaption", "DateOfBirth", "CountryName",
                                                       "Phone", "Email");

            dgvListPeople.DataSource = Peoplelist;
            lblNumOfRecords.Text = dgvListPeople.RowCount.ToString();

         }

        private void _AllignColumns()
        {

            if (dgvListPeople.Rows.Count > 0)
            {

                dgvListPeople.Columns[0].HeaderText = "Person ID";
                dgvListPeople.Columns[0].Width = 90;

                dgvListPeople.Columns[1].HeaderText = "National No.";
                dgvListPeople.Columns[1].Width = 90;


                dgvListPeople.Columns[2].HeaderText = "First Name";
                dgvListPeople.Columns[2].Width = 90;

                dgvListPeople.Columns[3].HeaderText = "Second Name";
                dgvListPeople.Columns[3].Width = 90;


                dgvListPeople.Columns[4].HeaderText = "Third Name";
                dgvListPeople.Columns[4].Width = 90;

                dgvListPeople.Columns[5].HeaderText = "Last Name";
                dgvListPeople.Columns[5].Width = 90;

                dgvListPeople.Columns[6].HeaderText = "Gender";
                dgvListPeople.Columns[6].Width = 55;

                dgvListPeople.Columns[7].HeaderText = "Date Of Birth";
                dgvListPeople.Columns[7].Width = 140;

                dgvListPeople.Columns[8].HeaderText = "Nationality";
                dgvListPeople.Columns[8].Width = 85;


                dgvListPeople.Columns[9].HeaderText = "Phone";
                dgvListPeople.Columns[9].Width = 90;


                dgvListPeople.Columns[10].HeaderText = "Email";
                dgvListPeople.Columns[10].Width = 170;
            }
        }
        private void frmPeople_Load(object sender, EventArgs e)
        {
            _RefreshPeopleList();
            _AllignColumns();
            cbPeopleFilter.SelectedIndex = 0;
        }

        private void msiShowDetails_Click(object sender, EventArgs e)
        {
            int _PersonID = int.Parse(dgvListPeople.CurrentRow.Cells[0].Value.ToString());
            frmPersonDetails frmPersonDetails = new frmPersonDetails(_PersonID);
            frmPersonDetails.ShowDialog();

        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddNewPerson frm = new frmAddNewPerson();
            frm.ShowDialog();
            _RefreshPeopleList();

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int _PersonID = int.Parse(dgvListPeople.SelectedRows[0].Cells[0].Value.ToString());
            frmAddNewPerson frm = new frmAddNewPerson(_PersonID);
            frm.ShowDialog();
            _RefreshPeopleList();

        }

        private void sendSMSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature will be applied later", "Next Updates", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewPerson frm = new frmAddNewPerson();

            frm.ShowDialog();

            _RefreshPeopleList();

        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this person?", "Delete Person", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (clsPerson.DeletePerson(int.Parse(dgvListPeople.CurrentRow.Cells[0].Value.ToString())))
                {
                    MessageBox.Show("Successfully deleted", "Delete Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to delete", "Delete Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature will be applied later", "Next Updates", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtFilterPeople_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtFilterPeople_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch(cbPeopleFilter.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "National Number":
                    FilterColumn = "NationalNo";
                    break;

                case "First Name":
                    FilterColumn = "FirstName";
                    break;

                case "Second Name":
                    FilterColumn = "SecondName";
                    break;

                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;

                case "Last Name":
                    FilterColumn = "LastName";
                    break;

                case "Nationality":
                    FilterColumn = "CountryName";
                    break;

                case "Gender":
                    FilterColumn = "GendorCaption";
                    break;

                case "Phone":
                    FilterColumn = "Phone";
                    break;

                case "Email":
                    FilterColumn = "Email";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterPeople.Text.Trim() == "" || FilterColumn == "None")
            {
                Peoplelist.DefaultView.RowFilter = "";
                lblNumOfRecords.Text = dgvListPeople.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "PersonID")
                //in this case we deal with integer not string.

                Peoplelist.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterPeople.Text.Trim());
            else
                Peoplelist.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterPeople.Text.Trim());

            lblNumOfRecords.Text = dgvListPeople.Rows.Count.ToString();
        }

        private void cbPeopleFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterPeople.Visible = (cbPeopleFilter.Text != "None");

            if (txtFilterPeople.Visible)
            {
                txtFilterPeople.Text = "";
                txtFilterPeople.Focus();
            }
        }
    }
}
