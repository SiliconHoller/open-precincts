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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "VoterFields" in code, svc and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class VoterFields : IVoterFields
    {

        public List<string> statevoterid(string stxt)
        {
            List<string> sres = new List<string>();
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                sres = db.voters
                        .Where(v=>v.statevoterid.Contains(stxt))
                        .OrderBy(v => v.statevoterid)
                        .Select(v => v.statevoterid)
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

        public List<string> countyvoterid(string stxt)
        {
            List<string> sres = new List<string>();
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                sres = db.voters
                        .Where(v => v.countyvoterid.Contains(stxt))
                        .OrderBy(v => v.countyvoterid)
                        .Select(v => v.countyvoterid)
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

        public List<string> lastname(string stxt)
        {
            List<string> sres = new List<string>();
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                sres = db.voters
                        .Where(v => v.lastname.Contains(stxt))
                        .OrderBy(v => v.lastname)
                        .Select(v => v.lastname)
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

        public List<string> firstname(string stxt)
        {
            List<string> sres = new List<string>();
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                sres = db.voters
                        .Where(v => v.firstname.Contains(stxt))
                        .OrderBy(v => v.firstname)
                        .Select(v => v.firstname)
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

        public List<string> middlename(string stxt)
        {
            List<string> sres = new List<string>();
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                sres = db.voters
                        .Where(v => v.middlename.Contains(stxt))
                        .OrderBy(v => v.middlename)
                        .Select(v => v.middlename)
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

        public List<string> suffix(string stxt)
        {
            List<string> sres = new List<string>();
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                sres = db.voters
                        .Where(v => v.suffix.Contains(stxt))
                        .OrderBy(v => v.suffix)
                        .Select(v => v.suffix)
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
