using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using VoterWatch.dataclasses;

namespace VoterWatchServices.voters
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IVoterInfo" in both code and config file together.
    [ServiceContract(Namespace = "http://siliconholler.net/geopolitical")]
    public interface IVoterInfo
    {
        #region Search data

        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json)]
        votersearch searchVoters(string stateid, string countyid, string districtid, string lastname, string firstname, string middlename, string suffix, string yob, string skip, string take, int seq);

        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json)]
        addresssearch searchAddresses(string addrid, string addr1, string addr2, string city, string state, string zip, string z4, string skip, string take, int seq);

        #endregion

        #region Individual Voter info

        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json)]
        voter getVoter(int vid);

        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json)]
        List<address> getAddresses(int vid);

        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json)]
        district getPollDistrict(int vid);

        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json)]
        List<district> getVotingDistricts(int vid);

        #endregion

        #region Individual address info

        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json)]
        votersearch getAddressVoters(int addrid);

        #endregion

        #region Mass voter info

        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json)]
        List<voter> getDistrictVoters(int districtid, string affid, string skip, string take);

        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json)]
        List<affiliation> getParties();

        #endregion

        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json)]
        int getRegisteredVoters(int districtid);

        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json)]
        int getPartyRegistered(int districtid, int affid);


    }
}
