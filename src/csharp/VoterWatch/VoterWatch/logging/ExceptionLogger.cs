using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading;

namespace VoterWatch.logging
{
    public class ExceptionLogger
    {
        protected MethodBase _method;
        protected Exception _ex;
        protected object[] _vals;
        protected string _clazz;
        protected string _func;

        protected MethodBase Method
        {
            get { return _method; }
            set { _method = value; }
        }

        protected Exception Ex
        {
            get { return _ex; }
            set { _ex = value; }
        }

        protected object[] Params
        {
            get { return _vals; }
            set { _vals = value; }
        }

        protected void LogMethodData()
        {
            ThreadStart ts = new ThreadStart(this.WriteMethodError);
            Thread t = new Thread(ts);
            t.Start();
        }

        protected void LogEx()
        {
            ThreadStart ts = new ThreadStart(this.WriteExceptionError);
            Thread t = new Thread(ts);
            t.Start();
        }

        protected void WriteMethodError()
        {
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                ParameterInfo[] parms = _method.GetParameters();
                object[] namevals = new object[2 * parms.Length];
                string paramlist = "";
                try
                {
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0, j = 0; i < parms.Length; i++, j += 2)
                    {
                        sb.Append("{");
                        sb.Append(j);
                        sb.Append("}={");
                        sb.Append(j + 1);
                        sb.Append("},");
                        namevals[j] = parms[i].Name;
                        if (i < _vals.Length) namevals[j + 1] = _vals[i];
                    }
                    paramlist = sb.ToString();
                }
                catch (Exception ex)
                {
                    //something went wrong--we just note that the params are not available and move on
                    paramlist = "Param listing unavailable";
                }

                errorlog entry = new errorlog
                {
                    errclass = _method.ReflectedType.Name,
                    errmethod = _method.Name,
                    errargs = paramlist,
                    errdepth = 0,
                    errmessage = _ex.Message,
                    errstacktrace = _ex.ToString()
                };
                db.errorlogs.AddObject(entry);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                //looks like we're sol
            }
            finally
            {
                db.Dispose();
            }

        }

        public void WriteExceptionError()
        {
            voterwatchEntities db = new voterwatchEntities();
            try
            {
                errorlog entry = new errorlog
                {
                    errclass = _clazz,
                    errmethod = _func,
                    errmessage = _ex.Message,
                    errstacktrace = _ex.ToString()
                };
                db.errorlogs.AddObject(entry);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                db.Dispose();
            }
        }

        public static void Log(MethodBase method, Exception ex, params object[] values)
        {
            ExceptionLogger mlogger = new ExceptionLogger();
            mlogger.Method = method;
            mlogger.Ex = ex;
            mlogger.Params = values;
            mlogger.LogMethodData();
        }

        public static void Log(string clazz, string method, Exception ex)
        {
            ExceptionLogger mlogger = new ExceptionLogger();
            mlogger.Ex = ex;

        }
    }
}
