using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoterWatch;

namespace precinctcaptain
{
    public partial class About : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                loadUserData();
            }
        }

        private void loadUserData()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                string uname = HttpContext.Current.User.Identity.Name;
                voterwatchEntities db = new voterwatchEntities();
                try
                {
                    user usr = db.users.Where(u => u.emailaddress == uname).Single();
                    fname.Text = usr.firstname;
                    lname.Text = usr.lastname;
                    loginname.Text = usr.emailaddress;
                    description.Text = usr.descr;
                    //load up application roles
                    var uarlist = db.user_apps
                                        .Where(aur => aur.userid == usr.userid)
                                        .Join(db.applications, a => a.appid, b => b.appid, (a, b) => new { b.appid, b.appname })
                                        .OrderBy(uar => uar.appname);
                                        
                    foreach (var uarval in uarlist)
                    {
                        TableRow arrow = new TableRow();
                        TableCell appcell = new TableCell { Text = uarval.appname};
                        arrow.Cells.Add(appcell);
                        approletable.Rows.Add(arrow);
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    db.Dispose();
                }
            }
        }
    }
}
