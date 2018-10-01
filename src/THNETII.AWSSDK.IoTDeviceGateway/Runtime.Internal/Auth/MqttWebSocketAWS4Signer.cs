using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Auth;
using Amazon.Runtime.Internal.Util;
using Amazon.Util;
using System.Diagnostics;

namespace Amazon.IoTDeviceGateway.Runtime.Internal.Auth
{
    public class MqttWebSocketAWS4Signer : AWS4PreSignedUrlSigner
    {
        public MqttWebSocketAWS4Signer() : base() { }

        public override void Sign(IRequest request, IClientConfig clientConfig, RequestMetrics metrics, string awsAccessKeyId, string awsSecretAccessKey)
        {
            var hasStsTokenHeader = request.Headers.TryGetValue(HeaderKeys.XAmzSecurityTokenHeader, out string stsTokenValue);
            if (hasStsTokenHeader)
            {
                var removedStsTokenHeader = request.Headers.Remove(HeaderKeys.XAmzSecurityTokenHeader);
                Debug.Assert(removedStsTokenHeader, $"Unable to remove {HeaderKeys.XAmzSecurityTokenHeader} header from request before signing");
            }
            var signingResult = SignRequest(request, clientConfig, metrics, awsAccessKeyId, awsSecretAccessKey);
            request.AWS4SignerResult = signingResult;
            if (hasStsTokenHeader)
                request.Parameters["X-Amz-Security-Token"] = stsTokenValue;
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
