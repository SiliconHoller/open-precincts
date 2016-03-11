using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoterWatchServices.districts;
using VoterWatch.dataclasses;
using VoterWatchServices.system;

namespace precinctcaptain.myvoters
{
    public partial class VoterCount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDistricts();
                loadTallies();
                loadParties();
                loadMethods();
            }
        }

        private void loadDistricts()
        {
            VotingDistricts dmodel = new VotingDistricts();
            List<district> udists = dmodel.userVotingDistricts();
            if (udists.Count > 0)
            {
                districtselect.Items.Add(new ListItem { Text = "", Value = "" });
                foreach (district d in udists) districtselect.Items.Add(new ListItem { Value = d.districtid.ToString(), Text = String.Format("{0} {1}", d.identifier, d.name) });
            }
        }

        private void loadTallies()
        {
            SystemLists listmodel = new SystemLists();
            List<option> elections = listmodel.getTallies();
            foreach (option opt in elections) tallyselect.Items.Add(new ListItem { Value = opt.optval, Text = opt.opttext });
        }

        private void loadParties()
        {
            SystemLists listmodel = new SystemLists();
            List<option> parties = listmodel.getParties();
            partyselect.Items.Add(new ListItem { Value = "0", Text = "All" });
            foreach (option opt in parties) partyselect.Items.Add(new ListItem { Value = opt.optval, Text = opt.opttext });
        }

        private void loadMethods()
        {
            SystemLists listmodel = new SystemLists();
            List<option> methods = listmodel.getVotingMethods();
            foreach (option opt in methods) methodselect.Items.Add(new ListItem { Value = opt.optval, Text = opt.opttext });
        }
    }
}