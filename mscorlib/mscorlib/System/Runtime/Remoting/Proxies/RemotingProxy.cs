using System;
using System.Reflection;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace System.Runtime.Remoting.Proxies
{
	// Token: 0x02000434 RID: 1076
	internal class RemotingProxy : RealProxy, IRemotingTypeInfo
	{
		// Token: 0x060023D1 RID: 9169 RVA: 0x00093A6A File Offset: 0x00091C6A
		internal RemotingProxy(Type type, ClientIdentity identity)
			: base(type, identity)
		{
			this._sink = identity.ChannelSink;
			this._hasEnvoySink = false;
			this._targetUri = identity.TargetUri;
		}

		// Token: 0x060023D2 RID: 9170 RVA: 0x00093A93 File Offset: 0x00091C93
		internal RemotingProxy(Type type, string activationUrl, object[] activationAttributes)
			: base(type)
		{
			this._hasEnvoySink = false;
			this._ctorCall = ActivationServices.CreateConstructionCall(type, activationUrl, activationAttributes);
		}

		// Token: 0x060023D3 RID: 9171 RVA: 0x00093AB4 File Offset: 0x00091CB4
		public override IMessage Invoke(IMessage request)
		{
			IMethodCallMessage methodCallMessage = request as IMethodCallMessage;
			if (methodCallMessage != null)
			{
				if (methodCallMessage.MethodBase == RemotingProxy._cache_GetHashCodeMethod)
				{
					return new MethodResponse(base.ObjectIdentity.GetHashCode(), null, null, methodCallMessage);
				}
				if (methodCallMessage.MethodBase == RemotingProxy._cache_GetTypeMethod)
				{
					return new MethodResponse(base.GetProxiedType(), null, null, methodCallMessage);
				}
			}
			IInternalMessage internalMessage = request as IInternalMessage;
			if (internalMessage != null)
			{
				if (internalMessage.Uri == null)
				{
					internalMessage.Uri = this._targetUri;
				}
				internalMessage.TargetIdentity = this._objectIdentity;
			}
			this._objectIdentity.NotifyClientDynamicSinks(true, request, true, false);
			IMessageSink messageSink;
			if (Thread.CurrentContext.HasExitSinks && !this._hasEnvoySink)
			{
				messageSink = Thread.CurrentContext.GetClientContextSinkChain();
			}
			else
			{
				messageSink = this._sink;
			}
			MonoMethodMessage monoMethodMessage = request as MonoMethodMessage;
			IMessage message;
			if (monoMethodMessage == null || monoMethodMessage.CallType == CallType.Sync)
			{
				message = messageSink.SyncProcessMessage(request);
			}
			else
			{
				AsyncResult asyncResult = monoMethodMessage.AsyncResult;
				IMessageCtrl messageCtrl = messageSink.AsyncProcessMessage(request, asyncResult);
				if (asyncResult != null)
				{
					asyncResult.SetMessageCtrl(messageCtrl);
				}
				message = new ReturnMessage(null, new object[0], 0, null, monoMethodMessage);
			}
			this._objectIdentity.NotifyClientDynamicSinks(false, request, true, false);
			return message;
		}

		// Token: 0x060023D4 RID: 9172 RVA: 0x00093BDC File Offset: 0x00091DDC
		internal void AttachIdentity(Identity identity)
		{
			this._objectIdentity = identity;
			if (identity is ClientActivatedIdentity)
			{
				ClientActivatedIdentity clientActivatedIdentity = (ClientActivatedIdentity)identity;
				this._targetContext = clientActivatedIdentity.Context;
				base.AttachServer(clientActivatedIdentity.GetServerObject());
				clientActivatedIdentity.SetClientProxy((MarshalByRefObject)this.GetTransparentProxy());
			}
			if (identity is ClientIdentity)
			{
				((ClientIdentity)identity).ClientProxy = (MarshalByRefObject)this.GetTransparentProxy();
				this._targetUri = ((ClientIdentity)identity).TargetUri;
			}
			else
			{
				this._targetUri = identity.ObjectUri;
			}
			if (this._objectIdentity.EnvoySink != null)
			{
				this._sink = this._objectIdentity.EnvoySink;
				this._hasEnvoySink = true;
			}
			else
			{
				this._sink = this._objectIdentity.ChannelSink;
			}
			this._ctorCall = null;
		}

		// Token: 0x060023D5 RID: 9173 RVA: 0x00093CA4 File Offset: 0x00091EA4
		internal IMessage ActivateRemoteObject(IMethodMessage request)
		{
			if (this._ctorCall == null)
			{
				return new ConstructionResponse(this, null, (IMethodCallMessage)request);
			}
			this._ctorCall.CopyFrom(request);
			return ActivationServices.Activate(this, this._ctorCall);
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x060023D6 RID: 9174 RVA: 0x00093CD4 File Offset: 0x00091ED4
		// (set) Token: 0x060023D7 RID: 9175 RVA: 0x000339FF File Offset: 0x00031BFF
		public string TypeName
		{
			get
			{
				if (this._objectIdentity is ClientIdentity)
				{
					ObjRef objRef = this._objectIdentity.CreateObjRef(null);
					if (objRef.TypeInfo != null)
					{
						return objRef.TypeInfo.TypeName;
					}
				}
				return base.GetProxiedType().AssemblyQualifiedName;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060023D8 RID: 9176 RVA: 0x00093D1C File Offset: 0x00091F1C
		public bool CanCastTo(Type fromType, object o)
		{
			if (this._objectIdentity is ClientIdentity)
			{
				ObjRef objRef = this._objectIdentity.CreateObjRef(null);
				if (objRef.IsReferenceToWellKnow && (fromType.IsInterface || base.GetProxiedType() == typeof(MarshalByRefObject)))
				{
					return true;
				}
				if (objRef.TypeInfo != null)
				{
					return objRef.TypeInfo.CanCastTo(fromType, o);
				}
			}
			return fromType.IsAssignableFrom(base.GetProxiedType());
		}

		// Token: 0x060023D9 RID: 9177 RVA: 0x00093D90 File Offset: 0x00091F90
		~RemotingProxy()
		{
			if (this._objectIdentity != null && !(this._objectIdentity is ClientActivatedIdentity))
			{
				RemotingServices.DisposeIdentity(this._objectIdentity);
			}
		}

		// Token: 0x0400114C RID: 4428
		private static MethodInfo _cache_GetTypeMethod = typeof(object).GetMethod("GetType");

		// Token: 0x0400114D RID: 4429
		private static MethodInfo _cache_GetHashCodeMethod = typeof(object).GetMethod("GetHashCode");

		// Token: 0x0400114E RID: 4430
		private IMessageSink _sink;

		// Token: 0x0400114F RID: 4431
		private bool _hasEnvoySink;

		// Token: 0x04001150 RID: 4432
		private ConstructionCall _ctorCall;
	}
}
