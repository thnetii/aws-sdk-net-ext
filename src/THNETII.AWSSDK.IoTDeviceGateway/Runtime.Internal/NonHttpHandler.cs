using Amazon.IoTDeviceGateway.Runtime.Internal.Transform;
using Amazon.Runtime;
using Amazon.Runtime.Internal;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;

namespace Amazon.IoTDeviceGateway.Runtime.Internal
{
    public class NonHttpHandler : PipelineHandler
    {
        /// <inheritdoc/>
        public override void InvokeSync(IExecutionContext executionContext)
        {
            if (executionContext is null)
                throw new ArgumentNullException(nameof(executionContext));

            if (!(executionContext.RequestContext.Request is NonHttpRequest request))
            {
                base.InvokeSync(executionContext);
                return;
            }

            HandleRequest(executionContext, request);
        }

        [return: MaybeNull]
        public override Task<T> InvokeAsync<T>(IExecutionContext executionContext)
        {
            if (executionContext is null)
                throw new ArgumentNullException(nameof(executionContext));

            if (!(executionContext.RequestContext.Request is NonHttpRequest request))
                return base.InvokeAsync<T>(executionContext);

            HandleRequest(executionContext, request);
            return Task.FromResult<T>(null!);
        }

        private static void HandleRequest(IExecutionContext executionContext, NonHttpRequest request)
        {
            executionContext.ResponseContext.HttpResponse = new NonHttpResponseData
            {
                OriginalRequest = request,
                StatusCode = HttpStatusCode.OK,
                IsSuccessStatusCode = true
            };
        }
    }
}
