using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoterWatch.logging;
using System.Reflection;
using VoterWatch;

namespace useradmin
{
    public partial class Applications : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CancelAdd(object sender, EventArgs e)
        {
            clearForm();
        }

        private void clearForm()
        {
            nappname.Text = "";
            ndisplay.Text = "";
            ndescr.Text = "";
            adderror.Text = "";
            adderror.Visible = false;
        }

        protected void AddNewApp(object sender, EventArgs e)
        {
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                //check for this role name
                int ecount = db.applications.Where(r => r.appname == nappname.Text).Count();
                if (ecount == 0)
                {
                    application napp = new application
                    {
                        appname = nappname.Text,
                        displayname = ndisplay.Text,
                        descr = ndescr.Text,
                        seq = 0
                    };
                    db.applications.AddObject(napp);
                    db.SaveChanges();
                    clearForm();
                    appgrid.DataBind();
                }
                else
                {
                    adderror.Visible = true;
                    adderror.Text = "This application already exists.  Rename or use the existing application.";
                }
            }
            catch (Exception ex)
            {
                adderror.Visible = true;
                adderror.Text = "There was a problem adding the application.";
                ExceptionLogger.Log(MethodBase.GetCurrentMethod(), ex, nappname.Text, ndescr.Text);
            }
            finally
            {
                db.Dispose();
            }
        }
    }
}