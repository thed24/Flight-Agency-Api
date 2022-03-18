using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Flight_Agency_Infrastructure.Common
{
    public static class Mapper
    {
        public static async Task<T?> MapStreamToObject<T>(Stream stream)
        {
            var content = await new StreamReader(stream).ReadToEndAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }

        public static T? MapQueryParamsToObject<T>(IQueryCollection collection)
        {
            var json = JsonConvert.SerializeObject(collection.ToDictionary(q => q.Key, q => q.Value.ToString()));
            json = json.Replace("\"", "");
            json = json.Replace("\\", "");

            Console.WriteLine(json);
            return JsonConvert.DeserializeObject<T>(json.Replace("\"", ""));
        }
    }
}
