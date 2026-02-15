using System;

namespace UnityEngine.ResourceManagement.AsyncOperations
{
	// Token: 0x02000080 RID: 128
	public struct DownloadStatus
	{
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0000BC85 File Offset: 0x00009E85
		public float Percent
		{
			get
			{
				if (this.TotalBytes > 0L)
				{
					return (float)this.DownloadedBytes / (float)this.TotalBytes;
				}
				if (!this.IsDone)
				{
					return 0f;
				}
				return 1f;
			}
		}

		// Token: 0x0400016A RID: 362
		public long TotalBytes;

		// Token: 0x0400016B RID: 363
		public long DownloadedBytes;

		// Token: 0x0400016C RID: 364
		public bool IsDone;
	}
}
