using System;
using System.Collections.Generic;
using TMPro;

namespace YgomGame.Menu.USAStateSelect
{
	// Token: 0x02000B08 RID: 2824
	public class USAStateSelectViewController : CommonScreenViewController
	{
		// Token: 0x0600520B RID: 21003 RVA: 0x0000216D File Offset: 0x0000036D
		private void createList(int defaultState)
		{
		}

		// Token: 0x0600520C RID: 21004 RVA: 0x0000216D File Offset: 0x0000036D
		private void updateButonText()
		{
		}

		// Token: 0x0600520D RID: 21005 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x0600520E RID: 21006 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x0600520F RID: 21007 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06005210 RID: 21008 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x0400907E RID: 36990
		private Action<int> m_resultCallback;

		// Token: 0x0400907F RID: 36991
		private IReadOnlyList<int> m_codeList;

		// Token: 0x04009080 RID: 36992
		private IReadOnlyList<string> m_nameList;

		// Token: 0x04009081 RID: 36993
		private int m_currentIndex;

		// Token: 0x04009082 RID: 36994
		private TMP_Text m_currentNameText;
	}
}
