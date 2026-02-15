using System;

namespace UnityEngine.Networking
{
	// Token: 0x02000003 RID: 3
	public struct DownloadedTextureParams
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static DownloadedTextureParams Default
		{
			get
			{
				return new DownloadedTextureParams
				{
					flags = (DownloadedTextureFlags.Readable | DownloadedTextureFlags.MipmapChain),
					mipmapCount = -1
				};
			}
		}

		// Token: 0x17000002 RID: 2
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002076 File Offset: 0x00000276
		public bool readable
		{
			set
			{
				this.SetFlags(DownloadedTextureFlags.Readable, value);
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002084 File Offset: 0x00000284
		private void SetFlags(DownloadedTextureFlags flgs, bool add)
		{
			if (add)
			{
				this.flags |= flgs;
			}
			else
			{
				this.flags &= ~flgs;
			}
		}

		// Token: 0x04000006 RID: 6
		public DownloadedTextureFlags flags;

		// Token: 0x04000007 RID: 7
		public int mipmapCount;
	}
}
