using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting.Channels;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200047E RID: 1150
	internal class CADMethodCallMessage : CADMessageBase
	{
		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06002520 RID: 9504 RVA: 0x00097ADF File Offset: 0x00095CDF
		internal string Uri
		{
			get
			{
				return this._uri;
			}
		}

		// Token: 0x06002521 RID: 9505 RVA: 0x00097AE8 File Offset: 0x00095CE8
		internal static CADMethodCallMessage Create(IMessage callMsg)
		{
			IMethodCallMessage methodCallMessage = callMsg as IMethodCallMessage;
			if (methodCallMessage == null)
			{
				return null;
			}
			return new CADMethodCallMessage(methodCallMessage);
		}

		// Token: 0x06002522 RID: 9506 RVA: 0x00097B08 File Offset: 0x00095D08
		internal CADMethodCallMessage(IMethodCallMessage callMsg)
			: base(callMsg)
		{
			this._uri = callMsg.Uri;
			ArrayList arrayList = null;
			this._propertyCount = CADMessageBase.MarshalProperties(callMsg.Properties, ref arrayList);
			this._args = base.MarshalArguments(callMsg.Args, ref arrayList);
			base.SaveLogicalCallContext(callMsg, ref arrayList);
			if (arrayList != null)
			{
				MemoryStream memoryStream = CADSerializer.SerializeObject(arrayList.ToArray());
				this._serializedArgs = memoryStream.GetBuffer();
			}
		}

		// Token: 0x06002523 RID: 9507 RVA: 0x00097B78 File Offset: 0x00095D78
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

		// Token: 0x06002524 RID: 9508 RVA: 0x00097BCF File Offset: 0x00095DCF
		internal object[] GetArgs(ArrayList args)
		{
			return base.UnmarshalArguments(this._args, args);
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06002525 RID: 9509 RVA: 0x00097BDE File Offset: 0x00095DDE
		internal int PropertiesCount
		{
			get
			{
				return this._propertyCount;
			}
		}

		// Token: 0x040011E4 RID: 4580
		private string _uri;
	}
}
