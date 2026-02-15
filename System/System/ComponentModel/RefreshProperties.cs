using System;

namespace System.ComponentModel
{
	/// <summary>Defines identifiers that indicate the type of a refresh of the Properties window.</summary>
	// Token: 0x020002D9 RID: 729
	public enum RefreshProperties
	{
		/// <summary>No refresh is necessary.</summary>
		// Token: 0x04000ADD RID: 2781
		None,
		/// <summary>The properties should be requeried and the view should be refreshed.</summary>
		// Token: 0x04000ADE RID: 2782
		All,
		/// <summary>The view should be refreshed.</summary>
		// Token: 0x04000ADF RID: 2783
		Repaint
	}
}
