using Middle.Models;
using Middle.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web
{
    public partial class Default : System.Web.UI.Page
    {
        CustomerService customerService = new CustomerService();
        protected void Page_Load(object sender, EventArgs e)
        {
            UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            //gvCustomer.DataSource = customerService.GetAll();
            //gvCustomer.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string startId = txtCustomerIdStart.Text;
            string endId = txtCustomerIdEnd.Text;
            gvCustomer.DataSource = customerService.GetByIdRange(startId, endId);
            gvCustomer.DataBind();
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            divInsert.Visible = true;
            txtCustomerId.Text = string.Empty;
            txtCustomerName.Text = string.Empty;
            btnInsert.Enabled = false;
        }

        protected void btnInsertSubmit_Click(object sender, EventArgs e)
        {
            string id = txtCustomerId.Text;
            string name = txtCustomerName.Text;
            string message = string.Empty;
            lblInsertMessage.Text =  string.Empty;
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name))
            {
                lblInsertMessage.Text = "客戶代碼、名稱不得空白";
                return;
            }
            ServiceResult result = customerService.Add(id, name);
            if (result.IsSuccess)
            {
                lblInsertMessage.Text = "新增成功";
                txtCustomerId.Text = string.Empty;
                txtCustomerName.Text = string.Empty;
                btnSearch_Click(null, null);
            }
            else
            {
                lblInsertMessage.Text = result.Message;
            }
        }

        protected void btnInsertCancel_Click(object sender, EventArgs e)
        {
            divInsert.Visible = false;
            btnInsert.Enabled = true;
        }
      
    }
}