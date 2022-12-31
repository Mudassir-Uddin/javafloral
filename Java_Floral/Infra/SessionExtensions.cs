using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Java_Floral.Infra
{
    public static class SessionExtensions
    {
        public static void SetSession(this ISession session /*session*/, string key/*Key*/, object value/*value*/)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        // T <T>  any Type of  Data ==> in (string , arrray , bool , int) 
        public static T GetSession<T>(this ISession session, string key)
        {
            var sessionData = session.GetString(key);
            return sessionData == null ? default(T) : JsonConvert.DeserializeObject<T>(sessionData);
        }
    }
}
