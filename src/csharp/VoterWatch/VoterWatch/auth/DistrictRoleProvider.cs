using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Reflection;
using VoterWatch.logging;

namespace VoterWatch.auth
{
    public class DistrictRoleProvider : RoleProvider
    {
        protected static string appname;
        protected static int appid;

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            //Not implemented for the general auth system
        }

        public override string ApplicationName
        {
            get
            {
                return appname;
            }
            set
            {
                appname = value;
                setAppId();
            }
        }

        public override void CreateRole(string roleName)
        {
            //not implemented for the general auth system
            //voterwatchEntities db = new voterwatchEntities();
            //try
            //{
            //    //get the count--don't create multiples of the same name
            //    int ecount = db.roles.Where(r => r.rolename == roleName).Count();
            //    if (ecount == 0)
            //    {
            //        role nr = new role
            //        {
            //            rolename = roleName,
            //            seq = 100,
            //            descr = String.Format("Created by DistrictRoleProvider on {0}",DateTime.Now)
            //        };
            //        db.roles.AddObject(nr);
            //        db.SaveChanges();
            //    }
            //    else
            //    {
            //        //we're going to throw an exception
            //        throw new Exception(String.Format("Role '{0}' already exists in available roles.", roleName));
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ExceptionLogger.Log(MethodBase.GetCurrentMethod(), ex, roleName);
            //}
            //finally
            //{
            //    db.Dispose();
            //}
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            bool deleted = false;
            //Not implemented for the general auth system
            //voterwatchEntities db = new voterwatchEntities();
            //try
            //{
            //    role dr = db.roles.Where(r => r.rolename == roleName).Single();
            //    db.roles.DeleteObject(dr);
            //    db.SaveChanges();
            //}
            //catch (Exception ex)
            //{
            //    deleted = false;
            //    ExceptionLogger.Log(MethodBase.GetCurrentMethod(), ex, roleName, throwOnPopulatedRole);
            //}
            //finally
            //{
            //    db.Dispose();
            //}
            return deleted;
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            List<string> urlist = new List<string>();
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                urlist = db.applications.Where(a => a.appname == roleName)
                                    .Join(db.user_apps, a => a.appid, b => b.appid, (a, b) => b)
                                    .Join(db.users, a => a.userid, b => b.userid, (a, b) => b)
                                    .Where(u => u.emailaddress.Contains(usernameToMatch))
                                    .OrderBy(u => u.lastname)
                                    .ThenBy(u=>u.firstname)
                                    .Select(u => u.emailaddress)
                                    .ToList<string>();

            }
            catch (Exception ex)
            {
                urlist = new List<string>();
                ExceptionLogger.Log(MethodBase.GetCurrentMethod(), ex, roleName, usernameToMatch);
            }
            finally
            {
                db.Dispose();
            }
            return urlist.ToArray<string>();
        }

        public override string[] GetAllRoles()
        {
            List<string> rlist = new List<string>();
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                rlist = db.applications.OrderBy(r => r.appname).Select(r => r.appname).ToList<string>();
            }
            catch (Exception ex)
            {
                rlist = new List<string>();
                ExceptionLogger.Log(MethodBase.GetCurrentMethod(), ex, new object[] { });
            }
            finally
            {
                db.Dispose();
            }
            return rlist.ToArray<string>();
        }

        public override string[] GetRolesForUser(string username)
        {
            List<string> uroles = new List<string>();
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                uroles = db.users.Where(u => u.emailaddress == username)
                            .Join(db.user_apps, a => a.userid, b => b.userid, (a, b) => b)
                            .Join(db.applications, a => a.appid, b => b.appid, (a, b) => b)
                            .OrderBy(a => a.appname)
                            .Select(r => r.appname)
                            .Distinct()
                            .ToList<string>();
            }
            catch (Exception ex)
            {
                uroles = new List<string>();
                ExceptionLogger.Log(MethodBase.GetCurrentMethod(), ex, username);
            }
            finally
            {
                db.Dispose();
            }

            return uroles.ToArray<string>();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            List<string> unames = new List<string>();
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                unames = db.applications.Where(r => r.appname == roleName)
                            .Join(db.user_apps, a => a.appid, b => b.appid, (a, b) => b)
                            .Join(db.users, a => a.userid, b => b.userid, (a, b) => b)
                            .OrderBy(u => u.lastname)
                            .ThenBy(u=>u.firstname)
                            .Select(u => u.emailaddress)
                            .ToList<string>();
            }
            catch (Exception ex)
            {
                unames = new List<string>();
                ExceptionLogger.Log(MethodBase.GetCurrentMethod(), ex, roleName);
            }
            finally
            {
                db.Dispose();
            }
            return unames.ToArray<string>();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            bool ur = false;
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                int uid = db.users.Where(u => u.emailaddress == username).Single().userid;
                int rid = db.applications.Where(r => r.appname == roleName).Single().appid;
                ur = db.user_apps.Where(aur => aur.userid == uid && aur.appid == rid).Count() > 0;
            }
            catch (Exception ex)
            {
                ur = false;
                ExceptionLogger.Log(MethodBase.GetCurrentMethod(), ex, username, roleName);
            }
            finally
            {
                db.Dispose();
            }
            return ur;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            //Not implemented for the general auth system
            //voterwatchEntities db = new voterwatchEntities();
            //try
            //{
            //    List<int> uidlist = db.users.Where(u => usernames.Contains(u.emailaddress)).Select(u => u.userid).ToList<int>();
            //    List<int> ridlist = db.roles.Where(r => roleNames.Contains(r.rolename)).Select(r => r.roleid).ToList<int>();
            //    int ecount;
            //    foreach (int uid in uidlist)
            //    {
            //        foreach (int rid in ridlist)
            //        {
            //            try
            //            {
            //                ecount = db.app_user_roles.Where(aur => aur.appid == appid && aur.userid == uid && aur.roleid == rid).Count();
            //                if (ecount > 0)
            //                {
            //                    //remove them
            //                    List<app_user_roles> dellist = db.app_user_roles.Where(aur => aur.appid == appid && aur.userid == uid && aur.roleid == rid).ToList<app_user_roles>();
            //                    foreach(app_user_roles delaur in dellist) db.app_user_roles.DeleteObject(delaur);
            //                    db.SaveChanges();
            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //                ExceptionLogger.Log(MethodBase.GetCurrentMethod(), ex, usernames, roleNames);
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ExceptionLogger.Log(MethodBase.GetCurrentMethod(), ex, usernames, roleNames);
            //}
            //finally
            //{
            //    db.Dispose();
            //}
        }

        public override bool RoleExists(string roleName)
        {
            bool rexists = false;
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                rexists = db.applications.Where(r => r.appname == roleName).Count() == 1;
            }
            catch (Exception ex)
            {
                rexists = false;
                ExceptionLogger.Log(MethodBase.GetCurrentMethod(), ex, roleName);
            }
            finally
            {
                db.Dispose();
            }
            return rexists;
        }

        private void setAppId()
        {
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                appid = db.applications.Where(a => a.appname == appname).Single().appid;
            }
            catch (Exception ex)
            {
                appid = 0;
                ExceptionLogger.Log(MethodBase.GetCurrentMethod(), ex, new object[] { });
            }
            finally
            {
                db.Dispose();
            }
        }
    }
}
