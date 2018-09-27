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
            var signedHeaderKeys = new HashSet<string>(signerResult.SignedHeaders.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries), StringComparer.OrdinalIgnoreCase);
            response.SigningDetails = new AmazonIoTDeviceGatewaySigningDetails
            {
                AccessKeyId = signerResult.AccessKeyId,
                AuthorizationHeader = signerResult.ForAuthorizationHeader,
                QueryParameters = signerResult.ForQueryParameters,
                EncodedQueryParameters = string.Join("&", signerResult.ForQueryParameters.Split('&')
                    .Select(qPair =>
                    {
                        string key, value = string.Empty;
                        int eqIdx = qPair.IndexOf('=');
                        if (eqIdx < 0)
                            key = Uri.EscapeDataString(qPair);
                        else
                        {
                            key = Uri.EscapeDataString(qPair.Substring(startIndex: 0, length: eqIdx));
                            value = Uri.EscapeDataString(qPair.Substring(eqIdx + 1));
                        }
                        return key + '=' + value;
                    })
                ),
                ISO8601Date = signerResult.ISO8601Date,
                ISO8601DateTime = signerResult.ISO8601DateTime,
                Scope = signerResult.Scope,
                Signature = signerResult.Signature,
                SignedHeaders = executionContext.RequestContext.Request.Headers
                    .Where(headerPair => signedHeaderKeys.Contains(headerPair.Key))
                    .ToDictionary(headerPair => headerPair.Key, headerPair => headerPair.Value, StringComparer.OrdinalIgnoreCase)
            };

            response.HttpStatusCode = HttpStatusCode.OK;
            executionContext.ResponseContext.Response = response;
        }
    }
}
