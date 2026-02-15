using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x02000129 RID: 297
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public class ProgressBar : AbstractProgressBar
	{
		// Token: 0x0200012A RID: 298
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<ProgressBar, AbstractProgressBar.UxmlTraits>
		{
		}
	}
}
