using System;
using System.Runtime.CompilerServices;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000B9C RID: 2972
	public abstract class MDMarkupContentPageBase : MDMarkupContentBase, IMDMarkupPageContent, IMDMarkupContent
	{
		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06005545 RID: 21829 RVA: 0x000029CC File Offset: 0x00000BCC
		public sealed override int contentIndent
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1400007F RID: 127
		// (add) Token: 0x06005546 RID: 21830 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06005547 RID: 21831 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action<bool> onFocusPageEvent
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

		// Token: 0x06005548 RID: 21832 RVA: 0x0000216D File Offset: 0x0000036D
		public void InvokeOnFocusPageEvent(bool isFirst)
		{
		}
	}
}
