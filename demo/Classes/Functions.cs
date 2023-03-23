using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.Classes
{
    public class Functions
    {

        public static string ConStr = System.Configuration.ConfigurationManager.ConnectionStrings["AppDbContext"].ConnectionString;

        /// <summary>
        /// Key to authenticate server-to-server requests from this application to manage.runtime portal
        /// </summary>
        public static string CrossOriginApiAuthKey = "";
        public static string ManageApiEndpoint = "";

        public static string GetRandomString(int len)
        {

            string arr = "ABCDEFGHKMNPQRSTUVWXYZ123456789abcdefghkmnpqrstuvwxyz";
            string code = "";
            Random rand = new Random();

            if (len < 1) { return ""; }

            for (int myi = 1; myi < len; myi++)
            {
                code += arr.Substring(rand.Next(1, arr.Length), 1);
            }

            return code;

        }

        public static string HashToString(byte[] bytes)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }

    }
}