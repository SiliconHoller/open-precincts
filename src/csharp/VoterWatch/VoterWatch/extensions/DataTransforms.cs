using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;

namespace VoterWatch.extensions
{
    public static class DataTransforms
    {
        public static VoterWatch.dataclasses.voter DataContract(this VoterWatch.voter db)
        {
            return new VoterWatch.dataclasses.voter
                {
                    voterid = db.voterid,
                    stateid = db.statevoterid,
                    countyid = db.countyvoterid,
                    lastname = db.lastname,
                    firstname = db.firstname,
                    middlename = db.middlename,
                    suffix = db.suffix,
                    yob = db.yearofbirth,
                    registrationdate = db.registrationdate,
                    affiliationid = db.partyaffiliation.HasValue ? db.partyaffiliation.Value : 0
                };
        }

        public static VoterWatch.dataclasses.district DataContract(this VoterWatch.district db)
        {
            return new VoterWatch.dataclasses.district
                    {
                        districtid = db.districtid,
                        identifier = db.identifier,
                        name = db.name,
                        description = db.descr,
                        districttypeid = db.districttypeid
                    };
        }

        public static VoterWatch.dataclasses.address DataContract(this VoterWatch.address db)
        {
            return new dataclasses.address
            {
                addressid = db.address_id,
                address1 = db.address1,
                address2 = db.address2,
                city = db.city,
                state = db.state,
                zip = db.zip,
                zipplus = db.zip_plusfour
            };
        }

        public static VoterWatch.dataclasses.affiliation DataContract(this VoterWatch.affiliation db)
        {
            return new dataclasses.affiliation
            {
                affiliationid = db.affiliationid,
                name = db.name,
                partycode = db.partycode,
                descr = db.descr
            };
        }

        public static VoterWatch.dataclasses.userdata DataContract(this VoterWatch.user usr) 
        {
            return new dataclasses.userdata 
            {
                userid = usr.userid,
                lname = usr.lastname,
                fname = usr.firstname,
                username = usr.emailaddress
            };
        }

        public static VoterWatch.dataclasses.appdata DataContract(this VoterWatch.application app)
        {
            return new dataclasses.appdata
            {
                appid = app.appid,
                name = app.appname,
                descr = app.descr,
                seq = app.seq
            };
        }

        public static VoterWatch.dataclasses.roledata DataContract(this VoterWatch.role r)
        {
            return new dataclasses.roledata
            {
                roleid = r.roleid,
                name = r.rolename,
                descr = r.descr,
                seq = r.seq
            };
        }

        public static VoterWatch.dataclasses.option toOption(this VoterWatch.affiliation p)
        {
            return new dataclasses.option
            {
                optval = p.affiliationid.ToString(),
                opttext = p.name,
                opttip = p.descr
            };
        }

        public static VoterWatch.dataclasses.option toOption(this VoterWatch.voter_method vm)
        {
            return new dataclasses.option
            {
                optval = vm.vmid.ToString(),
                opttext = vm.name,
                opttip = vm.descr
            };
        }

        public static VoterWatch.dataclasses.option toOption(this VoterWatch.tally t)
        {
            return new dataclasses.option
            {
                optval = t.tally_id.ToString(),
                opttext = String.Format("{0}", t.name),
                opttip = t.descr
            };
        }
    }
}
