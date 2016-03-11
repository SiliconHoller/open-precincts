using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace VoterWatch.extensions
{
    public static class DataReads
    {
        public static string SafeString(this SqlDataReader reader, int colno)
        {
            if (!reader.IsDBNull(colno))
                return reader.GetString(colno);
            else
                return string.Empty;
        }
    }
}
