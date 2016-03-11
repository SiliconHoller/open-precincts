using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoterWatch;

namespace precinctcaptain
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                setAppLinks();
            }
        }

        protected void setAppLinks()
        {
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                List<application> uapps = db.users
                                            .Where(u => u.emailaddress == HttpContext.Current.User.Identity.Name)
                                            .Join(db.user_apps, a => a.userid, b => b.userid, (a, b) => b)
                                            .Join(db.applications, a => a.appid, b => b.appid, (a, b) => b)
                                            .OrderBy(a=>a.seq)
                                            .Distinct()
                                            .ToList<application>();
                if (uapps.Count > 0)
                {
                    //Add a tools menu
                    MenuItem tmenu = new MenuItem { Text = "Tools", ToolTip = "Additional available utilities" };
                    foreach (application app in uapps)
                    {
                        MenuItem applink = new MenuItem();
                        applink.Text = app.displayname;
                        applink.ToolTip = app.descr;
                        applink.NavigateUrl = String.Format("/{1}", HttpContext.Current.Request.ApplicationPath, app.appname);
                        tmenu.ChildItems.Add(applink);
                    }
                    NavigationMenu.Items.Add(tmenu);
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
