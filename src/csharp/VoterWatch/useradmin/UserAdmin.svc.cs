using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using VoterWatch;
using VoterWatch.extensions;
using VoterWatch.auth;
using System.Web;
using System.ServiceModel.Activation;

namespace useradmin
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UserAdmin" in code, svc and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class UserAdmin : IUserAdmin
    {

        public List<VoterWatch.dataclasses.userdata> getUsers()
        {
            List<VoterWatch.dataclasses.userdata> ulist = new List<VoterWatch.dataclasses.userdata>();
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                var uline = db.users
                            .OrderBy(u => u.lastname)
                            .ThenBy(u => u.firstname)
                            .ThenBy(u => u.emailaddress);
                foreach (user u in uline)
                {
                    ulist.Add(u.DataContract());
                }
            }
            catch (Exception ex)
            {
                //todo:logging
            }
            finally
            {
                db.Dispose();
            }
            return ulist;
        }

        public VoterWatch.dataclasses.userdata addUser(VoterWatch.dataclasses.userdata udata)
        {
            bool added = false;
            Exception addex = null;
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                //first, find this email address, if it exists
                int ecount = db.users.Where(u => u.emailaddress == udata.username).Count();
                if (ecount == 0)
                {
                    //new user--add
                    user nu = new user
                    {
                        emailaddress = udata.username,
                        lastname = udata.lname,
                        firstname = udata.fname,
                        descr = udata.descr
                    };
                    db.users.AddObject(nu);
                    db.SaveChanges();
                    //fire off password reset
                    resetPassword(nu.userid);
                    udata.userid = nu.userid;
                    added = true;
                }
                else if (ecount == 1)
                {
                    //find this guy and return the user info
                    user eu = db.users.Where(u => u.emailaddress == udata.username).Single();
                    udata = new VoterWatch.dataclasses.userdata
                    {
                        userid = eu.userid,
                        lname = eu.lastname,
                        fname = eu.firstname,
                        username = eu.emailaddress
                    };
                    added = true;
                }
                
            }
            catch (Exception ex)
            {
                added = false;
            }
            finally
            {
                db.Dispose();
            }
            if (!added) udata = null;
            return udata;
        }

        public VoterWatch.dataclasses.userdata updateUser(VoterWatch.dataclasses.userdata udata)
        {
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                user dbu = db.users.Where(u => u.userid == udata.userid).Single();
                dbu.lastname = udata.lname;
                dbu.firstname = udata.fname;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                udata = null;
            }
            finally
            {
                db.Dispose();
            }
            return udata;
        }

        public bool deleteuser(int uid)
        {
            bool done = false;
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                //clear from application permissions
                List<app_user_roles> uroles = db.app_user_roles.Where(aur => aur.userid == uid).ToList<app_user_roles>();
                foreach (app_user_roles drole in uroles) db.app_user_roles.DeleteObject(drole);
                db.SaveChanges();
                //remove from districts
                List<user_districts> udists = db.user_districts.Where(ud => ud.userid == uid).ToList<user_districts>();
                foreach (user_districts ddist in udists) db.user_districts.DeleteObject(ddist);
                db.SaveChanges();
                //remove from user
                user duser = db.users.Where(u => u.userid == uid).Single();
                db.users.DeleteObject(duser);
                db.SaveChanges();
                done = true;
            }
            catch (Exception ex)
            {
                done = false;
                //todo:logging, better status messages
            }
            finally
            {
                db.Dispose();
            }

            return done;
        }

        public bool resetPassword(int uid)
        {
            bool isreset = false;
            try
            {
                //fire off password reset
                PasswordOps pops = new PasswordOps();
                string resetpage = String.Format("{0}://{1}/Account/ChangePassword.aspx", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Host);
                pops.resetPassword(uid, resetpage);
                isreset = true;
            }
            catch (Exception ex)
            {
                isreset = false;
            }
            return isreset;
        }


        public List<VoterWatch.dataclasses.roledata> getRoles()
        {
            List<VoterWatch.dataclasses.roledata> rlist = new List<VoterWatch.dataclasses.roledata>();
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                var rvals = db.roles.OrderBy(r => r.rolename);
                foreach (role r in rvals) rlist.Add(r.DataContract());
            }
            catch (Exception ex)
            {
                //log it, throw it
            }
            finally
            {
                db.Dispose();
            }
            return rlist;
        }

        public VoterWatch.dataclasses.roledata addRole(VoterWatch.dataclasses.roledata rdata)
        {
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                int ecount = db.roles.Where(r => r.rolename == rdata.name).Count();
                if (ecount == 0)
                {
                    //Add it
                    role nrole = new role
                    {
                        rolename = rdata.name,
                        descr = rdata.descr,
                        seq = rdata.seq
                    };
                    db.roles.AddObject(nrole);
                    db.SaveChanges();
                    rdata.roleid = nrole.roleid;
                }
                else
                {
                    throw new Exception("This role already exists.");
                }
            }
            catch (Exception ex)
            {
                //something went wrong--clear out the return val
                rdata = null;
            }
            finally
            {
                db.Dispose();
            }

            return rdata;
        }

        public VoterWatch.dataclasses.roledata updateRole(VoterWatch.dataclasses.roledata rdata)
        {
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                role urole = db.roles.Where(r => r.roleid == rdata.roleid).Single();
                urole.rolename = rdata.name;
                urole.descr = rdata.descr;
                urole.seq = rdata.seq;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                rdata = null;
            }
            finally
            {
                db.Dispose();
            }
            return rdata;
        }

        public bool deleteRole(int roleid)
        {
            bool done = false;
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                role drole = db.roles.Where(r => r.roleid == roleid).Single();
                //clear out the user/app combos
                List<app_user_roles> aurlist = db.app_user_roles.Where(aur => aur.roleid == roleid).ToList<app_user_roles>();
                foreach (app_user_roles daur in aurlist) db.app_user_roles.DeleteObject(daur);
                db.SaveChanges();
                //Clear out the role
                db.roles.DeleteObject(drole);
                db.SaveChanges();
                done = true;
            }
            catch (Exception ex)
            {
                done = false;
            }
            finally
            {
                db.Dispose();
            }
            return done;
        }

        public List<VoterWatch.dataclasses.appdata> getApps()
        {
            List<VoterWatch.dataclasses.appdata> alist = new List<VoterWatch.dataclasses.appdata>();
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                var avals = db.applications.OrderBy(a => a.appname);
                foreach (application app in avals) alist.Add(app.DataContract());
            }
            catch (Exception ex)
            {

            }
            finally
            {
                db.Dispose();
            }
            return alist;
        }

        public VoterWatch.dataclasses.appdata addApp(VoterWatch.dataclasses.appdata adata)
        {
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                int ecount = db.applications.Where(a => a.appname == adata.name).Count();
                if (ecount == 0)
                {
                    application napp = new application
                    {
                        appname = adata.name,
                        descr = adata.descr,
                        seq = adata.seq
                    };
                    db.applications.AddObject(napp);
                    db.SaveChanges();
                    adata.appid = napp.appid;
                }
                else
                {
                    throw new Exception("An application with this name already exists.");
                }
            }
            catch (Exception ex)
            {
                adata = null;
                //todo:  logit, return error
            }
            finally
            {
                db.Dispose();
            }
            return adata;
        }

        public VoterWatch.dataclasses.appdata updateApp(VoterWatch.dataclasses.appdata adata)
        {
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                application uapp = db.applications.Where(a => a.appid == adata.appid).Single();
                uapp.appname = adata.name;
                uapp.descr = adata.descr;
                uapp.seq = adata.seq;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                adata = null;
            }
            finally
            {
                db.Dispose();
            }
            return adata;
        }

        public bool deleteApp(int appid)
        {
            bool done = false;
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                application dapp = db.applications.Where(a => a.appid == appid).Single();
                List<app_user_roles> daur = db.app_user_roles.Where(a => a.appid == appid).ToList<app_user_roles>();
                foreach (app_user_roles del in daur) db.app_user_roles.DeleteObject(del);
                db.SaveChanges();
                db.applications.DeleteObject(dapp);
                db.SaveChanges();
                done = true;
            }
            catch (Exception ex)
            {
                done = false;
            }
            finally
            {
                db.Dispose();
            }
            return done;
        }

        public List<VoterWatch.dataclasses.userdata> getAppUsers(int appid)
        {
            List<VoterWatch.dataclasses.userdata> ulist = new List<VoterWatch.dataclasses.userdata>();
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                var uvals = db.app_user_roles.Where(a => a.appid == appid)
                                .Join(db.users, a => a.userid, b => b.userid, (a, b) => b)
                                .OrderBy(u => u.lastname)
                                .ThenBy(u => u.firstname);
                foreach (user u in uvals) ulist.Add(u.DataContract());
            }
            catch (Exception ex)
            {

            }
            finally
            {
                db.Dispose();
            }
            return ulist;
        }

        public List<VoterWatch.dataclasses.roledata> getRoles(int appid, int userid)
        {
            List<VoterWatch.dataclasses.roledata> rlist = new List<VoterWatch.dataclasses.roledata>();
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                var rvals = db.app_user_roles.Where(a => a.appid == appid && a.userid == userid)
                                .Join(db.roles, a => a.roleid, b => b.roleid, (a, b) => b)
                                .OrderBy(r => r.rolename);
                foreach (role r in rvals) rlist.Add(r.DataContract());
            }
            catch (Exception ex)
            {

            }
            finally
            {
                db.Dispose();
            }
            return rlist;
        }

        public List<VoterWatch.dataclasses.appdata> getApps(int userid)
        {
            List<VoterWatch.dataclasses.appdata> alist = new List<VoterWatch.dataclasses.appdata>();
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                var avals = db.app_user_roles.Where(a => a.userid == userid)
                            .Join(db.applications, a => a.appid, b => b.appid, (a, b) => b)
                            .OrderBy(a => a.appname);
                foreach (application app in avals) alist.Add(app.DataContract());
            }
            catch (Exception ex)
            {

            }
            finally
            {
                db.Dispose();
            }
            return alist;
        }

        public bool addUserRole(int appid, int userid, int roleid)
        {
            bool added = false;
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                app_user_roles naur = new app_user_roles { appid = appid, roleid = roleid, userid = userid };
                db.app_user_roles.AddObject(naur);
                db.SaveChanges();
                added = true;
            }
            catch (Exception ex)
            {
                added = false;
            }
            finally
            {
                db.Dispose();
            }

            return added;
        }

        public bool removeUserRole(int appid, int userid, int roleid)
        {
            bool removed = false;
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                app_user_roles del = db.app_user_roles.Where(a => a.appid == appid && a.userid == userid && a.roleid == roleid).Single();
                db.app_user_roles.DeleteObject(del);
                db.SaveChanges();
                removed = true;
            }
            catch (Exception ex)
            {
                removed = false;
            }
            finally
            {
                db.Dispose();
            }
            return removed;
        }
    }
}
