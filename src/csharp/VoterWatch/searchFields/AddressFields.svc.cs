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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AddressFields" in code, svc and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AddressFields : IAddressFields
    {

        public List<string> addr1(string stxt)
        {
            List<string> sres = new List<string>();
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                sres = db.addresses
                        .Where(add => add.address1.Contains(stxt))
                        .OrderBy(a => a.address1)
                        .Select(a => a.address1)
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

        public List<string> addr2(string stxt)
        {
            List<string> sres = new List<string>();
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                sres = db.addresses
                        .Where(add => add.address2.Contains(stxt))
                        .OrderBy(a => a.address2)
                        .Select(a => a.address2)
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

        public List<string> city(string stxt)
        {
            List<string> sres = new List<string>();
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                sres = db.addresses
                        .Where(add => add.city.Contains(stxt))
                        .OrderBy(a => a.city)
                        .Select(a => a.city)
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

        public List<string> state(string stxt)
        {
            List<string> sres = new List<string>();
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                sres = db.addresses
                        .Where(add => add.state.Contains(stxt))
                        .OrderBy(a => a.state)
                        .Select(a => a.state)
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

        public List<string> zip(string stxt)
        {
            List<string> sres = new List<string>();
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                sres = db.addresses
                        .Where(add => add.zip.Contains(stxt))
                        .OrderBy(a => a.zip)
                        .Select(a => a.zip)
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

        public List<string> z4(string stxt)
        {
            List<string> sres = new List<string>();
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                sres = db.addresses
                        .Where(add => add.zip_plusfour.Contains(stxt))
                        .OrderBy(a => a.zip_plusfour)
                        .Select(a => a.zip_plusfour)
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
