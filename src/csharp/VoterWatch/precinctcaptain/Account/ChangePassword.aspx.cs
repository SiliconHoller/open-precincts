using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoterWatch;
using System.Web.Security;
using VoterWatch.auth;
using VoterWatch.logging;
using System.Reflection;

namespace precinctcaptain.Account
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            userid.Value = Request.Params["userid"];
            resetval.Value = Request.Params["resetidentifier"];
            try
            {
                loadUserData();
            }
            catch (Exception ex)
            {
                ExceptionLogger.Log(MethodBase.GetCurrentMethod(), ex, Request.Params["userid"], Request.Params["resetidentifier"]);
                resettable.Visible = false;
                errordisplay.Visible = true;
                errorval.Text = "There was a problem--please try again later or call your system administrator.";
            }
        }

        protected void Check_And_Reset(object sender, EventArgs e)
        {
            try
            {
                if (npassword1.Text == npassword2.Text)
                {
                    PasswordOps pops = new PasswordOps();
                    int uid = Int32.Parse(userid.Value);
                    pops.setNewPassword(uid, npassword1.Text);
                    Response.Redirect("ChangePasswordSuccess.aspx", true);
                }
                else
                {
                    errordisplay.Visible = true;
                    errorval.Text = "The passwords do not match.";
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.Log(MethodBase.GetCurrentMethod(), ex, sender, e);
                resettable.Visible = false;
                errordisplay.Visible = true;
                errorval.Text = "There was a problem--please try again later or call your system administrator.";
            }
            
        }

        protected void CancelReset(object sender, EventArgs e)
        {
            Response.Redirect("ChangePasswordCancelled.aspx", true);
        }

        private void loadUserData()
        {
            int uid = Int32.Parse(userid.Value);
            voterwatchEntities db = new voterwatchEntities();
            
            user udata = db.users.Where(u => u.userid == uid).Single();
            if (Membership.ValidateUser(udata.emailaddress, resetval.Value))
            {
                usernamelabel.Text = udata.emailaddress;
            }
            else
            {
                resettable.Visible = false;
                errordisplay.Visible = true;
                errorval.Text = "Invalid Reset value.  Please contact your administrator.";
                //TODO:  Log a bad attempt here
            }
            db.Dispose();
        }
    }
}
