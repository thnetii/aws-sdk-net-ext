using Amazon.Runtime;
using Amazon.Util.Internal;
#if NETSTANDARD1_3
using System.Reflection;
#endif

namespace Amazon.IoTDeviceGateway
{
    /// <summary>
    /// Configuration for accessing Amazon IoT Device Gateway service
    /// </summary>
    public class AmazonIoTDeviceGatewayConfig : ClientConfig
    {
        /// <summary>
        /// The AWS service name for the Amazon IoT Device Gateway service.
        /// </summary>
        public const string ServiceName = "iotdevicegateway";

        private static readonly string UserAgentString =
            InternalSDKUtils.BuildUserAgentString(typeof(AmazonIoTDeviceGatewayConfig)
#if NETSTANDARD1_3
                .GetTypeInfo()
#endif
                .Assembly.GetName().Version.ToString()
                );

        /// <inheritdoc />
        public AmazonIoTDeviceGatewayConfig() : base()
        {
            AuthenticationServiceName = ServiceName;
        }

        /// <inheritdoc />
        public override string ServiceVersion => null;

        /// <inheritdoc />
        public override string UserAgent => UserAgentString;

        /// <inheritdoc />
        public override string RegionEndpointServiceName => ServiceName;
    }
}
