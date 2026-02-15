using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000455 RID: 1109
	[MonoTODO("Handle domain unloading?")]
	internal class CrossAppDomainSink : IMessageSink
	{
		// Token: 0x0600247B RID: 9339 RVA: 0x00095D3B File Offset: 0x00093F3B
		internal CrossAppDomainSink(int domainID)
		{
			this._domainID = domainID;
		}

		// Token: 0x0600247C RID: 9340 RVA: 0x00095D4C File Offset: 0x00093F4C
		internal static CrossAppDomainSink GetSink(int domainID)
		{
			object syncRoot = CrossAppDomainSink.s_sinks.SyncRoot;
			CrossAppDomainSink crossAppDomainSink;
			lock (syncRoot)
			{
				if (CrossAppDomainSink.s_sinks.ContainsKey(domainID))
				{
					crossAppDomainSink = (CrossAppDomainSink)CrossAppDomainSink.s_sinks[domainID];
				}
				else
				{
					CrossAppDomainSink crossAppDomainSink2 = new CrossAppDomainSink(domainID);
					CrossAppDomainSink.s_sinks[domainID] = crossAppDomainSink2;
					crossAppDomainSink = crossAppDomainSink2;
				}
			}
			return crossAppDomainSink;
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x0600247D RID: 9341 RVA: 0x00095DD0 File Offset: 0x00093FD0
		internal int TargetDomainId
		{
			get
			{
				return this._domainID;
			}
		}

		// Token: 0x0600247E RID: 9342 RVA: 0x00095DD8 File Offset: 0x00093FD8
		private static CrossAppDomainSink.ProcessMessageRes ProcessMessageInDomain(byte[] arrRequest, CADMethodCallMessage cadMsg)
		{
			CrossAppDomainSink.ProcessMessageRes processMessageRes = default(CrossAppDomainSink.ProcessMessageRes);
			try
			{
				AppDomain.CurrentDomain.ProcessMessageInDomain(arrRequest, cadMsg, out processMessageRes.arrResponse, out processMessageRes.cadMrm);
			}
			catch (Exception ex)
			{
				IMessage message = new MethodResponse(ex, new ErrorMessage());
				processMessageRes.arrResponse = CADSerializer.SerializeMessage(message).GetBuffer();
			}
			return processMessageRes;
		}

		// Token: 0x0600247F RID: 9343 RVA: 0x00095E3C File Offset: 0x0009403C
		public virtual IMessage SyncProcessMessage(IMessage msgRequest)
		{
			IMessage message = null;
			try
			{
				byte[] array = null;
				byte[] array2 = null;
				CADMethodReturnMessage cadmethodReturnMessage = null;
				CADMethodCallMessage cadmethodCallMessage = CADMethodCallMessage.Create(msgRequest);
				if (cadmethodCallMessage == null)
				{
					array2 = CADSerializer.SerializeMessage(msgRequest).GetBuffer();
				}
				Context currentContext = Thread.CurrentContext;
				try
				{
					CrossAppDomainSink.ProcessMessageRes processMessageRes = (CrossAppDomainSink.ProcessMessageRes)AppDomain.InvokeInDomainByID(this._domainID, CrossAppDomainSink.processMessageMethod, null, new object[] { array2, cadmethodCallMessage });
					array = processMessageRes.arrResponse;
					cadmethodReturnMessage = processMessageRes.cadMrm;
				}
				finally
				{
					AppDomain.InternalSetContext(currentContext);
				}
				if (array != null)
				{
					message = CADSerializer.DeserializeMessage(new MemoryStream(array), msgRequest as IMethodCallMessage);
				}
				else
				{
					message = new MethodResponse(msgRequest as IMethodCallMessage, cadmethodReturnMessage);
				}
			}
			catch (Exception ex)
			{
				try
				{
					message = new ReturnMessage(ex, msgRequest as IMethodCallMessage);
				}
				catch (Exception)
				{
				}
			}
			return message;
		}

		// Token: 0x06002480 RID: 9344 RVA: 0x00095F14 File Offset: 0x00094114
		public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
		{
			AsyncRequest asyncRequest = new AsyncRequest(reqMsg, replySink);
			ThreadPool.QueueUserWorkItem(delegate(object data)
			{
				try
				{
					this.SendAsyncMessage(data);
				}
				catch
				{
				}
			}, asyncRequest);
			return null;
		}

		// Token: 0x06002481 RID: 9345 RVA: 0x00095F40 File Offset: 0x00094140
		public void SendAsyncMessage(object data)
		{
			AsyncRequest asyncRequest = (AsyncRequest)data;
			IMessage message = this.SyncProcessMessage(asyncRequest.MsgRequest);
			asyncRequest.ReplySink.SyncProcessMessage(message);
		}

		// Token: 0x04001192 RID: 4498
		private static Hashtable s_sinks = new Hashtable();

		// Token: 0x04001193 RID: 4499
		private static MethodInfo processMessageMethod = typeof(CrossAppDomainSink).GetMethod("ProcessMessageInDomain", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x04001194 RID: 4500
		private int _domainID;

		// Token: 0x02000456 RID: 1110
		private struct ProcessMessageRes
		{
			// Token: 0x04001195 RID: 4501
			public byte[] arrResponse;

			// Token: 0x04001196 RID: 4502
			public CADMethodReturnMessage cadMrm;
		}
	}
}
