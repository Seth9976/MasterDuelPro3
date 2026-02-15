using System;
using System.Collections.Generic;
using UnityEngine;
using YgomSystem.Network;

namespace YgomSystem.UI
{
	// Token: 0x02000648 RID: 1608
	public class UINetworkHandler : MonoBehaviour
	{
		// Token: 0x0600323A RID: 12858 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool isIgnoreNetworkProgressCommand(string command)
		{
			return false;
		}

		// Token: 0x0600323B RID: 12859 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetSkipHandleSectionMainteEnabled(bool enabled)
		{
		}

		// Token: 0x0600323C RID: 12860 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetSystemHandler()
		{
		}

		// Token: 0x0600323D RID: 12861 RVA: 0x0000216D File Offset: 0x0000036D
		public void ClearOnReboot()
		{
		}

		// Token: 0x0600323E RID: 12862 RVA: 0x0000216D File Offset: 0x0000036D
		private void networkStartHandle(Handle handle)
		{
		}

		// Token: 0x0600323F RID: 12863 RVA: 0x0000216D File Offset: 0x0000036D
		private void networkCompleteHandle(Handle handle)
		{
		}

		// Token: 0x06003240 RID: 12864 RVA: 0x0000216D File Offset: 0x0000036D
		private void networkErrorHandle(Handle handle)
		{
		}

		// Token: 0x06003241 RID: 12865 RVA: 0x0000216D File Offset: 0x0000036D
		public void networkDisconnectErrorDialog()
		{
		}

		// Token: 0x06003242 RID: 12866 RVA: 0x0000216D File Offset: 0x0000036D
		private void resourceProgressHandle(bool isshow)
		{
		}

		// Token: 0x06003243 RID: 12867 RVA: 0x000F2978 File Offset: 0x000F0B78
		private bool? resourceRetryHandle(bool firstcall)
		{
			return null;
		}

		// Token: 0x06003244 RID: 12868 RVA: 0x0000216D File Offset: 0x0000036D
		private void resourceErrorHandle(string path)
		{
		}

		// Token: 0x06003245 RID: 12869 RVA: 0x0000216D File Offset: 0x0000036D
		public void OpenNetworkErrorDialog(Handle handle)
		{
		}

		// Token: 0x04002EE5 RID: 12005
		private List<Handle> m_NetworkProgressDispOwners;

		// Token: 0x04002EE6 RID: 12006
		private int m_ResourceNetworkProgressCnt;

		// Token: 0x04002EE7 RID: 12007
		private bool m_Retry;

		// Token: 0x04002EE8 RID: 12008
		private int m_SkipHandleSectionMainteCnt;

		// Token: 0x04002EE9 RID: 12009
		private List<Handle> m_RetryHandles;
	}
}
