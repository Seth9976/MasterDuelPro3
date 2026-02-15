using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomGame.Utility;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000B92 RID: 2962
	[Serializable]
	public class MDMarkupContentEmbedContainerTab : MDMarkupContentBase, IMDMarkupContentGlobalText, IMDMarkupContentAsyncLoader
	{
		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06005506 RID: 21766 RVA: 0x000029CC File Offset: 0x00000BCC
		public override MDMarkupDef.MarkupType markupType
		{
			get
			{
				return MDMarkupDef.MarkupType.None;
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06005507 RID: 21767 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int contentIndent
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06005508 RID: 21768 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005509 RID: 21769 RVA: 0x0000216D File Offset: 0x0000036D
		public MDMarkupAsset embedMarkupAsset
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x0600550A RID: 21770 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyList<string> GetTextGloups()
		{
			return null;
		}

		// Token: 0x0600550B RID: 21771 RVA: 0x0000216D File Offset: 0x0000036D
		public void LoadAsync(Action onComplete)
		{
		}

		// Token: 0x0600550C RID: 21772 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadAsyncCompleteCheck()
		{
		}

		// Token: 0x0600550D RID: 21773 RVA: 0x0000216A File Offset: 0x0000036A
		protected override object OnExportJsonObj(object jsonObj)
		{
			return null;
		}

		// Token: 0x0600550E RID: 21774 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnImportJsonObj(object jsonObj)
		{
		}

		// Token: 0x04009231 RID: 37425
		[SerializeField]
		public string mmaPath;

		// Token: 0x04009232 RID: 37426
		[SerializeField]
		public GlobalTextData overrideTitle;

		// Token: 0x04009233 RID: 37427
		private Action m_LoadAsyncOnComplete;

		// Token: 0x04009234 RID: 37428
		private int m_LoadingCnt;
	}
}
