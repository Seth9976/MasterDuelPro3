using System;

namespace UnityEngine
{
	// Token: 0x02000134 RID: 308
	public struct MipmapLimitDescriptor
	{
		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000D09 RID: 3337 RVA: 0x00018771 File Offset: 0x00016971
		public readonly bool useMipmapLimit { get; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x00018779 File Offset: 0x00016979
		public readonly string groupName { get; }

		// Token: 0x06000D0B RID: 3339 RVA: 0x00018781 File Offset: 0x00016981
		public MipmapLimitDescriptor(bool useMipmapLimit, string groupName)
		{
			this.useMipmapLimit = useMipmapLimit;
			this.groupName = groupName;
		}
	}
}
