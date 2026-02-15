using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies a value indicating whether the text appears from right to left, such as when using Hebrew or Arabic fonts.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000176 RID: 374
	public enum RightToLeft
	{
		/// <summary>The text reads from left to right. This is the default.</summary>
		// Token: 0x04000911 RID: 2321
		No,
		/// <summary>The text reads from right to left.</summary>
		// Token: 0x04000912 RID: 2322
		Yes,
		/// <summary>The direction the text read is inherited from the parent control.</summary>
		// Token: 0x04000913 RID: 2323
		Inherit
	}
}
