using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokatsApplication_v1._1
{
    internal class SessionManager
    {
        private static Dictionary<string, object> sessionData = new Dictionary<string, object>();

        public static void Set(string key, object value)
        {
            sessionData[key] = value;
        }

        public static T Get<T>(string key)
        {
            if (sessionData.ContainsKey(key))
                return (T)sessionData[key];
            return default(T);
        }

        public static void Clear()
        {
            sessionData.Clear();
        }
    }
}
