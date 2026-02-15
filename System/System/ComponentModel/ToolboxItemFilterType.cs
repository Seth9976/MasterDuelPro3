using System;

namespace System.ComponentModel
{
	/// <summary>Defines identifiers used to indicate the type of filter that a <see cref="T:System.ComponentModel.ToolboxItemFilterAttribute" /> uses.</summary>
	// Token: 0x020002A0 RID: 672
	public enum ToolboxItemFilterType
	{
		/// <summary>Indicates that a toolbox item filter string is allowed, but not required.</summary>
		// Token: 0x04000A4C RID: 2636
		Allow,
		/// <summary>Indicates that custom processing is required to determine whether to use a toolbox item filter string. </summary>
		// Token: 0x04000A4D RID: 2637
		Custom,
		/// <summary>Indicates that a toolbox item filter string is not allowed. </summary>
		// Token: 0x04000A4E RID: 2638
		Prevent,
		/// <summary>Indicates that a toolbox item filter string must be present for a toolbox item to be enabled. </summary>
		// Token: 0x04000A4F RID: 2639
		Require
	}
}
