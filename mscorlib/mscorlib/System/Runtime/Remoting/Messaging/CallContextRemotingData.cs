using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000476 RID: 1142
	[Serializable]
	internal class CallContextRemotingData : ICloneable
	{
		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x060024F4 RID: 9460 RVA: 0x00096ED5 File Offset: 0x000950D5
		// (set) Token: 0x060024F5 RID: 9461 RVA: 0x00096EDD File Offset: 0x000950DD
		internal string LogicalCallID
		{
			get
			{
				return this._logicalCallID;
			}
			set
			{
				this._logicalCallID = value;
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x060024F6 RID: 9462 RVA: 0x00096EE6 File Offset: 0x000950E6
		internal bool HasInfo
		{
			get
			{
				return this._logicalCallID != null;
			}
		}

		// Token: 0x060024F7 RID: 9463 RVA: 0x00096EF1 File Offset: 0x000950F1
		public object Clone()
		{
			return new CallContextRemotingData
			{
				LogicalCallID = this.LogicalCallID
			};
		}

		// Token: 0x040011BF RID: 4543
		private string _logicalCallID;
	}
}
