using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoterWatch;

namespace useradmin
{
    public partial class UserAppLine : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void showVals()
        {
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                int uappval;
                if (Int32.TryParse(userappid.Value, out uappval))
                {
                    var combo = db.user_apps.Where(ua => ua.userappid == uappval)
                                    .Join(db.users, a => a.userid, b => b.userid, (a, b) => new { b.emailaddress, a.appid })
                                    .Join(db.applications, a => a.appid, b => b.appid, (a, b) => new { a.emailaddress, b.appname })
                                    .Single();
                    username.Text = combo.emailaddress;
                    appname.Text = combo.appname;
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

        public string UserAppId
        {
            get
            {
                return userappid.Value;
            }
            set
            {
                userappid.Value = value;
                showVals();
            }

        }
    }
}