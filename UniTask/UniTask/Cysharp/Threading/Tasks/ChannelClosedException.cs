using System;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000022 RID: 34
	public class ChannelClosedException : InvalidOperationException
	{
		// Token: 0x060000BB RID: 187 RVA: 0x00003683 File Offset: 0x00001883
		public ChannelClosedException()
			: base("Channel is already closed.")
		{
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003690 File Offset: 0x00001890
		public ChannelClosedException(string message)
			: base(message)
		{
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003699 File Offset: 0x00001899
		public ChannelClosedException(Exception innerException)
			: base("Channel is already closed", innerException)
		{
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000036A7 File Offset: 0x000018A7
		public ChannelClosedException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
