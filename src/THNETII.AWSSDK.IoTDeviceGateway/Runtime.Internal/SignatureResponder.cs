using Amazon.IoTDeviceGateway.Model;
using Amazon.IoTDeviceGateway.Model.Internal.MarshallTransformations;
using Amazon.Runtime;
using Amazon.Runtime.Internal;
using System;
using System.Collections.Generic;
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
            response.Headers = new Dictionary<string, string>(executionContext.RequestContext.Request.Headers);

            var signerResult = executionContext.RequestContext.Request.AWS4SignerResult;
            if (signerResult is null)
                return;
            response.SigningDetails = new AmazonIoTDeviceGatewaySigningDetails
            {
                AccessKeyId = signerResult.AccessKeyId,
                ForAuthorizationHeader = signerResult.ForAuthorizationHeader,
                ForQueryParameters = signerResult.ForQueryParameters,
                ISO8601Date = signerResult.ISO8601Date,
                ISO8601DateTime = signerResult.ISO8601DateTime,
                Scope = signerResult.Scope,
                Signature = signerResult.Signature,
                SignedHeaders = signerResult.SignedHeaders
            };

            executionContext.ResponseContext.Response = response;
        }
    }
}
