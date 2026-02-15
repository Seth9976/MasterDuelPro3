using System;

namespace YgomSystem.LocalFileSystem
{
	// Token: 0x0200074C RID: 1868
	public class ReadRequest
	{
		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06003A68 RID: 14952 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isReading
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06003A69 RID: 14953 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isSuccess
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400343F RID: 13375
		public ReadRequest.Status status;

		// Token: 0x04003440 RID: 13376
		public byte[] readData;

		// Token: 0x0200074D RID: 1869
		public enum Status
		{
			// Token: 0x04003442 RID: 13378
			None,
			// Token: 0x04003443 RID: 13379
			Working,
			// Token: 0x04003444 RID: 13380
			Success,
			// Token: 0x04003445 RID: 13381
			Error
		}
	}
}
