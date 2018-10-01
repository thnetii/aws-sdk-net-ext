using Amazon.IoTDeviceGateway.Model.Internal.MarshallTransformations;
using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Auth;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Amazon.IoTDeviceGateway.Runtime.Internal
{
    public class SignatureResponder : PipelineHandler
    {
        public override void InvokeSync(IExecutionContext executionContext)
        {
            PreInvoke(executionContext);
            if (!(InnerHandler is null))
                base.InvokeSync(executionContext);
        }

        public override Task<T> InvokeAsync<T>(IExecutionContext executionContext)
        {
            PreInvoke(executionContext);
            return InnerHandler is null
                ? Task.FromResult((T)executionContext.ResponseContext.Response)
                : base.InvokeAsync<T>(executionContext);
        }

        private void PreInvoke(IExecutionContext executionContext)
        {
            if (!(executionContext.RequestContext.Unmarshaller is AmazonIoTDeviceGatewayResponseUnmarshaller unmarshaller))
                return;

            var response = unmarshaller.CreateResponse();

            IRequest request = executionContext.RequestContext.Request;

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

            executionContext.ResponseContext.Response = response;
        }
    }
}
