using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Linq.Expressions;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using System.ServiceModel.Activation;
using VoterWatch.dataclasses;
using VoterWatch.extensions;
using VoterWatch;
using System.Web;
using VoterWatch.logging;
using System.Reflection;

namespace hierarchies
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "VotingDistricts" in code, svc and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class VotingDistricts : IVotingDistricts
    {

        public List<districttype> getTypes()
        {
            List<districttype> tlist = new List<districttype>();
            VoterWatch.voterwatchEntities db = new VoterWatch.voterwatchEntities();
            try
            {
                var tvals = db.district_types.OrderBy(dt => dt.name);
                foreach (var typeval in tvals) tlist.Add(new districttype { districttypeid = typeval.districttypeid, name = typeval.name, description = typeval.descr });
            }
            catch (Exception ex)
            {
                ExceptionLogger.Log(MethodBase.GetCurrentMethod(), ex, new object[] { });
            }
            finally
            {
                db.Dispose();
            }
            return tlist;
        }

        public List<VoterWatch.dataclasses.district> getDistricts(int typeid, string skip, string take)
        {
            List<VoterWatch.dataclasses.district> dlist = new List<VoterWatch.dataclasses.district>();
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                int? skipval = null;
                if (!String.IsNullOrEmpty(skip) && !String.IsNullOrWhiteSpace(skip))
                {
                    try
                    {
                        skipval = Int32.Parse(skip);
                    }
                    catch (Exception ex)
                    {
                        skipval = null;
                    }
                }

                int? takeval = null;
                if (!String.IsNullOrEmpty(take) && !String.IsNullOrWhiteSpace(take))
                {
                    try
                    {
                        takeval = Int32.Parse(take);
                    }
                    catch (Exception ex)
                    {
                        takeval = null;
                    }
                }

                //search the types
                var matchdtypes = db.districts
                                    .Where(d => d.districttypeid == typeid)
                                    .OrderBy(d=>d.name)
                                    .Select(d => d);
                if (skipval.HasValue) matchdtypes = matchdtypes.Skip(skipval.Value);
                if (takeval.HasValue) matchdtypes = matchdtypes.Take(takeval.Value);
                foreach (VoterWatch.district md in matchdtypes)
                {
                    dlist.Add(md.DataContract());
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.Log(MethodBase.GetCurrentMethod(), ex, typeid, skip, take);
            }
            finally
            {
                db.Dispose();
            }

            return dlist;
        }

        public List<VoterWatch.dataclasses.district> getAllDistricts()
        {
            List<VoterWatch.dataclasses.district> dlist = new List<VoterWatch.dataclasses.district>();
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                var ad = db.districts
                            .OrderBy(d => d.name);
                foreach (VoterWatch.district d in ad) dlist.Add(d.DataContract());
            }
            catch (Exception ex)
            {
                ExceptionLogger.Log(MethodBase.GetCurrentMethod(), ex, new object[] { });
            }
            finally
            {
                db.Dispose();
            }
            return dlist;
        }

        public List<VoterWatch.dataclasses.district> getParents(int childid)
        {
            List<VoterWatch.dataclasses.district> dlist = new List<VoterWatch.dataclasses.district>();
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                var parents = db.district_relationships
                                .Where(r => r.districtid == childid)
                                .Join(db.districts, a => a.parentdistrict, b => b.districtid, (a, b) => b)
                                .OrderBy(p => p.name);
                foreach (VoterWatch.district par in parents)
                {
                    dlist.Add(par.DataContract());
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.Log(MethodBase.GetCurrentMethod(), ex, childid);
            }
            finally
            {
                db.Dispose();
            }
            return dlist;
        }

        public List<VoterWatch.dataclasses.district> getChildren(int parentid)
        {
            List<VoterWatch.dataclasses.district> dlist = new List<VoterWatch.dataclasses.district>();
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                var children = db.district_relationships
                                .Where(r => r.parentdistrict == parentid)
                                .Join(db.districts, a => a.districtid, b => b.districtid, (a, b) => b)
                                .OrderBy(c => c.name);
                foreach (VoterWatch.district child in children)
                {
                    dlist.Add(child.DataContract());
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.Log(MethodBase.GetCurrentMethod(), ex, parentid);
            }
            finally
            {
                db.Dispose();
            }
            return dlist;
        }



        public districtsearch searchDistricts(string typeid, string ident, string name, string descr, string skip, string take, int sseq)
        {
            //pull parameters;
            int? typeval = null;
            if (checkNull(typeid))
            {
                try
                {
                    typeval = Int32.Parse(typeid);
                }
                catch (Exception ex)
                {
                    typeval = null;
                }
            }

            int? skipval = null;
            if (checkNull(skip))
            {
                try
                {
                    skipval = Int32.Parse(skip);
                }
                catch (Exception ex)
                {
                    skipval = null;
                }
            }

            int? takeval = null;
            if (checkNull(take))
            {
                try
                {
                    takeval = Int32.Parse(take);
                }
                catch (Exception ex)
                {
                    takeval = null;
                }
            }

            districtsearch sres = new districtsearch { seq = sseq, skip = skipval.HasValue ? skipval.Value : 0, take = takeval.HasValue ? takeval.Value : 0 };

            voterwatchEntities db = new voterwatchEntities();
            try
            {
                var dsearch = db.districts.OrderBy(d=>d.identifier).Select(d => d);

                if (typeval.HasValue) dsearch = dsearch.Where(d => d.districttypeid == typeval.Value);
                if (checkNull(ident)) dsearch = dsearch.Where(d => d.identifier.Contains(ident));
                if (checkNull(name)) dsearch = dsearch.Where(d => d.name.Contains(name));
                if (checkNull(descr)) dsearch = dsearch.Where(d => d.descr.Contains(descr));
                if (skipval.HasValue) dsearch = dsearch.Skip(skipval.Value);
                if (takeval.HasValue) dsearch = dsearch.Take(takeval.Value);

                foreach (VoterWatch.district dres in dsearch)
                {
                    sres.dresults.Add(dres.DataContract());
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.Log(MethodBase.GetCurrentMethod(), ex, typeid, ident, name, descr, skip, take, sseq);
            }
            finally
            {
                db.Dispose();
            }

            return sres;
        }

        private bool checkNull(string sval)
        {
            return !String.IsNullOrEmpty(sval) && !String.IsNullOrWhiteSpace(sval);
        }


        public List<VoterWatch.dataclasses.district> userDistricts()
        {
            List<VoterWatch.dataclasses.district> dlist = new List<VoterWatch.dataclasses.district>();
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                string uname = HttpContext.Current.User.Identity.Name;
                var dvals = db.users.Where(u => u.emailaddress == uname)
                                    .Join(db.user_districts, a => a.userid, b => b.userid, (a, b) => b)
                                    .Join(db.districts, a => a.districtid, b => b.districtid, (a, b) => b)
                                    .Join(db.district_types, a=>a.districttypeid, b=>b.districttypeid, 
                                        (a,b)=> new {
                                            a.districtid,
                                            a.name,
                                            a.identifier,
                                            a.districttypeid,
                                            a.descr,
                                            tname = b.name
                                        })
                                    .OrderBy(d => d.name);
                foreach (var d in dvals)
                {
                    VoterWatch.dataclasses.district ud = new VoterWatch.dataclasses.district
                    {
                        districtid = d.districtid,
                        name = d.name,
                        description = d.descr,
                        districttype = d.tname,
                        districttypeid = d.districttypeid,
                        identifier = d.identifier
                    };
                    dlist.Add(ud);
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.Log(MethodBase.GetCurrentMethod(), ex, new object[] {});
            }
            finally
            {
                db.Dispose();
            }
            return dlist;
        }
    }
}
