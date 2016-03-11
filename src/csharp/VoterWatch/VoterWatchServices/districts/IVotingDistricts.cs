using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using VoterWatch.dataclasses;

namespace VoterWatchServices.districts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IVotingDistricts" in both code and config file together.
    [ServiceContract(Namespace = "http://siliconholler.net/geopolitical")]
    public interface IVotingDistricts
    {
        #region District Type operations

        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json)]
        List<districttype> getTypes();

        #endregion

        #region District Operations

        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json)]
        districtsearch searchDistricts(string typeid, string ident, string name, string descr, string skip, string take, int sseq);

        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json)]
        List<district> getDistricts(int typeid, string skip, string take);

        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json)]
        List<district> getAllDistricts();

        #endregion

        #region Graph structure operations

        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json)]
        List<district> getParents(int childid);

        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json)]
        List<district> getChildren(int parentid);

        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json)]
        bool isParent(int distid);

        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json)]
        bool isLeaf(int distid);

        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json)]
        bool isChild(int distid);

        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json)]
        bool isOrphan(int distid);

        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json)]
        bool isRoot(int distid);

        #endregion

        #region User Districts

        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json)]
        List<district> userDistricts();

        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json)]
        List<district> userVotingDistricts();

        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json)]
        List<district> userRegionalDistricts();

        #endregion
    }
}
