using System;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B0D RID: 2829
	public class AsyncContainContent : IAsyncProgressContent
	{
		// Token: 0x06005221 RID: 21025 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsDone()
		{
			return false;
		}

		// Token: 0x06005222 RID: 21026 RVA: 0x0000216D File Offset: 0x0000036D
		public void ProgressUpdate()
		{
		}

		// Token: 0x06005223 RID: 21027 RVA: 0x0000216D File Offset: 0x0000036D
		public void Clear()
		{
		}

		// Token: 0x06005224 RID: 21028 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddLoadingCount()
		{
		}

		// Token: 0x06005225 RID: 21029 RVA: 0x0000216D File Offset: 0x0000036D
		public void DecLoadingCount()
		{
		}

		// Token: 0x06005226 RID: 21030 RVA: 0x0000216D File Offset: 0x0000036D
		public void AssignProgressContent(IAsyncProgressContent progressContent)
		{
		}

		// Token: 0x04009092 RID: 37010
		private AsyncProgressLoadingCountContent m_LoadingCountContent;

		// Token: 0x04009093 RID: 37011
		private AsyncProgressContainContent m_AsyncProgressContainContent;
	}
}
