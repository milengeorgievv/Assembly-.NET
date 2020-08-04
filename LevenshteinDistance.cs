using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Globalization;

public class myFunctions
{
	[Microsoft.SqlServer.Server.SqlFunction]
	public static SqlInt32 getLevenshteinDistance(SqlString First, SqlString Second)
	{
		string a = First.ToString();
		string b = Second.ToString();

        if (string.IsNullOrEmpty(a))
        {
            if (!string.IsNullOrEmpty(b))
            {
                return b.Length;
            }
            return 0;
        }

        if (string.IsNullOrEmpty(b))
        {
            if (!string.IsNullOrEmpty(a))
            {
                return a.Length;
            }
            return 0;
        }

        int cost;
        int[,] d = new int[a.Length + 1, b.Length + 1];
        int min1;
        int min2;
        int min3;

        for (int i = 0; i <= d.GetUpperBound(0); i += 1)
        {
            d[i, 0] = i;
        }

        for (int i = 0; i <= d.GetUpperBound(1); i += 1)
        {
            d[0, i] = i;
        }

        for (int i = 1; i <= d.GetUpperBound(0); i += 1)
        {
            for (int j = 1; j <= d.GetUpperBound(1); j += 1)
            {
                cost = (string.Compare(a[i - 1].ToString(), b[j - 1].ToString(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase)==0) ? 0 : 1;

                min1 = d[i - 1, j] + 1;
                min2 = d[i, j - 1] + 1;
                min3 = d[i - 1, j - 1] + cost;
                d[i, j] = Math.Min(Math.Min(min1, min2), min3);
            }
        }

        return d[d.GetUpperBound(0), d.GetUpperBound(1)];
    }	
}
