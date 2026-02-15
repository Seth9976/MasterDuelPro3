using System;

namespace UnityWebSocket
{
	// Token: 0x02000004 RID: 4
	public class CloseEventArgs : EventArgs
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BF File Offset: 0x000002BF
		internal CloseEventArgs()
		{
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020C7 File Offset: 0x000002C7
		internal CloseEventArgs(ushort code)
			: this(code, null)
		{
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020C7 File Offset: 0x000002C7
		internal CloseEventArgs(CloseStatusCode code)
			: this((ushort)code, null)
		{
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020D1 File Offset: 0x000002D1
		internal CloseEventArgs(CloseStatusCode code, string reason)
			: this((ushort)code, reason)
		{
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020DB File Offset: 0x000002DB
		internal CloseEventArgs(ushort code, string reason)
		{
			this.Code = code;
			this.Reason = reason;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020F1 File Offset: 0x000002F1
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000020F9 File Offset: 0x000002F9
		public ushort Code { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002102 File Offset: 0x00000302
		// (set) Token: 0x0600000B RID: 11 RVA: 0x0000210A File Offset: 0x0000030A
		public string Reason { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002113 File Offset: 0x00000313
		// (set) Token: 0x0600000D RID: 13 RVA: 0x0000211B File Offset: 0x0000031B
		public bool WasClean { get; internal set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002124 File Offset: 0x00000324
		public CloseStatusCode StatusCode
		{
			get
			{
				if (Enum.IsDefined(typeof(CloseStatusCode), this.Code))
				{
					return (CloseStatusCode)this.Code;
				}
				return CloseStatusCode.Unknown;
			}
		}
	}
}
