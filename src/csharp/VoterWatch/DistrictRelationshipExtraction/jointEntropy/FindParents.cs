using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoterWatch;

namespace DistrictRelationshipExtraction.jointEntropy
{
    public abstract class FindParents
    {
        protected string typeidentifier;
        protected int typeid;
        protected voterwatchEntities db;
        protected Dictionary<string, int> typemap;

        protected FindParents(string typename)
        {
            typeidentifier = typename;
            db = new voterwatchEntities();
            db.CommandTimeout = 150;
            setTypeIdentifier();
            loadTypeMap();
        }

        public abstract void findParents();

        protected void setTypeIdentifier()
        {
            typeid = db.district_types.Where(dt => dt.name == typeidentifier).Single().districttypeid;
        }

        protected void loadTypeMap()
        {
            typemap = new Dictionary<string, int>();
            var dtypes = db.district_types;
            foreach (district_types dtype in dtypes)
            {
                typemap.Add(dtype.name, dtype.districttypeid);
            }
        }

        protected int countCounties(string county)
        {
            return db.ohioraws.Where(or => or.countynumber == county).Count();
        }

        protected int countCongressionals(string cdist)
        {
            return db.ohioraws.Where(or => or.congressionaldiscrict == cdist).Count();
        }

        protected int countCities(string cityname)
        {
            return db.ohioraws.Where(r => r.city == cityname).Count();
        }

        protected int countCitySchools(string cschool)
        {
            return db.ohioraws.Where(r => r.cityschooldistrict == cschool).Count();
        }

        protected int countCountyCourts(string countycourt)
        {
            return db.ohioraws.Where(r => r.countycourtdistrict == countycourt).Count();
        }

        protected int countCourtofAppeals(string appcourt)
        {
            return db.ohioraws.Where(r => r.courtofappeals == appcourt).Count();
        }

        protected int countVillages(string vname)
        {
            return db.ohioraws.Where(r => r.village == vname).Count();
        }

        protected int countExemptVillageSchools(string vname)
        {
            return db.ohioraws.Where(r => r.exemptedvillageschooldistrict == vname).Count();
        }

        protected int countLibraries(string libname)
        {
            return db.ohioraws.Where(r => r.librarydistrict == libname).Count();
        }

        protected int countLocalSchools(string lsname)
        {
            return db.ohioraws.Where(r => r.localschooldistrict == lsname).Count();
        }

        protected int countMunicipalCourts(string mcname)
        {
            return db.ohioraws.Where(r => r.municipalcourtdiscrict == mcname).Count();
        }

        protected int countPrecincts(string pcode)
        {
            return db.ohioraws.Where(r => r.precinctcode == pcode).Count();
        }

        protected int countESCs(string escname)
        {
            return db.ohioraws.Where(r => r.educationservicecenter == escname).Count();
        }

        protected int countStateBOE(string boename)
        {
            return db.ohioraws.Where(r => r.stateboardofeducation == boename).Count();
        }

        protected int countTownships(string tname)
        {
            return db.ohioraws.Where(r => r.township == tname).Count();
        }

        protected int countWards(string wname)
        {
            return db.ohioraws.Where(r => r.ward == wname).Count();
        }

        protected int countCareerCenters(string cname)
        {
            return db.ohioraws.Where(r => r.careercenter == cname).Count();
        }

        protected void addParentage(int childid, int parenttype, string parentname)
        {
            try
            {
                int parentid = db.districts.Where(d => d.districttypeid == parenttype && d.identifier == parentname).Single().districtid;
                db.district_relationships.AddObject(new district_relationships { districtid = childid, parentdistrict = parentid });
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Parentage problem: Child District: {0}, Parent Type: {1}, Parent Name: {2}", childid, parenttype, parentname);
                Console.WriteLine(ex.Message);
            }
        }


    }
}
