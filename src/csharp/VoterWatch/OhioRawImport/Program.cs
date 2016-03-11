using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using VoterWatch;

namespace OhioRawImport
{
    class Program
    {
        protected static string electionmarker = "TOWNSHIP,VILLAGE,WARD,";

        static void Main(string[] args)
        {
            
            DateTime importdt = DateTime.Now;
            string importname = importdt.ToShortDateString();

            try
            {
                //Open the file
                string importfile = ConfigurationManager.AppSettings["importfile"];
                StreamReader freader = new StreamReader(File.OpenRead(importfile));
                //Get the firstline
                string header = freader.ReadLine();
                int elindex = header.IndexOf(electionmarker);
                string rawelections = header.Substring(elindex + 22);
                Console.WriteLine("Election Data: {0}", rawelections);
                RawParser parser = new RawParser();
                int icount = 0;
                string txtdata;
                List<ohioraw> vlist = new List<ohioraw>();
                while (!freader.EndOfStream)
                {
                    txtdata = freader.ReadLine();
                    ohioraw voter = parser.parseLine(txtdata);
                    if (voter != null)
                    {
                        voter.rawelectionlist = rawelections;
                        voter.importdate = importdt;
                        voter.importidentifier = importname;
                        vlist.Add(voter);
                    }
                    if (vlist.Count % 10000 == 0)
                    {
                        voterwatchEntities db = new voterwatchEntities();
                        icount += vlist.Count;
                        foreach (ohioraw nvoter in vlist) db.ohioraws.AddObject(nvoter);
                        db.SaveChanges();
                        db.Dispose();
                        vlist = new List<ohioraw>();
                        System.GC.Collect();
                        Console.WriteLine("Flushed {0} to storage", icount);
                    }
                }
                voterwatchEntities dbf = new voterwatchEntities();
                //Save the last few
                icount += vlist.Count;
                foreach (ohioraw nvoter in vlist) dbf.ohioraws.AddObject(nvoter);
                dbf.SaveChanges();
                vlist = new List<ohioraw>();
                dbf.Dispose();
                Console.WriteLine("Finished with {0} parsed voter record.", icount);
            }
            catch (Exception ex)
            {
                while (ex != null)
                {
                    Console.WriteLine(ex.Message);
                    Console.Error.WriteLine(ex.StackTrace);
                    ex = ex.InnerException;
                }
            }
            Console.WriteLine("Finished.  Hit enter key to close.");
            Console.ReadLine();
        }
    }
}
