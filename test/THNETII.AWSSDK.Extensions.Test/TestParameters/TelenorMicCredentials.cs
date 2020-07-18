using Amazon.CognitoIdentity;
using Amazon.CognitoIdentityProvider;
using Amazon.Runtime;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

using Newtonsoft.Json;

using System;
using System.IO;
using System.Net.Http;
using System.Text;

namespace Amazon.TestParameters
{
    public static class TelenorMicCredentials
    {
        private const string manifestServiceUri = "https://1u31fuekv5.execute-api.eu-west-1.amazonaws.com/prod/manifest/";

        private static readonly Lazy<MicTestParameters> LazyTestParameters =
            new Lazy<MicTestParameters>(() =>
            {
                var config = new ConfigurationBuilder()
                    .AddUserSecrets(typeof(TelenorMicCredentials).Assembly, optional: true)
                    .Build();

                var sectionName = ConfigurationPath.Combine("TelenorMic", "Credentials");
                var parameters = new MicTestParameters();
                config.Bind(sectionName, parameters);
                return parameters;
            });
        private static readonly Lazy<(MicMetadataManifest? metadata, AWSCredentials? credentials)> LazyMetadataAndCredentials =
            new Lazy<(MicMetadataManifest? metadata, AWSCredentials? credentials)>(() =>
            {
                var parameters = TestParameters;
                if (parameters is null)
                    return default;

                using var httpClient = new HttpClient();

                static MicManifest? GetMicManifest(HttpClient httpClient, string? hostname)
                {
                    var uri = new Uri(manifestServiceUri + "?hostname=" + Uri.EscapeDataString(hostname ?? string.Empty));
                    using var response = httpClient.GetAsync(uri).ConfigureAwait(false).GetAwaiter().GetResult();
                    if (!response.IsSuccessStatusCode)
                        return null;
                    using var stream = response.Content.ReadAsStreamAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                    using var reader = new JsonTextReader(new StreamReader(stream, Encoding.UTF8));
                    return JsonSerializer.CreateDefault().Deserialize<MicManifest?>(reader);
                }
                MicManifest? manifest = GetMicManifest(httpClient, parameters.Hostname);
                if (manifest is null)
                    return default;
                string apiGatewayStackUrl = FormattableString.Invariant($"{manifest.ApiGatewayRootUrl}/{Uri.EscapeDataString(manifest.StackName!)}");

                static MicMetadataManifest? GetMicMetadataManifest(HttpClient httpClient, string apiGatewayStackUrl)
                {
                    const string metadataManifestUrl = "/metadata/manifest";
                    Uri uri = new Uri(apiGatewayStackUrl + metadataManifestUrl);
                    using var response = httpClient.GetAsync(uri).ConfigureAwait(false).GetAwaiter().GetResult();
                    if (!response.IsSuccessStatusCode)
                        return null;
                    using var stream = response.Content.ReadAsStreamAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                    using var reader = new JsonTextReader(new StreamReader(stream, Encoding.UTF8));
                    return JsonSerializer.CreateDefault().Deserialize<MicMetadataManifest?>(reader);
                }
                MicMetadataManifest? metadataManifest = GetMicMetadataManifest(httpClient, apiGatewayStackUrl);
                if (metadataManifest is null)
                    return default;

                static Login? GetLogin(HttpClient httpClient, string apiGatewayStackUrl, string? apiKey, string? userName, string? password)
                {
                    const string authLoginUrl = "/auth/login";
                    Uri uri = new Uri(apiGatewayStackUrl + authLoginUrl);
                    using var parameters = new StringContent(JsonConvert.SerializeObject(new { userName, password }), Encoding.UTF8, "application/json");
                    using var request = new HttpRequestMessage(HttpMethod.Post, uri) { Content = parameters };
                    request.Headers.Add("x-api-key", apiKey);
                    using var response = httpClient.SendAsync(request).ConfigureAwait(false).GetAwaiter().GetResult();
                    if (!response.IsSuccessStatusCode)
                        return null;
                    using var stream = response.Content.ReadAsStreamAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                    using var reader = new JsonTextReader(new StreamReader(stream, Encoding.UTF8));
                    return JsonSerializer.CreateDefault().Deserialize<Login?>(reader);
                }
                Login? login = GetLogin(httpClient, apiGatewayStackUrl, metadataManifest.ApiKey, parameters.Username, parameters.Password);
                if (!(login?.credentials is Credentials credentials))
                    return default;

                string? identityPool = metadataManifest.IdentityPool;
                RegionEndpoint region = RegionEndpoint.GetBySystemName(metadataManifest.Region);

                var cognitoConfig = new AmazonCognitoIdentityProviderConfig { RegionEndpoint = region };
                var endpoint = region.GetEndpointForService(cognitoConfig.RegionEndpointServiceName);
                string providerName = FormattableString.Invariant(
                    $"{endpoint}/{metadataManifest.UserPool}");

                var cognito = new CognitoAWSCredentials(identityPool, region);
                cognito.CacheIdentityId(credentials.identityId);
                cognito.AddLogin(providerName, credentials.token);

                return (metadataManifest, cognito);
            });

        internal static MicTestParameters TestParameters =>
            LazyTestParameters.Value;

        public static AWSCredentials? AWSCredentials =>
            LazyMetadataAndCredentials.Value.credentials;

        public static string? IotEndpoint =>
            LazyMetadataAndCredentials.Value.metadata?.IotEndpointATS;

        public static string? RegionSystemName =>
            LazyMetadataAndCredentials.Value.metadata?.Region;
    }
}
