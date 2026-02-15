using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting.Channels;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200047F RID: 1151
	internal class CADMethodReturnMessage : CADMessageBase
	{
		// Token: 0x06002526 RID: 9510 RVA: 0x00097BE8 File Offset: 0x00095DE8
		internal static CADMethodReturnMessage Create(IMessage callMsg)
		{
			IMethodReturnMessage methodReturnMessage = callMsg as IMethodReturnMessage;
			if (methodReturnMessage == null)
			{
				return null;
			}
			return new CADMethodReturnMessage(methodReturnMessage);
		}

		// Token: 0x06002527 RID: 9511 RVA: 0x00097C08 File Offset: 0x00095E08
		internal CADMethodReturnMessage(IMethodReturnMessage retMsg)
			: base(retMsg)
		{
			ArrayList arrayList = null;
			this._propertyCount = CADMessageBase.MarshalProperties(retMsg.Properties, ref arrayList);
			this._returnValue = base.MarshalArgument(retMsg.ReturnValue, ref arrayList);
			this._args = base.MarshalArguments(retMsg.Args, ref arrayList);
			this._sig = CADMessageBase.GetSignature(base.GetMethod(), true);
			if (retMsg.Exception != null)
			{
				if (arrayList == null)
				{
					arrayList = new ArrayList();
				}
				this._exception = new CADArgHolder(arrayList.Count);
				arrayList.Add(retMsg.Exception);
			}
			base.SaveLogicalCallContext(retMsg, ref arrayList);
			if (arrayList != null)
			{
				MemoryStream memoryStream = CADSerializer.SerializeObject(arrayList.ToArray());
				this._serializedArgs = memoryStream.GetBuffer();
			}
		}

		// Token: 0x06002528 RID: 9512 RVA: 0x00097CC0 File Offset: 0x00095EC0
		internal ArrayList GetArguments()
		{
			ArrayList arrayList = null;
			if (this._serializedArgs != null)
			{
				byte[] array = new byte[this._serializedArgs.Length];
				Array.Copy(this._serializedArgs, array, this._serializedArgs.Length);
				arrayList = new ArrayList((object[])CADSerializer.DeserializeObject(new MemoryStream(array)));
				this._serializedArgs = null;
			}
			return arrayList;
		}

		// Token: 0x06002529 RID: 9513 RVA: 0x00097BCF File Offset: 0x00095DCF
		internal object[] GetArgs(ArrayList args)
		{
			return base.UnmarshalArguments(this._args, args);
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x00097D17 File Offset: 0x00095F17
		internal object GetReturnValue(ArrayList args)
		{
			return base.UnmarshalArgument(this._returnValue, args);
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x00097D26 File Offset: 0x00095F26
		internal Exception GetException(ArrayList args)
		{
			if (this._exception == null)
			{
				return null;
			}
			return (Exception)args[this._exception.index];
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x0600252C RID: 9516 RVA: 0x00097BDE File Offset: 0x00095DDE
		internal int PropertiesCount
		{
			get
			{
				return this._propertyCount;
			}
		}

		// Token: 0x040011E5 RID: 4581
		private object _returnValue;

		// Token: 0x040011E6 RID: 4582
		private CADArgHolder _exception;

		// Token: 0x040011E7 RID: 4583
		private Type[] _sig;
	}
}
