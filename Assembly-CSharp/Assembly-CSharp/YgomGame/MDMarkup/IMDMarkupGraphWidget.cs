using System;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000B75 RID: 2933
	public interface IMDMarkupGraphWidget
	{
		// Token: 0x06005485 RID: 21637
		void OutputMarkupGraph(IMDMarkupContent mdMarkupContent, MDMarkupGraphFactory markupGraphFactory, Action onComplete);
	}
}
