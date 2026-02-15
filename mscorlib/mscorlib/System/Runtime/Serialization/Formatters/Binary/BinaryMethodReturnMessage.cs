using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020004FA RID: 1274
	[Serializable]
	internal class BinaryMethodReturnMessage
	{
		// Token: 0x060027FF RID: 10239 RVA: 0x000A14CC File Offset: 0x0009F6CC
		internal BinaryMethodReturnMessage(object returnValue, object[] args, Exception e, LogicalCallContext callContext, object[] properties)
		{
			this._returnValue = returnValue;
			if (args == null)
			{
				args = new object[0];
			}
			this._outargs = args;
			this._args = args;
			this._exception = e;
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

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06002800 RID: 10240 RVA: 0x000A1527 File Offset: 0x0009F727
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06002801 RID: 10241 RVA: 0x000A152F File Offset: 0x0009F72F
		public object ReturnValue
		{
			get
			{
				return this._returnValue;
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06002802 RID: 10242 RVA: 0x000A1537 File Offset: 0x0009F737
		public object[] Args
		{
			get
			{
				return this._args;
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06002803 RID: 10243 RVA: 0x000A153F File Offset: 0x0009F73F
		public LogicalCallContext LogicalCallContext
		{
			get
			{
				return this._logicalCallContext;
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06002804 RID: 10244 RVA: 0x000A1547 File Offset: 0x0009F747
		public bool HasProperties
		{
			get
			{
				return this._properties != null;
			}
		}

		// Token: 0x06002805 RID: 10245 RVA: 0x000A1554 File Offset: 0x0009F754
		internal void PopulateMessageProperties(IDictionary dict)
		{
			foreach (DictionaryEntry dictionaryEntry in this._properties)
			{
				dict[dictionaryEntry.Key] = dictionaryEntry.Value;
			}
		}

		// Token: 0x040013E8 RID: 5096
		private object[] _outargs;

		// Token: 0x040013E9 RID: 5097
		private Exception _exception;

		// Token: 0x040013EA RID: 5098
		private object _returnValue;

		// Token: 0x040013EB RID: 5099
		private object[] _args;

		// Token: 0x040013EC RID: 5100
		private LogicalCallContext _logicalCallContext;

		// Token: 0x040013ED RID: 5101
		private object[] _properties;
	}
}
