using Amazon.Runtime;
using Amazon.Runtime.Internal;
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
                ? Task.FromResult<T>(default)
                : base.InvokeAsync<T>(executionContext);
        }

        private void PreInvoke(IExecutionContext executionContext)
        {
            var signerResult = executionContext.RequestContext.Request.AWS4SignerResult;
            if (signerResult is null)
                return;

        }
    }
}
