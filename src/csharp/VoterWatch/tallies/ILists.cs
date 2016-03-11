using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace tallies
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ILists" in both code and config file together.
    [ServiceContract(Namespace = "http://siliconholler.net/geopolitical")]
    public interface ILists
    {
        [OperationContract]
        void DoWork();
    }
}
