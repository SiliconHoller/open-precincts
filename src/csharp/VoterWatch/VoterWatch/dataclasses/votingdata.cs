using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace VoterWatch.dataclasses
{
    [DataContract]
    public class votemethod
    {
        [DataMember]
        public int methodid { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public int seq { get; set; }
        [DataMember]
        public string descr { get; set; }
    }

    [DataContract]
    public class tallytype
    {
        [DataMember]
        public int typeid { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public int seq { get; set; }
        [DataMember]
        public string descr { get; set; }
    }

    [DataContract]
    public class tally 
    {
        [DataMember]
        public int tallyid { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public int ttypeid { get; set; }
        [DataMember]
        public string descr { get; set; }
        [DataMember]
        public DateTime tstart { get; set; }
        [DataMember]
        public DateTime tend { get; set; }
    }
}
