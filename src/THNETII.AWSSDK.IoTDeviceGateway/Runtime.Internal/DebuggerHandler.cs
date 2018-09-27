#if DEBUG
using Amazon.Runtime;
using Amazon.Runtime.Internal;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Amazon.IoTDeviceGateway.Runtime.Internal
{
    public class DebuggerHandler : PipelineHandler
    {
        public override void InvokeSync(IExecutionContext executionContext)
        {
            Debugger.Break();
            try { base.InvokeSync(executionContext); }
            finally { Debugger.Break(); }
        }

        public override Task<T> InvokeAsync<T>(IExecutionContext executionContext)
        {
            Debugger.Break();
            try { return base.InvokeAsync<T>(executionContext); }
            finally { Debugger.Break(); }
        }
    }
}
#endif
