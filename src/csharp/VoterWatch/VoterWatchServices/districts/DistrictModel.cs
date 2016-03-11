using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using VoterWatch;
using VoterWatch.extensions;
using System.ServiceModel.Activation;

namespace VoterWatchServices.districts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DistrictModel" in code, svc and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class DistrictModel : IDistrictModel
    {

        public VoterWatch.dataclasses.districttype addType(VoterWatch.dataclasses.districttype ntype)
        {
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                VoterWatch.district_types addtype = new district_types
                {
                    name = ntype.name,
                    descr = ntype.description
                };
                db.district_types.AddObject(addtype);
                db.SaveChanges();
                ntype.districttypeid = addtype.districttypeid;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                db.Dispose();
            }
            return ntype;
        }


        public VoterWatch.dataclasses.districttype updateType(VoterWatch.dataclasses.districttype udata)
        {
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                VoterWatch.district_types utype = db.district_types.Where(t => t.districttypeid == udata.districttypeid).Single();
                utype.name = udata.name;
                utype.descr = udata.description;
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


        public VoterWatch.dataclasses.district addDistrict(VoterWatch.dataclasses.district ndistrict)
        {
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                VoterWatch.district nd = new VoterWatch.district
                {
                    districttypeid = ndistrict.districttypeid,
                    identifier = ndistrict.identifier,
                    name = ndistrict.name,
                    descr = ndistrict.description
                };
                db.districts.AddObject(nd);
                db.SaveChanges();
                ndistrict.districtid = nd.districtid;
            }
            catch (Exception ex)
            {
            }
            finally
            {
                db.Dispose();
            }
            return ndistrict;
        }

        public VoterWatch.dataclasses.district updateDistrict(VoterWatch.dataclasses.district udata)
        {
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                VoterWatch.district udistrict = db.districts.Where(d => d.districtid == udata.districtid).Single();
                udistrict.identifier = udata.identifier;
                udistrict.name = udata.name;
                udistrict.descr = udata.description;
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


        public bool deleteDistrict(VoterWatch.dataclasses.district deldata)
        {
            bool completed = false;
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                district deldist = db.districts.Where(d => d.districtid == deldata.districtid).Single();
                db.districts.DeleteObject(deldist);
                db.SaveChanges();
                completed = true;
            }
            catch (Exception ex)
            {
                completed = false;
            }
            finally
            {
                db.Dispose();
            }
            return completed;
        }

        public bool addParent(VoterWatch.dataclasses.districtrelationship addrel)
        {
            bool completed = false;
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                int ecount = db.district_relationships.Where(dr => dr.districtid == addrel.childdistrict && dr.parentdistrict == addrel.parentdistrict).Count();
                if (ecount == 0)
                {
                    //we add...otherwise, it's already there
                    district_relationships pcr = new district_relationships { districtid = addrel.childdistrict, parentdistrict = addrel.parentdistrict };
                    db.district_relationships.AddObject(pcr);
                    db.SaveChanges();
                }
                completed = true;
            }
            catch (Exception ex)
            {
                completed = false;
            }
            finally
            {
                db.Dispose();
            }
            return completed;
        }

        public bool removeParent(VoterWatch.dataclasses.districtrelationship remrel)
        {
            bool completed = false;
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                district_relationships delrel = db.district_relationships.Where(dr => dr.districtid == remrel.childdistrict && dr.parentdistrict == remrel.parentdistrict).Single();
                db.district_relationships.DeleteObject(delrel);
                db.SaveChanges();
                completed = true;
            }
            catch (Exception ex)
            {
                completed = false;
            }
            finally
            {
                db.Dispose();
            }
            return completed;
        }



        public bool removeParents(int childid)
        {
            bool completed = false;
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                List<district_relationships> dlist = db.district_relationships.Where(d => d.districtid == childid).ToList<district_relationships>();
                foreach (district_relationships dr in dlist)
                {
                    db.district_relationships.DeleteObject(dr);
                }
                db.SaveChanges();
                completed = true;
            }
            catch (Exception ex)
            {
                completed = false;
            }
            finally
            {
                db.Dispose();
            }
            return completed;
        }

        public bool removeChildren(int parentid)
        {
            bool completed = false;
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                List<int> childlist = db.district_relationships
                                        .Where(dr => dr.parentdistrict == parentid)
                                        .Select(dr => dr.districtid)
                                        .ToList<int>();
                foreach (int childid in childlist) removeParent(new VoterWatch.dataclasses.districtrelationship { childdistrict = childid, parentdistrict = parentid });
            }
            catch (Exception ex)
            {

            }
            finally
            {
                db.Dispose();
            }

            return completed;
        }


        public List<VoterWatch.dataclasses.district> getRoots()
        {
            List<VoterWatch.dataclasses.district> rlist = new List<VoterWatch.dataclasses.district>();
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                //Get the children district ids
                List<int> children = db.district_relationships.Select(dr => dr.districtid).Distinct().ToList<int>();
                //Roots are all districts that are not children
                var roots = db.districts.Where(d => !children.Contains(d.districtid));
                foreach (VoterWatch.district r in roots) rlist.Add(r.DataContract());
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

        public List<VoterWatch.dataclasses.district> getLeafs()
        {
            List<VoterWatch.dataclasses.district> leaflist = new List<VoterWatch.dataclasses.district>();
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                //Get the parent district ids
                List<int> parents = db.district_relationships.Select(dr => dr.parentdistrict).Distinct().ToList<int>();
                //Roots are all districts that are not children
                var leafs = db.districts.Where(d => !parents.Contains(d.districtid));
                foreach (VoterWatch.district l in leafs) leaflist.Add(l.DataContract());
            }
            catch (Exception ex)
            {

            }
            finally
            {
                db.Dispose();
            }
            return leaflist;
        }

        public List<VoterWatch.dataclasses.district> getOrphans()
        {
            List<VoterWatch.dataclasses.district> orphanlist = new List<VoterWatch.dataclasses.district>();
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                //List of districts that are either parent or child or both
                List<int> parents = db.district_relationships.Select(dr => dr.parentdistrict).Distinct().ToList<int>();
                List<int> children = db.district_relationships.Select(dr => dr.districtid).Distinct().ToList<int>();
                parents.AddRange(children);
                List<int> family = parents.Distinct().ToList<int>();
                //Orphans are neither
                var orphans = db.districts.Where(d => !family.Contains(d.districtid));
                foreach (VoterWatch.district o in orphans) orphanlist.Add(o.DataContract());
            }
            catch (Exception ex)
            {

            }
            finally
            {
                db.Dispose();
            }
            return orphanlist;
        }
    }
}
