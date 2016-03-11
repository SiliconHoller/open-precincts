using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace tallies
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Counts" in code, svc and config file together.
    public class Counts : ICounts
    {
        public void DoWork()
        {
        }

        public int getRegisteredVoters(int distid)
        {
            throw new NotImplementedException();
        }

        public int getVoterAddresses(int distid)
        {
            throw new NotImplementedException();
        }

        public int getTalliedVoterCount(int distid, int tallyid)
        {
            throw new NotImplementedException();
        }

        public int getTalliedVoterByMethod(int distid, int tallyid, int methodid)
        {
            throw new NotImplementedException();
        }
    }
}
