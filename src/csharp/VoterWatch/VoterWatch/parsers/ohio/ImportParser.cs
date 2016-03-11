using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoterWatch.parsers.ohio
{
    public class ImportParser
    {
        public ohioraw parseLine(string dataline)
        {
            ohioraw idata = new ohioraw();
            try
            {
                string[] dsplit = dataline.Split(',');

                idata.sosvoterid = checknull(dsplit[0]);
                idata.countynumber = checknull(dsplit[1]);
                idata.countyid = checknull(dsplit[2]);
                idata.lastname = checknull(dsplit[3]);
                idata.firstname = checknull(dsplit[4]);
                idata.middlename = checknull(dsplit[5]);
                idata.suffix = checknull(dsplit[6]);
                idata.yearofbirth = checknull(dsplit[7]);
                idata.registrationdate = checknull(dsplit[8]);
                idata.partyaffiliation = checknull(dsplit[9]);
                idata.resaddress1 = checknull(dsplit[10]);
                idata.resaddress2 = checknull(dsplit[11]);
                idata.rescity = checknull(dsplit[12]);
                idata.resstate = checknull(dsplit[13]);
                idata.reszip = checknull(dsplit[14]);
                idata.reszipplusfour = checknull(dsplit[15]);
                idata.rescountry = checknull(dsplit[16]);
                idata.respostalcode = checknull(dsplit[17]);
                idata.mailaddress1 = checknull(dsplit[18]);
                idata.mailaddress2 = checknull(dsplit[19]);
                idata.mailcity = checknull(dsplit[20]);
                idata.mailstate = checknull(dsplit[21]);
                idata.mailzip = checknull(dsplit[22]);
                idata.mailziplusfour = checknull(dsplit[23]);
                idata.mailcountry = checknull(dsplit[24]);
                idata.mailpostalcode = checknull(dsplit[25]);
                idata.careercenter = checknull(dsplit[26]);
                idata.city = checknull(dsplit[27]);
                idata.cityschooldistrict = checknull(dsplit[28]);
                idata.countycourtdistrict = checknull(dsplit[29]);
                idata.congressionaldiscrict = checknull(dsplit[30]);
                idata.courtofappeals = checknull(dsplit[31]);
                idata.educationservicecenter = checknull(dsplit[32]);
                idata.exemptedvillageschooldistrict = checknull(dsplit[33]);
                idata.librarydistrict = checknull(dsplit[34]);
                idata.localschooldistrict = checknull(dsplit[35]);
                idata.municipalcourtdiscrict = checknull(dsplit[36]);
                idata.precinct = checknull(dsplit[37]);
                idata.precinctcode = checknull(dsplit[38]);
                idata.stateboardofeducation = checknull(dsplit[39]);
                idata.staterepdistrict = checknull(dsplit[40]);
                idata.statesenatedistrict = checknull(dsplit[41]);
                idata.township = checknull(dsplit[42]);
                idata.village = checknull(dsplit[43]);
                idata.ward = checknull(dsplit[44]);
                //Rebuild the voter data
                StringBuilder sb = new StringBuilder();
                for (int i = 45; i < dsplit.Length; i++)
                {
                    sb.Append(dsplit[i]);
                    if (i + 1 < dsplit.Length) sb.Append(",");
                }
                idata.rawvotingdata = sb.ToString();
            }
            catch (Exception ex)
            {
                //skip this one
                idata = null;
            }
            return idata;
        }

        
        public bool getDistrictIdentifiers(string dataline, 
            out string countynumber,
            out string careercenter,
            out string city, 
            out string cityschooldistrict,
            out string countycourtdistrict,
            out string congressionaldistrict,
            out string courtofappeals, 
            out string educationservicecenter,
            out string exemptedvillageschooldistrict,
            out string librarydistrict,
            out string localschooldistrict,
            out string municipalcourtdistrict,
            out string precinctcode,
            out string precinct,
            out string stateboardofeducation,
            out string staterepdistrict,
            out string statesenatedistrict,
            out string township,
            out string village, 
            out string ward
            )
        {
            bool parsed = false;
            string[] dsplit = new string[0];
            try
            {
                dsplit = dataline.Split(',');
                parsed = true;
            }
            catch (Exception ex)
            {
                //skip this one
                parsed = false;
            }
            if (parsed)
            {
                countynumber = checknull(dsplit[1]);
                careercenter = checknull(dsplit[26]);
                city = checknull(dsplit[27]);
                cityschooldistrict = checknull(dsplit[28]);
                countycourtdistrict = checknull(dsplit[29]);
                congressionaldistrict = checknull(dsplit[30]);
                courtofappeals = checknull(dsplit[31]);
                educationservicecenter = checknull(dsplit[32]);
                exemptedvillageschooldistrict = checknull(dsplit[33]);
                librarydistrict = checknull(dsplit[34]);
                localschooldistrict = checknull(dsplit[35]);
                municipalcourtdistrict = checknull(dsplit[36]);
                precinct = checknull(dsplit[37]);
                precinctcode = checknull(dsplit[38]);
                stateboardofeducation = checknull(dsplit[39]);
                staterepdistrict = checknull(dsplit[40]);
                statesenatedistrict = checknull(dsplit[41]);
                township = checknull(dsplit[42]);
                village = checknull(dsplit[43]);
                ward = checknull(dsplit[44]);
            }
            else
            {
                countynumber = null;
                careercenter = null;
                city = null;
                cityschooldistrict = null;
                countycourtdistrict = null;
                congressionaldistrict = null;
                courtofappeals = null;
                educationservicecenter = null;
                exemptedvillageschooldistrict = null;
                librarydistrict = null;
                localschooldistrict = null;
                municipalcourtdistrict = null;
                precinct = null;
                precinctcode = null;
                stateboardofeducation = null;
                staterepdistrict = null;
                statesenatedistrict = null;
                township = null;
                village = null;
                ward = null;
            }
            return parsed;
        }

        public VoterWatch.dataclasses.voter parseVoter(string dataline)
        {
            VoterWatch.dataclasses.voter pv = null;
            try
            {
                pv = new dataclasses.voter();
                string[] dsplit = dataline.Split(',');

                pv.stateid = checknull(dsplit[0]);
                pv.countyid = checknull(dsplit[2]);
                
                pv.lastname = checknull(dsplit[3]);
                pv.firstname = checknull(dsplit[4]);
                pv.middlename = checknull(dsplit[5]);
                pv.suffix = checknull(dsplit[6]);
                pv.yob = checkInt(dsplit[7]);
                pv.registrationdate = checkDate(dsplit[8]);
                pv.partyaffiliation = checknull(dsplit[9]);
            }
            catch (Exception ex)
            {
                //errored out--better return null for this guy
                pv = null;
            }

            return pv;
        }

        public VoterWatch.dataclasses.address parseMailingAddress(string dataline)
        {
            VoterWatch.dataclasses.address paddr = null;
            try
            {
                paddr = new dataclasses.address();
                string[] dsplit = dataline.Split(',');
                paddr.address1 = checknull(dsplit[18]);
                paddr.address2 = checknull(dsplit[19]);
                paddr.city = checknull(dsplit[20]);
                paddr.state = checknull(dsplit[21]);
                paddr.zip = checknull(dsplit[22]);
                paddr.zipplus = checknull(dsplit[23]);
                paddr.typeid = dataclasses.addressflags.mailing;
            }
            catch (Exception ex)
            {
                paddr = null;
            }
            return paddr;
        }

        public VoterWatch.dataclasses.address parseResidentialAddress(string dataline)
        {
            VoterWatch.dataclasses.address paddr = null;
            try
            {
                paddr = new dataclasses.address();
                string[] dsplit = dataline.Split(',');
                paddr.address1 = checknull(dsplit[10]);
                paddr.address2 = checknull(dsplit[11]);
                paddr.city = checknull(dsplit[12]);
                paddr.state = checknull(dsplit[13]);
                paddr.zip = checknull(dsplit[14]);
                paddr.zipplus = checknull(dsplit[15]);
                paddr.typeid = dataclasses.addressflags.residential;
            }
            catch (Exception ex)
            {
                paddr = null;
            }
            return paddr;
        }

        public bool parseVoterData(string dataline,
            out VoterWatch.dataclasses.voter vout,
            out VoterWatch.dataclasses.address maddout,
            out VoterWatch.dataclasses.address raddout,
            out string districtident,
            out string partyident)
        {
            bool success = false;
            VoterWatch.dataclasses.voter pv = null;
            VoterWatch.dataclasses.address raddr = null;
            VoterWatch.dataclasses.address maddr = null;
            string distid = null;
            string pval = null;
            try
            {
                string[] dsplit = dataline.Split(',');

                //compile voter info
                pv = new dataclasses.voter();
                pv.stateid = checknull(dsplit[0]);
                pv.countyid = checknull(dsplit[2]);

                pv.lastname = checknull(dsplit[3]);
                pv.firstname = checknull(dsplit[4]);
                pv.middlename = checknull(dsplit[5]);
                pv.suffix = checknull(dsplit[6]);
                pv.yob = checkInt(dsplit[7]);
                pv.registrationdate = checkDate(dsplit[8]);
                pv.partyaffiliation = checknull(dsplit[9]);

                //Compile mailing address
                maddr = new dataclasses.address();
                maddr.address1 = checknull(dsplit[18]);
                maddr.address2 = checknull(dsplit[19]);
                maddr.city = checknull(dsplit[20]);
                maddr.state = checknull(dsplit[21]);
                maddr.zip = checknull(dsplit[22]);
                maddr.zipplus = checknull(dsplit[23]);
                maddr.typeid = dataclasses.addressflags.mailing;

                //Compile residential address
                raddr = new dataclasses.address();
                raddr.address1 = checknull(dsplit[10]);
                raddr.address2 = checknull(dsplit[11]);
                raddr.city = checknull(dsplit[12]);
                raddr.state = checknull(dsplit[13]);
                raddr.zip = checknull(dsplit[14]);
                raddr.zipplus = checknull(dsplit[15]);
                raddr.typeid = dataclasses.addressflags.residential;

                //pick district identity
                string precinctcode = checknull(dsplit[38]);
                string ward = checknull(dsplit[44]);

                if (precinctcode != null && ward == null)
                {
                    distid = precinctcode;
                }
                else
                {
                    distid = ward;
                }

                pval = checknull(dsplit[9]);
                success = true;
            }
            catch (Exception ex)
            {
                success = false;
                pv = null;
                maddr = null;
                raddr = null;
                distid = null;
            }
            //set the values
            vout = pv;
            maddout = maddr;
            raddout = raddr;
            districtident = distid;
            partyident = pval;

            return success;
        }

        private string checknull(string data)
        {
            string rval = null;
            if (!String.IsNullOrEmpty(data) && !String.IsNullOrWhiteSpace(data)) rval = data.Trim();
            return rval;
        }

        private int checkInt(string data)
        {
            int rval = 0;
            if (!String.IsNullOrEmpty(data) && !String.IsNullOrWhiteSpace(data))
            {
                Int32.TryParse(data.Trim(), out rval);
            }
            return rval;
        }

        private DateTime checkDate(string data)
        {
            DateTime rval = new DateTime(2000, 1, 1);
            if (!String.IsNullOrEmpty(data) && !String.IsNullOrWhiteSpace(data))
            {
                DateTime.TryParse(data.Trim(), out rval);
            }
            return rval;
        }
    }
}
