using System.IO;
using System.Text;

using Microsoft.Extensions.FileProviders;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Amazon.TestParameters
{
    public static class IotEndpointAddress
    {
        private static readonly IFileProvider fileProvider =
            new EmbeddedFileProvider(typeof(IotEndpointAddress).Assembly, typeof(IotEndpointAddress).Namespace);

        public static JObject Embedded { get; } = GetEmbeddedTestParameters();

        public static RegionEndpoint Region { get; } = GetRegionEndpoint();

        public static string EndpointAddress { get; } = GetEndpointAddress();

        private static JObject GetEmbeddedTestParameters()
        {
            var file = fileProvider.GetFileInfo($"{nameof(IotEndpointAddress)}.json");
            if (!file.Exists)
                return null;
            using (var fileStream = file.CreateReadStream())
            using (var textReader = new StreamReader(fileStream, Encoding.UTF8))
            using (var jsonReader = new JsonTextReader(textReader))
                return JObject.Load(jsonReader);
        }

        private static RegionEndpoint GetRegionEndpoint()
        {
            if (Embedded is null)
                return null;

            string region;
            if (!Embedded.TryGetValue(nameof(region), out JToken jToken))
                return null;
            else
                region = jToken.ToString();

            return RegionEndpoint.GetBySystemName(region);
        }

        private static string GetEndpointAddress()
        {
            if (Embedded is null)
                return null;

            string iotEndpoint;
            if (!Embedded.TryGetValue(nameof(iotEndpoint), out JToken jToken))
                return null;
            else
                iotEndpoint = jToken.ToString();

            return iotEndpoint;
        }
    }
}
