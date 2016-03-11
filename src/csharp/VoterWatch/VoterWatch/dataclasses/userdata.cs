using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace VoterWatch.dataclasses
{
    [DataContract]
    public class userdata
    {
        protected List<district> dlist;

        [DataMember]
        public int userid { get; set; }
        [DataMember]
        public string fname { get; set; }
        [DataMember]
        public string lname { get; set; }
        [DataMember]
        public string username { get; set; }
        [DataMember]
        public string descr { get; set; }
        //[DataMember]
        //public DateTime createddt { get; set; }
        //[DataMember]
        //public DateTime modifieddt { get; set; }
        //[DataMember]
        //public string modifiedby { get; set; }
        [DataMember]
        public List<district> districts
        {
            get
            {
                if (dlist == null) dlist = new List<district>();
                return dlist;
            }
            set
            {
                dlist = value;
            }
        }
    }

    [DataContract]
    public class roledata
    {
        [DataMember]
        public int roleid { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string descr { get; set; }
        [DataMember]
        public int seq { get; set; }
    }

    [DataContract]
    public class appdata
    {
        [DataMember]
        public int appid { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string descr { get; set; }
        [DataMember]
        public int seq { get; set; }
    }
}
