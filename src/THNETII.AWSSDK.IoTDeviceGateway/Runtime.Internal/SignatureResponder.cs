using Amazon.IoTDeviceGateway.Model;
using Amazon.IoTDeviceGateway.Model.Internal.MarshallTransformations;
using Amazon.Runtime;
using Amazon.Runtime.Internal;
using System;
using System.Collections.Generic;
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
            response.RequestUri = new Uri(executionContext.RequestContext.Request.Endpoint, executionContext.RequestContext.Request.ResourcePath);

            var signerResult = executionContext.RequestContext.Request.AWS4SignerResult;
            if (signerResult is null)
                return;
            response.SigningDetails = new AmazonIoTDeviceGatewaySigningDetails
            {
                AccessKeyId = signerResult.AccessKeyId,
                AuthorizationHeader = signerResult.ForAuthorizationHeader,
                QueryParameters = signerResult.ForQueryParameters,
                ISO8601Date = signerResult.ISO8601Date,
                ISO8601DateTime = signerResult.ISO8601DateTime,
                Scope = signerResult.Scope,
                Signature = signerResult.Signature,
                SignedHeaders = signerResult.SignedHeaders.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToDictionary(header => header, header =>
                    {
                        executionContext.RequestContext.Request.Headers.TryGetValue(header, out string value);
                        return value;
                    }, StringComparer.OrdinalIgnoreCase)
            };

            response.HttpStatusCode = HttpStatusCode.OK;
            executionContext.ResponseContext.Response = response;
        }
    }
}
