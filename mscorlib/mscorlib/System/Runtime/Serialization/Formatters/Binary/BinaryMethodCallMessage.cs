using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020004F9 RID: 1273
	[Serializable]
	internal sealed class BinaryMethodCallMessage
	{
		// Token: 0x060027F6 RID: 10230 RVA: 0x000A13E0 File Offset: 0x0009F5E0
		internal BinaryMethodCallMessage(string uri, string methodName, string typeName, Type[] instArgs, object[] args, object methodSignature, LogicalCallContext callContext, object[] properties)
		{
			this._methodName = methodName;
			this._typeName = typeName;
			if (args == null)
			{
				args = new object[0];
			}
			this._inargs = args;
			this._args = args;
			this._instArgs = instArgs;
			this._methodSignature = methodSignature;
			if (callContext == null)
			{
				this._logicalCallContext = new LogicalCallContext();
			}
			else
			{
				this._logicalCallContext = callContext;
			}
			this._properties = properties;
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x060027F7 RID: 10231 RVA: 0x000A144E File Offset: 0x0009F64E
		public string MethodName
		{
			get
			{
				return this._methodName;
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x060027F8 RID: 10232 RVA: 0x000A1456 File Offset: 0x0009F656
		public string TypeName
		{
			get
			{
				return this._typeName;
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x060027F9 RID: 10233 RVA: 0x000A145E File Offset: 0x0009F65E
		public Type[] InstantiationArgs
		{
			get
			{
				return this._instArgs;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x060027FA RID: 10234 RVA: 0x000A1466 File Offset: 0x0009F666
		public object MethodSignature
		{
			get
			{
				return this._methodSignature;
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x060027FB RID: 10235 RVA: 0x000A146E File Offset: 0x0009F66E
		public object[] Args
		{
			get
			{
				return this._args;
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x060027FC RID: 10236 RVA: 0x000A1476 File Offset: 0x0009F676
		public LogicalCallContext LogicalCallContext
		{
			get
			{
				return this._logicalCallContext;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x060027FD RID: 10237 RVA: 0x000A147E File Offset: 0x0009F67E
		public bool HasProperties
		{
			get
			{
				return this._properties != null;
			}
		}

		// Token: 0x060027FE RID: 10238 RVA: 0x000A148C File Offset: 0x0009F68C
		internal void PopulateMessageProperties(IDictionary dict)
		{
			foreach (DictionaryEntry dictionaryEntry in this._properties)
			{
				dict[dictionaryEntry.Key] = dictionaryEntry.Value;
			}
		}

		// Token: 0x040013E0 RID: 5088
		private object[] _inargs;

		// Token: 0x040013E1 RID: 5089
		private string _methodName;

		// Token: 0x040013E2 RID: 5090
		private string _typeName;

		// Token: 0x040013E3 RID: 5091
		private object _methodSignature;

		// Token: 0x040013E4 RID: 5092
		private Type[] _instArgs;

		// Token: 0x040013E5 RID: 5093
		private object[] _args;

		// Token: 0x040013E6 RID: 5094
		private LogicalCallContext _logicalCallContext;

		// Token: 0x040013E7 RID: 5095
		private object[] _properties;
	}
}
