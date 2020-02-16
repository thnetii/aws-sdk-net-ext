using Amazon.IoTDeviceGateway.Runtime.Internal.Transform;
using Amazon.Runtime;
using Amazon.Runtime.Internal.Auth;
using Amazon.Runtime.Internal.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Amazon.IoTDeviceGateway.Model.Internal.MarshallTransformations
{
    public class CreateMqttWebSocketUriResponseUnmarshaller : NonHttpResponseUnmarshaller
    {
        private static readonly char[] signedHeadersSeparator = new[] { ';' };

        /// <summary>
        /// Given the original request and the AWSv4 Signing Result,
        /// constructs a response containing the signing headers, and a fully
        /// composed Uri instance with the signed headers as parameters.
        /// </summary>
        /// <param name="input">
        /// An <see cref="UnmarshallerContext"/> instance containing an
        /// <see cref="IWebResponseData"/> instance with the original request.
        /// </param>
        /// <returns>
        /// A <see cref="CreateMqttWebSocketUriResponse"/> instance constructed
        /// from the <see cref="NonHttpResponseData.OriginalRequest"/> contained
        /// in <paramref name="input"/>.
        /// </returns>
        public override AmazonWebServiceResponse Unmarshall(NonHttpUnmarshallerContext input)
        {
            var request = input?.ResponseData?.OriginalRequest;
            if (request is null)
                return null;

            IDictionary<string, string> headers = null;
            var signerResult = request.AWS4SignerResult;
            if (!(signerResult is null))
            {
                headers = signerResult.SignedHeaders
                    .Split(signedHeadersSeparator, StringSplitOptions.RemoveEmptyEntries)
                    .ToDictionary(
                        k => k,
                        k => request.Headers[k],
                        StringComparer.OrdinalIgnoreCase
                    )
                    ;

                request.Parameters["X-Amz-Algorithm"] = AWS4Signer.AWS4AlgorithmTag;
                request.Parameters["X-Amz-Credential"] = FormattableString.Invariant(
                    $"{signerResult.AccessKeyId}/{signerResult.Scope}");
                request.Parameters["X-Amz-Date"] = signerResult.ISO8601DateTime;
                request.Parameters["X-Amz-SignedHeaders"] = signerResult.SignedHeaders;
                request.Parameters["X-Amz-Signature"] = signerResult.Signature;
            }

            return new CreateMqttWebSocketUriResponse(
                AmazonServiceClient.ComposeUrl(request),
                headers ?? new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                )
            { HttpStatusCode = HttpStatusCode.OK };
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
