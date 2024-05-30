using Newtonsoft.Json;

namespace WarResolverClient.Helpers
{
    internal class SerializerHelper
    {
        public static JsonSerializerSettings GetJsonSerializerSettings() => new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
    }
}