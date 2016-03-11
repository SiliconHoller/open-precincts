using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace VoterWatch.dataclasses
{
    [DataContract]
    public class option
    {
        [DataMember]
        public string optval { get; set; }
        [DataMember]
        public string opttext { get; set; }
        [DataMember]
        public string opttip { get; set; }
    }
}
