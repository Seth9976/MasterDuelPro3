using System;
using System.Collections;
using System.Reflection;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000486 RID: 1158
	[Serializable]
	internal class ErrorMessage : IMethodCallMessage, IMethodMessage, IMessage
	{
		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06002551 RID: 9553 RVA: 0x00033991 File Offset: 0x00031B91
		public int ArgCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06002552 RID: 9554 RVA: 0x000082D2 File Offset: 0x000064D2
		public object[] Args
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06002553 RID: 9555 RVA: 0x000082D2 File Offset: 0x000064D2
		public MethodBase MethodBase
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06002554 RID: 9556 RVA: 0x0009829F File Offset: 0x0009649F
		public string MethodName
		{
			get
			{
				return "unknown";
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06002555 RID: 9557 RVA: 0x000082D2 File Offset: 0x000064D2
		public object MethodSignature
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06002556 RID: 9558 RVA: 0x000082D2 File Offset: 0x000064D2
		public virtual IDictionary Properties
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06002557 RID: 9559 RVA: 0x0009829F File Offset: 0x0009649F
		public string TypeName
		{
			get
			{
				return "unknown";
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06002558 RID: 9560 RVA: 0x000982A6 File Offset: 0x000964A6
		public string Uri
		{
			get
			{
				return this._uri;
			}
		}

		// Token: 0x06002559 RID: 9561 RVA: 0x000082D2 File Offset: 0x000064D2
		public object GetArg(int arg_num)
		{
			return null;
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x0600255A RID: 9562 RVA: 0x000082D2 File Offset: 0x000064D2
		public LogicalCallContext LogicalCallContext
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040011F4 RID: 4596
		private string _uri = "Exception";
	}
}
