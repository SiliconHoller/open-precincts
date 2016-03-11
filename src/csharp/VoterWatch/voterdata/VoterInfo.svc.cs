using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;
using VoterWatch.dataclasses;
using VoterWatch.extensions;
using System.Linq.Expressions;

namespace voterdata
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "VoterInfo" in code, svc and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class VoterInfo : IVoterInfo
    {

        public votersearch searchVoters(string stateid, string countyid, string districtid, string lastname, string firstname, string middlename, string suffix, string yob, string skip, string take, int seq)
        {
            votersearch sres = null;
            VoterWatch.voterwatchEntities db = new VoterWatch.voterwatchEntities();
            try
            {
                int _skip = 0;
                try
                {
                    if (skip.nonEmpty()) _skip = Int32.Parse(skip);
                }
                catch (Exception ex)
                {
                    _skip = 0;
                }
                int _take = 50;
                try
                {
                    if (take.nonEmpty()) _take = Int32.Parse(take);
                }
                catch (Exception ex)
                {
                    _take = 50;
                }
                int? _yob = null;
                try
                {
                    if (yob.nonEmpty()) _yob = Int32.Parse(yob);
                }
                catch (Exception ex)
                {
                    _yob = null;
                }
                int? _dist = null;
                try
                {
                    if (districtid.nonEmpty()) _dist = Int32.Parse(districtid);
                }
                catch (Exception ex)
                {
                    _dist = null;
                }
                var vsearch = db.voters.Select(v => v);
                if (stateid.nonEmpty()) vsearch = vsearch.Where(v => v.statevoterid.Contains(stateid));
                if (countyid.nonEmpty()) vsearch = vsearch.Where(v => v.countyvoterid.Contains(countyid));
                if (lastname.nonEmpty()) vsearch = vsearch.Where(v => v.lastname.Contains(lastname));
                if (firstname.nonEmpty()) vsearch = vsearch.Where(v => v.firstname.Contains(firstname));
                if (middlename.nonEmpty()) vsearch = vsearch.Where(v => v.middlename.Contains(middlename));
                if (suffix.nonEmpty()) vsearch = vsearch.Where(v => v.suffix.Contains(suffix));
                if (_yob.HasValue) vsearch = vsearch.Where(v => v.yearofbirth == _yob.Value);
                if (_dist.HasValue)
                {
                    vsearch = vsearch.Join(db.voter_districts, a => a.voterid, b => b.voterid, (a, b) => new { a, did = b.districtid })
                                .Where(vd => vd.did == _dist.Value)
                                .Select(vd => vd.a);
                }
                sres = new votersearch { seq = seq, skip = _skip, take = _take };
                var vres = vsearch.Skip(_skip).Take(_take);
                foreach (VoterWatch.voter fv in vres) sres.vresults.Add(fv.DataContract());
            }
            catch (Exception ex)
            {
                sres = null;
            }
            finally
            {
                db.Dispose();
            }
            return sres;
        }

        public addresssearch searchAddresses(string addrid, string addr1, string addr2, string city, string state, string zip, string z4, string skip, string take, int seq)
        {
            addresssearch sres = null;
            VoterWatch.voterwatchEntities db = new VoterWatch.voterwatchEntities();
            try
            {
                int? _addid = null;
                try
                {
                    if (addrid.nonEmpty()) _addid = Int32.Parse(addrid);
                }
                catch (Exception ex)
                {
                    _addid = null;
                }
                int _skip = 0;
                try
                {
                    if (skip.nonEmpty()) _skip = Int32.Parse(skip);
                }
                catch (Exception ex)
                {
                    _skip = 0;
                }
                int _take = 50;
                try
                {
                    if (take.nonEmpty()) _take = Int32.Parse(take);
                }
                catch (Exception ex)
                {
                    _take = 50;
                }
                var ares = db.addresses.Select(a => a);
                if (_addid.HasValue)
                {
                    ares = ares.Where(a => a.address_id == _addid.Value);
                }
                else
                {
                    if (addr1.nonEmpty()) ares = ares.Where(a => a.address1.Contains(addr1));
                    if (addr2.nonEmpty()) ares = ares.Where(a => a.address2.Contains(addr2));
                    if (city.nonEmpty()) ares = ares.Where(a => a.city.Contains(city));
                    if (state.nonEmpty()) ares = ares.Where(a => a.state.Contains(state));
                    if (zip.nonEmpty()) ares = ares.Where(a => a.zip.Contains(zip));
                    if (z4.nonEmpty()) ares = ares.Where(a => a.zip_plusfour.Contains(z4));
                }
                sres = new addresssearch { seq = seq, skip = _skip, take = _take };
                var addlist = ares.OrderBy(a => a.address1).Skip(_skip).Take(_take);
                foreach (VoterWatch.address fadd in addlist) sres.aresults.Add(fadd.DataContract());
            }
            catch (Exception ex)
            {
                sres = null;
            }
            finally
            {
                db.Dispose();
            }

            return sres;
        }

        public voter getVoter(int vid)
        {
            voter v = null;
            VoterWatch.voterwatchEntities db = new VoterWatch.voterwatchEntities();
            try
            {
                v = db.voters.Where(vtbl => vtbl.voterid == vid).Single().DataContract();
            }
            catch (Exception ex)
            {
                v = null;
            }
            finally
            {
                db.Dispose();
            }
            return v;
        }

        public List<address> getAddresses(int vid)
        {
            List<address> alist = new List<address>();
            VoterWatch.voterwatchEntities db = new VoterWatch.voterwatchEntities();
            try
            {
                var vadds = db.voter_addresses
                                .Where(va => va.voterid == vid)
                                .Join(db.addresses, a => a.addressid, b => b.address_id, (a, b) => b)
                                .OrderBy(add => add.city);
                foreach (VoterWatch.address va in vadds) alist.Add(va.DataContract());
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

        public district getPollDistrict(int vid)
        {
            district dval = null;
            VoterWatch.voterwatchEntities db = new VoterWatch.voterwatchEntities();
            try
            {
                dval = db.voter_districts
                            .Where(vd => vd.voterid == vid)
                            .Join(db.districts, a => a.districtid, b => b.districtid, (a, b) => b)
                            .Single()
                            .DataContract();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                db.Dispose();
            }
            return dval;
        }

        public List<district> getVotingDistricts(int vid)
        {
            List<district> dlist = new List<district>();
            VoterWatch.voterwatchEntities db = new VoterWatch.voterwatchEntities();
            try
            {
                //build the list of districts that a voter is connected to
                List<int> didlist = new List<int>();
                List<int> cidlist = db.voter_districts.Where(vd => vd.voterid == vid).Select(v => v.districtid).ToList<int>();
                while (cidlist.Count > 0)
                {
                    int currchild = cidlist[0];
                    try
                    {
                        List<int> parids = db.district_relationships.Where(c => c.districtid == currchild).Select(dr => dr.parentdistrict).ToList<int>();
                        var unseen = parids.Except(cidlist);
                        foreach (int parid in unseen) cidlist.Add(parid);
                    }
                    catch (Exception ex)
                    {

                    }
                    if (!didlist.Contains(currchild)) didlist.Add(currchild);
                    cidlist.RemoveAt(0);
                    
                }
                //turn each of the districtids into a district
                var fds = db.districts
                        .Where(d => didlist.Contains(d.districtid))
                        .OrderBy(d=>d.districttypeid);
                foreach (VoterWatch.district fd in fds) dlist.Add(fd.DataContract());

            }
            catch (Exception ex)
            {

            }
            finally
            {
                db.Dispose();
            }
            return dlist;
        }

        public List<voter> getDistrictVoters(int districtid, string affid, string skip, string take)
        {
            //pull the parameters
            int? _affid = null;
            try
            {
                _affid = Int32.Parse(affid);
            }
            catch (Exception ex)
            {
                _affid = null;
            }
            int? _skip = null;
            try
            {
                _skip = Int32.Parse(skip);
            }
            catch (Exception ex)
            {
                _skip = null;
            }
            int? _take = null;
            try
            {
                _take = Int32.Parse(take);
            }
            catch (Exception ex)
            {
                _take = null;
            }
            //pull the list
            List<voter> vlist = new List<voter>();
            VoterWatch.voterwatchEntities db = new VoterWatch.voterwatchEntities();
            try
            {
                //Load up the party hash table
                Dictionary<int, string> ptable = new Dictionary<int, string>();
                var plist = db.affiliations;
                foreach (var p in plist)
                {
                    try
                    {
                        ptable.Add(p.affiliationid, p.name);
                    }
                    catch (Exception ex)
                    {

                    }
                }
                var vsrch = db.voter_districts
                                .Where(vd => vd.districtid == districtid)
                                .Join(db.voters, a => a.voterid, b => b.voterid,
                                    (a, b) => b)
                                .OrderBy(v => v.lastname)
                                .ThenBy(v=>v.firstname)
                                .Select(v=>v);

                if (_affid.HasValue) vsrch = vsrch.Where(v => v.partyaffiliation == _affid.Value);
                if (_skip.HasValue) vsrch = vsrch.Skip(_skip.Value);
                if (_take.HasValue) vsrch = vsrch.Take(_take.Value);
                foreach (var fv in vsrch)
                {
                    VoterWatch.dataclasses.voter v = new VoterWatch.dataclasses.voter
                    {
                        voterid = fv.voterid,
                        stateid = fv.statevoterid,
                        countyid = fv.countyvoterid,
                        lastname = fv.lastname,
                        firstname = fv.firstname,
                        middlename = fv.middlename,
                        suffix = fv.suffix,
                        yob = fv.yearofbirth,
                        registrationdate = fv.registrationdate,
                        affiliationid = fv.partyaffiliation.HasValue ? fv.partyaffiliation.Value : 0
                    };
                    if (ptable.ContainsKey(v.affiliationid)) v.partyaffiliation = ptable[v.affiliationid];
                    vlist.Add(v);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                db.Dispose();
            }
            return vlist;
        }

        public votersearch getAddressVoters(int addrid)
        {
            votersearch sres = new votersearch();
            VoterWatch.voterwatchEntities db = new VoterWatch.voterwatchEntities();
            try
            {
                var addvs = db.addresses
                                        .Where(a => a.address_id == addrid)
                                        .Join(db.voter_addresses, a => a.address_id, b => b.addressid, (a, b) => b)
                                        .Join(db.voters, a => a.voterid, b => b.voterid, (a, b) => b)
                                        .OrderBy(v => v.lastname);
                foreach (VoterWatch.voter av in addvs) sres.vresults.Add(av.DataContract());
            }
            catch (Exception ex)
            {

            }
            finally
            {
                db.Dispose();
            }
            return sres;
        }


        public List<affiliation> getParties()
        {
            List<affiliation> plist = new List<affiliation>();
            VoterWatch.voterwatchEntities db = new VoterWatch.voterwatchEntities();
            try
            {
                var pvals = db.affiliations
                            .OrderBy(p => p.name);
                foreach (VoterWatch.affiliation p in pvals) plist.Add(p.DataContract());
            }
            catch (Exception ex)
            {

            }
            finally
            {
                db.Dispose();
            }
            return plist;
        }


        public int getRegisteredVoters(int districtid)
        {
            int vcount = 0;
            VoterWatch.voterwatchEntities db = new VoterWatch.voterwatchEntities();
            try
            {
                vcount = db.voter_districts
                            .Where(vd => vd.districtid == districtid)
                            .Count();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                db.Dispose();
            }
            return vcount;
        }

        public int getPartyRegistered(int districtid, int affid)
        {
            int vcount = 0;
            VoterWatch.voterwatchEntities db = new VoterWatch.voterwatchEntities();
            try
            {
                vcount = db.voter_districts
                            .Where(vd => vd.districtid == districtid)
                            .Join(db.voters, a=>a.voterid, b=>b.voterid, (a,b)=>b)
                            .Where(v=>v.partyaffiliation == affid)
                            .Select(v=>v.voterid)
                            .Count();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                db.Dispose();
            }
            return vcount;
        }
    }
}
