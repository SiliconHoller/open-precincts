using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoterWatch;
using VoterWatch.logging;
using System.Reflection;

namespace precinctcaptain.myvoters
{
    public partial class VoterParticipation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string partyval = Request.Params["affid"];
                string distval = Request.Params["districtid"];
                string tallyval = Request.Params["tallyid"];
                int partyid = 0;
                int distid = 0;
                int tallyid = 0;
                try
                {
                    distid = Int32.Parse(distval);
                    tallyid = Int32.Parse(tallyval);
                    if (Int32.TryParse(partyval, out partyid))
                    {
                        //filter on party
                        chartVoters(distid, tallyid, partyid);
                    }
                    else
                    {
                        //general electorate
                        chartVoters(distid, tallyid);
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void chartVoters(int distid, int tallyid)
        {
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                int allvoters = db.voter_districts.Where(vd => vd.districtid == distid)
                                    .Join(db.voters, a => a.voterid, b => b.voterid, (a, b) => b)
                                    .Count();
                int voted = db.voter_districts.Where(vd => vd.districtid == distid)
                                    .Join(db.voters, a => a.voterid, b => b.voterid, (a, b) => b)
                                    .Join(db.voter_count, a => a.voterid, b => b.voterid, (a, b) => b)
                                    .Where(t => t.tally_id == tallyid)
                                    .Count();
                int nonvoting = allvoters - voted;
                List<string> typenames = new List<string>();
                List<int> typecounts = new List<int>();
                typenames.Add("Non Voting");
                typecounts.Add(nonvoting);
                typenames.Add("Voted");
                typecounts.Add(voted);
                participationchart.Series["voterseries"].Points.DataBindXY(typenames, typecounts);
            }
            catch (Exception ex)
            {
                ExceptionLogger.Log(MethodBase.GetCurrentMethod(), ex, distid, tallyid);
            }
            finally
            {
                db.Dispose();
            }
        }

        private void chartVoters(int distid, int tallyid, int partyid)
        {
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                int allvoters = db.voter_districts.Where(vd => vd.districtid == distid)
                                    .Join(db.voters, a => a.voterid, b => b.voterid, (a, b) => b)
                                    .Where(v=>v.partyaffiliation == partyid)
                                    .Count();
                int voted = db.voter_districts.Where(vd => vd.districtid == distid)
                                    .Join(db.voters, a => a.voterid, b => b.voterid, (a, b) => b)
                                    .Where(v=>v.partyaffiliation == partyid)
                                    .Join(db.voter_count, a => a.voterid, b => b.voterid, (a, b) => b)
                                    .Where(t => t.tally_id == tallyid)
                                    .Count();
                int nonvoting = allvoters - voted;
                List<string> typenames = new List<string>();
                List<int> typecounts = new List<int>();
                typenames.Add("Non Voting");
                typecounts.Add(nonvoting);
                typenames.Add("Voted");
                typecounts.Add(voted);
                participationchart.Series["voterseries"].Points.DataBindXY(typenames, typecounts);
            }
            catch (Exception ex)
            {
                ExceptionLogger.Log(MethodBase.GetCurrentMethod(), ex, distid, tallyid);
            }
            finally
            {
                db.Dispose();
            }
        }
    }
}