﻿using BusinessLayer;
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
    public partial class frmPersonDetails : Form
    {
        public frmPersonDetails(int ID)
        {
            InitializeComponent();
            ucPersonInformation1.LoadPersonInfo(ID);
        }

        public frmPersonDetails(string NationalNumber)
        {
            InitializeComponent();
            ucPersonInformation1.LoadPersonInfo(NationalNumber);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPersonDetails_Load(object sender, EventArgs e)
        {

        }
    }
}
