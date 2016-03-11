using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace VoterWatch.auth
{
    public class DistrictUser : MembershipUser
    {
        protected string uname;
        protected string fname;
        protected string lname;
        protected string descr;
        protected int uid;
        protected DateTime createddt;

        public DistrictUser()
            : base()
        {
            uname = null;
        }

        public DistrictUser(string username)
            : base()
        {
            uname = username;    
        }

        public DistrictUser(string providername,
                            string username,
                            object providerUserKey,
                            string email,
                            string passwordQuestion,
                            string comment,
                            bool isApproved,
                            bool isLockedOut,
                            DateTime creationDate,
                            DateTime lastLoginDate,
                            DateTime lastActivityDate,
                            DateTime lastPasswordChangedDate,
                            DateTime lastLockedOutDate,
                            bool isSubscriber,
                            string customerID) :
                            base(providername,
                                username,
                                providerUserKey,
                                email,
                                passwordQuestion,
                                comment,
                                isApproved,
                                isLockedOut,
                                creationDate,
                                lastLoginDate,
                                lastActivityDate,
                                lastPasswordChangedDate,
                                lastLockedOutDate)
        {
            uname = username;
            
        }

        public string FirstName
        {
            get { return fname; }
            set { fname = value; }
        }

        public string LastName
        {
            get { return lname; }
            set { lname = value; }
        }

        public int UserId
        {
            get { return uid; }
            set { uid = value; }
        }

        public override string Comment 
        {
            get
            { return descr; }
            set
            {
                descr = value;
            }
        }

        public override string Email 
        {
            get
            { return uname; }
            set { }
        }



        public override bool ChangePassword(string oldPassword, string newPassword)
        {
            bool pchanged = false;

            return pchanged;
        }

        public override bool ChangePasswordQuestionAndAnswer(string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            //do not use Q&A this version
            return false;
        }

        public override string GetPassword() {
            //never store the password--unable to retrieve
            return "";   
        }

        public override string GetPassword(string passwordAnswer)
        {
            return GetPassword();
        }

        public override string ResetPassword()
        {
            return "";
        }

        public override string ResetPassword(string passwordAnswer)
        {
            return "";
        }

        public override string ToString()
        {
            return String.Format("{0}, {1} {2}", lname, fname, uname);
        }

        public override bool UnlockUser()
        {
            return false;
        }
    }
}
