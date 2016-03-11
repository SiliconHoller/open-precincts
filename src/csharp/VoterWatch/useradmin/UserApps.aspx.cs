using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoterWatch;

namespace useradmin
{
    public partial class UserApps : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CheckAdd(object sender, EventArgs e)
        {
            int uid;
            int appid;
            if (Int32.TryParse(adduname.SelectedValue, out uid) && Int32.TryParse(addappname.SelectedValue, out appid))
            {
                voterwatchEntities db = new voterwatchEntities();
                try
                {
                    int ecount = db.user_apps.Where(ua => ua.userid == uid && ua.appid == appid).Count();
                    if (ecount == 0)
                    {
                        user_apps nua = new user_apps { userid = uid, appid = appid };
                        db.user_apps.AddObject(nua);
                        db.SaveChanges();
                        uappgrid.DataBind();
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