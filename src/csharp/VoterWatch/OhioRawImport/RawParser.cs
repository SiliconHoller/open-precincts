using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoterWatch;

namespace OhioRawImport
{
    public class RawParser
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

        private string checknull(string data)
        {
            string rval = null;
            if (!String.IsNullOrEmpty(data) && !String.IsNullOrWhiteSpace(data)) rval = data.Trim();
            return rval;
        }
    }
}
