using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;

using Amazon.IoTDeviceGateway.Runtime.Internal.Transform;
using Amazon.Runtime;
using Amazon.Runtime.Internal.Auth;

namespace Amazon.IoTDeviceGateway.Model.Internal.MarshallTransformations
{
    public class CreateMqttWebSocketUriResponseUnmarshaller : NonHttpResponseUnmarshaller
    {
        public override AmazonWebServiceResponse Unmarshall(NonHttpUnmarshallerContext input)
        {
            var response = new CreateMqttWebSocketUriResponse();
            var request = input.ResponseData?.OriginalRequest;
            if (request is null)
                return null;

            var signerResult = request.AWS4SignerResult;
            if (!(signerResult is null))
            {
                var signedHeaderKeys = new HashSet<string>(signerResult.SignedHeaders.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries), StringComparer.OrdinalIgnoreCase);
                response.Headers = signedHeaderKeys.ToDictionary(k => k,
                    k => request.Headers[k], StringComparer.OrdinalIgnoreCase);

                request.Parameters["X-Amz-Algorithm"] = AWS4Signer.AWS4AlgorithmTag;
                request.Parameters["X-Amz-Credential"] = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", signerResult.AccessKeyId, signerResult.Scope);
                request.Parameters["X-Amz-Date"] = signerResult.ISO8601DateTime;
                request.Parameters["X-Amz-SignedHeaders"] = signerResult.SignedHeaders;
                request.Parameters["X-Amz-Signature"] = signerResult.Signature;
            }

            response.RequestUri = AmazonServiceClient.ComposeUrl(request);
            response.HttpStatusCode = HttpStatusCode.OK;

            return response;
        }

        internal static CreateMqttWebSocketUriResponseUnmarshaller GetInstance() =>
            Instance;

        /// <summary>
        /// Gets the singleton.
        /// </summary>  
        public static CreateMqttWebSocketUriResponseUnmarshaller Instance { get; } =
            new CreateMqttWebSocketUriResponseUnmarshaller();
    }
}
