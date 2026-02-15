using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000B98 RID: 2968
	[Serializable]
	public class MDMarkupContentHalfBannerMarkupPage : MDMarkupContentPageBase, IMDMarkupContentGlobalText, IMDMarkupContentAsyncLoader
	{
		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x0600552E RID: 21806 RVA: 0x000029CC File Offset: 0x00000BCC
		public override MDMarkupDef.MarkupType markupType
		{
			get
			{
				return MDMarkupDef.MarkupType.None;
			}
		}

		// Token: 0x0600552F RID: 21807 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyList<string> GetTextGloups()
		{
			return null;
		}

		// Token: 0x06005530 RID: 21808 RVA: 0x0000216D File Offset: 0x0000036D
		public void LoadAsync(Action onComplete)
		{
		}

		// Token: 0x06005531 RID: 21809 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadAsyncCompleteCheck()
		{
		}

		// Token: 0x06005532 RID: 21810 RVA: 0x0000216A File Offset: 0x0000036A
		protected override object OnExportJsonObj(object jsonObj)
		{
			return null;
		}

		// Token: 0x06005533 RID: 21811 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnImportJsonObj(object jsonObj)
		{
		}

		// Token: 0x0400923D RID: 37437
		[SerializeField]
		public MDMarkupBannerContext banner;

		// Token: 0x0400923E RID: 37438
		[SerializeField]
		public List<URLSchemeButton> buttons;

		// Token: 0x0400923F RID: 37439
		public List<IMDMarkupContent> contents;

		// Token: 0x04009240 RID: 37440
		private int m_LoadingCnt;

		// Token: 0x04009241 RID: 37441
		private Action m_LoadAsyncOnComplete;
	}
}
