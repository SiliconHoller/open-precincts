using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OhioImporter.Districts;
using VoterWatch.parsers.ohio;
using System.IO;
using VoterWatch.dataclasses;
using System.Threading;

namespace OhioImporter
{
    public partial class OhioImport : Form
    {
        private string importfile;
        private Dictionary<string, Dictionary<string, district>> typemap;
        private Dictionary<string, int> typeidmap;
        private Dictionary<int, List<district>> parentage;

        private VotingDistrictsClient dclient;
        private Structure.DistrictModelClient mclient;
        private VoterInfo.VoterInfoClient viclient;
        private VoterMaintenance.VoterMaintenanceClient vmclient;
        
        
        
        public OhioImport()
        {
            InitializeComponent();
            dclient = new VotingDistrictsClient();
            mclient = new Structure.DistrictModelClient();
            viclient = new VoterInfo.VoterInfoClient();
            vmclient = new VoterMaintenance.VoterMaintenanceClient();
        }

        private void fileSelectButton_Click(object sender, EventArgs e)
        {
            filechooser = new OpenFileDialog();
            if (filechooser.ShowDialog(this) == DialogResult.OK)
            {
                importfile = filechooser.FileName;
                importFileLabel.Text = importfile;
                parseFile();
            }
            
        }

        delegate void logitdelegate(string ltxt);

        private void logit(string ltxt)
        {
            if (logText.InvokeRequired)
            {
                logText.Invoke(new logitdelegate(logit), new object[] { ltxt });
            }
            else
            {
                List<string> llines = logText.Lines.Take(500).ToList<string>();
                llines.Insert(0,ltxt);
                logText.Lines = llines.ToArray<string>();
            }
        }

        private void parseFile()
        {
            //pullDistricts();
            ThreadStart ts = new ThreadStart(this.pullVoters);
            Thread t = new Thread(ts);
            t.Start();
        }

        private void pullDistricts()
        {
            //build the type mapping
            typemap = new Dictionary<string, Dictionary<string, district>>();
            typeidmap = new Dictionary<string, int>();
            parentage = new Dictionary<int, List<district>>();

            districttype[] dtypes = dclient.getTypes();
            foreach (districttype dtype in dtypes)
            {
                typeidmap.Add(dtype.name, dtype.districttypeid);
                district[] darr = dclient.getDistricts(dtype.districttypeid, null, null);
                Dictionary<string, district> knowndistricts = new Dictionary<string, district>();
                foreach (district d in darr)
                {
                    knowndistricts.Add(d.identifier, d);
                }
                typemap.Add(dtype.name, knowndistricts);
            }

            ImportParser rowparser = new ImportParser();
            StreamReader freader = new StreamReader(File.OpenRead(importfile));
            string skipit = freader.ReadLine();
            try
            {
                string countynumber, careercenter, city, cityschooldistrict, countycourtdistrict, congressionaldistrict, courtofappeals, educationservicecenter,
                    exemptedvillageschooldistrict, librarydistrict, localschooldistrict, municipalcourtdistrict, precinctcode, precinct, stateboardofeducation, staterepdistrict,
                    statesenatedistrict, township, village, ward;
                //Start reading
                while (!freader.EndOfStream)
                {
                    try
                    {
                        if (rowparser.getDistrictIdentifiers(freader.ReadLine(),
                            out countynumber,
                            out careercenter,
                            out city,
                            out cityschooldistrict,
                            out countycourtdistrict,
                            out congressionaldistrict,
                            out courtofappeals,
                            out educationservicecenter,
                            out exemptedvillageschooldistrict,
                            out librarydistrict,
                            out localschooldistrict,
                            out municipalcourtdistrict,
                            out precinctcode,
                            out precinct,
                            out stateboardofeducation,
                            out staterepdistrict,
                            out statesenatedistrict,
                            out township,
                            out village,
                            out ward))
                        {
                            //Add and refresh non null values
                            checkAdd(careercenter, "Career Center");
                            checkAdd(city, "City");
                            checkAdd(cityschooldistrict, "City School");
                            checkAdd(countycourtdistrict, "County Court");
                            checkAdd(congressionaldistrict, "Congressional");
                            checkAdd(courtofappeals, "Court of Appeals");
                            checkAdd(educationservicecenter, "Education Service Center");
                            checkAdd(exemptedvillageschooldistrict, "Exempted Village School");
                            checkAdd(librarydistrict, "Library");
                            checkAdd(localschooldistrict, "Local School");
                            checkAdd(municipalcourtdistrict, "Municipal Court");
                            checkAdd(stateboardofeducation, "State Board of Education");
                            checkAdd(staterepdistrict, "State Representative");
                            checkAdd(statesenatedistrict, "State Senate");
                            checkAdd(township, "Township");
                            checkAdd(village, "Village");
                            checkAdd(ward, "Ward");
                            checkAddPrecinct(precinctcode, precinct, countynumber);

                            //general county ancestry
                            checkLineage("County", countynumber, "Township", township);
                            checkLineage("County", countynumber, "Village", village);
                            checkLineage("County", countynumber, "Career Center", careercenter);
                            checkLineage("County", countynumber, "Education Service Center", educationservicecenter);

                            //precinct ancestry
                            checkLineage("County", countynumber, "Precinct", precinctcode);
                            checkLineage("Education Service Center", educationservicecenter, "Precinct", precinctcode);
                            checkLineage("Congressional", congressionaldistrict, "Precinct", precinctcode);
                            checkLineage("State Board of Education", stateboardofeducation, "Precinct", precinctcode);
                            checkLineage("State Representative", staterepdistrict, "Precinct", precinctcode);
                            checkLineage("State Senate", statesenatedistrict, "Precinct", precinctcode);

                            //other general ancestry
                            checkLineage("Education Service Center", educationservicecenter, "Local School", localschooldistrict);
                            checkLineage("City", city, "Ward", ward);
                        }
                    }
                    catch (Exception ex)
                    {

                    }

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                freader.Close();
            }
        }

        private void pullVoters()
        {
            logit("Pulling ward and precinct lists");
            List<district> alldists = dclient.getDistricts(14, null, null).ToList<district>();
            alldists.AddRange(dclient.getDistricts(19,null,null).ToList<district>());
            //build the distid dictionary
            Dictionary<string, int> distmap = new Dictionary<string, int>();
            foreach (district d in alldists)
            {
                try
                {
                    distmap.Add(d.identifier, d.districtid);
                }
                catch (Exception ex)
                {

                }
            }
            logit("Pulling party affiliations");
            List<affiliation> plist = viclient.getParties().ToList<affiliation>();
            logit("Parsing file");
            ImportParser rowparser = new ImportParser();
            StreamReader freader = new StreamReader(File.OpenRead(importfile));
            string skipit = freader.ReadLine();
            try
            {
                voter nv;
                voter addedvoter;
                address nradd;
                address addedres;
                address nmadd;
                address addedmail;
                string regdist;
                string party;
                while (!freader.EndOfStream)
                {
                    logit("Read and parse...");
                    string dline = freader.ReadLine();
                    if (rowparser.parseVoterData(dline, out nv, out nmadd, out nradd, out regdist, out party))
                    {
                        if (party != null)
                        {
                            try
                            {
                                affiliation vaff = plist.Where(p => p.partycode == party).Single();
                                nv.affiliationid = vaff.affiliationid;
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        logit("\tParsed.");
                        try
                        {
                            addedvoter = vmclient.addVoter(nv);
                            logit("\t\tAdded voter...");
                            try
                            {

                                addedres = vmclient.addAddress(nradd);
                                logit("\t\tAdded residential address...");
                            }
                            catch (Exception ex)
                            {
                                addedres = null;
                            }
                            try
                            {
                                addedmail = vmclient.addAddress(nmadd);
                                logit("\t\tAdded mailing address...");
                            }
                            catch (Exception ex)
                            {
                                addedmail = null;
                            }

                            if (addedres != null && addedmail != null)
                            {
                                if (addedres.addressid == addedmail.addressid)
                                {
                                    logit("\t\tSame--logical and...");
                                    vmclient.addVoterAddress(addedvoter.voterid, addedres.addressid, addedres.typeid & addedmail.typeid);
                                }
                                else
                                {
                                    logit("\t\tDifferent addresses--adding separately...");
                                    vmclient.addVoterAddress(addedvoter.voterid, addedres.addressid, addedres.typeid);
                                    vmclient.addVoterAddress(addedvoter.voterid, addedmail.addressid, addedmail.typeid);
                                }
                            }
                            else if (addedres != null)
                            {
                                logit("\t\tOnly Residential Address");
                                vmclient.addVoterAddress(addedvoter.voterid, addedres.addressid, addedres.typeid);
                                    
                            }
                            else if (addedmail != null)
                            {
                                logit("\t\tOnly Mailing address");
                                vmclient.addVoterAddress(addedvoter.voterid, addedmail.addressid, addedmail.typeid);
                            }
                            else
                            {
                                logit("\t\tNo valid address!!");
                            }
                            if (regdist != null)
                            {
                                logit(String.Format("\t\tSetting voting district for {0}...", regdist));
                                if (distmap.ContainsKey(regdist))
                                {
                                    try
                                    {
                                        vmclient.setVoterDistrict(addedvoter.voterid, distmap[regdist]);
                                        logit("\t\tDistrict set.");
                                    }
                                    catch (Exception ex)
                                    {
                                        logit("\t\tDistrict problem...");
                                    }
                                }
                                else
                                {
                                    logit("\t\tUNRECOGNIZED DISTRICT!!!");
                                }
                            }
                            else
                            {
                                logit("\t\tDid not find a matching district!!");
                            }
                        }
                        catch (Exception ex)
                        {
                            logit(ex.Message);
                        }
                    }
                    else
                    {
                        logit("Parser error!!");
                        logit(dline);
                    }
                    addedvoter = null;
                    addedres = null;
                    addedmail = null;
                }
            }
            catch (Exception ex)
            {
                logit(ex.Message);
            }


        }

        private void checkAdd(string dident, string typename)
        {
            if (dident != null)
            {
                try
                {
                    int typeid = typeidmap[typename];
                    Dictionary<string, district> dmap = typemap[typename];
                    if (!dmap.ContainsKey(dident))
                    {
                        //we're going to make it and refresh the list
                        district ndistrict = new district { identifier = dident, districttypeid = typeid };
                        ndistrict = mclient.addDistrict(ndistrict);
                        if (ndistrict != null)
                        {
                            if (ndistrict.districtid > 0)
                            {
                                dmap.Add(dident, new district { districtid = ndistrict.districtid, identifier = dident });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void checkAddPrecinct(string precinctcode, string precinct, string countynumber)
        {
            //for combination values
            if (precinctcode != null)
            {
                try
                {
                    int typeid = typeidmap["Precinct"];
                    Dictionary<string, district> dmap = typemap["Precinct"];
                    if (!dmap.ContainsKey(precinctcode))
                    {
                        Dictionary<string,district> cmap = typemap["County"];
                        string countyname = cmap[countynumber].name;
                        //we're going to make it and refresh the list
                        district ndistrict = new district 
                        { 
                            identifier = precinctcode, 
                            districttypeid = typeid,
                            name = precinct,
                            description = String.Format("{0} precinct in {1} County", precinct, countyname)
                        };
                        ndistrict = mclient.addDistrict(ndistrict);
                        if (ndistrict != null)
                        {
                            if (ndistrict.districtid > 0)
                            {
                                dmap.Add(precinctcode, new district { districtid = ndistrict.districtid, identifier = precinctcode });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void checkLineage(string parenttype, string parident, string childtype, string childident)
        {
            if (parident != null && childident != null)
            {
                try
                {
                    Dictionary<string, district> parmap = typemap[parenttype];
                    Dictionary<string, district> childmap = typemap[childtype];
                    int parid = parmap[parident].districtid;
                    int childid = childmap[childident].districtid;
                    if (!parentage.ContainsKey(parid))
                    {
                        //load and cache it
                        List<district> pkids = dclient.getChildren(parid).ToList<district>();
                        parentage.Add(parid, pkids);
                    }
                    //Evaluate
                    int ecount = parentage[parid].Where(k => k.districtid == childid).Count();
                    if (ecount == 0)
                    {
                        //we're going to add it, then reset the list
                        mclient.addParent(new districtrelationship { childdistrict = childid, parentdistrict = parid });
                        List<district> pkids = dclient.getChildren(parid).ToList<district>();
                        parentage[parid] = pkids;
                    }
                }
                catch (Exception ex) { }
            }
        }

    }
}
