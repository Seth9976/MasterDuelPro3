using System;
using System.Collections.Generic;
using TMPro;

namespace YgomGame.Menu.CountrySelect
{
	// Token: 0x02000B0B RID: 2827
	public class CountrySelectViewController : CommonScreenViewController
	{
		// Token: 0x0600521A RID: 21018 RVA: 0x0000216D File Offset: 0x0000036D
		private void createList(int defaultCountry)
		{
		}

		// Token: 0x0600521B RID: 21019 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x0600521C RID: 21020 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x0600521D RID: 21021 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x0600521E RID: 21022 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x04009089 RID: 37001
		private Action<int> m_resultCallback;

		// Token: 0x0400908A RID: 37002
		private IReadOnlyList<int> m_codeList;

		// Token: 0x0400908B RID: 37003
		private IReadOnlyList<string> m_nameList;

		// Token: 0x0400908C RID: 37004
		private int m_currentIndex;

		// Token: 0x0400908D RID: 37005
		private TMP_Text m_currentNameText;
	}
}
