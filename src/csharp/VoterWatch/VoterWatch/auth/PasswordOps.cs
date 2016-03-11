using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using VoterWatch.logging;
using System.Reflection;
using System.Net.Mail;

namespace VoterWatch.auth
{
    public sealed class PasswordOps
    {
        protected string linkformat = "{0}?userid={1}&resetidentifier={2}";
        protected static string resetmsgformat = "<p>This is an automated message; please do not reply.</p>"+
            "<p>Your account for Precinct Captain has been reset.  In order to login, use <a href='{0}'>this link</a> to set a new password.</p>"+
            "<p>This email is sent when your account has been created, and whenever there is a clear need, such as requesting a new password when a user has "+
            "forgotten their password, or on when upgrading the security system of Precinct Captain.</p>"+
            "<p>If you believe your password should <strong>not</strong> have been reset, contact your site administrator for further information "+
            "or to report suspicious activity.</p>";
        protected static string resetsubjformat = "Password Reset for {0} {1}";

        public string generateSaltString()
        {
            return Guid.NewGuid().ToString();
        }

        public byte[] generateSalt()
        {
            return getBytes(Guid.NewGuid().ToString());
        }

        public byte[] generateHash(string salt, string pass)
        {
            byte[] saltbytes = getBytes(salt);
            byte[] passbytes = getBytes(pass);
            return generateHash(saltbytes, passbytes);
        }

        public byte[] generateHash(string salt, byte[] passbytes)
        {
            byte[] saltbytes = getBytes(salt);
            return generateHash(saltbytes, passbytes);
        }

        public byte[] generateHash(byte[] saltbytes, string pass)
        {
            byte[] passbytes = getBytes(pass);
            return generateHash(saltbytes, passbytes);
        }

        public byte[] generateHash(byte[] saltbytes, byte[] passbytes)
        {
            int slen = saltbytes.Length;
            int plen = passbytes.Length;
            int htotal = slen + plen;
            byte[] hashbuff = new byte[htotal];
            //copy into buffer
            for (int i = 0; i < saltbytes.Length; i++) hashbuff[i] = saltbytes[i];
            for (int j = slen; j < htotal; j++) hashbuff[j] = passbytes[j - slen];
            SHA256 hasher = SHA256Managed.Create();
            byte[] hashval = hasher.ComputeHash(hashbuff);
            //clear buffer and passbytes
            for (int z = 0; z < hashbuff.Length; z++) hashbuff[z] = 0;
            for (int z = 0; z < passbytes.Length; z++) passbytes[z] = 0;
            return hashval;
        }

        public byte[] getBytes(string bstring)
        {
            return Encoding.UTF8.GetBytes(bstring);
        }

        public bool setNewPassword(int userid, string npass)
        {
            bool pset = false;
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                user puser = db.users.Where(u => u.userid == userid).Single();
                byte[] nsalt = generateSalt();
                byte[] phash = generateHash(nsalt, npass);
                puser.usersalt = nsalt;
                puser.userpass = phash;
                db.SaveChanges();
                pset = true;
            }
            catch (Exception ex)
            {
                ExceptionLogger.Log(MethodBase.GetCurrentMethod(), ex, userid, "Passwords are not logged");
                pset = false;
            }
            finally
            {
                db.Dispose();
            }

            return pset;
        }

        public bool resetPassword(int userid, string reseturl)
        {
            bool preset = false;
            voterwatchEntities db = new voterwatchEntities();
            SmtpClient mclient = new SmtpClient();
            try
            {
                user udata = db.users.Where(u => u.userid == userid).Single();
                //get a salt and temp password;
                string nsalt = generateSaltString();
                string npass = generateSaltString();
                //construct the message
                string passlink = String.Format(linkformat, reseturl, udata.userid, npass);
                string mailsubj = String.Format(resetsubjformat, udata.firstname, udata.lastname);
                string mailbody = String.Format(resetmsgformat, passlink);
                //update the values
                udata.usersalt = getBytes(nsalt);
                udata.userpass = generateHash(nsalt, npass);
                //save and then fire off message
                db.SaveChanges();
                MailMessage msg = new MailMessage("UserAdmin@precinctcaptain.com", udata.emailaddress);
                msg.Subject = mailsubj;
                msg.IsBodyHtml = true;
                msg.Body = mailbody;
                mclient.Send(msg);
                preset = true;
            }
            catch (Exception ex)
            {
                preset = false;
                ExceptionLogger.Log(MethodBase.GetCurrentMethod(), ex, userid, reseturl);
            }
            finally
            {
                mclient.Dispose();
                db.Dispose();
            }
            return preset;
        }

    }
}
