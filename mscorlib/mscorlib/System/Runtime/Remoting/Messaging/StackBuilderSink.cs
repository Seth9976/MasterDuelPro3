using System;
using System.Reflection;
using System.Runtime.Remoting.Proxies;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020004A0 RID: 1184
	internal class StackBuilderSink : IMessageSink
	{
		// Token: 0x06002618 RID: 9752 RVA: 0x0009A490 File Offset: 0x00098690
		public StackBuilderSink(MarshalByRefObject obj, bool forceInternalExecute)
		{
			this._target = obj;
			if (!forceInternalExecute && RemotingServices.IsTransparentProxy(obj))
			{
				this._rp = RemotingServices.GetRealProxy(obj);
			}
		}

		// Token: 0x06002619 RID: 9753 RVA: 0x0009A4B6 File Offset: 0x000986B6
		public IMessage SyncProcessMessage(IMessage msg)
		{
			this.CheckParameters(msg);
			if (this._rp != null)
			{
				return this._rp.Invoke(msg);
			}
			return RemotingServices.InternalExecuteMessage(this._target, (IMethodCallMessage)msg);
		}

		// Token: 0x0600261A RID: 9754 RVA: 0x0009A4E8 File Offset: 0x000986E8
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			object[] array = new object[] { msg, replySink };
			ThreadPool.QueueUserWorkItem(delegate(object data)
			{
				try
				{
					this.ExecuteAsyncMessage(data);
				}
				catch
				{
				}
			}, array);
			return null;
		}

		// Token: 0x0600261B RID: 9755 RVA: 0x0009A518 File Offset: 0x00098718
		private void ExecuteAsyncMessage(object ob)
		{
			object[] array = (object[])ob;
			IMethodCallMessage methodCallMessage = (IMethodCallMessage)array[0];
			IMessageSink messageSink = (IMessageSink)array[1];
			this.CheckParameters(methodCallMessage);
			IMessage message;
			if (this._rp != null)
			{
				message = this._rp.Invoke(methodCallMessage);
			}
			else
			{
				message = RemotingServices.InternalExecuteMessage(this._target, methodCallMessage);
			}
			messageSink.SyncProcessMessage(message);
		}

		// Token: 0x0600261C RID: 9756 RVA: 0x0009A570 File Offset: 0x00098770
		private void CheckParameters(IMessage msg)
		{
			IMethodCallMessage methodCallMessage = (IMethodCallMessage)msg;
			ParameterInfo[] parameters = methodCallMessage.MethodBase.GetParameters();
			int num = 0;
			foreach (ParameterInfo parameterInfo in parameters)
			{
				object arg = methodCallMessage.GetArg(num++);
				Type type = parameterInfo.ParameterType;
				if (type.IsByRef)
				{
					type = type.GetElementType();
				}
				if (arg != null && !type.IsInstanceOfType(arg))
				{
					throw new RemotingException(string.Concat(new string[]
					{
						"Cannot cast argument ",
						parameterInfo.Position.ToString(),
						" of type '",
						arg.GetType().AssemblyQualifiedName,
						"' to type '",
						type.AssemblyQualifiedName,
						"'"
					}));
				}
			}
		}

		// Token: 0x0400123F RID: 4671
		private MarshalByRefObject _target;

		// Token: 0x04001240 RID: 4672
		private RealProxy _rp;
	}
}
