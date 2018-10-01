using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Auth;
using Amazon.Runtime.Internal.Util;
using Amazon.Util;

namespace Amazon.IoTDeviceGateway.Runtime.Internal.Auth
{
    public class MqttWebSocketAWS4Signer : AWS4PreSignedUrlSigner
    {
        public MqttWebSocketAWS4Signer() : base() { }

        public override ClientProtocol Protocol => ClientProtocol.QueryStringProtocol;

        public override void Sign(IRequest request, IClientConfig clientConfig, RequestMetrics metrics, string awsAccessKeyId, string awsSecretAccessKey)
        {
            //request.Headers.Remove(HeaderKeys.XAmzSecurityTokenHeader);
            var signingResult = SignRequest(request, clientConfig, metrics, awsAccessKeyId, awsSecretAccessKey);
            request.AWS4SignerResult = signingResult;
        }

        public new AWS4SigningResult SignRequest(IRequest request, IClientConfig clientConfig, RequestMetrics metrics, string awsAccessKeyId, string awsSecretAccessKey)
        {
            var service = !string.IsNullOrEmpty(clientConfig.AuthenticationServiceName)
                ? clientConfig.AuthenticationServiceName
                : AWSSDKUtils.DetermineService(clientConfig.DetermineServiceURL());

            return SignRequest(request, clientConfig, metrics, awsAccessKeyId, awsSecretAccessKey, service, overrideSigningRegion: null);
        }
    }
}
