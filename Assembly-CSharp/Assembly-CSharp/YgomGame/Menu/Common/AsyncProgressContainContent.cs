using System;
using System.Collections.Generic;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B0E RID: 2830
	public class AsyncProgressContainContent : IAsyncProgressContent
	{
		// Token: 0x06005228 RID: 21032 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsDone()
		{
			return false;
		}

		// Token: 0x06005229 RID: 21033 RVA: 0x0000216D File Offset: 0x0000036D
		public void Clear()
		{
		}

		// Token: 0x0600522A RID: 21034 RVA: 0x0000216D File Offset: 0x0000036D
		public void Assign(IAsyncProgressContent progressContent)
		{
		}

		// Token: 0x0600522B RID: 21035 RVA: 0x0000216D File Offset: 0x0000036D
		public void ProgressUpdate()
		{
		}

		// Token: 0x04009094 RID: 37012
		private List<IAsyncProgressContent> m_AsyncContents;
	}
}
