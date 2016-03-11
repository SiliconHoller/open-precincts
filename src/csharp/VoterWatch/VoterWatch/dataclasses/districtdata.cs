using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace VoterWatch.dataclasses
{
    [DataContract(Namespace="http://siliconholler.net/geopolitical")]
    public class district
    {
        protected List<int> _parents;
        protected List<int> _children;

        /// <summary>
        /// Database identifier (primary key
        /// </summary>
        [DataMember]
        public int districtid { get; set; }
        /// <summary>
        /// Foreign key to types
        /// </summary>
        [DataMember]
        public int districttypeid { get; set; }
        /// <summary>
        /// String value for type
        /// </summary>
        [DataMember]
        public string districttype { get; set; }
        /// <summary>
        /// Governing-body's specific identifier for the district
        /// </summary>
        [DataMember]
        public string identifier { get; set; }
        /// <summary>
        /// Common name for the distrinct, may be same as the identifier, depending upon usage
        /// </summary>
        [DataMember]
        public string name { get; set; }
        /// <summary>
        /// Additional information needed to resolve common values
        /// </summary>
        [DataMember]
        public string description { get; set; }
        /// <summary>
        /// List of primary key values for immediate parents, if any.
        /// </summary>
        [DataMember]
        public List<int> parents
        {
            get
            {
                if (_parents == null) _parents = new List<int>();
                return _parents;
            }
            set { _parents = value; }
        }

        /// <summary>
        /// List of primary key values for immediate children, if any.
        /// </summary>
        [DataMember]
        public List<int> children
        {
            get
            {
                if (_children == null) _children = new List<int>();
                return _children;
            }
            set { _children = value; }
        }
    }

    [DataContract(Namespace = "http://siliconholler.net/geopolitical")]
    public class districttype
    {
        /// <summary>
        /// primary key for this type
        /// </summary>
        [DataMember]
        public int districttypeid { get; set; }
        /// <summary>
        /// Common  name for the type
        /// </summary>
        [DataMember]
        public string name { get; set; }
        /// <summary>
        /// General description for this type.
        /// </summary>
        [DataMember]
        public string description { get; set; }
    }

    [DataContract(Namespace = "http://siliconholler.net/geopolitical")]
    public class districtsearch
    {
        private List<district> _sres;

        [DataMember]
        public int seq { get; set; }
        [DataMember]
        public int skip { get; set; }
        [DataMember]
        public int take { get; set; }

        [DataMember]
        public List<district> dresults 
        {
            get
            {
                if (_sres == null) _sres = new List<district>();
                return _sres;
            }
            set { _sres = value; }
        }
    }

    [DataContract(Namespace = "http://siliconholler.net/geopolitical")]
    public class districtrelationship
    {
        [DataMember]
        public int childdistrict { get; set; }
        [DataMember]
        public int parentdistrict { get; set; }
    }
}
