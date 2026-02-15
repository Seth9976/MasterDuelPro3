using System;
using System.Collections.Generic;
using TMPro;

namespace YgomGame.Menu
{
	// Token: 0x02000AA2 RID: 2722
	public class LanguageSelectViewController : CommonScreenViewController
	{
		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06004F4B RID: 20299 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004F4C RID: 20300 RVA: 0x0000216D File Offset: 0x0000036D
		private void createLanguageList()
		{
		}

		// Token: 0x06004F4D RID: 20301 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004F4E RID: 20302 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06004F4F RID: 20303 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06004F50 RID: 20304 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x04008D3E RID: 36158
		private List<string> m_langCodeList;

		// Token: 0x04008D3F RID: 36159
		private List<string> m_langNameList;

		// Token: 0x04008D40 RID: 36160
		private int m_currentIndex;

		// Token: 0x04008D41 RID: 36161
		private TMP_Text m_currentLangText;

		// Token: 0x04008D42 RID: 36162
		private Action<string> m_resultCallback;
	}
}
