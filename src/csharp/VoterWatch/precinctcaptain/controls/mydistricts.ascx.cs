using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoterWatch.dataclasses;
using VoterWatchServices.districts;

namespace precinctcaptain.controls
{
    public partial class mydistricts : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                VotingDistricts vd = new VotingDistricts();
                List<district> dlist = vd.userDistricts();
                if (dlist.Count > 0)
                {
                    foreach (district d in dlist)
                    {
                        distselect.Items.Add(new ListItem
                        {
                            Text = String.Format("{0} {1}",d.identifier, d.name),
                            Value = d.districtid.ToString()
                        });
                    }
                    distselect.SelectedIndex = 0;
                }
            }
        }

        public int SelectedDistrictId
        {
            get
            {
                int rid = 0;
                if (distselect.SelectedIndex >= 0)
                {
                    Int32.TryParse(distselect.SelectedValue, out rid);
                }
                return rid;
            }
        }
    }
}