using System;
using UnityEngine.Events;
using YgomSystem.UI;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000B6D RID: 2925
	public interface IMDMarkupButtonWidget
	{
		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x0600546E RID: 21614
		SelectionButton button { get; }

		// Token: 0x0600546F RID: 21615
		void SetOnClick(UnityAction callback);
	}
}
