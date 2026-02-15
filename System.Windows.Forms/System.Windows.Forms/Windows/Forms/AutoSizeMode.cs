using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies how a control will behave when its <see cref="P:System.Windows.Forms.Control.AutoSize" /> property is enabled.</summary>
	// Token: 0x02000016 RID: 22
	public enum AutoSizeMode
	{
		/// <summary>The control grows or shrinks to fit its contents. The control cannot be resized manually.</summary>
		// Token: 0x04000091 RID: 145
		GrowAndShrink,
		/// <summary>The control grows as much as necessary to fit its contents but does not shrink smaller than the value of its <see cref="P:System.Windows.Forms.Control.Size" />   property. The form can be resized, but cannot be made so small that any of its contained controls are hidden.</summary>
		// Token: 0x04000092 RID: 146
		GrowOnly
	}
}
