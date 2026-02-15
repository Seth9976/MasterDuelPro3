using System;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Mono.Net.Dns
{
	// Token: 0x02000096 RID: 150
	internal class SimpleResolverEventArgs : EventArgs
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000251 RID: 593 RVA: 0x00009914 File Offset: 0x00007B14
		// (remove) Token: 0x06000252 RID: 594 RVA: 0x0000994C File Offset: 0x00007B4C
		public event EventHandler<SimpleResolverEventArgs> Completed;

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00009989 File Offset: 0x00007B89
		// (set) Token: 0x06000255 RID: 597 RVA: 0x00009991 File Offset: 0x00007B91
		public ResolverError ResolverError { get; set; }

		// Token: 0x17000089 RID: 137
		// (set) Token: 0x06000256 RID: 598 RVA: 0x0000999A File Offset: 0x00007B9A
		public string ErrorMessage
		{
			[CompilerGenerated]
			set
			{
				this.<ErrorMessage>k__BackingField = value;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000257 RID: 599 RVA: 0x000099A3 File Offset: 0x00007BA3
		// (set) Token: 0x06000258 RID: 600 RVA: 0x000099AB File Offset: 0x00007BAB
		public string HostName { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000259 RID: 601 RVA: 0x000099B4 File Offset: 0x00007BB4
		// (set) Token: 0x0600025A RID: 602 RVA: 0x000099BC File Offset: 0x00007BBC
		public IPHostEntry HostEntry { get; internal set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600025B RID: 603 RVA: 0x000099C5 File Offset: 0x00007BC5
		// (set) Token: 0x0600025C RID: 604 RVA: 0x000099CD File Offset: 0x00007BCD
		public object UserToken { get; set; }

		// Token: 0x0600025D RID: 605 RVA: 0x000099D6 File Offset: 0x00007BD6
		internal void Reset(ResolverAsyncOperation op)
		{
			this.ResolverError = ResolverError.NoError;
			this.ErrorMessage = null;
			this.HostEntry = null;
			this.LastOperation = op;
			this.QueryID = 0;
			this.Retries = 0;
			this.PTRAddress = null;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00009A0C File Offset: 0x00007C0C
		protected internal void OnCompleted(object sender)
		{
			EventHandler<SimpleResolverEventArgs> completed = this.Completed;
			if (completed != null)
			{
				completed(sender, this);
			}
		}

		// Token: 0x04000264 RID: 612
		public ResolverAsyncOperation LastOperation;

		// Token: 0x04000268 RID: 616
		internal ushort QueryID;

		// Token: 0x04000269 RID: 617
		internal ushort Retries;

		// Token: 0x0400026A RID: 618
		internal Timer Timer;

		// Token: 0x0400026B RID: 619
		internal IPAddress PTRAddress;
	}
}
