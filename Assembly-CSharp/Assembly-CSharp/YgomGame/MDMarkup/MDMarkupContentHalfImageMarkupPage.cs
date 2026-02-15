using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000B99 RID: 2969
	[Serializable]
	public class MDMarkupContentHalfImageMarkupPage : MDMarkupContentPageBase, IMDMarkupContentGlobalText, IMDMarkupContentAsyncLoader
	{
		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06005535 RID: 21813 RVA: 0x000029CC File Offset: 0x00000BCC
		public override MDMarkupDef.MarkupType markupType
		{
			get
			{
				return MDMarkupDef.MarkupType.None;
			}
		}

		// Token: 0x06005536 RID: 21814 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyList<string> GetTextGloups()
		{
			return null;
		}

		// Token: 0x06005537 RID: 21815 RVA: 0x0000216D File Offset: 0x0000036D
		public void LoadAsync(Action onComplete)
		{
		}

		// Token: 0x06005538 RID: 21816 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadAsyncCompleteCheck()
		{
		}

		// Token: 0x06005539 RID: 21817 RVA: 0x0000216A File Offset: 0x0000036A
		protected override object OnExportJsonObj(object jsonObj)
		{
			return null;
		}

		// Token: 0x0600553A RID: 21818 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnImportJsonObj(object jsonObj)
		{
		}

		// Token: 0x04009242 RID: 37442
		[SerializeField]
		public string resourcePath;

		// Token: 0x04009243 RID: 37443
		[SerializeField]
		public List<URLSchemeButton> buttons;

		// Token: 0x04009244 RID: 37444
		public List<IMDMarkupContent> contents;

		// Token: 0x04009245 RID: 37445
		private int m_LoadingCnt;

		// Token: 0x04009246 RID: 37446
		private Action m_LoadAsyncOnComplete;
	}
}
