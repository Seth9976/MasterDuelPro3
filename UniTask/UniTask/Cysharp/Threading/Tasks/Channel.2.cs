using System;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200001D RID: 29
	public abstract class Channel<TWrite, TRead>
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000034DF File Offset: 0x000016DF
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x000034E7 File Offset: 0x000016E7
		public ChannelReader<TRead> Reader { get; protected set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x000034F0 File Offset: 0x000016F0
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x000034F8 File Offset: 0x000016F8
		public ChannelWriter<TWrite> Writer { get; protected set; }

		// Token: 0x060000AA RID: 170 RVA: 0x00003501 File Offset: 0x00001701
		public static implicit operator ChannelReader<TRead>(Channel<TWrite, TRead> channel)
		{
			return channel.Reader;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003509 File Offset: 0x00001709
		public static implicit operator ChannelWriter<TWrite>(Channel<TWrite, TRead> channel)
		{
			return channel.Writer;
		}
	}
}
