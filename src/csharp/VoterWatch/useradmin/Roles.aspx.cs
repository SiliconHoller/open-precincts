using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoterWatch;
using VoterWatch.logging;
using System.Reflection;

namespace useradmin
{
    public partial class Roles : System.Web.UI.Page
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
            nrolename.Text = "";
            ndescr.Text = "";
            adderror.Text = "";
            adderror.Visible = false;
        }

        protected void AddNewRole(object sender, EventArgs e)
        {
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                //check for this role name
                int ecount = db.roles.Where(r => r.rolename == nrolename.Text).Count();
                if (ecount == 0)
                {
                    role nr = new role
                    {
                        rolename = nrolename.Text,
                        descr = ndescr.Text,
                        seq = 0
                    };
                    db.roles.AddObject(nr);
                    db.SaveChanges();
                    clearForm();
                    rgrid.DataBind();
                }
                else
                {
                    adderror.Visible = true;
                    adderror.Text = "This role already exists.  Rename or use the existing role.";
                }
            }
            catch (Exception ex)
            {
                adderror.Visible = true;
                adderror.Text = "There was a problem adding the role.";
                ExceptionLogger.Log(MethodBase.GetCurrentMethod(), ex, nrolename.Text, ndescr.Text);
            }
            finally
            {
                db.Dispose();
            }
        }
    }
}