using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoterWatch.dataclasses;
using VoterWatch.logging;
using System.Reflection;

namespace useradmin
{
    public partial class Users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CancelAdd(object sender, EventArgs e)
        {
            //just clear the form
            clearForm();
        }

        protected void AddNewUser(object sender, EventArgs e)
        {
            userdata ndata = new userdata
            {
                lname = nlast.Text,
                fname = nfirst.Text,
                username = nusername.Text,
                descr = ndescr.Text
            };
            UserAdmin uadmin = new UserAdmin();
            if (uadmin.addUser(ndata) != null)
            {
                ugrid.DataBind();
                clearForm();
            }
        }

        private void clearForm()
        {
            nusername.Text = "";
            nlast.Text = "";
            nfirst.Text = "";
            ndescr.Text = "";
        }

        protected void Send_Password_Reset(object sender, EventArgs e)
        {
            string userval = ((LinkButton)sender).CommandArgument;
            try
            {
                int uid = Int32.Parse(userval);
                UserAdmin uadmin = new UserAdmin();
                uadmin.resetPassword(uid);
            }
            catch (Exception ex)
            {
                ExceptionLogger.Log(MethodBase.GetCurrentMethod(), ex, sender.ToString(), e.ToString());
            }
        }
    }
}