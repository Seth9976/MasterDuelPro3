using System;

namespace Mono.Net.Security
{
	// Token: 0x02000067 RID: 103
	internal abstract class AsyncReadOrWriteRequest : AsyncProtocolRequest
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00005587 File Offset: 0x00003787
		protected BufferOffsetSize UserBuffer { get; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000137 RID: 311 RVA: 0x0000558F File Offset: 0x0000378F
		// (set) Token: 0x06000138 RID: 312 RVA: 0x00005597 File Offset: 0x00003797
		protected int CurrentSize { get; set; }

		// Token: 0x06000139 RID: 313 RVA: 0x000055A0 File Offset: 0x000037A0
		public AsyncReadOrWriteRequest(MobileAuthenticatedStream parent, bool sync, byte[] buffer, int offset, int size)
			: base(parent, sync)
		{
			this.UserBuffer = new BufferOffsetSize(buffer, offset, size);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000055BA File Offset: 0x000037BA
		public override string ToString()
		{
			return string.Format("[{0}: {1}]", base.Name, this.UserBuffer);
		}
	}
}
