using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Auth;
using Amazon.Runtime.Internal.Util;
using Amazon.Util;

using System;

namespace Amazon.IoTDeviceGateway.Runtime.Internal.Auth
{
    public class MqttWebSocketAWS4Signer : AWS4PreSignedUrlSigner
    {
        public MqttWebSocketAWS4Signer() : base() { }

        public override void Sign(IRequest request, IClientConfig clientConfig, RequestMetrics metrics, string awsAccessKeyId, string awsSecretAccessKey)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var hasStsTokenHeader = request.Headers.TryGetValue(HeaderKeys.XAmzSecurityTokenHeader, out string stsTokenValue);
            request.Headers.Clear();
            var signingResult = SignRequest(request, clientConfig, metrics, awsAccessKeyId, awsSecretAccessKey);
            request.AWS4SignerResult = signingResult;
            if (hasStsTokenHeader)
                request.Parameters["X-Amz-Security-Token"] = stsTokenValue;
        }

        public static new AWS4SigningResult SignRequest(IRequest request, IClientConfig clientConfig, RequestMetrics metrics, string awsAccessKeyId, string awsSecretAccessKey)
        {
            if (clientConfig is null)
                throw new ArgumentNullException(nameof(clientConfig));

            var service = !string.IsNullOrEmpty(clientConfig.AuthenticationServiceName)
                ? clientConfig.AuthenticationServiceName
                : AWSSDKUtils.DetermineService(clientConfig.DetermineServiceURL());

            return SignRequest(request, clientConfig, metrics, awsAccessKeyId, awsSecretAccessKey, service, overrideSigningRegion: null);
        }
    }
}
