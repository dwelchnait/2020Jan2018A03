using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.SamplePages
{
    public partial class ListViewODSCRUD : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DeleteCheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception == null)
            {
                MessageUserControl.ShowInfo("Success", "Album Removed");
            }
            else
            {
                MessageUserControl.HandleDataBoundException(e);
            }
        }
        protected void UpdateCheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception == null)
            {
                MessageUserControl.ShowInfo("Success", "Album Updated");
            }
            else
            {
                MessageUserControl.HandleDataBoundException(e);
            }
        }
        protected void InsertCheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception == null)
            {
                MessageUserControl.ShowInfo("Success", "Album Added");
            }
            else
            {
                MessageUserControl.HandleDataBoundException(e);
            }
        }
        protected void SelectedCheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
                MessageUserControl.HandleDataBoundException(e);
        }
    }
}