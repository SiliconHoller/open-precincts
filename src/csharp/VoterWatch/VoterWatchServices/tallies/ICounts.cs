using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace VoterWatchServices.tallies
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICounts" in both code and config file together.
    [ServiceContract(Namespace = "http://siliconholler.net/geopolitical")]
    public interface ICounts
    {
        [OperationContract]
        int getRegisteredVoters(int distid);

        [OperationContract]
        int getVoterAddresses(int distid);

        [OperationContract]
        int getTalliedVoterCount(int distid, int tallyid);

        [OperationContract]
        int getTalliedVoterByMethod(int distid, int tallyid, int methodid);

    }
}
