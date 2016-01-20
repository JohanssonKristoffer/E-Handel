﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_Handel
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RetrieveCartCount();
        }

        private void RetrieveCartCount()
        {
            if (Session["cartCount"] != null)
                CartCountLabel.Text = ((int) Session["cartCount"]).ToString();
        }

        protected void SendSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect($"Result.aspx?search={SearchBox.Text}");
        }

        protected void SendCategoryChoice_SelectChange(object sender, EventArgs e)
        {
            if(DropDownCategories.SelectedValue != "0")
                Response.Redirect($"Result.aspx?categoryId={DropDownCategories.SelectedValue}");
        }
    }
}