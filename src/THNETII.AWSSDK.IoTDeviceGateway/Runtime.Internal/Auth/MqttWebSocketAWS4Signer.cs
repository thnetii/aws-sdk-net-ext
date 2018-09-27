using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Auth;
using Amazon.Runtime.Internal.Util;

namespace Amazon.IoTDeviceGateway.Runtime.Internal.Auth
{
    public class MqttWebSocketAWS4Signer : AWS4PreSignedUrlSigner
    {
        public MqttWebSocketAWS4Signer() : base() { }

        public override void Sign(IRequest request, IClientConfig clientConfig, RequestMetrics metrics, string awsAccessKeyId, string awsSecretAccessKey)
        {
            var signingResult = SignRequest(request, clientConfig, metrics, awsAccessKeyId, awsSecretAccessKey, clientConfig.RegionEndpointServiceName, overrideSigningRegion: null);
            request.AWS4SignerResult = signingResult;
        }
    }
}
