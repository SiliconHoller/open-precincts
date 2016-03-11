using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoterWatch;

namespace precinctcaptain.myvoters
{
    public partial class VoterDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int vid;
                if (Int32.TryParse(Request.Params["voterid"], out vid)) loadVoterData(vid);                
            }
        }

        protected void loadVoterData(int vid)
        {
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                //Personal info
                voter v = db.voters.Where(vd => vd.voterid == vid).Single();
                namelit.Text = String.Format("{0} {1} {2} {3}", v.firstname, v.middlename, v.lastname, String.IsNullOrEmpty(v.suffix) ? "" : ", " + v.suffix);
                stateid.Text = v.statevoterid;
                countyid.Text = v.countyvoterid;
                yob.Text = v.yearofbirth.ToString();
                regdate.Text = v.registrationdate.ToShortDateString();
                //Party
                if (v.partyaffiliation.HasValue)
                {
                    try 
                    {
                        party.Text = db.affiliations.Where(a => a.affiliationid == v.partyaffiliation.Value).Single().name;
                    } catch (Exception ignore) {}
                }

                //Address information
                var addlist = db.voter_addresses.Where(va => va.voterid == vid)
                                .Join(db.addresses, a => a.addressid, b => b.address_id, (a, b) => b)
                                .OrderBy(a=>a.address1)
                                .ThenBy(a=>a.address2);
                addgrid.DataSource = addlist;
                addgrid.DataBind();

                //District registration
                var dlist = db.voter_districts.Where(vd => vd.voterid == vid)
                                .Join(db.districts, a => a.districtid, b => b.districtid, (a, b) => b)
                                .Join(db.district_types, a => a.districttypeid, b => b.districttypeid,
                                        (a, b) => new
                                        {
                                            a.districtid,
                                            a.identifier,
                                            a.name,
                                            a.descr,
                                            typename = b.name
                                        });
                distgrid.DataSource = dlist;
                distgrid.DataBind();
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