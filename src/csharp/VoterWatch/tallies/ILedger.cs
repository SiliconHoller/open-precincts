using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace tallies
{
    
    [ServiceContract(Namespace = "http://siliconholler.net/geopolitical")]
    public interface ILedger
    {
        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json)]
        List<VoterWatch.dataclasses.voter> getVotingLedger(int distid, int tallyid, int skip, int take);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json)]
        bool addVoterTally(int voterid, int tallyid, int vmethod);

        [OperationContract]
        [WebInvoke(Method = "DELETE",
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json)]
        bool deleteVoterTally(int countid);
    }
}
