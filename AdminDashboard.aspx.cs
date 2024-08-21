﻿using System;
using System.Data;
using System.Web.UI.WebControls;

namespace ITP11AM
{
    public partial class AdminDashboard : System.Web.UI.Page
    {
        private DataTable dtEmployees;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadEmployeeList();
            }
        }

       
        private void InitializeEmployeeTable()
        {
            dtEmployees = new DataTable();
            dtEmployees.Columns.Add("EmployeeID");
            dtEmployees.Columns.Add("FirstName");
            dtEmployees.Columns.Add("LastName");
            dtEmployees.Columns.Add("Department");
            dtEmployees.Columns.Add("HireDate");

            ViewState["Employees"] = dtEmployees;
        }

        private void LoadEmployeeList()
        {
            if (ViewState["Employees"] == null)
            {
                InitializeEmployeeTable();
            }
            dtEmployees = (DataTable)ViewState["Employees"];
            gvEmployeeList.DataSource = dtEmployees;
            gvEmployeeList.DataBind();
        }

       
        protected void btnAddEmployee_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AddEmployee.aspx");
        }

     
        protected void btnManageDepartments_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ManageDepartments.aspx");
        }

      
        protected void gvEmployeeList_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            gvEmployeeList.EditIndex = e.NewEditIndex;
            LoadEmployeeList();
        }

    
        protected void gvEmployeeList_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            dtEmployees = (DataTable)ViewState["Employees"];
            GridViewRow row = gvEmployeeList.Rows[e.RowIndex];

            string employeeID = gvEmployeeList.DataKeys[e.RowIndex].Value.ToString();
            DataRow dr = dtEmployees.Select("EmployeeID='" + employeeID + "'")[0];
            dr["FirstName"] = ((TextBox)(row.Cells[1].Controls[0])).Text;
            dr["LastName"] = ((TextBox)(row.Cells[2].Controls[0])).Text;
            dr["Department"] = ((TextBox)(row.Cells[3].Controls[0])).Text;
            dr["HireDate"] = ((TextBox)(row.Cells[4].Controls[0])).Text;

            gvEmployeeList.EditIndex = -1;
            ViewState["Employees"] = dtEmployees;
            LoadEmployeeList();
        }

       
        protected void gvEmployeeList_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            gvEmployeeList.EditIndex = -1;
            LoadEmployeeList();
        }

       
        protected void gvEmployeeList_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            dtEmployees = (DataTable)ViewState["Employees"];
            string employeeID = gvEmployeeList.DataKeys[e.RowIndex].Value.ToString();
            DataRow dr = dtEmployees.Select("EmployeeID='" + employeeID + "'")[0];
            dtEmployees.Rows.Remove(dr);

            ViewState["Employees"] = dtEmployees;
            LoadEmployeeList();
        }
    }
}
