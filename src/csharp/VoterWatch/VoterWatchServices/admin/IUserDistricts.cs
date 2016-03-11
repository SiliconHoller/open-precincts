using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace VoterWatchServices.admin
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUserDistricts" in both code and config file together.
    [ServiceContract(Namespace = "http://siliconholler.net/geopolitical")]
    public interface IUserDistricts
    {
        [OperationContract]
        [WebInvoke(Method = "PUT",
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json)]
        bool addUserDistrict(int uid, int distid);

        [OperationContract]
        [WebInvoke(Method = "DELETE",
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json)]
        bool removeUserDistrict(int uid, int distid);
    }
}
