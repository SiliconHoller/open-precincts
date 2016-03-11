using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using VoterWatch;
using System.ServiceModel.Activation;

namespace searchFields
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DistrictFields" in code, svc and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class DistrictFields : IDistrictFields
    {

        public List<string> identifiers(string stxt)
        {
            List<string> sres = new List<string>();
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                sres = db.districts
                        .Where(d => d.identifier.Contains(stxt))
                        .OrderBy(d => d.identifier)
                        .Select(d => d.identifier)
                        .Distinct()
                        .Take(10)
                        .ToList<string>();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                db.Dispose();
            }
            return sres;
        }

        public List<string> names(string stxt)
        {
            List<string> sres = new List<string>();
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                sres = db.districts
                        .Where(d => d.name.Contains(stxt))
                        .OrderBy(d => d.name)
                        .Select(d => d.name)
                        .Distinct()
                        .Take(10)
                        .ToList<string>();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                db.Dispose();
            }
            return sres;
        }

        public List<string> descr(string stxt)
        {
            List<string> sres = new List<string>();
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                sres = db.districts
                        .Where(d => d.descr.Contains(stxt))
                        .OrderBy(d => d.descr)
                        .Select(d => d.descr)
                        .Distinct()
                        .Take(10)
                        .ToList<string>();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                db.Dispose();
            }
            return sres;
        }
    }
}
