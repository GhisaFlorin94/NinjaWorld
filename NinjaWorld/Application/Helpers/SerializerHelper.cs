using Newtonsoft.Json;

namespace NinjaWorld.Application.Helpers
{
    internal class SerializerHelper
    {
        public static JsonSerializerSettings GetJsonSerializerSettings() => new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
    }
}
