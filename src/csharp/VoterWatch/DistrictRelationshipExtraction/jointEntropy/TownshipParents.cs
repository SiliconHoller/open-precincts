using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoterWatch;

namespace DistrictRelationshipExtraction.jointEntropy
{
    public class TownshipParents : FindParents
    {

        public TownshipParents() :
            base("Township")
        {

        }

        public override void findParents()
        {
            try
            {
                List<district> tlist = db.districts.Where(d => d.districttypeid == typeid).ToList<district>();
                foreach (district township in tlist)
                {
                    checkAppealsCourt(township);
                    checkCareerCenter(township);
                    checkCongressionals(township);
                    checkCounties(township);
                    checkCountyCourt(township);
                    checkESC(township);
                    checkSBOE(township);
                    checkStateRep(township);
                    checkStateSenate(township);
                    int containercount = db.district_relationships.Where(dr => dr.districtid == township.districtid).Count();
                    Console.WriteLine("{0} is contained in {1} geopolitical divisions", township.identifier, containercount);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Can't determine township parentage");
                Console.WriteLine(ex.Message);
            }
        }

        protected void checkCounties(district township)
        {
            try
            {
                int ctype = typemap["County"];
                List<string> cnames = db.ohioraws.Where(r => r.township == township.identifier && r.countynumber != null).Select(c => c.countynumber).Distinct().ToList<string>();
                if (cnames.Count == 1)
                {
                    //we have a winner--one county every time
                    addParentage(township.districtid, ctype, cnames.Single());
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void checkCongressionals(district township)
        {
            try
            {
                int ctype = typemap["Congressional"];
                List<string> congdistricts = db.ohioraws.Where(r => r.township == township.identifier && r.congressionaldiscrict != null).Select(c => c.congressionaldiscrict).Distinct().ToList<string>();
                if (congdistricts.Count == 1)
                {
                    addParentage(township.districtid, ctype, congdistricts.Single());
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void checkCountyCourt(district township)
        {
            try
            {
                int ptype = typemap["County Court"];
                List<string> parnames = db.ohioraws.Where(r => r.township == township.identifier && r.countycourtdistrict != null).Select(cs => cs.countycourtdistrict).Distinct().ToList<string>();
                if (parnames.Count == 1)
                {
                    addParentage(township.districtid, ptype, parnames.Single());
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void checkAppealsCourt(district township)
        {
            try
            {
                int ptype = typemap["Court of Appeals"];
                List<string> parnames = db.ohioraws.Where(r => r.township == township.identifier && r.courtofappeals != null).Select(cs => cs.courtofappeals).Distinct().ToList<string>();
                if (parnames.Count == 1)
                {
                    addParentage(township.districtid, ptype, parnames.Single());
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void checkESC(district township)
        {
            try
            {
                int ptype = typemap["Education Service Center"];
                List<string> parnames = db.ohioraws.Where(r => r.township == township.identifier && r.educationservicecenter != null).Select(cs => cs.educationservicecenter).Distinct().ToList<string>();
                if (parnames.Count == 1)
                {
                    addParentage(township.districtid, ptype, parnames.Single());
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void checkSBOE(district township)
        {
            try
            {
                int ptype = typemap["State Board of Education"];
                List<string> parnames = db.ohioraws.Where(r => r.township == township.identifier && r.stateboardofeducation != null).Select(cs => cs.stateboardofeducation).Distinct().ToList<string>();
                if (parnames.Count == 1)
                {
                    addParentage(township.districtid, ptype, parnames.Single());
                }
            }
            catch (Exception ex)
            {

            }
        
        }

        protected void checkStateSenate(district township)
        {
            try
            {
                int ptype = typemap["State Senate"];
                List<string> parnames = db.ohioraws.Where(r => r.township == township.identifier && r.countycourtdistrict != null).Select(cs => cs.countycourtdistrict).Distinct().ToList<string>();
                if (parnames.Count == 1)
                {
                    addParentage(township.districtid, ptype, parnames.Single());
                }
            }
            catch (Exception ex)
            {

            }
        
        }

        protected void checkStateRep(district township)
        {
            try
            {
                int ptype = typemap["State Representative"];
                List<string> parnames = db.ohioraws.Where(r => r.township == township.identifier && r.staterepdistrict != null).Select(cs => cs.staterepdistrict).Distinct().ToList<string>();
                if (parnames.Count == 1)
                {
                    addParentage(township.districtid, ptype, parnames.Single());
                }
            }
            catch (Exception ex)
            {

            }
        
        }

        protected void checkCareerCenter(district township)
        {
            try
            {
                int ptype = typemap["Career Center"];
                List<string> parnames = db.ohioraws.Where(r => r.township == township.identifier && r.careercenter != null).Select(cs => cs.careercenter).Distinct().ToList<string>();
                if (parnames.Count == 1)
                {
                    addParentage(township.districtid, ptype, parnames.Single());
                }
            }
            catch (Exception ex)
            {

            }
        
        }
    }
}
