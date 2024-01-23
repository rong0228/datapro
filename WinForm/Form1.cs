using Middle.Models;
using Middle.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm
{
    public partial class Form1 : Form
    {
        CustomerService customerService = new CustomerService();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            pnlInsert.Visible = true;
            btnInsert.Enabled = false;
            txtCustomerId.Text = string.Empty;
            txtCustomerName.Text = string.Empty;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string startId = txtCustomerIdStart.Text;
            string endId = txtCustomerIdEnd.Text;
            IEnumerable<Customer> customers = customerService.GetByIdRange(startId, endId);
            gvCustomer.DataSource = customers;
        }

        private void btnInsertCancel_Click(object sender, EventArgs e)
        {
            pnlInsert.Visible = false;
            btnInsert.Enabled = true;
        }

        private void btnInsertSubmit_Click(object sender, EventArgs e)
        {
            string id = txtCustomerId.Text;
            string name = txtCustomerName.Text;

            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name))
            {
                MessageBox.Show("客戶代碼、名稱不得空白");
                return;
            }
            ServiceResult result = customerService.Add(id, name);
            if (result.IsSuccess)
            {

                txtCustomerId.Text = string.Empty;
                txtCustomerName.Text = string.Empty;
                btnSearch_Click(null, null);
                MessageBox.Show("新增成功");
            }
            else
            {
                MessageBox.Show(result.Message);
            }
        }
    }
}
