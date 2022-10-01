using Newtonsoft.Json;

namespace IDistributedCacheImp.Helper
{
    public static class Extension
    {
        public static string ToJsonString(this object data)
        {
            return JsonConvert.SerializeObject(data, Formatting.Indented);
        }
    }
}
