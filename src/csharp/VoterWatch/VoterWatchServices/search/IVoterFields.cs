using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace VoterWatchServices.search
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IVoterFields" in both code and config file together.
    [ServiceContract]
    public interface IVoterFields
    {
        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json)]
        List<string> statevoterid(string stxt);

        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json)]
        List<string> countyvoterid(string stxt);

        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json)]
        List<string> lastname(string stxt);

        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json)]
        List<string> firstname(string stxt);

        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json)]
        List<string> middlename(string stxt);

        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json)]
        List<string> suffix(string stxt);

    }
}
