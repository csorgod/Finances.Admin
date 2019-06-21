using Newtonsoft.Json;

namespace Finances.Common.Helpers
{
    public class JsonTransformer
    {
        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

    }
}
