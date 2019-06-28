using System;
using System.IO;
using System.Text;

using Amazon.CognitoIdentity;
using Amazon.Runtime;

using Microsoft.Extensions.FileProviders;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Amazon.TestParameters
{
    public static class Credentials
    {
        private static readonly IFileProvider fileProvider =
            new EmbeddedFileProvider(typeof(Credentials).Assembly, typeof(Credentials).Namespace);

        public static JObject Embedded { get; } = GetEmbeddedTestParameters();

        public static AWSCredentials AWSCredentials { get; } = GetAWSCredentials();

        private static JObject GetEmbeddedTestParameters()
        {
            var file = fileProvider.GetFileInfo($"{nameof(Credentials)}.json");
            if (!file.Exists)
                return null;
            using (var fileStream = file.CreateReadStream())
            using (var textReader = new StreamReader(fileStream, Encoding.UTF8))
            using (var jsonReader = new JsonTextReader(textReader))
                return JObject.Load(jsonReader);
        }

        private static AWSCredentials GetAWSCredentials()
        {
            if (Embedded is null)
                return null;
            JObject credentials = Embedded;
            string type = null;
            if (credentials.TryGetValue("type", out JToken jToken))
                type = jToken.ToString();
            switch (type)
            {
                case var _ when string.Equals("cognito", type, StringComparison.OrdinalIgnoreCase):
                    return GetAWSCognitoIdentityCredentials(credentials);
            }
            return null;
        }

        private static AWSCredentials GetAWSCognitoIdentityCredentials(JObject credentials)
        {
            string identityPoolId; RegionEndpoint region;
            if (!credentials.TryGetValue(nameof(identityPoolId), out JToken jsonToken))
                return null;
            else
                identityPoolId = jsonToken.ToString();
            if (!credentials.TryGetValue(nameof(region), out jsonToken))
                return null;
            else
                region = RegionEndpoint.GetBySystemName(jsonToken.ToString());

            var cognito = new CognitoAWSCredentials(identityPoolId, region);

            string loginProvider, identityId, token;
            if (credentials.TryGetValue(nameof(loginProvider), out jsonToken))
            {
                loginProvider = jsonToken.ToString();
                if (credentials.TryGetValue(nameof(token), out jsonToken))
                {
                    token = jsonToken.ToString();
                    cognito.AddLogin(loginProvider, token);
                }
                if (credentials.TryGetValue(nameof(identityId), out jsonToken))
                {
                    identityId = jsonToken.ToString();
                    cognito.CacheIdentityId(identityId);
                }

                try { cognito.GetCredentials(); }
                catch { return null; }
            }

            return cognito;
        }
    }
}
