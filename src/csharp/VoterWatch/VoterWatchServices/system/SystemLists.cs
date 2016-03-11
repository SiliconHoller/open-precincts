using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using VoterWatch.dataclasses;
using VoterWatch;
using VoterWatch.logging;
using VoterWatch.extensions;
using System.Reflection;
using System.ServiceModel.Activation;

namespace VoterWatchServices.system
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SystemLists" in code, svc and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SystemLists : ISystemLists
    {

        public List<option> getParties()
        {
            List<option> olist = new List<option>();
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                var pvals = db.affiliations.OrderBy(p => p.name);
                foreach (var p in pvals) olist.Add(p.toOption());
            }
            catch (Exception ex)
            {
                ExceptionLogger.Log(MethodBase.GetCurrentMethod(), ex, new object[] { });
            }
            finally
            {
                db.Dispose();
            }
            return olist;
        }


        public List<option> getVotingMethods()
        {
            List<option> olist = new List<option>();
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                var mvals = db.voter_method.OrderBy(vm => vm.seq);
                foreach (var p in mvals) olist.Add(p.toOption());
            }
            catch (Exception ex)
            {
                ExceptionLogger.Log(MethodBase.GetCurrentMethod(), ex, new object[] { });
            }
            finally
            {
                db.Dispose();
            }
            return olist;
        }


        public List<option> getTallies()
        {
            List<option> olist = new List<option>();
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                var mvals = db.tallies.OrderBy(t => t.event_start);
                foreach (var p in mvals) olist.Add(p.toOption());
            }
            catch (Exception ex)
            {
                ExceptionLogger.Log(MethodBase.GetCurrentMethod(), ex, new object[] { });
            }
            finally
            {
                db.Dispose();
            }
            return olist;
        }
    }
}
