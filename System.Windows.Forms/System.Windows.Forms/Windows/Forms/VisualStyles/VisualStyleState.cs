using System;

namespace System.Windows.Forms.VisualStyles
{
	/// <summary>Specifies how visual styles are applied to the current application.</summary>
	// Token: 0x0200036C RID: 876
	public enum VisualStyleState
	{
		/// <summary>Visual styles are not applied to the application.</summary>
		// Token: 0x04001823 RID: 6179
		NoneEnabled,
		/// <summary>Visual styles are applied only to the nonclient area.</summary>
		// Token: 0x04001824 RID: 6180
		NonClientAreaEnabled,
		/// <summary>Visual styles are applied only to the client area.</summary>
		// Token: 0x04001825 RID: 6181
		ClientAreaEnabled,
		/// <summary>Visual styles are applied to client and nonclient areas.</summary>
		// Token: 0x04001826 RID: 6182
		ClientAndNonClientAreasEnabled
	}
}
