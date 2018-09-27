using System.Threading.Tasks;
using Amazon.Runtime;
using Amazon.Runtime.Internal;

namespace Amazon.IoTDeviceGateway.Runtime.Internal
{
    public class SignatureResponder : PipelineHandler
    {
        public override void InvokeSync(IExecutionContext executionContext)
        {
            // Intentionally empty
        }

        public override Task<T> InvokeAsync<T>(IExecutionContext executionContext) =>
            Task.FromResult<T>(default);
    }
}
