using System;
using System.ComponentModel;

namespace UnityEngine.UI
{
	// Token: 0x02000024 RID: 36
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("Not supported anymore.", true)]
	public interface IMask
	{
		// Token: 0x0600013D RID: 317
		bool Enabled();

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600013E RID: 318
		RectTransform rectTransform { get; }
	}
}
