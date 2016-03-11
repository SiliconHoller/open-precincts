using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoterWatch;

namespace precinctcaptain.myvoters
{
    public partial class DistrictDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int distid;
                if (Int32.TryParse(Request.Params["districtid"], out distid)) loadDistrictData(distid);
            }
        }

        protected void loadDistrictData(int distid)
        {
            List<string> pnames = new List<string>();
            List<int> pcounts = new List<int>();

            voterwatchEntities db = new voterwatchEntities();
            try
            {
              
                List<int?> pvals = db.voter_districts
                                    .Where(ud => ud.districtid == distid)
                                    .Join(db.voters, a => a.voterid, b => b.voterid, (a, b) => b)
                                    .Select(v => v.partyaffiliation)
                                    .OrderBy(p=>p)
                                    .Distinct()
                                    .ToList<int?>();
                foreach (int? pid in pvals)
                {
                    if (!pid.HasValue)
                    {
                        pnames.Add("No party");
                        int pcount = db.voter_districts
                                        .Where(ud => ud.districtid == distid)
                                        .Join(db.voters, a => a.voterid, b => b.voterid, (a, b) => b)
                                        .Where(v => v.partyaffiliation == null)
                                        .Count();
                        pcounts.Add(pcount);
                    }
                    else
                    {
                        pnames.Add(db.affiliations.Where(p => p.affiliationid == pid.Value).Single().name);
                        pcounts.Add(db.voter_districts
                                        .Where(ud => ud.districtid == distid)
                                        .Join(db.voters, a => a.voterid, b => b.voterid, (a, b) => b)
                                        .Where(v => v.partyaffiliation == pid.Value)
                                        .Count());
                    }
                }
                demogchart.Series["partyseries"].Points.DataBindXY(pnames, pcounts);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                db.Dispose();
            }
        }
    }
}