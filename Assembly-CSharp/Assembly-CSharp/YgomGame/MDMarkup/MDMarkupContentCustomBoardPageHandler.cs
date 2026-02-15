using System;
using System.Runtime.CompilerServices;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000B91 RID: 2961
	[Serializable]
	public class MDMarkupContentCustomBoardPageHandler : IMDMarkupContent
	{
		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x060054FD RID: 21757 RVA: 0x000029CC File Offset: 0x00000BCC
		public MDMarkupDef.MarkupType markupType
		{
			get
			{
				return MDMarkupDef.MarkupType.None;
			}
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x060054FE RID: 21758 RVA: 0x000029CC File Offset: 0x00000BCC
		public int contentIndent
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1400007E RID: 126
		// (add) Token: 0x060054FF RID: 21759 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06005500 RID: 21760 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action<int, bool> onFocusPageEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x06005501 RID: 21761 RVA: 0x0000216D File Offset: 0x0000036D
		public void InvokeOnFocusPageEvent(int idx, bool isFirst)
		{
		}

		// Token: 0x06005502 RID: 21762 RVA: 0x0000216A File Offset: 0x0000036A
		public object ExportJsonObj()
		{
			return null;
		}

		// Token: 0x06005503 RID: 21763 RVA: 0x0000216D File Offset: 0x0000036D
		public void ImportJsonObj(object jsonObj)
		{
		}

		// Token: 0x06005504 RID: 21764 RVA: 0x0000216A File Offset: 0x0000036A
		public string ToJson()
		{
			return null;
		}

		// Token: 0x0400922C RID: 37420
		public int startIdx;

		// Token: 0x0400922D RID: 37421
		public int length;

		// Token: 0x0400922E RID: 37422
		public string title;

		// Token: 0x0400922F RID: 37423
		public Func<int, object> onCreatePageMMAContainerFunc;

		// Token: 0x04009230 RID: 37424
		public Action<MDMarkupBoardPagerContainerWidget.Context, int> onUpdatePageCallback;
	}
}
