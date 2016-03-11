using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;
using VoterWatch;
using System.Data.SqlClient;
using System.Data;
using VoterWatch.logging;
using System.Reflection;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace VoterWatchServices.tallies
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Ledger" in code, svc and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Ledger : ILedger
    {
        protected static string votinglistsql = @"select v.voterid, v.statevoterid, v.countyvoterid, v.lastname, v.firstname, "+
                                                    "vc.count_id, vc.tally_id, vc.methodid from voter_districts vd "+
	                                                "inner join voters v on vd.voterid = v.voterid "+
	                                                "left join voter_count vc on v.voterid = vc.voterid and vc.tally_id = @tallid "+
	                                                "where vd.districtid = @distid "+
	                                                "order by v.lastname asc " +
	                                                "limit @take offset @skip;";

        protected static string partyvotingsql = @"select v.voterid, v.statevoterid, v.countyvoterid, v.lastname, v.firstname, " +
                                                    "vc.count_id, vc.tally_id, vc.methodid from voter_districts vd " +
                                                    "inner join voters v on vd.voterid = v.voterid and v.partyaffiliation = @partyid " +
                                                    "left join voter_count vc on v.voterid = vc.voterid and vc.tally_id = @tallid " +
                                                    "where vd.districtid = @distid " +
                                                    "order by v.lastname asc " +
                                                    "limit @take offset @skip;";

        public List<VoterWatch.dataclasses.voter> getVotingLedger(int distid, int tallyid, int skip, int take)
        {
            List<VoterWatch.dataclasses.voter> vlist = new List<VoterWatch.dataclasses.voter>();
            try
            {
                string connstring = ConfigurationManager.ConnectionStrings["voterwatchdb"].ConnectionString;
                MySqlConnection conn = new MySqlConnection(connstring);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(votinglistsql,conn);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@distid", distid);
                cmd.Parameters.AddWithValue("@tallid", tallyid);
                cmd.Parameters.AddWithValue("@skip", skip);
                cmd.Parameters.AddWithValue("@take", take);
                MySqlDataReader reader = cmd.ExecuteReader();
                int vidcol = reader.GetOrdinal("voterid");
                int statecol = reader.GetOrdinal("statevoterid");
                int countycol = reader.GetOrdinal("countyvoterid");
                int lastcol = reader.GetOrdinal("lastname");
                int firstcol = reader.GetOrdinal("firstname");
                int tallcol = reader.GetOrdinal("tally_id");
                int methcol = reader.GetOrdinal("methodid");
                int countcol = reader.GetOrdinal("count_id");
                while (reader.Read())
                {
                    vlist.Add(new VoterWatch.dataclasses.voter
                    {
                        voterid = reader.IsDBNull(vidcol) ? 0:reader.GetInt32(vidcol),
                        stateid = reader.GetString(statecol),
                        countyid = reader.GetString(countycol),
                        lastname = reader.GetString(lastcol),
                        firstname = reader.GetString(firstcol),
                        votercountid = reader.IsDBNull(countcol) ? 0:reader.GetInt32(countcol),
                        tallyid = reader.IsDBNull(tallcol) ? 0:reader.GetInt32(tallcol),
                        methodid = reader.IsDBNull(methcol) ? 0:reader.GetInt32(methcol)
                    });
                }
                reader.Close();
                cmd.Dispose();
                conn.Close();
            }
            catch (Exception ex)
            {
                ExceptionLogger.Log(MethodBase.GetCurrentMethod(), ex, distid, tallyid, skip, take);
            }

            return vlist;
        }

        public List<VoterWatch.dataclasses.voter> getPartyVotingLedger(int distid, int tallyid, int partyid, int skip, int take)
        {
            List<VoterWatch.dataclasses.voter> vlist = new List<VoterWatch.dataclasses.voter>();
            try
            {
                string connstring = ConfigurationManager.ConnectionStrings["voterwatchdb"].ConnectionString;
                MySqlConnection conn = new MySqlConnection(connstring);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(partyvotingsql,conn);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@distid", distid);
                cmd.Parameters.AddWithValue("@tallid", tallyid);
                cmd.Parameters.AddWithValue("@partyid", partyid);
                cmd.Parameters.AddWithValue("@skip", skip);
                cmd.Parameters.AddWithValue("@take", take);
                MySqlDataReader reader = cmd.ExecuteReader();
                int vidcol = reader.GetOrdinal("voterid");
                int statecol = reader.GetOrdinal("statevoterid");
                int countycol = reader.GetOrdinal("countyvoterid");
                int lastcol = reader.GetOrdinal("lastname");
                int firstcol = reader.GetOrdinal("firstname");
                int tallcol = reader.GetOrdinal("tally_id");
                int methcol = reader.GetOrdinal("methodid");
                int countcol = reader.GetOrdinal("count_id");
                while (reader.Read())
                {
                    vlist.Add(new VoterWatch.dataclasses.voter
                    {
                        voterid = reader.IsDBNull(vidcol) ? 0 : reader.GetInt32(vidcol),
                        stateid = reader.GetString(statecol),
                        countyid = reader.GetString(countycol),
                        lastname = reader.GetString(lastcol),
                        firstname = reader.GetString(firstcol),
                        votercountid = reader.IsDBNull(countcol) ? 0 : reader.GetInt32(countcol),
                        tallyid = reader.IsDBNull(tallcol) ? 0 : reader.GetInt32(tallcol),
                        methodid = reader.IsDBNull(methcol) ? 0 : reader.GetInt32(methcol)
                    });
                }
                reader.Close();
                cmd.Dispose();
                conn.Close();
            }
            catch (Exception ex)
            {
                ExceptionLogger.Log(MethodBase.GetCurrentMethod(), ex, distid, tallyid, skip, take);
            }
            return vlist;
        }

        public bool addVoterTally(int voterid, int tallyid, int vmethod)
        {
            bool added = false;
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                int ecount = db.voter_count.Where(vc => vc.voterid == voterid && vc.tally_id == tallyid).Count();
                if (ecount == 0)
                {
                    //no others--add
                    voter_count nvc = new voter_count { voterid = voterid, tally_id = tallyid, methodid = vmethod };
                    db.voter_count.AddObject(nvc);
                    db.SaveChanges();
                    added = true;
                }
                else
                {
                    //Not adding twice--just return an error
                    added = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.Log(MethodBase.GetCurrentMethod(), ex, voterid, tallyid, vmethod);
                added = false;
            }
            finally
            {
                db.Dispose();
            }


            return added;
        }


        public bool deleteVoterTally(int countid)
        {
            bool removed = false;
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                voter_count rvc = db.voter_count.Where(vc => vc.count_id == countid).Single();
                db.voter_count.DeleteObject(rvc);
                db.SaveChanges();
                removed = true;
            }
            catch (Exception ex)
            {
                ExceptionLogger.Log(MethodBase.GetCurrentMethod(), ex, countid);
                removed = false;
            }
            finally
            {
                db.Dispose();
            }
            return removed;
        }
    }

}
