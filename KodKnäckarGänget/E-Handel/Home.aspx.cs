using E_Handel.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_Handel
{
    public partial class Home : System.Web.UI.Page
    {
        List<BLDiscountProduct> discountList = new List<BLDiscountProduct>;

        protected void Page_Load(object sender, EventArgs e)
        {
            RetrieveDiscountList();
            SelectRandomDiscounts();
        }

        private void SelectRandomDiscounts()
        {
            throw new NotImplementedException();
        }

        private void RetrieveDiscountList()
        {
            throw new NotImplementedException();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void AdRotator2_AdCreated(object sender, AdCreatedEventArgs e)
        {

        }
    }
}