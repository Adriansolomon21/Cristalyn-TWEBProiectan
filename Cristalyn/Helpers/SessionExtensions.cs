using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Cristalyn.Helpers
{
    public static class SessionExtensions
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            string json = JsonConvert.SerializeObject(value);
            session.SetString(key, json);
        }

        public static T? GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return string.IsNullOrEmpty(value) ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
