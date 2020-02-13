using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.SamplePages
{
    public partial class AccessingDataSetControls : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e);
        }

        protected void TrackListGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            string msg = null;
            GridViewRow agrow = TrackListGV.Rows[TrackListGV.SelectedIndex];
            string albumName = agrow.Cells[0].Text; //first column BoundField
            int milliseconds = int.Parse(agrow.Cells[1].Text); //second column BoundField
            int size = int.Parse((agrow.FindControl("Bytes") as Label).Text); //third column Template Label
            decimal price = decimal.Parse((agrow.FindControl("UnitPrice") as TextBox).Text); //third column Template TextBox
            msg = "GridView Name is: " + albumName + " M/S is: " + milliseconds.ToString() +
                " Size is: " + size.ToString() + " Price is: " + price.ToString();
            MessageUserControl.ShowInfo(msg);
        }

        protected void WalkThroughGV_Click(object sender, EventArgs e)
        {
            int trackcount = 0;
            int albumplaytime = 0;
            int albumsize = 0;
            decimal albumprice = 0m;
            string msg = null;
            foreach (GridViewRow item in TrackListGV.Rows)
            {
                trackcount += 1;
                albumplaytime += int.Parse(item.Cells[1].Text);
                albumsize += int.Parse((item.FindControl("Bytes") as Label).Text);
                albumprice += decimal.Parse((item.FindControl("UnitPrice") as TextBox).Text);
            }
            msg = "GridView Totals: # of tracks is: " + trackcount.ToString() + " M/S is: " + albumplaytime.ToString() +
              " Size is: " + albumsize.ToString() + " Price is: " + albumprice.ToString();
            MessageUserControl.ShowInfo(msg);
        }

        protected void WalkThroughLV_Click(object sender, EventArgs e)
        {
            int trackcount = 0;
            int albumplaytime = 0;
            int albumsize = 0;
            decimal albumprice = 0m;
            string msg = null;
            foreach (ListViewDataItem item in TrackListLV.Items)
            {
                trackcount += 1;
                albumplaytime += int.Parse((item.FindControl("Milliseconds") as Label).Text);
                albumsize += int.Parse((item.FindControl("Bytes") as Label).Text);
                albumprice += decimal.Parse((item.FindControl("UnitPrice") as TextBox).Text);
            }
            msg = "ListView Totals: # of tracks is: " + trackcount.ToString() + " M/S is: " + albumplaytime.ToString() +
              " Size is: " + albumsize.ToString() + " Price is: " + albumprice.ToString();
            MessageUserControl.ShowInfo(msg);
        }

        protected void TrackListLV_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string msg = null;
            ListViewDataItem alrow = e.Item as ListViewDataItem;
            string albumName = (alrow.FindControl("Name") as Label).Text; //first column BoundField
            int milliseconds = int.Parse((alrow.FindControl("Milliseconds") as Label).Text); //second column BoundField
            int size = int.Parse((alrow.FindControl("Bytes") as Label).Text); //third column Template Label
            decimal price = decimal.Parse((alrow.FindControl("UnitPrice") as TextBox).Text); //third column Template TextBox
            msg = "ListView Name is: " + albumName + " M/S is: " + milliseconds.ToString() +
                " Size is: " + size.ToString() + " Price is: " + price.ToString();
            MessageUserControl.ShowInfo(msg);
        }
    }
}