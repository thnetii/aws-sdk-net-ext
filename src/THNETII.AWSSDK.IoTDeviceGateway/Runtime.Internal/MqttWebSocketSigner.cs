using System.Threading.Tasks;
using Amazon.IoTDeviceGateway.Runtime.Internal.Auth;
using Amazon.Runtime;
using Amazon.Runtime.Internal;

namespace Amazon.IoTDeviceGateway.Runtime.Internal
{
    public class MqttWebSocketSigner : Signer
    {
        public override void InvokeSync(IExecutionContext executionContext)
        {
            PreInvoke(executionContext);
            base.InvokeSync(executionContext);
        }

        public override Task<T> InvokeAsync<T>(IExecutionContext executionContext)
        {
            PreInvoke(executionContext);
            return base.InvokeAsync<T>(executionContext);
        }

        private static void PreInvoke(IExecutionContext executionContext)
        {
            if (executionContext.RequestContext.Signer is MqttWebSocketAWS4Signer signer)
            {
                signer.OnSignatureResult = r => executionContext.RequestContext.Request.AWS4SignerResult = r;
            }
        }
    }
}
