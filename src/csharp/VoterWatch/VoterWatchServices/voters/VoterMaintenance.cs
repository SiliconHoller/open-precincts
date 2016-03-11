using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;
using VoterWatch;
using VoterWatch.extensions;
using System.Web;

namespace VoterWatchServices.voters
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "VoterMaintenance" in code, svc and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class VoterMaintenance : IVoterMaintenance
    {

        public VoterWatch.dataclasses.voter addVoter(VoterWatch.dataclasses.voter nvoter)
        {
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                voter nv = new voter
                {
                    statevoterid = nvoter.stateid,
                    countyvoterid = nvoter.countyid,
                    lastname = nvoter.lastname,
                    firstname = nvoter.firstname,
                    middlename = nvoter.middlename,
                    suffix = nvoter.suffix,
                    yearofbirth = nvoter.yob,
                    registrationdate = nvoter.registrationdate
                };
                if (nvoter.affiliationid != 0) nv.partyaffiliation = nvoter.affiliationid;
                try
                {
                    if (HttpContext.Current != null)
                    {
                        if (HttpContext.Current.User != null)
                        {
                            if (HttpContext.Current.User.Identity != null)
                            {
                                if (HttpContext.Current.User.Identity.Name != null)
                                {
                                    nv.modified_by = HttpContext.Current.User.Identity.Name;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                int ecount = db.voters.Where(v => v.statevoterid == nv.statevoterid).Count();
                if (ecount == 0)
                {
                    //Add the voter
                    db.voters.AddObject(nv);
                    db.SaveChanges();
                    nvoter.voterid = nv.voterid;
                }
                else if (ecount == 1)
                {
                    nvoter.voterid = db.voters.Where(v => v.statevoterid == nv.statevoterid).Single().voterid;
                }
                else
                {
                    nvoter = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.Dispose();
            }
            return nvoter;
        }

        public bool delete(VoterWatch.dataclasses.voter nvoter)
        {
            bool gone = false;


            return gone;
        }

        public bool updateVoter(VoterWatch.dataclasses.voter uvoter)
        {
            bool complete = false;
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                voter uv = db.voters.Where(v => v.voterid == uvoter.voterid).Single();
                if (uvoter.lastname != null) uv.lastname = uvoter.lastname;
                if (uvoter.firstname != null) uv.firstname = uvoter.firstname;
                if (uvoter.middlename != null) uv.middlename = uvoter.middlename;
                if (uvoter.suffix != null) uv.suffix = uvoter.suffix;
                if (uvoter.yob != 0) uv.yearofbirth = uvoter.yob;
                if (uvoter.affiliationid != 0) uv.partyaffiliation = uvoter.affiliationid;
                uv.modified_dt = DateTime.Now;
                try
                {
                    if (HttpContext.Current != null)
                    {
                        if (HttpContext.Current.User != null)
                        {
                            if (HttpContext.Current.User.Identity != null)
                            {
                                if (HttpContext.Current.User.Identity.Name != null)
                                {
                                    uv.modified_by = HttpContext.Current.User.Identity.Name;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                db.SaveChanges();
                complete = true;
            }
            catch (Exception ex)
            {
                complete = false;
            }
            finally
            {
                db.Dispose();
            }
            return complete;
        }

        public VoterWatch.dataclasses.address addAddress(VoterWatch.dataclasses.address addr)
        {
            //acts like an upsert--either way, we are going to return an address id
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                int ecount = -1;
                var addsrch = db.addresses.Select(a => a);
                if (addr.address1 != null) addsrch = addsrch.Where(a => a.address1 == addr.address1);
                if (addr.address2 != null) addsrch = addsrch.Where(a => a.address2 == addr.address2);
                if (addr.city != null) addsrch = addsrch.Where(a => a.city == addr.city);
                if (addr.state != null) addsrch = addsrch.Where(a => a.state == addr.state);
                if (addr.zip != null) addsrch = addsrch.Where(a => a.zip == addr.zip);
                if (addr.zipplus != null) addsrch = addsrch.Where(a => a.zip_plusfour == addr.zipplus);
                ecount = addsrch.Count();
                if (ecount == 0)
                {
                    address nadd = new address
                    {
                        address1 = addr.address1,
                        address2 = addr.address2,
                        city = addr.city,
                        state = addr.state,
                        zip = addr.zip,
                        zip_plusfour = addr.zipplus
                    };
                    db.addresses.AddObject(nadd);
                    try
                    {
                        if (HttpContext.Current != null)
                        {
                            if (HttpContext.Current.User != null)
                            {
                                if (HttpContext.Current.User.Identity != null)
                                {
                                    if (HttpContext.Current.User.Identity.Name != null)
                                    {
                                        nadd.modified_by = HttpContext.Current.User.Identity.Name;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    db.SaveChanges();
                    addr.addressid = nadd.address_id;
                }
                else if (ecount == 1)
                {
                    //find this one
                    addr.addressid = addsrch
                                        .Single()
                                        .address_id;
                }
                else
                {
                    //problem
                    throw new Exception("Multiple addresses meet these criteria.");
                }
            }
            catch (Exception ex)
            {
                //problem--return null
                addr = null;
            }
            finally
            {
                db.Dispose();
            }

            return addr;
        }

        public bool updateAddress(VoterWatch.dataclasses.address addr)
        {
            bool complete = false;
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                address dbaddr = db.addresses
                                    .Where(a => a.address_id == addr.addressid)
                                    .Single();
                if (dbaddr.address1 != addr.address1) dbaddr.address1 = addr.address1;
                if (dbaddr.address2 != addr.address2) dbaddr.address2 = addr.address2;
                if (dbaddr.city != addr.city) dbaddr.city = addr.city;
                if (dbaddr.state != addr.state) dbaddr.state = addr.state;
                if (dbaddr.zip != addr.zip) dbaddr.zip = addr.zip;
                if (dbaddr.zip_plusfour != addr.zipplus) dbaddr.zip_plusfour = addr.zipplus;
                dbaddr.modified_dt = DateTime.Now;
                try
                {
                    if (HttpContext.Current != null)
                    {
                        if (HttpContext.Current.User != null)
                        {
                            if (HttpContext.Current.User.Identity != null)
                            {
                                if (HttpContext.Current.User.Identity.Name != null)
                                {
                                    dbaddr.modified_by = HttpContext.Current.User.Identity.Name;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                db.SaveChanges();
                complete = true;
            }
            catch (Exception ex)
            {
                complete = false;
            }
            finally
            {
                db.Dispose();
            }
            return complete;
        }

        public bool removeAddress(VoterWatch.dataclasses.address addr)
        {
            bool complete = false;
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                List<voter_addresses> valist = db.voter_addresses.Where(va => va.addressid == addr.addressid).ToList<voter_addresses>();
                foreach (voter_addresses rva in valist) db.voter_addresses.DeleteObject(rva);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                complete = false;
            }
            finally
            {
                db.Dispose();
            }
            return complete;
        }

        public bool addVoterAddress(int vid, int addrid, VoterWatch.dataclasses.addressflags tflags)
        {
            bool added = false;
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                int ecount = db.voter_addresses.Where(va => va.voterid == vid && va.addressid == addrid).Count();
                if (ecount == 0)
                {
                    //none here--add
                    voter_addresses nva = new voter_addresses { voterid = vid, addressid = addrid, address_flags = (int)tflags };
                    try
                    {
                        if (HttpContext.Current != null)
                        {
                            if (HttpContext.Current.User != null)
                            {
                                if (HttpContext.Current.User.Identity != null)
                                {
                                    if (HttpContext.Current.User.Identity.Name != null)
                                    {
                                        nva.modified_by = HttpContext.Current.User.Identity.Name;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    db.voter_addresses.AddObject(nva);
                    db.SaveChanges();
                    added = true;
                }
                else if (ecount == 1)
                {
                    //update current
                    voter_addresses ava = db.voter_addresses.Where(va => va.voterid == vid && va.addressid == addrid).Single();
                    ava.address_flags = ava.address_flags & (int)tflags;
                    ava.modified_dt = DateTime.Now;
                    try
                    {
                        if (HttpContext.Current != null)
                        {
                            if (HttpContext.Current.User != null)
                            {
                                if (HttpContext.Current.User.Identity != null)
                                {
                                    if (HttpContext.Current.User.Identity.Name != null)
                                    {
                                        ava.modified_by = HttpContext.Current.User.Identity.Name;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    db.SaveChanges();
                    added = true;
                }
                else
                {
                    added = false;
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

            return added;
        }

        public bool removeVoterAddress(int vid, int addrid)
        {
            bool removed = false;
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                voter_addresses rva = db.voter_addresses.Where(va => va.voterid == vid && va.addressid == addrid).Single();
                db.voter_addresses.DeleteObject(rva);
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

        public bool updateVoterAddress(int vid, int addrid, VoterWatch.dataclasses.addressflags tflags)
        {
            bool updated = false;
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                voter_addresses uva = db.voter_addresses.Where(va => va.voterid == vid && va.addressid == addrid).Single();
                uva.address_flags = (int)tflags;
                uva.modified_dt = DateTime.Now;
                try
                {
                    if (HttpContext.Current != null)
                    {
                        if (HttpContext.Current.User != null)
                        {
                            if (HttpContext.Current.User.Identity != null)
                            {
                                if (HttpContext.Current.User.Identity.Name != null)
                                {
                                    uva.modified_by = HttpContext.Current.User.Identity.Name;

                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                db.SaveChanges();
                updated = true;
            }
            catch (Exception ex)
            {
                updated = false;
            }
            finally
            {
                db.Dispose();
            }

            return updated;
        }

        public bool setVoterDistrict(int vid, int distid)
        {
            bool dset = false;
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                int ecount = db.voter_districts.Where(vd => vd.voterid == vid).Count();
                if (ecount == 0)
                {
                    voter_districts nvd = new voter_districts { voterid = vid, districtid = distid };
                    try
                    {
                        if (HttpContext.Current != null)
                        {
                            if (HttpContext.Current.User != null)
                            {
                                if (HttpContext.Current.User.Identity != null)
                                {
                                    if (HttpContext.Current.User.Identity.Name != null)
                                    {
                                        nvd.modified_by = HttpContext.Current.User.Identity.Name;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    db.voter_districts.AddObject(nvd);
                    db.SaveChanges();
                }
                else if (ecount == 1)
                {
                    voter_districts uvd = db.voter_districts.Where(vd => vd.voterid == vid).Single();
                    uvd.districtid = distid;
                    uvd.modified_dt = DateTime.Now;
                    try
                    {
                        if (HttpContext.Current != null)
                        {
                            if (HttpContext.Current.User != null)
                            {
                                if (HttpContext.Current.User.Identity != null)
                                {
                                    if (HttpContext.Current.User.Identity.Name != null)
                                    {
                                        uvd.modified_by = HttpContext.Current.User.Identity.Name;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    db.SaveChanges();
                }
                else
                {
                    //multiples--wipe em out
                    List<voter_districts> avd = db.voter_districts.Where(vd => vd.voterid == vid).ToList<voter_districts>();
                    foreach (voter_districts rvd in avd) db.voter_districts.DeleteObject(rvd);
                    db.SaveChanges();
                    voter_districts nvd = new voter_districts { voterid = vid, districtid = distid };
                    try
                    {
                        if (HttpContext.Current != null)
                        {
                            if (HttpContext.Current.User != null)
                            {
                                if (HttpContext.Current.User.Identity != null)
                                {
                                    if (HttpContext.Current.User.Identity.Name != null)
                                    {
                                        nvd.modified_by = HttpContext.Current.User.Identity.Name;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    db.voter_districts.AddObject(nvd);
                    db.SaveChanges();
                }
                dset = true;
            }
            catch (Exception ex)
            {
                dset = false;
            }
            finally
            {
                db.Dispose();
            }
            return dset;
        }
    }
}
