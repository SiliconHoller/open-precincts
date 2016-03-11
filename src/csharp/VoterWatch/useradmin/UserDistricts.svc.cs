using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using VoterWatch;
using System.ServiceModel.Activation;

namespace useradmin
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UserDistricts" in code, svc and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class UserDistricts : IUserDistricts
    {

        public bool addUserDistrict(int uid, int distid)
        {
            bool added = false;
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                //check counts
                int ecount = db.user_districts.Where(ud => ud.userid == uid && ud.districtid == distid).Count();
                if (ecount == 0)
                {
                    user_districts nud = new user_districts { userid = uid, districtid = distid };
                    db.user_districts.AddObject(nud);
                    db.SaveChanges();
                    added = true;
                }
                else if (ecount == 1)
                {
                    //already done--just return true;
                    added = true;
                }
                else
                {
                    //remove all existing, and re-add
                    List<user_districts> delist = db.user_districts.Where(ud => ud.userid == uid && ud.districtid == distid).ToList<user_districts>();
                    foreach (user_districts dud in delist) db.user_districts.DeleteObject(dud);
                    db.SaveChanges();
                    user_districts nud = new user_districts { userid = uid, districtid = distid };
                    db.user_districts.AddObject(nud);
                    db.SaveChanges();
                    added = true;
                }
            }
            catch (Exception ex)
            {
                added = false;
                //todo: logging, better error message
            }
            finally
            {
                db.Dispose();
            }

            return added;
        }

        public bool removeUserDistrict(int uid, int distid)
        {
            bool removed = false;
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                //remove all existing, and re-add
                List<user_districts> delist = db.user_districts.Where(ud => ud.userid == uid && ud.districtid == distid).ToList<user_districts>();
                foreach (user_districts dud in delist) db.user_districts.DeleteObject(dud);
                db.SaveChanges();
                removed = true;
            }
            catch (Exception ex)
            {
                removed = false;
                //todo:  logging, better error messages
            }
            finally
            {
                db.Dispose();
            }
            return removed;
        }
    }
}
