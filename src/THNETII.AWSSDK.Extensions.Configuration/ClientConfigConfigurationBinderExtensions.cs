extern alias AWSSDK_Extensions_NETCore_Setup;

using AwsConfigurationExtensions = AWSSDK_Extensions_NETCore_Setup::Microsoft.Extensions.Configuration.ConfigurationExtensions;

namespace Amazon.Runtime
{
    public static class ClientConfigConfigurationBinderExtensions
    {
        public const string DefaultConfigSection =
            AwsConfigurationExtensions.DEFAULT_CONFIG_SECTION;
    }
}
