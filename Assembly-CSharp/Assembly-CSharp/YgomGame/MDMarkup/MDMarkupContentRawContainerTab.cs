using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Utility;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000B9D RID: 2973
	[Serializable]
	public class MDMarkupContentRawContainerTab : MDMarkupContentBase, IMDMarkupContentGlobalText, IMDMarkupContentAsyncLoader
	{
		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x0600554A RID: 21834 RVA: 0x000029CC File Offset: 0x00000BCC
		public override MDMarkupDef.MarkupType markupType
		{
			get
			{
				return MDMarkupDef.MarkupType.None;
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x0600554B RID: 21835 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int contentIndent
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600554C RID: 21836 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyList<string> GetTextGloups()
		{
			return null;
		}

		// Token: 0x0600554D RID: 21837 RVA: 0x0000216D File Offset: 0x0000036D
		public void LoadAsync(Action onComplete)
		{
		}

		// Token: 0x0600554E RID: 21838 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadAsyncCompleteCheck()
		{
		}

		// Token: 0x0600554F RID: 21839 RVA: 0x0000216A File Offset: 0x0000036A
		protected override object OnExportJsonObj(object jsonObj)
		{
			return null;
		}

		// Token: 0x06005550 RID: 21840 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnImportJsonObj(object jsonObj)
		{
		}

		// Token: 0x0400924E RID: 37454
		[SerializeField]
		public GlobalTextData title;

		// Token: 0x0400924F RID: 37455
		public List<IMDMarkupContent> contents;

		// Token: 0x04009250 RID: 37456
		private Action m_LoadAsyncOnComplete;

		// Token: 0x04009251 RID: 37457
		private int m_LoadingCnt;
	}
}
