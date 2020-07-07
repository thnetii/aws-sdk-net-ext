using System.Diagnostics.CodeAnalysis;

namespace Amazon.TestParameters
{
    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes")]
    internal class MicTestParameters
    {
        public string? Hostname { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes")]
    internal class MicManifest
    {
        public string? ApiGatewayRootUrl { get; set; }
        public string? StackName { get; set; }
    }


    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes")]
    internal class MicMetadataManifest
    {
        public string? ApiKey { get; set; }
        public string? IotEndpoint { get; set; }
        public string? IotEndpointATS { get; set; }
        public string? IdentityPool { get; set; }
        public string? UserPool { get; set; }
        public string? Region { get; set; }
    }


    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes")]
    [SuppressMessage("Style", "IDE1006: Naming Styles")]
    internal class Login
    {
        public Credentials? credentials { get; set; }
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes")]
    [SuppressMessage("Style", "IDE1006: Naming Styles")]
    internal class Credentials
    {
        public string? identityId { get; set; }
        public string? token { get; set; }
        public string? refreshToken { get; set; }
    }
}
