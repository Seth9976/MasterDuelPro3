using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting
{
	// Token: 0x02000426 RID: 1062
	internal class SingleCallIdentity : ServerIdentity
	{
		// Token: 0x06002384 RID: 9092 RVA: 0x0009285B File Offset: 0x00090A5B
		public SingleCallIdentity(string objectUri, Context context, Type objectType)
			: base(objectUri, context, objectType)
		{
		}

		// Token: 0x06002385 RID: 9093 RVA: 0x00092964 File Offset: 0x00090B64
		public override IMessage SyncObjectProcessMessage(IMessage msg)
		{
			MarshalByRefObject marshalByRefObject = (MarshalByRefObject)Activator.CreateInstance(this._objectType, true);
			if (marshalByRefObject.ObjectIdentity == null)
			{
				marshalByRefObject.ObjectIdentity = this;
			}
			IMessage message = this._context.CreateServerObjectSinkChain(marshalByRefObject, false).SyncProcessMessage(msg);
			if (marshalByRefObject is IDisposable)
			{
				((IDisposable)marshalByRefObject).Dispose();
			}
			return message;
		}

		// Token: 0x06002386 RID: 9094 RVA: 0x000929B8 File Offset: 0x00090BB8
		public override IMessageCtrl AsyncObjectProcessMessage(IMessage msg, IMessageSink replySink)
		{
			MarshalByRefObject marshalByRefObject = (MarshalByRefObject)Activator.CreateInstance(this._objectType, true);
			IMessageSink messageSink = this._context.CreateServerObjectSinkChain(marshalByRefObject, false);
			if (marshalByRefObject is IDisposable)
			{
				replySink = new DisposerReplySink(replySink, (IDisposable)marshalByRefObject);
			}
			return messageSink.AsyncProcessMessage(msg, replySink);
		}
	}
}
