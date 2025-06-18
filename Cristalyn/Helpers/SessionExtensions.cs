using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;

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

        public static void SetDecimal(this ISession session, string key, decimal value)
        {
            session.Set(key, BitConverter.GetBytes(decimal.ToDouble(value)));
        }

        public static decimal? GetDecimal(this ISession session, string key)
        {
            var data = session.Get(key);
            if (data == null || data.Length < 8)
                return null;
            return (decimal)BitConverter.ToDouble(data, 0);
        }
    }
}
