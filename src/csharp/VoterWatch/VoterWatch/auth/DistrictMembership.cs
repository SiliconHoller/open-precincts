using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Security.Cryptography;
using VoterWatch.logging;
using System.Reflection;
using System.IO;


namespace VoterWatch.auth
{
    public class DistrictMembership : MembershipProvider
    {
        protected static int appid;
        protected static string appname;

        public override string ApplicationName
        {
            get
            {
                return appname;
            }
            set
            {
                appname = value;
                setAppId();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            //Not implemented for general membership provider
            return false;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            //Not implemented for general membership provider
            return false;
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            //Not implemented for general membership provider
            status = MembershipCreateStatus.UserRejected;
            return new MembershipUser(null,null,null,null,null,null,false, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now);
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            //Not implemented for general membership provider
            return false;
        }

        public override bool EnablePasswordReset
        {
            get { return false; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return false; }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            totalRecords = 0;
            return new MembershipUserCollection();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            totalRecords = 0;
            return new MembershipUserCollection();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            totalRecords = 0;
            return new MembershipUserCollection();
        }

        public override int GetNumberOfUsersOnline()
        {
            return 0;
        }

        public override string GetPassword(string username, string answer)
        {
            return "";
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            return new MembershipUser(null, null, null, null, null, null, false, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now);
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            return new MembershipUser(null, null, null, null, null, null, false, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now);
        }

        public override string GetUserNameByEmail(string email)
        {
            return email;
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return 0; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return 0; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return 6; }
        }

        public override int PasswordAttemptWindow
        {
            get { return 0; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { return MembershipPasswordFormat.Clear; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return ""; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return false; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return true; }
        }

        public override string ResetPassword(string username, string answer)
        {
            return "";
        }

        public override bool UnlockUser(string userName)
        {
            return false;
        }

        public override void UpdateUser(MembershipUser user)
        {
            
        }

        public override bool ValidateUser(string username, string password)
        {
            bool validated = false;
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                user usr = db.users.Where(u => u.emailaddress == username).Single();
                byte[] salt = usr.usersalt;
                byte[] passhash = usr.userpass;
                PasswordOps passops = new PasswordOps();
                byte[] hashval = passops.generateHash(salt, password);
                bool vtest = true;
                for (int i = 0; i < passhash.Length; i++) vtest = vtest && (hashval[i] == passhash[i]);
                validated = vtest;
            }
            catch (Exception ex)
            {
                validated = false;
                ExceptionLogger.Log(MethodBase.GetCurrentMethod(), ex, username, "Password not logged.");
            }
            finally
            {
                db.Dispose();
            }

            return validated;
        }

        private void setAppId()
        {
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                appid = db.applications.Where(a => a.appname == appname).Single().appid;
            }
            catch (Exception ex)
            {
                appid = 0;
                ExceptionLogger.Log(MethodBase.GetCurrentMethod(), ex, new object[] {});
            }
            finally
            {
                db.Dispose();
            }
        }
    }
}
