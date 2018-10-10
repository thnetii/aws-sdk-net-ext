using Amazon.IoTDeviceGateway.Model;
using Amazon.IoTDeviceGateway.Model.Internal.MarshallTransformations;
using Amazon.IoTDeviceGateway.Runtime.Internal;
using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Auth;
using System;
#if NETSTANDARD1_3
using System.Reflection;
#endif
using System.Threading;
using System.Threading.Tasks;

namespace Amazon.IoTDeviceGateway
{
    /// <summary>
    /// Implementation for accessing IoT Device Gateway
    /// </summary>
    public class AmazonIoTDeviceGatewayClient : AmazonServiceClient, IAmazonIoTDeviceGateway
    {
        #region Constructors

        /// <summary>
        /// Constructs an <see cref="AmazonIoTDeviceGatewayClient"/> with the credentials loaded from the application's
        /// default configuration, and if unsuccessful from the Instance Profile service on an EC2 instance.
        /// <para>
        /// Example App.config with credentials set. 
        /// <code>
        /// &lt;?xml version="1.0" encoding="utf-8" ?&gt;
        /// &lt;configuration&gt;
        ///     &lt;appSettings&gt;
        ///         &lt;add key="AWSProfileName" value="AWS Default"/&gt;
        ///     &lt;/appSettings&gt;
        /// &lt;/configuration&gt;
        /// </code>
        /// </para>
        /// </summary>
        public AmazonIoTDeviceGatewayClient()
            : base(FallbackCredentialsFactory.GetCredentials(), new AmazonIoTDeviceGatewayConfig()) { }

        /// <summary>
        /// Constructs AmazonIoTDeviceGatewayClient with the credentials loaded from the application's
        /// default configuration, and if unsuccessful from the Instance Profile service on an EC2 instance.
        /// <para>
        /// Example App.config with credentials set. 
        /// <code>
        /// &lt;?xml version="1.0" encoding="utf-8" ?&gt;
        /// &lt;configuration&gt;
        ///     &lt;appSettings&gt;
        ///         &lt;add key="AWSProfileName" value="AWS Default"/&gt;
        ///     &lt;/appSettings&gt;
        /// &lt;/configuration&gt;
        /// </code>
        /// </para>
        /// </summary>
        /// <param name="region">The region to connect.</param>
        public AmazonIoTDeviceGatewayClient(RegionEndpoint region)
            : base(FallbackCredentialsFactory.GetCredentials(), new AmazonIoTDeviceGatewayConfig { RegionEndpoint = region }) { }

        /// <summary>
        /// Constructs AmazonIoTDeviceGatewayClient with the credentials loaded from the application's
        /// default configuration, and if unsuccessful from the Instance Profile service on an EC2 instance.
        /// <para>
        /// Example App.config with credentials set. 
        /// <code>
        /// &lt;?xml version="1.0" encoding="utf-8" ?&gt;
        /// &lt;configuration&gt;
        ///     &lt;appSettings&gt;
        ///         &lt;add key="AWSProfileName" value="AWS Default"/&gt;
        ///     &lt;/appSettings&gt;
        /// &lt;/configuration&gt;
        /// </code>
        /// </para>
        /// </summary>
        /// <param name="config">The AmazonIoTDeviceGatewayClient Configuration Object</param>
        public AmazonIoTDeviceGatewayClient(AmazonIoTDeviceGatewayConfig config)
            : base(FallbackCredentialsFactory.GetCredentials(), config) { }

        /// <summary>
        /// Constructs AmazonIoTDeviceGatewayClient with AWS Credentials
        /// </summary>
        /// <param name="credentials">AWS Credentials</param>
        public AmazonIoTDeviceGatewayClient(AWSCredentials credentials)
            : this(credentials, new AmazonIoTDeviceGatewayConfig())
        {
        }

        /// <summary>
        /// Constructs AmazonIoTDeviceGatewayClient with AWS Credentials
        /// </summary>
        /// <param name="credentials">AWS Credentials</param>
        /// <param name="region">The region to connect.</param>
        public AmazonIoTDeviceGatewayClient(AWSCredentials credentials, RegionEndpoint region)
            : this(credentials, new AmazonIoTDeviceGatewayConfig { RegionEndpoint = region })
        {
        }

        /// <summary>
        /// Constructs AmazonIoTDeviceGatewayClient with AWS Credentials and an
        /// AmazonIoTDeviceGatewayClient Configuration object.
        /// </summary>
        /// <param name="credentials">AWS Credentials</param>
        /// <param name="clientConfig">The AmazonIoTDeviceGatewayClient Configuration Object</param>
        public AmazonIoTDeviceGatewayClient(AWSCredentials credentials, AmazonIoTDeviceGatewayConfig clientConfig)
            : base(credentials, clientConfig)
        {
        }

        /// <summary>
        /// Constructs AmazonIoTDeviceGatewayClient with AWS Access Key ID and AWS Secret Key
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        public AmazonIoTDeviceGatewayClient(string awsAccessKeyId, string awsSecretAccessKey)
            : this(awsAccessKeyId, awsSecretAccessKey, new AmazonIoTDeviceGatewayConfig())
        {
        }

        /// <summary>
        /// Constructs AmazonIoTDeviceGatewayClient with AWS Access Key ID and AWS Secret Key
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="region">The region to connect.</param>
        public AmazonIoTDeviceGatewayClient(string awsAccessKeyId, string awsSecretAccessKey, RegionEndpoint region)
            : this(awsAccessKeyId, awsSecretAccessKey, new AmazonIoTDeviceGatewayConfig() { RegionEndpoint = region })
        {
        }

        /// <summary>
        /// Constructs AmazonIoTDeviceGatewayClient with AWS Access Key ID, AWS Secret Key and an
        /// AmazonIoTDeviceGatewayClient Configuration object. 
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="clientConfig">The AmazonIoTDeviceGatewayClient Configuration Object</param>
        public AmazonIoTDeviceGatewayClient(string awsAccessKeyId, string awsSecretAccessKey, AmazonIoTDeviceGatewayConfig clientConfig)
            : base(awsAccessKeyId, awsSecretAccessKey, clientConfig)
        {
        }

        /// <summary>
        /// Constructs AmazonIoTDeviceGatewayClient with AWS Access Key ID and AWS Secret Key
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="awsSessionToken">AWS Session Token</param>
        public AmazonIoTDeviceGatewayClient(string awsAccessKeyId, string awsSecretAccessKey, string awsSessionToken)
            : this(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, new AmazonIoTDeviceGatewayConfig())
        {
        }

        /// <summary>
        /// Constructs AmazonIoTDeviceGatewayClient with AWS Access Key ID and AWS Secret Key
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="awsSessionToken">AWS Session Token</param>
        /// <param name="region">The region to connect.</param>
        public AmazonIoTDeviceGatewayClient(string awsAccessKeyId, string awsSecretAccessKey, string awsSessionToken, RegionEndpoint region)
            : this(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, new AmazonIoTDeviceGatewayConfig { RegionEndpoint = region })
        {
        }

        /// <summary>
        /// Constructs AmazonIoTDeviceGatewayClient with AWS Access Key ID, AWS Secret Key and an
        /// AmazonIoTDeviceGatewayClient Configuration object. 
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="awsSessionToken">AWS Session Token</param>
        /// <param name="clientConfig">The AmazonIoTDeviceGatewayClient Configuration Object</param>
        public AmazonIoTDeviceGatewayClient(string awsAccessKeyId, string awsSecretAccessKey, string awsSessionToken, AmazonIoTDeviceGatewayConfig clientConfig)
            : base(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, clientConfig)
        {
        }

        #endregion

        #region Overrides

        /// <inheritdoc />
        protected override AbstractAWSSigner CreateSigner() => new AWS4Signer();

        /// <inheritdoc />
        protected override void CustomizeRuntimePipeline(RuntimePipeline pipeline)
        {
            base.CustomizeRuntimePipeline(pipeline);

            var httpHandlerOpenGenericType = typeof(HttpHandler<>);
            foreach (var pipelineHandler in pipeline.EnumerateHandlers())
            {
                bool doBreak = false;
                for (Type handlerType = pipelineHandler.GetType(); !(handlerType is null) && handlerType != typeof(object); handlerType = GetBaseType(handlerType))
                {
                    var handlerTypeInfo = GetTypeInfo(handlerType);
                    if (handlerTypeInfo.IsGenericType && handlerTypeInfo.GetGenericTypeDefinition() == httpHandlerOpenGenericType)
                    {
                        var nonHttpHandler = new NonHttpHandler()
                        {
                            OuterHandler = pipelineHandler.OuterHandler,
                            InnerHandler = pipelineHandler,
                            Logger = pipelineHandler.Logger
                        };

                        pipelineHandler.OuterHandler.InnerHandler = nonHttpHandler;
                        pipelineHandler.OuterHandler = nonHttpHandler;

                        doBreak = true;
                        break;
                    }
                }
                if (doBreak)
                    break;
            }
        }

        #endregion

        #region Private Helper methods

        private static
#if NETSTANDARD1_3
            TypeInfo
#else
            Type
#endif
            GetTypeInfo(Type type)
        {
            return type
#if NETSTANDARD1_3
                .GetTypeInfo()
#endif
                ;
        }

        private static Type GetBaseType(Type type)
        {
            return GetTypeInfo(type).BaseType;
        }

#endregion

#region CreateMqttWebSocketUri

        public virtual Task<CreateMqttWebSocketUriResponse> CreateMqttWebSocketUriAsync(CreateMqttWebSocketUriRequest request, CancellationToken cancelToken = default)
        {
            var marshaller = CreateMqttWebSocketUriRequestMarshaller.Instance;
            var unmarshaller = CreateMqttWebSocketUriResponseUnmarshaller.Instance;

            return InvokeAsync<CreateMqttWebSocketUriRequest, CreateMqttWebSocketUriResponse>(request, marshaller,
                unmarshaller, cancelToken);
        }

#endregion
    }
}
