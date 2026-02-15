using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Windows.Forms
{
	/// <summary>Specifies how a control anchors to the edges of its container.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200000A RID: 10
	[Flags]
	[Editor("System.Windows.Forms.Design.AnchorEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public enum AnchorStyles
	{
		/// <summary>The control is not anchored to any edges of its container.</summary>
		// Token: 0x0400004B RID: 75
		None = 0,
		/// <summary>The control is anchored to the top edge of its container.</summary>
		// Token: 0x0400004C RID: 76
		Top = 1,
		/// <summary>The control is anchored to the bottom edge of its container.</summary>
		// Token: 0x0400004D RID: 77
		Bottom = 2,
		/// <summary>The control is anchored to the left edge of its container.</summary>
		// Token: 0x0400004E RID: 78
		Left = 4,
		/// <summary>The control is anchored to the right edge of its container.</summary>
		// Token: 0x0400004F RID: 79
		Right = 8
	}
}
