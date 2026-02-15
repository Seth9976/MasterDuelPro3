using System;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B0F RID: 2831
	public class AsyncProgressLoadingCountContent : IAsyncProgressContent
	{
		// Token: 0x0600522D RID: 21037 RVA: 0x0000216D File Offset: 0x0000036D
		public void Clear()
		{
		}

		// Token: 0x0600522E RID: 21038 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddLoadingCount()
		{
		}

		// Token: 0x0600522F RID: 21039 RVA: 0x0000216D File Offset: 0x0000036D
		public void DecLoadingCount()
		{
		}

		// Token: 0x06005230 RID: 21040 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsDone()
		{
			return false;
		}

		// Token: 0x06005231 RID: 21041 RVA: 0x0000216D File Offset: 0x0000036D
		public void ProgressUpdate()
		{
		}

		// Token: 0x04009095 RID: 37013
		private int m_LoadingCount;
	}
}
