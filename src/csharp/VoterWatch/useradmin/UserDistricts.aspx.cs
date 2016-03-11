using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoterWatch;

namespace useradmin
{
    public partial class UserDistricts1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadUserselect();
                loadDistrictSelect();
            }
        }

        private void loadUserselect()
        {
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                userselect.Items.Add(new ListItem { Text = "", Value = "0"});
                var ulist = db.users
                                .OrderBy(u=>u.lastname)
                                .ThenBy(u=>u.firstname)
                                .Select( u=> new {u.userid, u.lastname, u.firstname, u.emailaddress});
                foreach(var u in ulist) userselect.Items.Add(new ListItem {
                    Text = String.Format("{0}, {1} - {2}", u.lastname, u.firstname, u.emailaddress),
                    Value = u.userid.ToString()
                });
            }
            catch (Exception ex)
            {

            }
            finally
            {
                db.Dispose();
            }
        }

        private void loadDistrictSelect()
        {
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                var dlist = db.districts
                                .Join(db.district_types, a => a.districttypeid, b => b.districttypeid,
                                        (a, b) => new { a.districtid, a.identifier, dname = a.name, tname = b.name })
                                .OrderBy(d => d.tname)
                                .ThenBy(d => d.dname)
                                .ThenBy(d => d.identifier);
                foreach (var d in dlist)
                {
                    ListItem opt = new ListItem
                    {
                        Text = String.Format("{0} - {1}, {2}",d.identifier, d.dname, d.tname),
                        Value = d.districtid.ToString()
                    };
                    districtselect.Items.Add(opt);
                }
                districtselect.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                db.Dispose();
            }
        }

        protected void ShowUserDistricts(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        protected void AddUserDistrict(object sender, EventArgs e)
        {
            try
            {
                int uid = Int32.Parse(userselect.SelectedValue);
                int did = Int32.Parse(districtselect.SelectedValue);
                UserDistricts udsvc = new UserDistricts();
                if (udsvc.addUserDistrict(uid, did)) RefreshGrid();
            }
            catch (Exception ex)
            {

            }
        }

        protected void DeleteUserDistrict(object sender, EventArgs e)
        {
            try
            {
                int uid = Int32.Parse(userselect.SelectedValue);
                int did = Int32.Parse(((LinkButton)sender).CommandArgument);
                UserDistricts udsvc = new UserDistricts();
                if (udsvc.removeUserDistrict(uid, did)) RefreshGrid();
            }
            catch (Exception ex)
            {

            }
        }

        protected void RefreshGrid()
        {
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                int uid = Int32.Parse(userselect.SelectedValue);
                var gridvals = db.user_districts.Where(ud => ud.userid == uid)
                                .Join(db.districts, a => a.districtid, b => b.districtid, (a, b) => b)
                                .Join(db.district_types, a => a.districttypeid, b => b.districttypeid,
                                        (a, b) => new { typename = b.name, a.districtid, a.name, a.identifier, a.descr });
                if (gridvals.Count() > 0)
                {
                    udgrid.DataSource = gridvals;
                    udgrid.DataBind();
                }
                else
                {
                    udgrid.DataSource = null;
                    udgrid.DataBind();
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