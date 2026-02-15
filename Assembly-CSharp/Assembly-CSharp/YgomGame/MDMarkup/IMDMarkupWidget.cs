using System;
using UnityEngine;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000B7F RID: 2943
	public interface IMDMarkupWidget : IMDMarkupAsyncWidget
	{
		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06005495 RID: 21653
		GameObject gameObject { get; }

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06005496 RID: 21654
		Transform transform { get; }

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06005497 RID: 21655
		MDMarkupIndentWidget indentWidget { get; }

		// Token: 0x06005498 RID: 21656
		void BindContentData(IMDMarkupContent mdMarkupContent);
	}
}
