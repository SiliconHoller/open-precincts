using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace VoterWatch.dataclasses
{
    [DataContract(Namespace = "http://siliconholler.net/geopolitical")]
    public class voter
    {
        private List<district> _districts;
        private List<address> _addresses;

        [DataMember]
        public int voterid { get; set; }
        [DataMember]
        public string stateid { get; set; }
        [DataMember]
        public string countyid { get; set; }
        [DataMember]
        public string lastname { get; set; }
        [DataMember]
        public string firstname { get; set; }
        [DataMember]
        public string middlename { get; set; }
        [DataMember]
        public string suffix { get; set; }
        [DataMember]
        public int yob { get; set; }
        [DataMember]
        public DateTime registrationdate { get; set; }
        [DataMember]
        public int affiliationid { get; set; }
        [DataMember]
        public string partyaffiliation { get; set; }
        [DataMember]
        public int votercountid { get; set; }
        [DataMember]
        public int tallyid { get; set; }
        [DataMember]
        public int methodid { get; set; }

        [DataMember]
        public List<district> districts
        {
            get
            {
                if (_districts == null) _districts = new List<district>();
                return _districts;
            }
            set
            {
                _districts = value;
            }
        }

        [DataMember]
        public List<address> addresses
        {
            get
            {
                if (_addresses == null) _addresses = new List<address>();
                return _addresses;
            }
            set { _addresses = value; }
        }
    }

    [DataContract(Namespace = "http://siliconholler.net/geopolitical")]
    public class address
    {
        [DataMember]
        public addressflags typeid { get; set; }
        [DataMember]
        public int addressid { get; set; }
        [DataMember]
        public string address1 { get; set; }
        [DataMember]
        public string address2 { get; set; }
        [DataMember]
        public string city { get; set; }
        [DataMember]
        public string state { get; set; }
        [DataMember]
        public string zip { get; set; }
        [DataMember]
        public string zipplus { get; set; }
        
    }

    [DataContract(Namespace = "http://siliconholler.net/geopolitical")]
    public class votersearch
    {
        private List<voter> _sres;

        [DataMember]
        public int seq { get; set; }
        [DataMember]
        public int skip { get; set; }
        [DataMember]
        public int take { get; set; }

        [DataMember]
        public List<voter> vresults
        {
            get
            {
                if (_sres == null) _sres = new List<voter>();
                return _sres;
            }
            set { _sres = value; }
        }
    }

    [DataContract(Namespace = "http://siliconholler.net/geopolitical")]
    public class addresssearch
    {
        private List<address> _sres;

        [DataMember]
        public int seq { get; set; }
        [DataMember]
        public int skip { get; set; }
        [DataMember]
        public int take { get; set; }
        [DataMember]
        public List<address> aresults
        {
            get
            {
                if (_sres == null) _sres = new List<address>();
                return _sres;
            }
            set { _sres = value; }
        }
    }

    [DataContract(Namespace = "http://siliconholler.net/geopolitical")]
    public class affiliation
    {
        [DataMember]
        public int affiliationid { get; set; }
        [DataMember]
        public string partycode { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string descr { get; set; }
    }

    [Flags]
    [DataContract(Namespace = "http://siliconholler.net/geopolitical")]
    public enum addressflags
    {
        [EnumMember]
        unset = 0,
        [EnumMember]
        residential = 1,
        [EnumMember]
        mailing = 2
    }

}
