using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;
using VoterWatch;

namespace tallies
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Ledger" in code, svc and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Ledger : ILedger
    {

        public List<VoterWatch.dataclasses.voter> getVotingLedger(int distid, int tallyid, int skip, int take)
        {
            List<VoterWatch.dataclasses.voter> vlist = new List<VoterWatch.dataclasses.voter>();
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                var ledger = db.votercounts(distid, tallyid, skip, take);
                foreach (var v in ledger)
                {
                    
                    VoterWatch.dataclasses.voter nv = new VoterWatch.dataclasses.voter
                    {
                        
                    };
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                db.Dispose();
            }

            return vlist;
        }

        public bool addVoterTally(int voterid, int tallyid, int vmethod)
        {
            throw new NotImplementedException();
        }


        public bool deleteVoterTally(int countid)
        {
            throw new NotImplementedException();
        }
    }
}
